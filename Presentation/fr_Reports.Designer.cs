namespace Report_Center.Presentation
{
    partial class fr_Reports
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.bt_BC = new System.Windows.Forms.Button();
            this.bt_Exit = new System.Windows.Forms.Button();
            this.report_name = new System.Windows.Forms.Label();
            this.Node_id = new System.Windows.Forms.Label();
            this.Pro_name = new System.Windows.Forms.Label();
            this.gr_para_name = new System.Windows.Forms.Label();
            this.gr_fr_to_date = new System.Windows.Forms.GroupBox();
            this.todate = new System.Windows.Forms.DateTimePicker();
            this.frdate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gr_dept_id = new System.Windows.Forms.GroupBox();
            this.dept_id = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gr_stk_id = new System.Windows.Forms.GroupBox();
            this.stk_id = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.para_name = new System.Windows.Forms.Label();
            this.lbl_API = new System.Windows.Forms.Label();
            this.gr_sku_code = new System.Windows.Forms.GroupBox();
            this.sku_code = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gr_supp_id = new System.Windows.Forms.GroupBox();
            this.supp_id = new System.Windows.Forms.TextBox();
            this.lbl_supp_id = new System.Windows.Forms.Label();
            this.gr_fr_to_date.SuspendLayout();
            this.gr_dept_id.SuspendLayout();
            this.gr_stk_id.SuspendLayout();
            this.gr_sku_code.SuspendLayout();
            this.gr_supp_id.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(8, 8);
            this.treeView1.Margin = new System.Windows.Forms.Padding(2);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(328, 549);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // bt_BC
            // 
            this.bt_BC.Location = new System.Drawing.Point(475, 517);
            this.bt_BC.Margin = new System.Windows.Forms.Padding(2);
            this.bt_BC.Name = "bt_BC";
            this.bt_BC.Size = new System.Drawing.Size(96, 38);
            this.bt_BC.TabIndex = 2;
            this.bt_BC.Text = "&Báo Cáo";
            this.bt_BC.UseVisualStyleBackColor = true;
            this.bt_BC.Click += new System.EventHandler(this.bt_BC_Click);
            // 
            // bt_Exit
            // 
            this.bt_Exit.Location = new System.Drawing.Point(649, 517);
            this.bt_Exit.Margin = new System.Windows.Forms.Padding(2);
            this.bt_Exit.Name = "bt_Exit";
            this.bt_Exit.Size = new System.Drawing.Size(99, 38);
            this.bt_Exit.TabIndex = 3;
            this.bt_Exit.Text = "&Exit";
            this.bt_Exit.UseVisualStyleBackColor = true;
            this.bt_Exit.Click += new System.EventHandler(this.bt_Exit_Click);
            // 
            // report_name
            // 
            this.report_name.AutoSize = true;
            this.report_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.report_name.ForeColor = System.Drawing.Color.Maroon;
            this.report_name.Location = new System.Drawing.Point(369, 23);
            this.report_name.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.report_name.Name = "report_name";
            this.report_name.Size = new System.Drawing.Size(65, 17);
            this.report_name.TabIndex = 4;
            this.report_name.Text = "Reports";
            this.report_name.Click += new System.EventHandler(this.report_name_Click);
            // 
            // Node_id
            // 
            this.Node_id.AutoSize = true;
            this.Node_id.Location = new System.Drawing.Point(369, 46);
            this.Node_id.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Node_id.Name = "Node_id";
            this.Node_id.Size = new System.Drawing.Size(47, 13);
            this.Node_id.TabIndex = 5;
            this.Node_id.Text = "Node_id";
            this.Node_id.Visible = false;
            // 
            // Pro_name
            // 
            this.Pro_name.AutoSize = true;
            this.Pro_name.Location = new System.Drawing.Point(423, 46);
            this.Pro_name.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Pro_name.Name = "Pro_name";
            this.Pro_name.Size = new System.Drawing.Size(55, 13);
            this.Pro_name.TabIndex = 6;
            this.Pro_name.Text = "Pro_name";
            this.Pro_name.Visible = false;
            // 
            // gr_para_name
            // 
            this.gr_para_name.AutoSize = true;
            this.gr_para_name.Location = new System.Drawing.Point(378, 456);
            this.gr_para_name.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.gr_para_name.Name = "gr_para_name";
            this.gr_para_name.Size = new System.Drawing.Size(75, 13);
            this.gr_para_name.TabIndex = 7;
            this.gr_para_name.Text = "gr_para_name";
            this.gr_para_name.Visible = false;
            // 
            // gr_fr_to_date
            // 
            this.gr_fr_to_date.Controls.Add(this.todate);
            this.gr_fr_to_date.Controls.Add(this.frdate);
            this.gr_fr_to_date.Controls.Add(this.label5);
            this.gr_fr_to_date.Controls.Add(this.label4);
            this.gr_fr_to_date.Location = new System.Drawing.Point(372, 82);
            this.gr_fr_to_date.Margin = new System.Windows.Forms.Padding(2);
            this.gr_fr_to_date.Name = "gr_fr_to_date";
            this.gr_fr_to_date.Padding = new System.Windows.Forms.Padding(2);
            this.gr_fr_to_date.Size = new System.Drawing.Size(381, 46);
            this.gr_fr_to_date.TabIndex = 8;
            this.gr_fr_to_date.TabStop = false;
            this.gr_fr_to_date.Visible = false;
            // 
            // todate
            // 
            this.todate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.todate.Location = new System.Drawing.Point(262, 16);
            this.todate.Margin = new System.Windows.Forms.Padding(1);
            this.todate.Name = "todate";
            this.todate.Size = new System.Drawing.Size(94, 20);
            this.todate.TabIndex = 21;
            // 
            // frdate
            // 
            this.frdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.frdate.Location = new System.Drawing.Point(64, 16);
            this.frdate.Margin = new System.Windows.Forms.Padding(1);
            this.frdate.Name = "frdate";
            this.frdate.Size = new System.Drawing.Size(102, 20);
            this.frdate.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(190, 18);
            this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Đến ngày";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(4, 18);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Từ ngày";
            // 
            // gr_dept_id
            // 
            this.gr_dept_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr_dept_id.Controls.Add(this.dept_id);
            this.gr_dept_id.Controls.Add(this.label2);
            this.gr_dept_id.Location = new System.Drawing.Point(372, 237);
            this.gr_dept_id.Margin = new System.Windows.Forms.Padding(2);
            this.gr_dept_id.Name = "gr_dept_id";
            this.gr_dept_id.Padding = new System.Windows.Forms.Padding(2);
            this.gr_dept_id.Size = new System.Drawing.Size(498, 46);
            this.gr_dept_id.TabIndex = 11;
            this.gr_dept_id.TabStop = false;
            // 
            // dept_id
            // 
            this.dept_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dept_id.ForeColor = System.Drawing.Color.DarkOrange;
            this.dept_id.Location = new System.Drawing.Point(54, 18);
            this.dept_id.Margin = new System.Windows.Forms.Padding(1);
            this.dept_id.Name = "dept_id";
            this.dept_id.Size = new System.Drawing.Size(432, 20);
            this.dept_id.TabIndex = 20;
            this.dept_id.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(3, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Dept_id";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(533, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(337, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "* Chú ý: Điều kiện BC phân cách nhau bằng dấu ,(phẩy) Vd : 001,002";
            // 
            // gr_stk_id
            // 
            this.gr_stk_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr_stk_id.Controls.Add(this.stk_id);
            this.gr_stk_id.Controls.Add(this.label3);
            this.gr_stk_id.Location = new System.Drawing.Point(372, 187);
            this.gr_stk_id.Margin = new System.Windows.Forms.Padding(2);
            this.gr_stk_id.Name = "gr_stk_id";
            this.gr_stk_id.Padding = new System.Windows.Forms.Padding(2);
            this.gr_stk_id.Size = new System.Drawing.Size(498, 46);
            this.gr_stk_id.TabIndex = 9;
            this.gr_stk_id.TabStop = false;
            this.gr_stk_id.Visible = false;
            // 
            // stk_id
            // 
            this.stk_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stk_id.ForeColor = System.Drawing.Color.DarkOrange;
            this.stk_id.Location = new System.Drawing.Point(54, 18);
            this.stk_id.Margin = new System.Windows.Forms.Padding(1);
            this.stk_id.Name = "stk_id";
            this.stk_id.Size = new System.Drawing.Size(432, 20);
            this.stk_id.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(3, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "STK_id";
            // 
            // para_name
            // 
            this.para_name.AutoSize = true;
            this.para_name.Location = new System.Drawing.Point(378, 476);
            this.para_name.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.para_name.Name = "para_name";
            this.para_name.Size = new System.Drawing.Size(60, 13);
            this.para_name.TabIndex = 25;
            this.para_name.Text = "para_name";
            this.para_name.Visible = false;
            // 
            // lbl_API
            // 
            this.lbl_API.AutoSize = true;
            this.lbl_API.Location = new System.Drawing.Point(375, 67);
            this.lbl_API.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_API.Name = "lbl_API";
            this.lbl_API.Size = new System.Drawing.Size(35, 13);
            this.lbl_API.TabIndex = 26;
            this.lbl_API.Text = "label6";
            this.lbl_API.Visible = false;
            // 
            // gr_sku_code
            // 
            this.gr_sku_code.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr_sku_code.Controls.Add(this.sku_code);
            this.gr_sku_code.Controls.Add(this.label6);
            this.gr_sku_code.Location = new System.Drawing.Point(372, 287);
            this.gr_sku_code.Margin = new System.Windows.Forms.Padding(2);
            this.gr_sku_code.Name = "gr_sku_code";
            this.gr_sku_code.Padding = new System.Windows.Forms.Padding(2);
            this.gr_sku_code.Size = new System.Drawing.Size(498, 46);
            this.gr_sku_code.TabIndex = 12;
            this.gr_sku_code.TabStop = false;
            this.gr_sku_code.Visible = false;
            // 
            // sku_code
            // 
            this.sku_code.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sku_code.ForeColor = System.Drawing.Color.DarkOrange;
            this.sku_code.Location = new System.Drawing.Point(78, 18);
            this.sku_code.Margin = new System.Windows.Forms.Padding(1);
            this.sku_code.Name = "sku_code";
            this.sku_code.Size = new System.Drawing.Size(408, 20);
            this.sku_code.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(3, 20);
            this.label6.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "SKU_CODE";
            // 
            // gr_supp_id
            // 
            this.gr_supp_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gr_supp_id.Controls.Add(this.supp_id);
            this.gr_supp_id.Controls.Add(this.lbl_supp_id);
            this.gr_supp_id.Location = new System.Drawing.Point(372, 348);
            this.gr_supp_id.Margin = new System.Windows.Forms.Padding(2);
            this.gr_supp_id.Name = "gr_supp_id";
            this.gr_supp_id.Padding = new System.Windows.Forms.Padding(2);
            this.gr_supp_id.Size = new System.Drawing.Size(498, 46);
            this.gr_supp_id.TabIndex = 10;
            this.gr_supp_id.TabStop = false;
            this.gr_supp_id.Visible = false;
            // 
            // supp_id
            // 
            this.supp_id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.supp_id.ForeColor = System.Drawing.Color.DarkOrange;
            this.supp_id.Location = new System.Drawing.Point(78, 18);
            this.supp_id.Margin = new System.Windows.Forms.Padding(1);
            this.supp_id.Name = "supp_id";
            this.supp_id.Size = new System.Drawing.Size(408, 20);
            this.supp_id.TabIndex = 20;
            // 
            // lbl_supp_id
            // 
            this.lbl_supp_id.AutoSize = true;
            this.lbl_supp_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_supp_id.ForeColor = System.Drawing.Color.Blue;
            this.lbl_supp_id.Location = new System.Drawing.Point(3, 20);
            this.lbl_supp_id.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lbl_supp_id.Name = "lbl_supp_id";
            this.lbl_supp_id.Size = new System.Drawing.Size(53, 13);
            this.lbl_supp_id.TabIndex = 18;
            this.lbl_supp_id.Text = "Supp_id";
            // 
            // fr_Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 564);
            this.Controls.Add(this.gr_supp_id);
            this.Controls.Add(this.gr_sku_code);
            this.Controls.Add(this.lbl_API);
            this.Controls.Add(this.para_name);
            this.Controls.Add(this.gr_stk_id);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gr_dept_id);
            this.Controls.Add(this.gr_fr_to_date);
            this.Controls.Add(this.gr_para_name);
            this.Controls.Add(this.Pro_name);
            this.Controls.Add(this.Node_id);
            this.Controls.Add(this.report_name);
            this.Controls.Add(this.bt_Exit);
            this.Controls.Add(this.bt_BC);
            this.Controls.Add(this.treeView1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "fr_Reports";
            this.Text = "Báo cáo";
            this.Load += new System.EventHandler(this.fr_Reports_Load);
            this.gr_fr_to_date.ResumeLayout(false);
            this.gr_fr_to_date.PerformLayout();
            this.gr_dept_id.ResumeLayout(false);
            this.gr_dept_id.PerformLayout();
            this.gr_stk_id.ResumeLayout(false);
            this.gr_stk_id.PerformLayout();
            this.gr_sku_code.ResumeLayout(false);
            this.gr_sku_code.PerformLayout();
            this.gr_supp_id.ResumeLayout(false);
            this.gr_supp_id.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button bt_BC;
        private System.Windows.Forms.Button bt_Exit;
        private System.Windows.Forms.Label report_name;
        private System.Windows.Forms.Label Node_id;
        private System.Windows.Forms.Label Pro_name;
        private System.Windows.Forms.Label gr_para_name;
        private System.Windows.Forms.GroupBox gr_fr_to_date;
        private System.Windows.Forms.DateTimePicker todate;
        private System.Windows.Forms.DateTimePicker frdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gr_dept_id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dept_id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gr_stk_id;
        private System.Windows.Forms.TextBox stk_id;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label para_name;
        private System.Windows.Forms.Label lbl_API;
        private System.Windows.Forms.GroupBox gr_sku_code;
        private System.Windows.Forms.TextBox sku_code;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gr_supp_id;
        private System.Windows.Forms.TextBox supp_id;
        private System.Windows.Forms.Label lbl_supp_id;
    }
}