using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewROI;

namespace Anda.Fluid.App.EditHalcon
{
    public interface IHalconEditable
    {
        void SelectROI(ROI roi);

        void CreateROI(ROI roi);

        void RegisterRefImage(HImage image);

        HRegion Execute(ROI roi);

        List<HRegion> ExecuteAll();

        void OnOkClicked();
    }
}
