using System;
using HalconDotNet;
using ViewROI;
using System.Collections;



namespace ViewROI
{

	public delegate void FuncROIDelegate();

	/// <summary>
	/// This class creates and manages ROI objects. It responds 
	/// to  mouse device inputs using the methods mouseDownAction and 
	/// mouseMoveAction. You don't have to know this class in detail when you 
	/// build your own C# project. But you must consider a few things if 
	/// you want to use interactive ROIs in your application: There is a
	/// quite close connection between the ROIController and the HWndCtrl 
	/// class, which means that you must 'register' the ROIController
	/// with the HWndCtrl, so the HWndCtrl knows it has to forward user input
	/// (like mouse events) to the ROIController class.  
	/// The visualization and manipulation of the ROI objects is done 
	/// by the ROIController.
	/// This class provides special support for the matching
	/// applications by calculating a model region from the list of ROIs. For
	/// this, ROIs are added and subtracted according to their sign.
	/// </summary>
	public class ROIController
	{

		/// <summary>
		/// Constant for setting the ROI mode: positive ROI sign.
		/// </summary>
		public const int MODE_ROI_POS           = 21;

		/// <summary>
		/// Constant for setting the ROI mode: negative ROI sign.
		/// </summary>
		public const int MODE_ROI_NEG           = 22;

		/// <summary>
		/// Constant for setting the ROI mode: no model region is computed as
		/// the sum of all ROI objects.
		/// </summary>
		public const int MODE_ROI_NONE          = 23;

		/// <summary>Constant describing an update of the model region</summary>
		public const int EVENT_UPDATE_ROI       = 50;

		public const int EVENT_CHANGED_ROI_SIGN = 51;

		/// <summary>Constant describing an update of the model region</summary>
		public const int EVENT_MOVING_ROI       = 52;

		public const int EVENT_DELETED_ACTROI  	= 53;

		public const int EVENT_DELETED_ALL_ROIS = 54;

		public const int EVENT_ACTIVATED_ROI   	= 55;

		public const int EVENT_CREATED_ROI   	= 56;

        public const int EVENT_RESET_ROI        = 57;

		private ROI     roiMode;
		private int     stateROI;
		private double  currX, currY;


        /// <summary>Index of the active ROI object</summary>
        private int activeROIidx;
        public int ActiveROIidx
        {
            get { return this.activeROIidx; }
            set
            {
                if (this.activeROIidx == value) return;
                this.activeROIidx = value;
                this.ROISelected?.Invoke(this.activeROIidx);
            }
        }

        public event Action<int> ROISelected;

        public int deletedIdx;

		/// <summary>List containing all created ROI objects so far</summary>
		public ArrayList ROIList;

		/// <summary>
		/// Region obtained by summing up all negative 
		/// and positive ROI objects from the ROIList 
		/// </summary>
		public HRegion ModelROI;

		private string activeCol    = "green";
		private string activeHdlCol = "green";
		private string inactiveCol  = "blue";
        private string errorCol = "red";

		/// <summary>
		/// Reference to the HWndCtrl, the ROI Controller is registered to
		/// </summary>
		public HWndCtrl viewController;

		/// <summary>
		/// Delegate that notifies about changes made in the model region
		/// </summary>
		public IconicDelegate  NotifyRCObserver;

		/// <summary>Constructor</summary>
		public ROIController()
		{
			stateROI = MODE_ROI_NONE;
			ROIList = new ArrayList();
			ActiveROIidx = -1;
			ModelROI = new HRegion();
			NotifyRCObserver = new IconicDelegate(dummyI);
			deletedIdx = -1;
			currX = currY = -1;
		}

		/// <summary>Registers the HWndCtrl to this ROIController instance</summary>
		public void setViewController(HWndCtrl view)
		{
			viewController = view;
		}

		/// <summary>Gets the ModelROI object</summary>
		public HRegion getModelRegion()
		{
			return ModelROI;
		}

		/// <summary>Gets the List of ROIs created so far</summary>
		public ArrayList getROIList()
		{
			return ROIList;
		}

		/// <summary>Get the active ROI</summary>
		public ROI getActiveROI()
		{
			if (ActiveROIidx != -1)
				return ((ROI)ROIList[ActiveROIidx]);

			return null;
		}

		public int getActiveROIIdx()
		{
			return ActiveROIidx;
		}

		public void setActiveROIIdx(int active)
		{
			ActiveROIidx = active;
		}

		public int getDelROIIdx()
		{
			return deletedIdx;
		}

		/// <summary>
		/// To create a new ROI object the application class initializes a 
		/// 'seed' ROI instance and passes it to the ROIController.
		/// The ROIController now responds by manipulating this new ROI
		/// instance.
		/// </summary>
		/// <param name="r">
		/// 'Seed' ROI object forwarded by the application forms class.
		/// </param>
		public void setROIShape(ROI r)
		{
			roiMode = r;
			roiMode.setOperatorFlag(stateROI);
		}


		/// <summary>
		/// Sets the sign of a ROI object to the value 'mode' (MODE_ROI_NONE,
		/// MODE_ROI_POS,MODE_ROI_NEG)
		/// </summary>
		public void setROISign(int mode)
		{
			stateROI = mode;

			if (ActiveROIidx != -1)
			{
				((ROI)ROIList[ActiveROIidx]).setOperatorFlag(stateROI);
				viewController.repaint();
				NotifyRCObserver(ROIController.EVENT_CHANGED_ROI_SIGN);
			}
		}

		/// <summary>
		/// Removes the ROI object that is marked as active. 
		/// If no ROI object is active, then nothing happens. 
		/// </summary>
		public void removeActive()
		{
			if (ActiveROIidx != -1)
			{
				ROIList.RemoveAt(ActiveROIidx);
				deletedIdx = ActiveROIidx;
				ActiveROIidx = -1;
				viewController.repaint();
				NotifyRCObserver(EVENT_DELETED_ACTROI);
			}
		}


		/// <summary>
		/// Calculates the ModelROI region for all objects contained 
		/// in ROIList, by adding and subtracting the positive and 
		/// negative ROI objects.
		/// </summary>
		public bool defineModelROI()
		{
			HRegion tmpAdd, tmpDiff, tmp;
			double row, col;

			if (stateROI == MODE_ROI_NONE)
				return true;

			tmpAdd = new HRegion();
			tmpDiff = new HRegion();
      tmpAdd.GenEmptyRegion();
      tmpDiff.GenEmptyRegion();

			for (int i=0; i < ROIList.Count; i++)
			{
				switch (((ROI)ROIList[i]).getOperatorFlag())
				{
					case ROI.POSITIVE_FLAG:
						tmp = ((ROI)ROIList[i]).getRegion();
						tmpAdd = tmp.Union2(tmpAdd);
						break;
					case ROI.NEGATIVE_FLAG:
						tmp = ((ROI)ROIList[i]).getRegion();
						tmpDiff = tmp.Union2(tmpDiff);
						break;
					default:
						break;
				}//end of switch
			}//end of for

			ModelROI = null;

			if (tmpAdd.AreaCenter(out row, out col) > 0)
			{
				tmp = tmpAdd.Difference(tmpDiff);
				if (tmp.AreaCenter(out row, out col) > 0)
					ModelROI = tmp;
			}

			//in case the set of positiv and negative ROIs dissolve 
			if (ModelROI == null || ROIList.Count == 0)
				return false;

			return true;
		}


		/// <summary>
		/// Clears all variables managing ROI objects
		/// </summary>
		public void reset()
		{
			ROIList.Clear();
			ActiveROIidx = -1;
			ModelROI = null;
			roiMode = null;
			NotifyRCObserver(EVENT_DELETED_ALL_ROIS);
		}


		/// <summary>
		/// Deletes this ROI instance if a 'seed' ROI object has been passed
		/// to the ROIController by the application class.
		/// 
		/// </summary>
		public void resetROI()
		{
			ActiveROIidx = -1;
			roiMode = null;
		}

		/// <summary>Defines the colors for the ROI objects</summary>
		/// <param name="aColor">Color for the active ROI object</param>
		/// <param name="inaColor">Color for the inactive ROI objects</param>
		/// <param name="aHdlColor">
		/// Color for the active handle of the active ROI object
		/// </param>
		public void setDrawColor(string aColor,
								   string aHdlColor,
								   string inaColor)
		{
			if (aColor != "")
				activeCol = aColor;
			if (aHdlColor != "")
				activeHdlCol = aHdlColor;
			if (inaColor != "")
				inactiveCol = inaColor;
		}


		/// <summary>
		/// Paints all objects from the ROIList into the HALCON window
		/// </summary>
		/// <param name="window">HALCON window</param>
		public void paintData(HalconDotNet.HWindow window)
		{
			window.SetDraw("margin");
			window.SetLineWidth(1);

			if (ROIList.Count > 0)
			{
				//window.SetDraw("margin");

				for (int i=0; i < ROIList.Count; i++)
				{
                    ROI roi = (ROI)ROIList[i];
                    window.SetLineStyle(roi.flagLineStyle);
                    if (roi.error)
                    {
                        window.SetColor(errorCol);
                    }
                    else
                    {
                        window.SetColor(inactiveCol);
                    }
					roi.draw(window);
				}

				if (ActiveROIidx != -1)
				{
					window.SetColor(activeCol);
					window.SetLineStyle(((ROI)ROIList[ActiveROIidx]).flagLineStyle);
					((ROI)ROIList[ActiveROIidx]).draw(window);

					window.SetColor(activeHdlCol);
					((ROI)ROIList[ActiveROIidx]).displayActive(window);
				}
			}
		}

		/// <summary>
		/// Reaction of ROI objects to the 'mouse button down' event: changing
		/// the shape of the ROI and adding it to the ROIList if it is a 'seed'
		/// ROI.
		/// </summary>
		/// <param name="imgX">x coordinate of mouse event</param>
		/// <param name="imgY">y coordinate of mouse event</param>
		/// <returns></returns>
		public int mouseDownAction(double imgX, double imgY)
		{
			int idxROI= -1;
			double max = 10000, dist = 0;
			double epsilon = 35.0;			//maximal shortest distance to one of
			//the handles

			if (roiMode != null)			 //either a new ROI object is created
			{
				roiMode.createROI(imgX, imgY);
				ROIList.Add(roiMode);
				roiMode = null;
				ActiveROIidx = ROIList.Count - 1;
				viewController.repaint();

				NotifyRCObserver(ROIController.EVENT_CREATED_ROI);
			}
			else if (ROIList.Count > 0)		// ... or an existing one is manipulated
			{
				ActiveROIidx = -1;

				for (int i =0; i < ROIList.Count; i++)
				{
					dist = ((ROI)ROIList[i]).distToClosestHandle(imgX, imgY);
					if ((dist < max) && (dist < epsilon))
					{
						max = dist;
						idxROI = i;
					}
				}//end of for

				if (idxROI >= 0)
				{
					ActiveROIidx = idxROI;
					NotifyRCObserver(ROIController.EVENT_ACTIVATED_ROI);
				}

				viewController.repaint();
			}
			return ActiveROIidx;
		}

		/// <summary>
		/// Reaction of ROI objects to the 'mouse button move' event: moving
		/// the active ROI.
		/// </summary>
		/// <param name="newX">x coordinate of mouse event</param>
		/// <param name="newY">y coordinate of mouse event</param>
		public void mouseMoveAction(double newX, double newY)
		{
			if ((newX == currX) && (newY == currY))
				return;

			((ROI)ROIList[ActiveROIidx]).moveByHandle(newX, newY);
			viewController.repaint();
			currX = newX;
			currY = newY;
			NotifyRCObserver(ROIController.EVENT_MOVING_ROI);
		}

        public void addROI(ROI roi)
        {
            if (roi != null)
            {
                roi.construct();
                ROIList.Add(roi);
                roiMode = null;
                viewController.repaint();
                NotifyRCObserver(ROIController.EVENT_CREATED_ROI);
            }
        }

		/***********************************************************/
		public void dummyI(int v)
		{
		}

	}//end of class
}//end of namespace
