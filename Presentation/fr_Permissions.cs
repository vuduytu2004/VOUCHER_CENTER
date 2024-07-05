using VOUCHER_CENTER.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lib.Utils.Package;
using static VOUCHER_CENTER.Main;

namespace VOUCHER_CENTER.Presentation
{
    public partial class fr_Permissions : Form
    {
        //private string connectionString = "Your_Connection_String"; // Thay thế bằng chuỗi kết nối của bạn
        private DataTable userDataTable;
        public fr_Permissions()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized; // Mở rộng cửa sổ để che phủ toàn bộ màn hình
            LoadUserData(); // Load dữ liệu cho DataGridView và ComboBox
            LoadMenuData();
            LoadRoleGroups();
            //dgvUserPermissions.CellFormatting += dgvUserPermissions_CellFormatting;

        }

        private void LoadUserData()
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                string query = "SELECT UserID, UserName,[FullName],[Locations_Group],[Locations_Detail],[status] FROM Users WITH (NOLOCK) where UserID<>'1'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                userDataTable = new DataTable();
                adapter.Fill(userDataTable);

                // Load dữ liệu vào ComboBox
                cmbUsers.DataSource = userDataTable;
                cmbUsers.DisplayMember = "UserName";
                cmbUsers.ValueMember = "UserID";

                // Load dữ liệu vào DataGridView
                dgvUsers.DataSource = userDataTable;
                dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            }
        }

        private void LoadMenuData()
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();
                string query = "";
                if (GlobalVariables.User_Name.UpperCase() == "ADMIN" || GlobalVariables.User_Name.UpperCase() == "BL160")
                {
                    query = "SELECT MenuItemID, MenuItemName, ParentMenuID FROM MenuItems WITH (NOLOCK) where Enable_Check=1 ORDER BY ord_by,COALESCE(ParentMenuID, MenuItemID), MenuItemID;";
                }
                else
                {
                    query = "SELECT MenuItemID, MenuItemName, ParentMenuID FROM MenuItems WITH (NOLOCK) where Enable_Check=1 and MenuItemID not in (5,6,7,8,9) ORDER BY ord_by,COALESCE(ParentMenuID, MenuItemID), MenuItemID;";
                }
                
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Load dữ liệu vào DataGridView
                dgvUserPermissions.DataSource = dataTable;
                DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
                checkboxColumn.HeaderText = "Enable";
                checkboxColumn.Name = "CheckboxColumn";
                dgvUserPermissions.Columns.Add(checkboxColumn);

                // Thiết lập tự động điều chỉnh kích thước cột
                dgvUserPermissions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvUserPermissions.Columns[0].ReadOnly = true;
                dgvUserPermissions.Columns[1].ReadOnly = true;
                dgvUserPermissions.Columns[2].ReadOnly = true;
            }
        }
        private void LoadRoleGroups()
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Locations_Group WITH (NOLOCK) where status=1";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Load dữ liệu vào DataGridView
                dgvBRGGroup.DataSource = dataTable;
                DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
                checkboxColumn.HeaderText = "Enable";
                checkboxColumn.Name = "CheckboxColumn";
                dgvBRGGroup.Columns.Add(checkboxColumn);
                // Thiết lập tự động điều chỉnh kích thước cột
                dgvBRGGroup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvBRGGroup.Columns[0].ReadOnly = true;
                dgvBRGGroup.Columns[1].ReadOnly = true;
            }
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Locations_Detail WITH (NOLOCK) where status=1";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Load dữ liệu vào DataGridView
                dgvBRGDetail.DataSource = dataTable;
                DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
                checkboxColumn.HeaderText = "Enable";
                checkboxColumn.Name = "CheckboxColumn";
                dgvBRGDetail.Columns.Add(checkboxColumn);
                // Thiết lập tự động điều chỉnh kích thước cột
                dgvBRGDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvBRGDetail.Columns[0].ReadOnly = true;
                dgvBRGDetail.Columns[1].ReadOnly = true;
            }
        }

        private void btnAddToRoleGroup_Click(object sender, EventArgs e)
        {
            if (cmbUsers.SelectedValue != null)
            {
                int userID = Convert.ToInt32(cmbUsers.SelectedValue);

                // Thêm userID vào UserPermissionRoleGroups
                using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO UserPermissionRoleGroups (UserPermissionID, RoleGroupID) VALUES (@UserID, @RoleGroupID)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@RoleGroupID", 1); // Giả sử RoleGroupID là 1 (thay đổi theo cấu trúc thực tế của bạn)

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("User added to Role Group successfully!");
                }
            }
            else
            {
                MessageBox.Show("Please select a user first.");
            }
        }

        private void btnAddToUserPermission_Click(object sender, EventArgs e)
        {
            if (cmbUsers.SelectedValue != null && dgvUserPermissions.SelectedRows.Count > 0)
            {
                int userID = Convert.ToInt32(cmbUsers.SelectedValue);
                int menuItemID = Convert.ToInt32(dgvUserPermissions.SelectedRows[0].Cells["MenuItemID"].Value);

                // Thêm userID và menuItemID vào UserPermissions
                using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO UserPermissions (UserID, MenuItemID) VALUES (@UserID, @MenuItemID)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@MenuItemID", menuItemID);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("User added to Permission successfully!");
                }
            }
            else
            {
                MessageBox.Show("Please select a user and a menu item first.");
            }
        }
        private void cmbUsers_KeyUp(object sender, KeyEventArgs e)
        {
            // Lọc dữ liệu trong DataTable dựa trên chuỗi tìm kiếm
            if (userDataTable != null)
            {
                string filterExpression = $"UserName LIKE '%{cmbUsers.Text}%'";
                userDataTable.DefaultView.RowFilter = filterExpression;
            }

            //Mở danh sách dọc xuống khi có dữ liệu lọc và có ít nhất một item
            if (!string.IsNullOrEmpty(cmbUsers.Text) && cmbUsers.Items.Count >= 0)
            {
                cmbUsers.DroppedDown = true;
            }
            else
            {
                var a = cmbUsers.SelectedIndex;
                if (a > 0)
                {
                    cmbUsers.DroppedDown = false;
                }
            }
        }


        private void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Đặt giá trị Text của ComboBox thành giá trị đã chọn
            //cmbUsers.Text = cmbUsers.SelectedItem?.ToString();
            cmbUsers.SelectionStart = cmbUsers.Text.Length;
            cmbUsers.SelectionLength = 0;
        }

        private void txt_Fillter_TextChanged(object sender, EventArgs e)
        {
            if (txt_Fillter != null)
            {
                string filterExpression = $"UserName LIKE '%{txt_Fillter.Text}%'";
                filterExpression += $"or FullName LIKE '%{txt_Fillter.Text}%'";
                filterExpression += $"or Locations_Group LIKE '%{txt_Fillter.Text}%'";
                filterExpression += $"or Locations_Detail LIKE '%{txt_Fillter.Text}%'";
                //filterExpression += $"or DSMART LIKE '%{txt_Fillter.Text}%'";
                userDataTable.DefaultView.RowFilter = filterExpression;
            }
            else
            {
                string filterExpression = $"UserName LIKE '%{txt_Fillter.Text}'";
                userDataTable.DefaultView.RowFilter = filterExpression;
            }
        }
        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                int selectedUserID = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);
                txt_UserID.Text = selectedUserID.ToString();
            }
        }
        private void txt_UserID_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_UserID.Text))
            {
                int userID = Convert.ToInt32(txt_UserID.Text);
                List<int[]> menuItemIDs = GetMenuItemIDsForUser(userID);

                // Tiếp theo, thực hiện so sánh với các MenuItemID trên dgvUserPermissions
                CompareMenuItemIDs(menuItemIDs);
            }
        }

        private List<int[]> GetMenuItemIDsForUser(int userID)
        {
            List<int[]> menuItemIDs = new List<int[]>();

            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetUser_MenuItemID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số vào thủ tục
                    command.Parameters.AddWithValue("@UserID", userID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int menuItemID1 = reader.GetInt32(0); // Giả sử MenuItemID là cột đầu tiên trong kết quả
                            int menuItemID2 = reader.GetInt32(1); // Giả sử Locations_Group là cột thứ hai trong kết quả
                            int menuItemID3 = reader.GetInt32(2); // Giả sử Locations_Detail là cột thứ hai trong kết quả

                            // Thêm mảng chứa 2 giá trị vào danh sách
                            menuItemIDs.Add(new int[] { menuItemID1, menuItemID2, menuItemID3 });
                            //menuItemIDs.Add(new int[] { menuItemID1 });
                        }
                    }
                }
            }

            return menuItemIDs;
        }

        private void CompareMenuItemIDs(List<int[]> menuItemIDs)
        {
            HashSet<int> uniqueMenuItemIDs = new HashSet<int>();
            // Duyệt qua các dòng trong dgvUserPermissions
            foreach (DataGridViewRow row in dgvUserPermissions.Rows)
            {
                // Lấy giá trị MenuItemID từ dòng hiện tại trong dgvUserPermissions
                int menuItemID = Convert.ToInt32(row.Cells["MenuItemID"].Value);

                // Kiểm tra xem MenuItemID có trong mảng menuItemIDs không
                bool menuItemExists = menuItemIDs.Any(pair => pair[0] == menuItemID || pair[1] == menuItemID);

                // Gán giá trị cho ô checkbox
                row.Cells["CheckboxColumn"].Value = menuItemExists;
                //row.Cells["ParentMenuID"].Value = menuItemFarther;
                //// Gán màu xám cho dòng nếu MenuItemID không tồn tại
                //row.DefaultCellStyle.BackColor = menuItemExists ? Color.White : Color.DarkGoldenrod;
                // Gán màu xám cho dòng nếu MenuItemID tồn tại

                var aaa = row.Cells["ParentMenuID"].Value?.ToString().Length ?? 0;
                if (aaa == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.IndianRed;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = menuItemExists && aaa != 0 ? Color.CadetBlue : Color.White;
                }

            }
            // Duyệt qua các dòng trong dgvUserPermissions
            foreach (DataGridViewRow row in dgvBRGGroup.Rows)
            {
                // Lấy giá trị MenuItemID từ dòng hiện tại trong dgvUserPermissions
                int menuItemID = Convert.ToInt32(row.Cells["ID"].Value);

                // Kiểm tra xem MenuItemID có trong mảng menuItemIDs không
                bool menuItemExists = menuItemIDs.Any(pair => pair[1] == menuItemID  );

                // Gán giá trị cho ô checkbox
                row.Cells["CheckboxColumn"].Value = menuItemExists;

                //// Gán màu xám cho dòng nếu MenuItemID không tồn tại
                //row.DefaultCellStyle.BackColor = menuItemExists ? Color.White : Color.DarkGoldenrod;
                // Gán màu xám cho dòng nếu MenuItemID tồn tại
                row.DefaultCellStyle.BackColor = menuItemExists ? Color.Blue : Color.White;
            }
            // Duyệt qua các dòng trong dgvUserPermissions
            foreach (DataGridViewRow row in dgvBRGDetail.Rows)
            {
                // Lấy giá trị MenuItemID từ dòng hiện tại trong dgvUserPermissions
                int menuItemID = Convert.ToInt32(row.Cells["LocationType"].Value);
                int menuItemID1 = Convert.ToInt32(row.Cells["LocationCode"].Value);

                // Kiểm tra xem MenuItemID có trong mảng menuItemIDs không
                bool menuItemExists = menuItemIDs.Any(pair => pair[1] == menuItemID && pair[2] == menuItemID1);

                // Gán giá trị cho ô checkbox
                row.Cells["CheckboxColumn"].Value = menuItemExists;

                //// Gán màu xám cho dòng nếu MenuItemID không tồn tại
                //row.DefaultCellStyle.BackColor = menuItemExists ? Color.White : Color.DarkGoldenrod;
                // Gán màu xám cho dòng nếu MenuItemID tồn tại
                row.DefaultCellStyle.BackColor = menuItemExists ? Color.Blue : Color.White;
            }
        }

        //private void txt_UserID_TextChanged888(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txt_UserID.Text))
        //    {
        //        int userID = Convert.ToInt32(txt_UserID.Text);
        //        Tuple<int, int> menuItemIDs = GetMenuItemIDsForUser888(userID);

        //        // Tiếp theo, thực hiện so sánh với các MenuItemID trên dgvUserPermissions
        //        CompareMenuItemIDs888(menuItemIDs);
        //    }
        //}
        //private List<Tuple<int, int>> GetMenuItemIDsForUser888(int userID)
        //{
        //    Tuple<int, int> menuItemIDs = new Tuple<int, int>;

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        using (SqlCommand command = new SqlCommand("GetUser_MenuItemID", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;

        //            // Thêm tham số vào thủ tục
        //            command.Parameters.AddWithValue("@UserID", userID);

        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    int menuItemID1 = reader.GetInt32(0); // Giả sử MenuItemID là cột đầu tiên trong kết quả
        //                    int menuItemID2 = reader.GetInt32(1); // Giả sử MenuItemID là cột thứ hai trong kết quả
        //                    menuItemIDs.Add(Tuple.Create(menuItemID1, menuItemID2));
        //                }
        //            }
        //        }
        //    }

        //    return menuItemIDs;
        //}
        //private void CompareMenuItemIDs888(List<Tuple<int, int>> menuItemIDs)
        //{
        //    HashSet<int> uniqueMenuItemIDs = new HashSet<int>();
        //    // Duyệt qua các dòng trong dgvUserPermissions
        //    foreach (DataGridViewRow row in dgvUserPermissions.Rows)
        //    {
        //        // Lấy giá trị MenuItemID từ dòng hiện tại trong dgvUserPermissions
        //        int menuItemID = Convert.ToInt32(row.Cells["MenuItemID"].Value);

        //        // Kiểm tra xem MenuItemID có trong mảng menuItemIDs không
        //        bool menuItemExists = menuItemIDs.Any(tuple => tuple.Item1 == menuItemID); // || tuple.Item2 == menuItemID);

        //        // Gán giá trị cho ô checkbox
        //        row.Cells["CheckboxColumn"].Value = menuItemExists;

        //        // Gán màu xám cho dòng nếu MenuItemID không tồn tại
        //        row.DefaultCellStyle.BackColor = menuItemExists ? Color.White : Color.DarkGoldenrod;
        //    }
        //    // Duyệt qua các dòng trong dgvUserPermissions
        //    foreach (DataGridViewRow row in dgvRoleGroup.Rows)
        //    {
        //        // Lấy giá trị MenuItemID từ dòng hiện tại trong dgvUserPermissions
        //        int menuItemID = Convert.ToInt32(row.Cells["RoleGroupID"].Value);

        //        // Kiểm tra xem MenuItemID có trong mảng menuItemIDs không
        //        bool menuItemExists = menuItemIDs.Any(tuple => tuple.Item2 == menuItemID); // || tuple.Item2 == menuItemID);

        //        // Gán giá trị cho ô checkbox
        //        row.Cells["CheckboxColumn"].Value = menuItemExists;

        //        // Gán màu xám cho dòng nếu MenuItemID không tồn tại
        //        row.DefaultCellStyle.BackColor = menuItemExists ? Color.White : Color.DarkGoldenrod;
        //    }
        //}
        //private void txt_UserID_TextChanged99(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txt_UserID.Text))
        //    {
        //        int userID = Convert.ToInt32(txt_UserID.Text);
        //        List<Tuple<int, int>> menuItemIDs = GetMenuItemIDsForUser99(userID);

        //        // Tiếp theo, thực hiện so sánh với các MenuItemID trên dgvUserPermissions
        //        CompareMenuItemIDs(menuItemIDs);
        //    }
        //}
        //private List<Tuple<int, int>> GetMenuItemIDsForUser99(int userID)
        //{
        //    List<Tuple<int, int>> menuItemIDs = new List<Tuple<int, int>>();

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        using (SqlCommand command = new SqlCommand("GetUser_MenuItemID", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;

        //            // Thêm tham số vào thủ tục
        //            command.Parameters.AddWithValue("@UserID", userID);

        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    int menuItemID1 = reader.GetInt32(0); // Giả sử MenuItemID là cột đầu tiên trong kết quả
        //                    int menuItemID2 = reader.GetInt32(1); // Giả sử MenuItemID là cột thứ hai trong kết quả
        //                    menuItemIDs.Add(Tuple.Create(menuItemID1, menuItemID2));
        //                }
        //            }
        //        }
        //    }

        //    return menuItemIDs;
        //}


        private void CompareMenuItemIDs11(Tuple<int, int> menuItemIDs)
        {
            // Duyệt qua các dòng trong dgvUserPermissions
            foreach (DataGridViewRow row in dgvUserPermissions.Rows)
            {
                // Lấy giá trị MenuItemID từ dòng hiện tại trong dgvUserPermissions
                int menuItemID = Convert.ToInt32(row.Cells["MenuItemID"].Value);

                // Kiểm tra xem MenuItemID có trong mảng menuItemIDs không
                bool menuItemExists = menuItemID == menuItemIDs.Item1 || menuItemID == menuItemIDs.Item2;

                // Gán giá trị cho ô checkbox
                row.Cells["CheckboxColumn"].Value = menuItemExists;

                // Gán màu xám cho dòng nếu MenuItemID không tồn tại
                row.DefaultCellStyle.BackColor = menuItemExists ? Color.White : Color.DarkGoldenrod;
            }
        }
        private Tuple<int, int> GetMenuItemIDsForUser2(int userID)
        {
            int menuItemID1 = -1;
            int menuItemID2 = -1;

            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetUser_MenuItemID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số vào thủ tục
                    command.Parameters.AddWithValue("@UserID", userID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            menuItemID1 = reader.GetInt32(0); // Giả sử MenuItemID là cột đầu tiên trong kết quả

                            // Kiểm tra xem cột thứ hai có tồn tại không
                            if (reader.FieldCount > 1)
                            {
                                menuItemID2 = reader.GetInt32(1); // Giả sử MenuItemID là cột thứ hai trong kết quả
                            }
                        }
                        // Không cần xử lý trường hợp không có dòng dữ liệu trả về, vì chúng ta đã gán giá trị mặc định cho menuItemID1 và menuItemID2
                    }
                }
            }

            return Tuple.Create(menuItemID1, menuItemID2);
        }

        private void txt_UserID_TextChanged1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_UserID.Text))
            {
                int userID = Convert.ToInt32(txt_UserID.Text);
                int[] menuItemIDs = GetMenuItemIDsForUser1(userID);

                // Tiếp theo, thực hiện so sánh với các MenuItemID trên dgvUserPermissions
                CompareMenuItemIDs1(menuItemIDs);
            }
        }

        private int[] GetMenuItemIDsForUser1(int userID)
        {
            List<int> menuItemIDs = new List<int>();

            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetUser_MenuItemID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số vào thủ tục
                    command.Parameters.AddWithValue("@UserID", userID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int menuItemID = reader.GetInt32(0); // Giả sử MenuItemID là cột đầu tiên trong kết quả
                            menuItemIDs.Add(menuItemID);
                        }
                    }
                }
            }

            return menuItemIDs.ToArray();
        }


        private void CompareMenuItemIDs1(int[] menuItemIDs)
        {
            // Duyệt qua các dòng trong dgvUserPermissions
            foreach (DataGridViewRow row in dgvUserPermissions.Rows)
            {
                // Lấy giá trị MenuItemID từ dòng hiện tại trong dgvUserPermissions
                int menuItemID = Convert.ToInt32(row.Cells["MenuItemID"].Value);

                // Kiểm tra xem MenuItemID có trong mảng menuItemIDs không
                bool menuItemExists = menuItemIDs.Contains(menuItemID);

                // Gán giá trị cho ô checkbox
                row.Cells["CheckboxColumn"].Value = menuItemExists;

                // Gán màu xám cho dòng nếu MenuItemID không tồn tại
                row.DefaultCellStyle.BackColor = menuItemExists ? Color.White : Color.DarkGoldenrod;
            }
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int selectedUserID = Convert.ToInt32(dgvUsers.Rows[e.RowIndex].Cells["UserID"].Value);
                txt_UserID.Text = selectedUserID.ToString();
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////////////////////////////////////
            //  Khi khai bao thêm 1  nhóm thì thêm vào GroupsPermissions, [RoleGroupID]             //
            //////////////////////////////////////////////////////////////////////////////////////////
            
            var convert_txt_UserID = Convert.ToInt32(txt_UserID.Text);
            foreach (DataGridViewRow row in dgvBRGGroup.Rows)
            {
                int RoleGroupID = Convert.ToInt32(row.Cells["RoleGroupID"].Value);
                int RoleGroupID_Check = Convert.ToInt32(row.Cells["CheckboxColumn"].Value);
                // Thêm userID và menuItemID vào UserPermissions
                using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
                {
                    connection.Open();

                    string insertQuery = "IF EXISTS (SELECT * FROM [UserPermissionRoleGroups] WITH (NOLOCK) WHERE UserPermissionID = @UserID AND RoleGroupID = @RoleGroupID) BEGIN IF  @RoleGroupID_Check=0 DELETE FROM [UserPermissionRoleGroups] WHERE UserPermissionID = @UserID AND RoleGroupID = @RoleGroupID; END " +
                        "ELSE BEGIN IF  @RoleGroupID_Check=1 INSERT INTO [UserPermissionRoleGroups] (UserPermissionID, RoleGroupID) VALUES (@UserID, @RoleGroupID); END";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", convert_txt_UserID);
                        command.Parameters.AddWithValue("@RoleGroupID", RoleGroupID);
                        command.Parameters.AddWithValue("@RoleGroupID_Check", RoleGroupID_Check);
                        command.ExecuteNonQuery();
                    }

                    //MessageBox.Show("User added to Permission successfully!");
                }
            }
            ///// phần này hơi lôm côm, nhg để sửa lai sau vậy
            //foreach (DataGridViewRow row in dgvUserPermissions.Rows)
            //{
            //    int MenuItemID = Convert.ToInt32(row.Cells["MenuItemID"].Value);
            //    int MenuItemID_Check = Convert.ToInt32(row.Cells["CheckboxColumn"].Value);
            //    // Thêm userID và menuItemID vào UserPermissions
            //    using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            //    {
            //        connection.Open();

            //        string insertQuery = "IF EXISTS (SELECT * FROM [UserPermissions] WITH (NOLOCK) WHERE UserID = @UserID AND MenuItemID = @MenuItemID )" +
            //            "BEGIN IF @MenuItemID_Check=0 DELETE FROM [UserPermissions] WHERE UserID = @UserID AND MenuItemID  = @MenuItemID; END " +
            //            "ELSE BEGIN IF  @MenuItemID_Check=1 INSERT INTO [UserPermissions] (UserID, MenuItemID) VALUES (@UserID, @MenuItemID); END";
            //        using (SqlCommand command = new SqlCommand(insertQuery, connection))
            //        {
            //            command.Parameters.AddWithValue("@UserID", convert_txt_UserID);
            //            command.Parameters.AddWithValue("@MenuItemID", MenuItemID);
            //            command.Parameters.AddWithValue("@MenuItemID_Check", MenuItemID_Check);
            //            command.ExecuteNonQuery();
            //        }

            //        //MessageBox.Show("User added to Permission successfully!");
            //    }
            //}
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                string insertQuery = "IF EXISTS (SELECT * FROM [Users] WITH (NOLOCK) WHERE [UserID] = @UserID ) BEGIN UPDATE dbo.Users SET status=1 WHERE [UserID] = @UserID  END";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", convert_txt_UserID);
                    command.ExecuteNonQuery();
                }

                //MessageBox.Show("User added to Permission successfully!");
            }

            txt_UserID_TextChanged(sender, e);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Update_CT_Click(object sender, EventArgs e)
        {
            var convert_txt_UserID = Convert.ToInt32(txt_UserID.Text);

            /// phần này hơi lôm côm, nhg để sửa lai sau vậy
            foreach (DataGridViewRow row in dgvUserPermissions.Rows)
            {
                int MenuItemID = Convert.ToInt32(row.Cells["MenuItemID"].Value);
                int MenuItemID_Check = Convert.ToInt32(row.Cells["CheckboxColumn"].Value);
                // Thêm userID và menuItemID vào UserPermissions
                using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
                {
                    connection.Open();

                    string insertQuery = "IF EXISTS (SELECT * FROM [UserPermissions] WITH (NOLOCK) WHERE UserID = @UserID AND MenuItemID = @MenuItemID )" +
                        "BEGIN IF @MenuItemID_Check=0 DELETE FROM [UserPermissions] WHERE UserID = @UserID AND MenuItemID  = @MenuItemID; END " +
                        "ELSE BEGIN IF  @MenuItemID_Check=1 INSERT INTO [UserPermissions] (UserID, MenuItemID) VALUES (@UserID, @MenuItemID); END";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", convert_txt_UserID);
                        command.Parameters.AddWithValue("@MenuItemID", MenuItemID);
                        command.Parameters.AddWithValue("@MenuItemID_Check", MenuItemID_Check);
                        command.ExecuteNonQuery();
                    }

                    //MessageBox.Show("User added to Permission successfully!");
                }
            }
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                string insertQuery = "IF EXISTS (SELECT * FROM [Users] WITH (NOLOCK) WHERE [UserID] = @UserID ) BEGIN UPDATE dbo.Users SET status=1 WHERE [UserID] = @UserID  END";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserID", convert_txt_UserID);
                    command.ExecuteNonQuery();
                }

                //MessageBox.Show("User added to Permission successfully!");
            }

            txt_UserID_TextChanged(sender, e);
        }
    }
}
