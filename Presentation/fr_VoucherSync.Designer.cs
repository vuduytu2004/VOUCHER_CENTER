namespace VOUCHER_CENTER.Presentation
{
    partial class fr_VoucherSync
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
            this.txtVoucherSerial = new System.Windows.Forms.TextBox();
            this.txtTransNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpCreatedDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.lb_LocationsGroup = new System.Windows.Forms.Label();
            this.lb_LocationsDetail = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lb_LocationDetailName = new System.Windows.Forms.Label();
            this.lb_LocationGroupName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Cmd_Delete = new System.Windows.Forms.Button();
            this.Voucher_Serial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // txtVoucherSerial
            // 
            this.txtVoucherSerial.Location = new System.Drawing.Point(178, 126);
            this.txtVoucherSerial.Name = "txtVoucherSerial";
            this.txtVoucherSerial.Size = new System.Drawing.Size(172, 20);
            this.txtVoucherSerial.TabIndex = 0;
            this.txtVoucherSerial.Leave += new System.EventHandler(this.txtVoucherSerial_Leave);
            // 
            // txtTransNum
            // 
            this.txtTransNum.Location = new System.Drawing.Point(178, 163);
            this.txtTransNum.Name = "txtTransNum";
            this.txtTransNum.Size = new System.Drawing.Size(254, 20);
            this.txtTransNum.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mã Voucher";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Số GD";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ngày GD";
            // 
            // dtpCreatedDate
            // 
            this.dtpCreatedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCreatedDate.Location = new System.Drawing.Point(178, 200);
            this.dtpCreatedDate.Name = "dtpCreatedDate";
            this.dtpCreatedDate.Size = new System.Drawing.Size(172, 20);
            this.dtpCreatedDate.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Khách hàng";
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Location = new System.Drawing.Point(178, 237);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(309, 20);
            this.txtPlayerName.TabIndex = 7;
            // 
            // lb_LocationsGroup
            // 
            this.lb_LocationsGroup.AutoSize = true;
            this.lb_LocationsGroup.BackColor = System.Drawing.Color.Transparent;
            this.lb_LocationsGroup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_LocationsGroup.ForeColor = System.Drawing.Color.Maroon;
            this.lb_LocationsGroup.Location = new System.Drawing.Point(227, 36);
            this.lb_LocationsGroup.Name = "lb_LocationsGroup";
            this.lb_LocationsGroup.Size = new System.Drawing.Size(67, 15);
            this.lb_LocationsGroup.TabIndex = 9;
            this.lb_LocationsGroup.Text = "Mã Công Ty";
            // 
            // lb_LocationsDetail
            // 
            this.lb_LocationsDetail.AutoSize = true;
            this.lb_LocationsDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_LocationsDetail.ForeColor = System.Drawing.Color.Maroon;
            this.lb_LocationsDetail.Location = new System.Drawing.Point(227, 68);
            this.lb_LocationsDetail.Name = "lb_LocationsDetail";
            this.lb_LocationsDetail.Size = new System.Drawing.Size(99, 15);
            this.lb_LocationsDetail.TabIndex = 10;
            this.lb_LocationsDetail.Text = "Mã Điểm bán hàng";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(65, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Mã Điểm bán hàng";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(65, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Mã Công Ty";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(385, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Điểm bán hàng";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(385, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Công Ty";
            // 
            // lb_LocationDetailName
            // 
            this.lb_LocationDetailName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_LocationDetailName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lb_LocationDetailName.Location = new System.Drawing.Point(512, 68);
            this.lb_LocationDetailName.Name = "lb_LocationDetailName";
            this.lb_LocationDetailName.Size = new System.Drawing.Size(234, 15);
            this.lb_LocationDetailName.TabIndex = 16;
            this.lb_LocationDetailName.Text = "Tên điểm bán hàng";
            // 
            // lb_LocationGroupName
            // 
            this.lb_LocationGroupName.AutoSize = true;
            this.lb_LocationGroupName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_LocationGroupName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lb_LocationGroupName.Location = new System.Drawing.Point(512, 36);
            this.lb_LocationGroupName.Name = "lb_LocationGroupName";
            this.lb_LocationGroupName.Size = new System.Drawing.Size(71, 15);
            this.lb_LocationGroupName.TabIndex = 15;
            this.lb_LocationGroupName.Text = "Tên Công Ty";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(57, 278);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Ghi chú";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(178, 274);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(593, 20);
            this.txtDescription.TabIndex = 17;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(221, 308);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 27);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(465, 308);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 27);
            this.button2.TabIndex = 20;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupBox1.Location = new System.Drawing.Point(26, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(745, 88);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Voucher_Serial});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(493, 126);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(217, 131);
            this.listView1.TabIndex = 22;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // Cmd_Delete
            // 
            this.Cmd_Delete.BackColor = System.Drawing.Color.RosyBrown;
            this.Cmd_Delete.Location = new System.Drawing.Point(717, 126);
            this.Cmd_Delete.Name = "Cmd_Delete";
            this.Cmd_Delete.Size = new System.Drawing.Size(54, 22);
            this.Cmd_Delete.TabIndex = 24;
            this.Cmd_Delete.Text = "Delete";
            this.Cmd_Delete.UseVisualStyleBackColor = false;
            // 
            // Voucher_Serial
            // 
            this.Voucher_Serial.Text = "";
            // 
            // fr_VoucherSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(801, 346);
            this.Controls.Add(this.Cmd_Delete);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lb_LocationDetailName);
            this.Controls.Add(this.lb_LocationGroupName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lb_LocationsDetail);
            this.Controls.Add(this.lb_LocationsGroup);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPlayerName);
            this.Controls.Add(this.dtpCreatedDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTransNum);
            this.Controls.Add(this.txtVoucherSerial);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "fr_VoucherSync";
            this.Text = "Cập nhật Voucher đã sử dụng";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtVoucherSerial;
        private System.Windows.Forms.TextBox txtTransNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpCreatedDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.Label lb_LocationsGroup;
        private System.Windows.Forms.Label lb_LocationsDetail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lb_LocationDetailName;
        private System.Windows.Forms.Label lb_LocationGroupName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button Cmd_Delete;
        private System.Windows.Forms.ColumnHeader Voucher_Serial;
    }
}