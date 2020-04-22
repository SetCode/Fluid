namespace Anda.Fluid.Domain.DataStatistics.YiedAndCapacity
{
    partial class YiedAndCapacityForm
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
            this.txtOkNg = new System.Windows.Forms.TextBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.dateTimePickerQueryEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerQueryStart = new System.Windows.Forms.DateTimePicker();
            this.btnInput = new System.Windows.Forms.Button();
            this.dateTimePickerInputEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerInputStart = new System.Windows.Forms.DateTimePicker();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.yiedAndCapacityChart1 = new Anda.Fluid.Domain.DataStatistics.YiedAndCapacityChart();
            this.pnlChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOkNg
            // 
            this.txtOkNg.Location = new System.Drawing.Point(547, 161);
            this.txtOkNg.Name = "txtOkNg";
            this.txtOkNg.Size = new System.Drawing.Size(100, 21);
            this.txtOkNg.TabIndex = 13;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(671, 312);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 12;
            this.btnQuery.Text = "Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dateTimePickerQueryEnd
            // 
            this.dateTimePickerQueryEnd.CustomFormat = "yyyy-MM-dd HH";
            this.dateTimePickerQueryEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerQueryEnd.Location = new System.Drawing.Point(547, 253);
            this.dateTimePickerQueryEnd.Name = "dateTimePickerQueryEnd";
            this.dateTimePickerQueryEnd.Size = new System.Drawing.Size(200, 21);
            this.dateTimePickerQueryEnd.TabIndex = 11;
            // 
            // dateTimePickerQueryStart
            // 
            this.dateTimePickerQueryStart.CustomFormat = "yyyy-MM-dd HH";
            this.dateTimePickerQueryStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerQueryStart.Location = new System.Drawing.Point(547, 203);
            this.dateTimePickerQueryStart.Name = "dateTimePickerQueryStart";
            this.dateTimePickerQueryStart.Size = new System.Drawing.Size(200, 21);
            this.dateTimePickerQueryStart.TabIndex = 10;
            // 
            // btnInput
            // 
            this.btnInput.Location = new System.Drawing.Point(671, 161);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(75, 23);
            this.btnInput.TabIndex = 9;
            this.btnInput.Text = "Input";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // dateTimePickerInputEnd
            // 
            this.dateTimePickerInputEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePickerInputEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerInputEnd.Location = new System.Drawing.Point(547, 102);
            this.dateTimePickerInputEnd.Name = "dateTimePickerInputEnd";
            this.dateTimePickerInputEnd.Size = new System.Drawing.Size(200, 21);
            this.dateTimePickerInputEnd.TabIndex = 8;
            // 
            // dateTimePickerInputStart
            // 
            this.dateTimePickerInputStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePickerInputStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerInputStart.Location = new System.Drawing.Point(547, 52);
            this.dateTimePickerInputStart.Name = "dateTimePickerInputStart";
            this.dateTimePickerInputStart.Size = new System.Drawing.Size(200, 21);
            this.dateTimePickerInputStart.TabIndex = 7;
            // 
            // pnlChart
            // 
            this.pnlChart.Controls.Add(this.yiedAndCapacityChart1);
            this.pnlChart.Location = new System.Drawing.Point(7, 8);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(529, 462);
            this.pnlChart.TabIndex = 14;
            // 
            // yiedAndCapacityChart1
            // 
            this.yiedAndCapacityChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yiedAndCapacityChart1.Location = new System.Drawing.Point(0, 0);
            this.yiedAndCapacityChart1.Name = "yiedAndCapacityChart1";
            this.yiedAndCapacityChart1.Size = new System.Drawing.Size(529, 462);
            this.yiedAndCapacityChart1.TabIndex = 0;
            // 
            // YiedAndCapacityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 486);
            this.Controls.Add(this.pnlChart);
            this.Controls.Add(this.txtOkNg);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.dateTimePickerQueryEnd);
            this.Controls.Add(this.dateTimePickerQueryStart);
            this.Controls.Add(this.btnInput);
            this.Controls.Add(this.dateTimePickerInputEnd);
            this.Controls.Add(this.dateTimePickerInputStart);
            this.Name = "YiedAndCapacityForm";
            this.Text = "YiedAndCapacity";
            this.pnlChart.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOkNg;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DateTimePicker dateTimePickerQueryEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerQueryStart;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.DateTimePicker dateTimePickerInputEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerInputStart;
        private System.Windows.Forms.Panel pnlChart;
        private YiedAndCapacityChart yiedAndCapacityChart1;
    }
}