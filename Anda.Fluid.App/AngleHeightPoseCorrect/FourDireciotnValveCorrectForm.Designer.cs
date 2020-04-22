namespace Anda.Fluid.App.AngleHeightPoseCorrect
{
    partial class FourDireciotnValveCorrectForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ValveAngle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValveCameraOffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValveStandardZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DispenseValveOffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnDeleteSelected = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ValveAngle,
            this.ValveCameraOffset,
            this.ValveStandardZ,
            this.Gap,
            this.DispenseValveOffset});
            this.dataGridView1.Location = new System.Drawing.Point(8, 2);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(664, 314);
            this.dataGridView1.TabIndex = 0;
            // 
            // ValveAngle
            // 
            this.ValveAngle.HeaderText = "胶阀角度";
            this.ValveAngle.Name = "ValveAngle";
            this.ValveAngle.ReadOnly = true;
            // 
            // ValveCameraOffset
            // 
            this.ValveCameraOffset.HeaderText = "胶阀与相机偏差";
            this.ValveCameraOffset.Name = "ValveCameraOffset";
            this.ValveCameraOffset.ReadOnly = true;
            this.ValveCameraOffset.Width = 150;
            // 
            // ValveStandardZ
            // 
            this.ValveStandardZ.HeaderText = "胶阀标准高度";
            this.ValveStandardZ.Name = "ValveStandardZ";
            this.ValveStandardZ.ReadOnly = true;
            this.ValveStandardZ.Width = 110;
            // 
            // Gap
            // 
            this.Gap.HeaderText = "胶阀距板高度";
            this.Gap.Name = "Gap";
            this.Gap.ReadOnly = true;
            this.Gap.Width = 110;
            // 
            // DispenseValveOffset
            // 
            this.DispenseValveOffset.HeaderText = "胶点到阀组偏差";
            this.DispenseValveOffset.Name = "DispenseValveOffset";
            this.DispenseValveOffset.ReadOnly = true;
            this.DispenseValveOffset.Width = 150;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(597, 326);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "进行校正";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(183, 336);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "注意：当添加的新校正中的胶阀角度与已存在的重复时，将会覆盖。";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(102, 326);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(75, 35);
            this.btnClearAll.TabIndex = 3;
            this.btnClearAll.Text = "清空";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnDeleteSelected
            // 
            this.btnDeleteSelected.Location = new System.Drawing.Point(13, 326);
            this.btnDeleteSelected.Name = "btnDeleteSelected";
            this.btnDeleteSelected.Size = new System.Drawing.Size(75, 35);
            this.btnDeleteSelected.TabIndex = 4;
            this.btnDeleteSelected.Text = "删除选中";
            this.btnDeleteSelected.UseVisualStyleBackColor = true;
            this.btnDeleteSelected.Click += new System.EventHandler(this.btnDeleteSelected_Click);
            // 
            // FourDireciotnValveCorrectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 371);
            this.Controls.Add(this.btnDeleteSelected);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FourDireciotnValveCorrectForm";
            this.Text = "四方位阀组校准";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValveAngle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValveCameraOffset;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValveStandardZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gap;
        private System.Windows.Forms.DataGridViewTextBoxColumn DispenseValveOffset;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnDeleteSelected;
    }
}