namespace VOUCHER_CENTER.Presentation
{
    partial class fr_change_pass
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
            this.cmdthoat = new System.Windows.Forms.Button();
            this.cmddn = new System.Windows.Forms.Button();
            this.txt_new_pass = new System.Windows.Forms.TextBox();
            this.txt_old_pass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_confirm_pass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdthoat
            // 
            this.cmdthoat.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdthoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdthoat.Location = new System.Drawing.Point(230, 149);
            this.cmdthoat.Name = "cmdthoat";
            this.cmdthoat.Size = new System.Drawing.Size(125, 37);
            this.cmdthoat.TabIndex = 3;
            this.cmdthoat.Text = "Exit";
            this.cmdthoat.UseVisualStyleBackColor = true;
            this.cmdthoat.Click += new System.EventHandler(this.cmdthoat_Click);
            // 
            // cmddn
            // 
            this.cmddn.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmddn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmddn.Location = new System.Drawing.Point(63, 149);
            this.cmddn.Name = "cmddn";
            this.cmddn.Size = new System.Drawing.Size(125, 37);
            this.cmddn.TabIndex = 2;
            this.cmddn.Text = "Change";
            this.cmddn.UseVisualStyleBackColor = true;
            this.cmddn.Click += new System.EventHandler(this.cmddn_Click);
            // 
            // txt_new_pass
            // 
            this.txt_new_pass.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_new_pass.Location = new System.Drawing.Point(196, 49);
            this.txt_new_pass.Name = "txt_new_pass";
            this.txt_new_pass.PasswordChar = '*';
            this.txt_new_pass.Size = new System.Drawing.Size(214, 27);
            this.txt_new_pass.TabIndex = 1;
            // 
            // txt_old_pass
            // 
            this.txt_old_pass.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_old_pass.Location = new System.Drawing.Point(196, 12);
            this.txt_old_pass.Name = "txt_old_pass";
            this.txt_old_pass.PasswordChar = '*';
            this.txt_old_pass.Size = new System.Drawing.Size(214, 27);
            this.txt_old_pass.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 19);
            this.label3.TabIndex = 68;
            this.label3.Text = "New Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 19);
            this.label2.TabIndex = 69;
            this.label2.Text = "Old Password";
            // 
            // txt_confirm_pass
            // 
            this.txt_confirm_pass.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_confirm_pass.Location = new System.Drawing.Point(196, 86);
            this.txt_confirm_pass.Name = "txt_confirm_pass";
            this.txt_confirm_pass.PasswordChar = '*';
            this.txt_confirm_pass.Size = new System.Drawing.Size(214, 27);
            this.txt_confirm_pass.TabIndex = 70;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 19);
            this.label1.TabIndex = 71;
            this.label1.Text = "Confirm Password";
            // 
            // fr_change_pass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 201);
            this.Controls.Add(this.txt_confirm_pass);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdthoat);
            this.Controls.Add(this.cmddn);
            this.Controls.Add(this.txt_new_pass);
            this.Controls.Add(this.txt_old_pass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "fr_change_pass";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change User Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdthoat;
        private System.Windows.Forms.Button cmddn;
        private System.Windows.Forms.TextBox txt_new_pass;
        private System.Windows.Forms.TextBox txt_old_pass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_confirm_pass;
        private System.Windows.Forms.Label label1;
    }
}