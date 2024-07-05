using Lib.Utils.Package;
using VOUCHER_CENTER.DataAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static VOUCHER_CENTER.Main;

namespace VOUCHER_CENTER.Presentation
{
    public partial class fr_change_pass : Form
    {
        //private string connectionString = "Data Source=172.16.71.170;Initial Catalog=HCRC_VOUCHER_CENTER;User ID=hieund;Password=123@123123;";
        //public int UserID { get; private set; } // Thuộc tính để lưu trữ UserID
        public fr_change_pass()
        {
            InitializeComponent();
            ApplyEnterKeyToAllControls(this);
        }
        

        private void cmddn_Click(object sender, EventArgs e)
        {
            string oldPassword = AES.Encrypt(txt_old_pass.Text);
            string newPassword = AES.Encrypt(txt_new_pass.Text);
            string confirmPassword = AES.Encrypt(txt_confirm_pass.Text);
            //GlobalVariables.User_Pass = AES.Encrypt(txt_new_pass.Text);

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int userID = GlobalVariables.UserID;
            int result = ChangeUserPassword(userID, oldPassword, newPassword);

            if (result == 1)
            {
                MessageBox.Show("Đổi mật khẩu thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Mật khẩu cũ không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmdthoat_Click(object sender, EventArgs e)
        {
            //if (GlobalVariables.First_Time == 1)
            //{
            //    Application.Exit();
            //}
            //else
            //{
                this.Close();
            //}
        }
        private int ChangeUserPassword(int userID, string oldPassword, string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ChangeUserPassword", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@OldPassword", oldPassword);
                        command.Parameters.AddWithValue("@NewPassword", newPassword);

                        SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int);
                        resultParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(resultParam);

                        command.ExecuteNonQuery();

                        int result = (int)resultParam.Value;
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }

        // Sự kiện xử lý khi nhấn Enter
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Chuyển focus sang control tiếp theo
                SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        // Phương thức áp dụng sự kiện KeyDown cho tất cả các control trên form
        private void ApplyEnterKeyToAllControls(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                ctrl.KeyDown += OnKeyDown;
                ApplyEnterKeyToAllControls(ctrl);
            }
        }

    }
}
