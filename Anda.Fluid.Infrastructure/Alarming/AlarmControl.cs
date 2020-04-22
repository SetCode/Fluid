using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Anda.Fluid.Infrastructure.UI;
using Anda.Fluid.Infrastructure.International;

namespace Anda.Fluid.Infrastructure.Alarming
{
    public partial class AlarmControl : UserControlEx, IAlarmObservable
    {
        private Queue<AlarmEvent> buffer = new Queue<AlarmEvent>();
        private Timer timer;
        private ContextMenuStrip cms = new ContextMenuStrip();

        private Image imgFatal = Properties.Resources.Flash_Bang_16px;
        private Image imgError = Properties.Resources.Cancel_16px;
        private Image imgWarn = Properties.Resources.Error_16px;
        private Image imgInfo = Properties.Resources.Info_16px;
        private Image imgDebug = Properties.Resources.Bug_16px;

        private Dictionary<string, string> lngResources = new Dictionary<string, string>();
        private string strDateTime = "DateTime";
        private string strCode = "Code";
        private string strWho = "Who";
        private string strWhere = "Where";
        private string strMessage = "Message";
        public AlarmControl()
        {
            InitializeComponent();

            lngResources.Add(strDateTime, "DateTime");
            lngResources.Add(strCode, "Code");
            lngResources.Add(strWho, "Who");
            lngResources.Add(strWhere, "Where");
            lngResources.Add(strMessage, "Message");


            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AllowUserToOrderColumns = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.CellMouseClick += DataGridView1_CellMouseClick;

            this.dataGridView1.Columns.Add(new DataGridViewImageColumn());
            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());

            this.dataGridView1.Columns[0].HeaderText = "";
            this.dataGridView1.Columns[1].HeaderText = "DateTime";
            this.dataGridView1.Columns[2].HeaderText = "Code";
            this.dataGridView1.Columns[3].HeaderText = "Who";
            this.dataGridView1.Columns[4].HeaderText = "Where";
            this.dataGridView1.Columns[5].HeaderText = "Message";

            this.dataGridView1.Columns[0].Width = 20;
            this.dataGridView1.Columns[1].Width = 100;
            this.dataGridView1.Columns[2].Width = 40;
            this.dataGridView1.Columns[3].Width = 50;
            this.dataGridView1.Columns[4].Width = 100;

            this.cms.Items.Add("Clear");
            this.cms.ItemClicked += Cms_ItemClicked;

            this.timer = new Timer();
            this.timer.Interval = 100;
            this.timer.Tick += Timer_Tick;
            this.timer.Start();
            this.ReadLanguageResources();
        }

        public override void ReadLanguageResources(bool skipButton = false, bool skipRadioButton = false, bool skipCheckBox = false, bool skipLabel = false)
        {
            if (this.HasLngResources())
            {


                lngResources[strDateTime] = this.ReadKeyValueFromResources(strDateTime);
                lngResources[strCode] = this.ReadKeyValueFromResources(strCode);
                lngResources[strWho] = this.ReadKeyValueFromResources(strWho);
                lngResources[strWhere] = this.ReadKeyValueFromResources(strWhere);
                lngResources[strMessage] = this.ReadKeyValueFromResources(strMessage);
            }
            this.dataGridView1.Columns[1].HeaderText = lngResources[strDateTime];
            this.dataGridView1.Columns[2].HeaderText = lngResources[strCode];
            this.dataGridView1.Columns[3].HeaderText = lngResources[strWho];
            this.dataGridView1.Columns[4].HeaderText = lngResources[strWhere];
            this.dataGridView1.Columns[5].HeaderText = lngResources[strMessage];

        }

        private void Cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Clear")
            {
                this.ClearAlarms();
            }
        }

        private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.cms.Show(this, 0, 0);
            }
        }

        public DataGridView Table => this.dataGridView1;

        public void HandleAlarmEvent(AlarmEvent e)
        {
            if (!this.IsHandleCreated)
            {
                this.buffer.Enqueue(e);
                return;
            }

            this.BeginInvoke(new Action(() =>
            {
                Image img = null;
                switch (e.Info.Level)
                {
                    case AlarmLevel.Fatal: img = this.imgFatal; break;
                    case AlarmLevel.Error: img = this.imgError; break;
                    case AlarmLevel.Warn: img = this.imgWarn; break;
                }

                DataGridViewRow dr = new DataGridViewRow();
                DataGridViewImageCell cell0 = new DataGridViewImageCell();
                cell0.Value = img;
                dr.Cells.Add(cell0);
                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = e.Info.DateTime;
                dr.Cells.Add(cell1);
                cell1 = new DataGridViewTextBoxCell();
                cell1.Value = e.Info.ErrorCode;
                dr.Cells.Add(cell1);
                cell1 = new DataGridViewTextBoxCell();
                cell1.Value = e.Sender?.Name;
                dr.Cells.Add(cell1);
                cell1 = new DataGridViewTextBoxCell();
                cell1.Value = e.Info.Where;
                dr.Cells.Add(cell1);
                cell1 = new DataGridViewTextBoxCell();
                cell1.Value = e.Info.Message;
                dr.Cells.Add(cell1);

                this.dataGridView1.Rows.Insert(0, dr);
                if (this.dataGridView1.Rows.Count > 200)
                {
                    this.dataGridView1.Rows.RemoveAt(this.dataGridView1.Rows.Count - 1);
                }
                this.dataGridView1.CurrentCell = cell0;

            }));
        }

        public void ClearAlarms()
        {
            this.dataGridView1.Rows.Clear();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.IsHandleCreated && this.buffer.Count > 0)
            {
                this.HandleAlarmEvent(this.buffer.Dequeue());
            }
        }
    }
}
