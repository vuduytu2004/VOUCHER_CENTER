namespace VOUCHER_CENTER.Presentation
{
    partial class fr_Permissions
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
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.dgvUserPermissions = new System.Windows.Forms.DataGridView();
            this.dgvBRGGroup = new System.Windows.Forms.DataGridView();
            this.cmbUsers = new System.Windows.Forms.ComboBox();
            this.txt_Fillter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_UserID = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_Update_CT = new System.Windows.Forms.Button();
            this.dgvBRGDetail = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserPermissions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBRGGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBRGDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(8, 38);
            this.dgvUsers.Margin = new System.Windows.Forms.Padding(2);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersWidth = 62;
            this.dgvUsers.RowTemplate.Height = 28;
            this.dgvUsers.Size = new System.Drawing.Size(626, 244);
            this.dgvUsers.TabIndex = 0;
            this.dgvUsers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellClick);
            this.dgvUsers.SelectionChanged += new System.EventHandler(this.dgvUsers_SelectionChanged);
            // 
            // dgvUserPermissions
            // 
            this.dgvUserPermissions.AllowUserToAddRows = false;
            this.dgvUserPermissions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUserPermissions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserPermissions.Location = new System.Drawing.Point(638, 38);
            this.dgvUserPermissions.Margin = new System.Windows.Forms.Padding(2);
            this.dgvUserPermissions.Name = "dgvUserPermissions";
            this.dgvUserPermissions.RowHeadersWidth = 62;
            this.dgvUserPermissions.RowTemplate.Height = 28;
            this.dgvUserPermissions.Size = new System.Drawing.Size(511, 436);
            this.dgvUserPermissions.TabIndex = 1;
            // 
            // dgvBRGGroup
            // 
            this.dgvBRGGroup.AllowUserToAddRows = false;
            this.dgvBRGGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvBRGGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBRGGroup.Location = new System.Drawing.Point(8, 286);
            this.dgvBRGGroup.Margin = new System.Windows.Forms.Padding(2);
            this.dgvBRGGroup.Name = "dgvBRGGroup";
            this.dgvBRGGroup.RowHeadersWidth = 62;
            this.dgvBRGGroup.RowTemplate.Height = 28;
            this.dgvBRGGroup.Size = new System.Drawing.Size(283, 188);
            this.dgvBRGGroup.TabIndex = 5;
            // 
            // cmbUsers
            // 
            this.cmbUsers.FormattingEnabled = true;
            this.cmbUsers.Location = new System.Drawing.Point(885, 16);
            this.cmbUsers.Margin = new System.Windows.Forms.Padding(2);
            this.cmbUsers.Name = "cmbUsers";
            this.cmbUsers.Size = new System.Drawing.Size(73, 21);
            this.cmbUsers.TabIndex = 4;
            this.cmbUsers.Visible = false;
            this.cmbUsers.SelectedIndexChanged += new System.EventHandler(this.cmbUsers_SelectedIndexChanged);
            this.cmbUsers.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbUsers_KeyUp);
            // 
            // txt_Fillter
            // 
            this.txt_Fillter.Location = new System.Drawing.Point(99, 14);
            this.txt_Fillter.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Fillter.Name = "txt_Fillter";
            this.txt_Fillter.Size = new System.Drawing.Size(130, 20);
            this.txt_Fillter.TabIndex = 6;
            this.txt_Fillter.TextChanged += new System.EventHandler(this.txt_Fillter_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Tìm kiếm";
            // 
            // txt_UserID
            // 
            this.txt_UserID.AutoSize = true;
            this.txt_UserID.Location = new System.Drawing.Point(516, 14);
            this.txt_UserID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txt_UserID.Name = "txt_UserID";
            this.txt_UserID.Size = new System.Drawing.Size(13, 13);
            this.txt_UserID.TabIndex = 8;
            this.txt_UserID.Text = "1";
            this.txt_UserID.TextChanged += new System.EventHandler(this.txt_UserID_TextChanged);
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.Location = new System.Drawing.Point(947, 490);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(131, 23);
            this.btn_Close.TabIndex = 9;
            this.btn_Close.Text = "Thoát";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Update.Location = new System.Drawing.Point(232, 490);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(155, 23);
            this.btn_Update.TabIndex = 10;
            this.btn_Update.Text = "Update Nhóm";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Visible = false;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_Update_CT
            // 
            this.btn_Update_CT.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Update_CT.Location = new System.Drawing.Point(638, 490);
            this.btn_Update_CT.Name = "btn_Update_CT";
            this.btn_Update_CT.Size = new System.Drawing.Size(130, 23);
            this.btn_Update_CT.TabIndex = 11;
            this.btn_Update_CT.Text = "Update Menu";
            this.btn_Update_CT.UseVisualStyleBackColor = true;
            this.btn_Update_CT.Click += new System.EventHandler(this.btn_Update_CT_Click);
            // 
            // dgvBRGDetail
            // 
            this.dgvBRGDetail.AllowUserToAddRows = false;
            this.dgvBRGDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvBRGDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBRGDetail.Location = new System.Drawing.Point(295, 286);
            this.dgvBRGDetail.Margin = new System.Windows.Forms.Padding(2);
            this.dgvBRGDetail.Name = "dgvBRGDetail";
            this.dgvBRGDetail.RowHeadersWidth = 62;
            this.dgvBRGDetail.RowTemplate.Height = 28;
            this.dgvBRGDetail.Size = new System.Drawing.Size(339, 188);
            this.dgvBRGDetail.TabIndex = 12;
            // 
            // fr_Permissions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 523);
            this.Controls.Add(this.dgvBRGDetail);
            this.Controls.Add(this.btn_Update_CT);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.txt_UserID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Fillter);
            this.Controls.Add(this.dgvBRGGroup);
            this.Controls.Add(this.cmbUsers);
            this.Controls.Add(this.dgvUserPermissions);
            this.Controls.Add(this.dgvUsers);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "fr_Permissions";
            this.Text = "fr_Permissions";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserPermissions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBRGGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBRGDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.DataGridView dgvUserPermissions;
        private System.Windows.Forms.DataGridView dgvBRGGroup;
        private System.Windows.Forms.ComboBox cmbUsers;
        private System.Windows.Forms.TextBox txt_Fillter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txt_UserID;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Button btn_Update_CT;
        private System.Windows.Forms.DataGridView dgvBRGDetail;
    }
}