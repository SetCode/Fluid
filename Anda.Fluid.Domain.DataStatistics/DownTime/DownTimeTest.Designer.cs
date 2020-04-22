namespace Anda.Fluid.Domain.DataStatistics.DownTime
{
    partial class DownTimeTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.btnDownTime = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.dtStartQuery = new System.Windows.Forms.DateTimePicker();
            this.dtEndQuery = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // dtStart
            // 
            this.dtStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Location = new System.Drawing.Point(608, 24);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(200, 21);
            this.dtStart.TabIndex = 0;
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Location = new System.Drawing.Point(608, 64);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(200, 21);
            this.dtEnd.TabIndex = 1;
            // 
            // btnDownTime
            // 
            this.btnDownTime.Location = new System.Drawing.Point(733, 108);
            this.btnDownTime.Name = "btnDownTime";
            this.btnDownTime.Size = new System.Drawing.Size(75, 23);
            this.btnDownTime.TabIndex = 2;
            this.btnDownTime.Text = "DownTime";
            this.btnDownTime.UseVisualStyleBackColor = true;
            this.btnDownTime.Click += new System.EventHandler(this.btnDownTime_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(733, 296);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dtStartQuery
            // 
            this.dtStartQuery.CustomFormat = "yyyy-MM-dd HH";
            this.dtStartQuery.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStartQuery.Location = new System.Drawing.Point(608, 214);
            this.dtStartQuery.Name = "dtStartQuery";
            this.dtStartQuery.Size = new System.Drawing.Size(200, 21);
            this.dtStartQuery.TabIndex = 5;
            // 
            // dtEndQuery
            // 
            this.dtEndQuery.CustomFormat = "yyyy-MM-dd HH";
            this.dtEndQuery.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEndQuery.Location = new System.Drawing.Point(608, 258);
            this.dtEndQuery.Name = "dtEndQuery";
            this.dtEndQuery.Size = new System.Drawing.Size(200, 21);
            this.dtEndQuery.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 501);
            this.panel1.TabIndex = 7;
            // 
            // DownTimeTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 504);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dtEndQuery);
            this.Controls.Add(this.dtStartQuery);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.btnDownTime);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.dtStart);
            this.Name = "DownTimeTest";
            this.Text = "DownTimeTest";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Button btnDownTime;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DateTimePicker dtStartQuery;
        private System.Windows.Forms.DateTimePicker dtEndQuery;
        private System.Windows.Forms.Panel panel1;
    }
}