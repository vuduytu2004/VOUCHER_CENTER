namespace Report_Center.Presentation
{
    partial class Fr_Mar_Imp
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
            this.import_data = new System.Windows.Forms.Button();
            this.Exp_data = new System.Windows.Forms.Button();
            this.bt_Exit = new System.Windows.Forms.Button();
            this.todate = new System.Windows.Forms.DateTimePicker();
            this.frdate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // import_data
            // 
            this.import_data.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.import_data.Location = new System.Drawing.Point(92, 55);
            this.import_data.Margin = new System.Windows.Forms.Padding(2);
            this.import_data.Name = "import_data";
            this.import_data.Size = new System.Drawing.Size(225, 48);
            this.import_data.TabIndex = 2;
            this.import_data.Text = "Import";
            this.import_data.UseVisualStyleBackColor = true;
            this.import_data.Click += new System.EventHandler(this.import_data_Click);
            // 
            // Exp_data
            // 
            this.Exp_data.Location = new System.Drawing.Point(460, 71);
            this.Exp_data.Margin = new System.Windows.Forms.Padding(2);
            this.Exp_data.Name = "Exp_data";
            this.Exp_data.Size = new System.Drawing.Size(121, 48);
            this.Exp_data.TabIndex = 3;
            this.Exp_data.Text = "&Xuất BC";
            this.Exp_data.UseVisualStyleBackColor = true;
            this.Exp_data.Click += new System.EventHandler(this.Exp_data_Click);
            // 
            // bt_Exit
            // 
            this.bt_Exit.Location = new System.Drawing.Point(640, 71);
            this.bt_Exit.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Exit.Name = "bt_Exit";
            this.bt_Exit.Size = new System.Drawing.Size(121, 48);
            this.bt_Exit.TabIndex = 5;
            this.bt_Exit.Text = "&Exit";
            this.bt_Exit.UseVisualStyleBackColor = true;
            this.bt_Exit.Click += new System.EventHandler(this.bt_Exit_Click);
            // 
            // todate
            // 
            this.todate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.todate.Location = new System.Drawing.Point(677, 24);
            this.todate.Margin = new System.Windows.Forms.Padding(1);
            this.todate.Name = "todate";
            this.todate.Size = new System.Drawing.Size(94, 20);
            this.todate.TabIndex = 25;
            // 
            // frdate
            // 
            this.frdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.frdate.Location = new System.Drawing.Point(479, 24);
            this.frdate.Margin = new System.Windows.Forms.Padding(1);
            this.frdate.Name = "frdate";
            this.frdate.Size = new System.Drawing.Size(102, 20);
            this.frdate.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(605, 26);
            this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Đến ngày";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(419, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Từ ngày";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(113, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "Link down Template (nếu cần)";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 123);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(408, 23);
            this.progressBar1.TabIndex = 27;
            // 
            // Fr_Mar_Imp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 159);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.todate);
            this.Controls.Add(this.frdate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bt_Exit);
            this.Controls.Add(this.Exp_data);
            this.Controls.Add(this.import_data);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Fr_Mar_Imp";
            this.Text = "Import Data";
            this.Load += new System.EventHandler(this.Fr_Mar_Imp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button import_data;
        private System.Windows.Forms.Button Exp_data;
        private System.Windows.Forms.Button bt_Exit;
        private System.Windows.Forms.DateTimePicker todate;
        private System.Windows.Forms.DateTimePicker frdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}