using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using VOUCHER_CENTER.DataAccess;
using static VOUCHER_CENTER.Main;

namespace VOUCHER_CENTER.Presentation
{
    public partial class fr_VoucherSync : Form
    {
        private string connectionString = bientoancuc.connectionString;
        bool cap_nhat = false;

        public fr_VoucherSync()
        {
            InitializeComponent();
            ApplyEnterKeyToAllControls(this);
            dtpCreatedDate.CustomFormat = "dd/MM/yyyy";
            //frdate.CustomFormat = "dd/MM/yyyy";
            //todate.CustomFormat = "dd/MM/yyyy";
            dtpCreatedDate.Value = DateTime.Now;
            dtpCreatedDate.MaxDate = DateTime.Now;
            LoadUser_Location();
            lb_LocationsGroup.Text = GlobalVariables.Locations_Group.ToString();
            lb_LocationsDetail.Text = GlobalVariables.Locations_Detail.ToString();

        }

        private void LoadUser_Location()
        {
            try
            {
                if (GlobalVariables.User_Name == null || GlobalVariables.Locations_Group == null || GlobalVariables.Locations_Detail == null)
                {
                    MessageBox.Show("Thông tin người dùng hoặc vị trí không hợp lệ.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //int check_usr_group = int.Parse(GlobalVariables.Locations_Group);
                int check_usr_group = GlobalVariables.Locations_Group.Length;

                if (check_usr_group == 0)
                {
                    MessageBox.Show("Người này chưa đăng ký nhập Voucher cho điểm nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnSave.Enabled = false;
                    return;
                }

                //lb_LocationsGroup.Text = GlobalVariables.Locations_Group.ToString();
                //lb_LocationsDetail.Text = GlobalVariables.Locations_Detail.ToString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("Load_User_Location", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@use_name", GlobalVariables.User_Name.ToString());
                        command.Parameters.AddWithValue("@Locations_Group", GlobalVariables.Locations_Group.ToString());
                        command.Parameters.AddWithValue("@Locations_Detail", GlobalVariables.Locations_Detail.ToString());

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lb_LocationGroupName.Text = reader["LocationType"].ToString();
                                lb_LocationDetailName.Text = reader["locationname"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Không load được Location", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không load được Location: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            // Hỏi người dùng trước khi thực hiện lưu
            DialogResult dialogResult = MessageBox.Show("Mày chắc chưa :) ", "Phan Anh", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return; // Nếu người dùng chọn No, thoát khỏi phương thức
            }
            try
            {
                // Kiểm tra xem txtVoucherSerial có dữ liệu không
                if (string.IsNullOrWhiteSpace(txtVoucherSerial.Text))
                {
                    return; // Nếu không có dữ liệu, thoát khỏi phương thức
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var result = GetVoucherSerialDetails(connection, txtVoucherSerial.Text);

                    // Kiểm tra xem Voucher_Serial đã tồn tại trong HCRC_VOUCHER
                    if (result.Exists && cap_nhat == false)
                    {
                        MessageBox.Show($"Voucher Serial: {txtVoucherSerial.Text}\r\nĐã sử dụng tại {result.LocationType} - {result.LocationName}.\r\nCập nhật lần cuối vào {result.LastUpdate}.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtVoucherSerial.Focus();
                        return;
                    }
                    var result1 = GetVoucherSerialDetails_Local(connection, txtVoucherSerial.Text);

                    // Kiểm tra xem Voucher_Serial đã tồn tại trong local VOUCHER_SYNC
                    if (result1.Exists && cap_nhat == false)
                    {
                        MessageBox.Show($"Voucher Serial: {txtVoucherSerial.Text}\r\nĐã sử dụng tại {result1.LocationType} - {result1.LocationName}.\r\nCập nhật lần cuối vào {result1.LastUpdate}.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtVoucherSerial.Focus();
                        return;
                    }

                    // Thực hiện Insert dữ liệu
                    InsertVoucherSync(connection);
                }

                MessageBox.Show("Record saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Xóa các giá trị nhập vào
                ClearInputFields();
                cap_nhat = false;
                txtVoucherSerial.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool VoucherSerialExists(SqlConnection connection, string voucherSerial)
        {
            using (SqlCommand command = new SqlCommand("CheckVoucherSerialExistence", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Voucher_Serial", voucherSerial);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private void InsertVoucherSync(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("InsertVoucherSync", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", GlobalVariables.User_Name.ToString());
                command.Parameters.AddWithValue("@computername", GlobalVariables.Computer_Name.ToString());
                command.Parameters.AddWithValue("@TRANS_NUM", txtTransNum.Text);
                command.Parameters.AddWithValue("@Voucher_Serial", txtVoucherSerial.Text);
                //command.Parameters.AddWithValue("@Voucher_Code", Right(txtVoucherSerial.Text.Trim(), txtVoucherSerial.Text.Trim().Length - 1));
                command.Parameters.AddWithValue("@Created_Date", dtpCreatedDate.Value);
                //command.Parameters.AddWithValue("@Status", "USED");
                command.Parameters.AddWithValue("@Player_Name", txtPlayerName.Text);
                command.Parameters.AddWithValue("@Locations_Group", lb_LocationsGroup.Text);
                command.Parameters.AddWithValue("@Location_GroupName", lb_LocationGroupName.Text);
                command.Parameters.AddWithValue("@Locations_Detail", lb_LocationsDetail.Text);
                command.Parameters.AddWithValue("@Location_DetailName", lb_LocationDetailName.Text);
                command.Parameters.AddWithValue("@Description", txtDescription.Text);
                //command.Parameters.AddWithValue("@Last_update", DateTime.Now);
                //command.Parameters.AddWithValue("@Sync", "N");
                //command.Parameters.AddWithValue("@Sync_update", DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        private void txtVoucherSerial_TextChanged(object sender, EventArgs e)
        {

        }

        private bool VoucherSerialExist_Dsmart(SqlConnection connection, string voucherSerial)
        {
            using (SqlCommand command = new SqlCommand("CheckVoucherSerialExist_Dsmart", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Voucher_Serial", voucherSerial);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void txtVoucherSerial_Leave(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem txtVoucherSerial có dữ liệu không
                if (string.IsNullOrWhiteSpace(txtVoucherSerial.Text))
                {
                    return; // Nếu không có dữ liệu, thoát khỏi phương thức
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var result = GetVoucher(connection, txtVoucherSerial.Text);

                    // Kiểm tra xem Voucher_Serial 
                    if (result.Exists)
                    {
                        if (result.VoucherCheckValue == "0") 
                        {
                            MessageBox.Show($"Voucher : {txtVoucherSerial.Text} không có. \r\nVui lòng nhập giá trị khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtVoucherSerial.Focus();
                            return;
                        }
                        else if (result.VoucherCheckValue == "1")
                        {
                            MessageBox.Show($"Voucher : {txtVoucherSerial.Text} đã hết hạn sử dụng. \r\nVui lòng nhập giá trị khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtVoucherSerial.Focus();
                            return;
                        }
                        else if(result.VoucherCheckValue == "3" && (GlobalVariables.UserID != result.user_id))                           
                        {
                            MessageBox.Show($"Voucher Serial: {txtVoucherSerial.Text}\r\nĐã sử dụng tại {result.HcrcLocationType} - {result.HcrcLocationName}.\r\nCập nhật lần cuối vào {result.HcrcLastUpdate}.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtVoucherSerial.Focus();
                            return;
                        }
                        else if (result.VoucherCheckValue == "4" )//&& (GlobalVariables.UserID != result.user_id))
                        {
                            MessageBox.Show($"Voucher Serial: {txtVoucherSerial.Text}\r\nĐã sử dụng tại {result.VoucherSyncLocationGroupName} - {result.VoucherSyncLocationDetailName}.\r\nCập nhật lần cuối vào {result.VoucherSyncLastUpdate}.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtVoucherSerial.Focus();
                            return;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking Voucher serial: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVoucherSerial.Focus();
            }
        }
        private (bool Exists, int user_id, string VoucherCheckValue, string CardId, int Activate, int Status, DateTime DueDate, string VoucherSyncVoucherSerial, string VoucherSyncLocationGroupName, string VoucherSyncLocationDetailName, DateTime VoucherSyncLastUpdate, string VoucherSyncComputerName, string VoucherSyncTransNum, DateTime VoucherSyncCreatedDate, string VoucherSyncPlayerName, string VoucherSyncDescription, string HcrcVoucherSerial, string HcrcLocationType, string HcrcLocationName, DateTime HcrcLastUpdate) GetVoucher(SqlConnection connection, string voucherSerial)
        {
            using (SqlCommand command = new SqlCommand("CheckVoucher", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Voucher_Serial", voucherSerial);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string voucherCheckValue = reader.GetString(reader.GetOrdinal("Voucher_check_Value"));

                            string cardId = reader.GetString(reader.GetOrdinal("CARD_ID"));
                            int activate = reader.GetInt32(reader.GetOrdinal("ACTIVATE"));
                            int status = reader.GetInt32(reader.GetOrdinal("STATUS"));
                            DateTime dueDate = reader.GetDateTime(reader.GetOrdinal("DUE_DATE"));

                            int user_id = reader.GetInt32(reader.GetOrdinal("VOUCHER_SYNC_userid"));
                            string voucherSyncVoucherSerial = reader.IsDBNull(reader.GetOrdinal("VOUCHER_SYNC_Voucher_Serial")) ? null : reader.GetString(reader.GetOrdinal("VOUCHER_SYNC_Voucher_Serial"));
                            string voucherSyncLocationGroupName = reader.IsDBNull(reader.GetOrdinal("VOUCHER_SYNC_Location_GroupName")) ? null : reader.GetString(reader.GetOrdinal("VOUCHER_SYNC_Location_GroupName"));
                            string voucherSyncLocationDetailName = reader.IsDBNull(reader.GetOrdinal("VOUCHER_SYNC_Location_DetailName")) ? null : reader.GetString(reader.GetOrdinal("VOUCHER_SYNC_Location_DetailName"));
                            DateTime voucherSyncLastUpdate = reader.IsDBNull(reader.GetOrdinal("VOUCHER_SYNC_Last_update")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("VOUCHER_SYNC_Last_update"));
                            string voucherSyncComputerName = reader.IsDBNull(reader.GetOrdinal("VOUCHER_SYNC_Computer_name")) ? null : reader.GetString(reader.GetOrdinal("VOUCHER_SYNC_Computer_name"));
                            string voucherSyncTransNum = reader.IsDBNull(reader.GetOrdinal("VOUCHER_SYNC_TRANS_NUM")) ? null : reader.GetString(reader.GetOrdinal("VOUCHER_SYNC_TRANS_NUM"));
                            DateTime voucherSyncCreatedDate = reader.IsDBNull(reader.GetOrdinal("VOUCHER_SYNC_Created_Date")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("VOUCHER_SYNC_Created_Date"));
                            string voucherSyncPlayerName = reader.IsDBNull(reader.GetOrdinal("VOUCHER_SYNC_Player_Name")) ? null : reader.GetString(reader.GetOrdinal("VOUCHER_SYNC_Player_Name"));
                            string voucherSyncDescription = reader.IsDBNull(reader.GetOrdinal("VOUCHER_SYNC_Description")) ? null : reader.GetString(reader.GetOrdinal("VOUCHER_SYNC_Description"));

                            string hcrcVoucherSerial = reader.IsDBNull(reader.GetOrdinal("HCRC_Voucher_Serial")) ? null : reader.GetString(reader.GetOrdinal("HCRC_Voucher_Serial"));
                            string hcrcLocationType = reader.IsDBNull(reader.GetOrdinal("HCRC_Location_Type")) ? null : reader.GetString(reader.GetOrdinal("HCRC_Location_Type"));
                            string hcrcLocationName = reader.IsDBNull(reader.GetOrdinal("HCRC_Location_Name")) ? null : reader.GetString(reader.GetOrdinal("HCRC_Location_Name"));
                            DateTime hcrcLastUpdate = reader.IsDBNull(reader.GetOrdinal("HCRC_Last_update")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("HCRC_Last_update"));

                            return (true, user_id,voucherCheckValue, cardId, activate, status, dueDate, voucherSyncVoucherSerial, voucherSyncLocationGroupName, voucherSyncLocationDetailName, voucherSyncLastUpdate, voucherSyncComputerName, voucherSyncTransNum, voucherSyncCreatedDate, voucherSyncPlayerName, voucherSyncDescription, hcrcVoucherSerial, hcrcLocationType, hcrcLocationName, hcrcLastUpdate);

                    }
                }
            }
            return (false, 0,null, null, 0, 0, DateTime.MinValue, null, null, null, DateTime.MinValue, null, null, DateTime.MinValue, null, null, null, null, null, DateTime.MinValue);
        }


        //private void txtVoucherSerial_Leave_old1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Kiểm tra xem txtVoucherSerial có dữ liệu không
        //        if (string.IsNullOrWhiteSpace(txtVoucherSerial.Text))
        //        {
        //            return; // Nếu không có dữ liệu, thoát khỏi phương thức
        //        }

        //        //// Kiểm tra xem listView1 có bản ghi nào không
        //        //if (listView1.Items.Count == 0)
        //        //{
        //        //    MessageBox.Show("Không có bản ghi nào trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        //    return; // Nếu không có bản ghi, thoát khỏi phương thức
        //        //}






        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            var result = GetVoucherSerialDetails(connection, txtVoucherSerial.Text);

        //            // Kiểm tra xem Voucher_Serial đã tồn tại chưa trong HCRC_VOUCHER
        //            if (result.Exists)
        //            {
        //                MessageBox.Show($"Voucher Serial: {txtVoucherSerial.Text}\r\nĐã sử dụng tại {result.LocationType} - {result.LocationName}.\r\nCập nhật lần cuối vào {result.LastUpdate}.",
        //                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                txtVoucherSerial.Focus();
        //                return;
        //            }
        //            var result1 = GetVoucherSerialDetails_Local(connection, txtVoucherSerial.Text);

        //            // Kiểm tra xem Voucher_Serial đã tồn tại chưa trong local VOUCHER_SYNC
        //            if (result1.Exists)
        //            {
        //                if (GlobalVariables.User_Name.Trim() == result1.User_name.Trim())
        //                {
        //                    DialogResult dialogResult = MessageBox.Show($"Voucher Serial: {txtVoucherSerial.Text}\r\nĐã sử dụng tại {result1.LocationType} - {result1.LocationName}.\r\nCập nhật lần cuối vào {result1.LastUpdate}.\r\nBạn có muốn sửa lại không?",
        //                        "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //                    if (dialogResult == DialogResult.Yes)
        //                    {
        //                        // Gán các giá trị từ kết quả vào các điều khiển trên form
        //                        txtVoucherSerial.Enabled = false;
        //                        txtTransNum.Text = result1.TRANS_NUM;
        //                        txtPlayerName.Text = result1.Player_Name;
        //                        txtDescription.Text = result1.Description;
        //                        dtpCreatedDate.Value = result1.Created_Date;
        //                        cap_nhat = true;
        //                        //btnSave.Text = "Update";
        //                        // Thêm các gán biến khác nếu cần thiết
        //                        return;
        //                    }
        //                    else
        //                    {
        //                        txtVoucherSerial.Focus();
        //                        return;
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show($"Voucher Serial: {txtVoucherSerial.Text}\r\nĐã sử dụng tại {result1.LocationType} - {result1.LocationName}.\r\nCập nhật lần cuối vào {result1.LastUpdate}.",
        //                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    txtVoucherSerial.Focus();
        //                    return;
        //                }
        //            }

        //            // Kiểm tra xem Voucher_Serial có tồn tại trong Dsmart không
        //            if (!VoucherSerialExist_Dsmart(connection, txtVoucherSerial.Text))
        //            {
        //                MessageBox.Show("Voucher Serial Không có. Vui lòng nhập giá trị khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                txtVoucherSerial.Focus();
        //                return;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error checking Voucher serial: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        txtVoucherSerial.Focus();
        //    }
        //}
        private (bool Exists, string LocationType, string LocationName, DateTime LastUpdate) GetVoucherSerialDetails(SqlConnection connection, string voucherSerial)
        {
            using (SqlCommand command = new SqlCommand("CheckVoucherSerialExistence", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Voucher_Serial", voucherSerial);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int count = reader.GetInt32(0);
                        if (count > 0)
                        {
                            if (reader.NextResult() && reader.Read())
                            {
                                string locationType = reader.GetString(reader.GetOrdinal("Location_Type"));
                                string locationName = reader.GetString(reader.GetOrdinal("Location_Name"));
                                DateTime lastUpdate = reader.GetDateTime(reader.GetOrdinal("Last_update"));

                                return (true, locationType, locationName, lastUpdate);
                            }
                        }
                    }
                }
            }
            return (false, null, null, DateTime.MinValue);
        }
        private (bool Exists, string User_name, string LocationType, string LocationName, DateTime LastUpdate, string Computer_name, string TRANS_NUM, string Player_Name, string Description, DateTime Created_Date) GetVoucherSerialDetails_Local(SqlConnection connection, string voucherSerial)
        {
            using (SqlCommand command = new SqlCommand("CheckVoucherSerialExistence_Local", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Voucher_Serial", voucherSerial);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int count = reader.GetInt32(0);
                        if (count > 0)
                        {
                            if (reader.NextResult() && reader.Read())
                            {
                                string User_name = reader.GetString(reader.GetOrdinal("User_name"));
                                string locationType = reader.GetString(reader.GetOrdinal("Location_GroupName"));
                                string locationName = reader.GetString(reader.GetOrdinal("Location_DetailName"));
                                DateTime lastUpdate = reader.GetDateTime(reader.GetOrdinal("Last_update"));
                                string Computer_name = reader.GetString(reader.GetOrdinal("Computer_name"));
                                string TRANS_NUM = reader.GetString(reader.GetOrdinal("TRANS_NUM"));
                                string Player_Name = reader.GetString(reader.GetOrdinal("Player_Name"));
                                string Description = reader.GetString(reader.GetOrdinal("Description"));
                                DateTime Created_Date = reader.GetDateTime(reader.GetOrdinal("Created_Date"));

                                return (true, User_name, locationType, locationName, lastUpdate, Computer_name, TRANS_NUM, Player_Name, Description, Created_Date);
                            }
                        }
                    }
                }
            }
            return (false, null, null, null, DateTime.MinValue, null, null, null, null, DateTime.MinValue);
        }
        private void ClearInputFields()
        {
            txtVoucherSerial.Clear();
            txtDescription.Clear();
            txtPlayerName.Clear();
            txtTransNum.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
