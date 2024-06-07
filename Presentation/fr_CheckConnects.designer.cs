
namespace Report_Center.Presentation
{
    partial class fr_CheckConnects
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.List_Connected = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.List_Not_Connect = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.frdate = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.Refresh = new System.Windows.Forms.Button();
            this.export_not_connect = new System.Windows.Forms.Button();
            this.export_all = new System.Windows.Forms.Button();
            this.check_not_connect = new System.Windows.Forms.Button();
            this.check_all = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.List_Connected)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.List_Not_Connect)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = "";
            this.panel1.AccessibleName = "";
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.List_Connected);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1142, 166);
            this.panel1.TabIndex = 0;
            // 
            // List_Connected
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.List_Connected.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.List_Connected.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.List_Connected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.List_Connected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.List_Connected.Location = new System.Drawing.Point(0, 0);
            this.List_Connected.Name = "List_Connected";
            this.List_Connected.RowHeadersWidth = 62;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            this.List_Connected.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.List_Connected.RowTemplate.Height = 28;
            this.List_Connected.Size = new System.Drawing.Size(1142, 166);
            this.List_Connected.TabIndex = 0;
            this.List_Connected.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.List_Connected_CellContentClick_1);
            this.List_Connected.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.List_Connected_ColumnHeaderMouseClick);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.List_Not_Connect);
            this.panel2.Location = new System.Drawing.Point(12, 185);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1142, 235);
            this.panel2.TabIndex = 1;
            // 
            // List_Not_Connect
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Salmon;
            this.List_Not_Connect.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.List_Not_Connect.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.List_Not_Connect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.List_Not_Connect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.List_Not_Connect.Location = new System.Drawing.Point(0, 0);
            this.List_Not_Connect.Name = "List_Not_Connect";
            this.List_Not_Connect.RowHeadersWidth = 62;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            this.List_Not_Connect.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.List_Not_Connect.RowTemplate.Height = 28;
            this.List_Not_Connect.Size = new System.Drawing.Size(1142, 235);
            this.List_Not_Connect.TabIndex = 1;
            this.List_Not_Connect.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.List_Not_Connect_CellContentClick);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.frdate);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.Refresh);
            this.panel3.Controls.Add(this.export_not_connect);
            this.panel3.Controls.Add(this.export_all);
            this.panel3.Controls.Add(this.check_not_connect);
            this.panel3.Controls.Add(this.check_all);
            this.panel3.Controls.Add(this.progressBar1);
            this.panel3.Location = new System.Drawing.Point(12, 426);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1142, 72);
            this.panel3.TabIndex = 2;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(20, 43);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Sum Time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Crimson;
            this.label2.Location = new System.Drawing.Point(15, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Ngày check";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(118, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 22);
            this.label1.TabIndex = 8;
            this.label1.Text = " 0 : 0 : 0 : 0";
            // 
            // frdate
            // 
            this.frdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.frdate.Location = new System.Drawing.Point(120, 11);
            this.frdate.Name = "frdate";
            this.frdate.Size = new System.Drawing.Size(142, 26);
            this.frdate.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(988, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 54);
            this.button1.TabIndex = 5;
            this.button1.Text = "&Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Refresh
            // 
            this.Refresh.Location = new System.Drawing.Point(850, 11);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(129, 54);
            this.Refresh.TabIndex = 4;
            this.Refresh.Text = "Refresh";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // export_not_connect
            // 
            this.export_not_connect.Location = new System.Drawing.Point(712, 11);
            this.export_not_connect.Name = "export_not_connect";
            this.export_not_connect.Size = new System.Drawing.Size(129, 54);
            this.export_not_connect.TabIndex = 3;
            this.export_not_connect.Text = "Export Not Connects";
            this.export_not_connect.UseVisualStyleBackColor = true;
            this.export_not_connect.Click += new System.EventHandler(this.export_not_connect_Click);
            // 
            // export_all
            // 
            this.export_all.Location = new System.Drawing.Point(574, 11);
            this.export_all.Name = "export_all";
            this.export_all.Size = new System.Drawing.Size(129, 54);
            this.export_all.TabIndex = 2;
            this.export_all.Text = "Export All";
            this.export_all.UseVisualStyleBackColor = true;
            this.export_all.Click += new System.EventHandler(this.export_all_Click);
            // 
            // check_not_connect
            // 
            this.check_not_connect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.check_not_connect.Location = new System.Drawing.Point(436, 11);
            this.check_not_connect.Name = "check_not_connect";
            this.check_not_connect.Size = new System.Drawing.Size(129, 54);
            this.check_not_connect.TabIndex = 1;
            this.check_not_connect.Text = "Check Not Connect";
            this.check_not_connect.UseVisualStyleBackColor = true;
            this.check_not_connect.Click += new System.EventHandler(this.check_not_connect_Click);
            // 
            // check_all
            // 
            this.check_all.Location = new System.Drawing.Point(298, 11);
            this.check_all.Name = "check_all";
            this.check_all.Size = new System.Drawing.Size(129, 54);
            this.check_all.TabIndex = 0;
            this.check_all.Text = "Check All";
            this.check_all.UseVisualStyleBackColor = true;
            this.check_all.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(4, 0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1137, 72);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // fr_CheckConnects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 509);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "fr_CheckConnects";
            this.Text = "Check Connections-Doanh Số - BRG";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.List_Connected)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.List_Not_Connect)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button check_all;
        private System.Windows.Forms.Button export_not_connect;
        private System.Windows.Forms.Button export_all;
        private System.Windows.Forms.Button check_not_connect;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView List_Connected;
        private System.Windows.Forms.Button Refresh;
        private System.Windows.Forms.DataGridView List_Not_Connect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker frdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}