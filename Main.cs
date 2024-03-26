using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using Lib.Utils.Package;
using Report_Center.DataAccess;
using Report_Center.Presentation;

namespace Report_Center
{
    public partial class Main : Form
    {

        ConnectDB cn = new ConnectDB();
        //public static string connectionString = "Data Source=" + bientoancuc.Server_nguon + ";Initial Catalog =" + bientoancuc.Database_nguon + "; User ID=" + bientoancuc.user_nguon + ";Password=" + bientoancuc.pass_nguon + ";Trusted_Connection=False;";
        public static string pass_all = AES.Encrypt("thoichanson");
        private bool isLoggedIn = false;
        private bool isMainFormHidden = false;
        // Lấy đường dẫn của thư mục chạy của dự án
        public static string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // Tạo đường dẫn đến thư mục GOMSL
        public static string LogsDirectory = Path.Combine(projectDirectory, "LOGS");

        // Tạo tên file theo tên máy
        public static string coputerName = System.Environment.MachineName;

        // Tạo đường dẫn đầy đủ đến file txt
        public static string filePath = Path.Combine(LogsDirectory, coputerName);
        // Tạo đường dẫn đầy đủ đến file txt
        public static string File_INI = Path.Combine(projectDirectory, "Rp_Center");


        public static class GlobalVariables
        {
            public static int UserID { get; set; }
            public static string User_Name { get; set; }
            public static string User_Pass { get; set; }
            public static int First_Time { get; set; }
            public static int demSl { get; set; }

            // Các biến toàn cục khác có thể được thêm vào đây
        }
        public Main()
        {
            InitializeComponent();
            khoi_tao_cac_bien();
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized; // Mở rộng cửa sổ để che phủ toàn bộ màn hình
            GlobalVariables.First_Time = 1;
            ShowLoginForm();


            // Tạo một ToolStripStatusLabel
            ToolStripStatusLabel toolStripStatusLabel3 = new ToolStripStatusLabel();

            // Đặt văn bản cho ToolStripStatusLabel
            toolStripStatusLabel3.Text = "Nội dung của bạn";

            // Đặt Spring thành true để chiếm hết không gian từ bên trái
            toolStripStatusLabel3.Spring = true;

            // Thêm nó vào StatusStrip
            statusStrip1.Items.Add(toolStripStatusLabel3);


            //BuildDynamicMenu();
        }

        private void ShowLoginForm()
        {
            fr_DangNhap loginForm = new fr_DangNhap();
            // Gắn sự kiện FormClosed để xác định khi nào form đã đóng
            loginForm.FormClosed += (sender, e) => LoginFormClosed();

            // Đặt cửa sổ chính làm chủ của biểu mẫu đăng nhập
            // Kiểm tra xem có phải lần đầu tiên hay không
            if (GlobalVariables.First_Time == 0)
            {
                // Ẩn form Main
                this.Hide();
                isMainFormHidden = true;

            }

                loginForm.ShowDialog();
   
            // Hiển thị biểu mẫu đăng nhập như một cửa sổ top-level

            //DialogResult result = loginForm.ShowDialog();

            // Kiểm tra xem người dùng đã đăng nhập thành công hay không
            if (loginForm.DialogResult == DialogResult.OK)
            {
                // Thực hiện các bước thoát chương trình
                foreach (Form frm in this.MdiChildren)
                {
                    frm.Close();
                }
                var chua_pham_quyen = GlobalVariables.UserID;
                isLoggedIn = true;
                if (!Directory.Exists(LogsDirectory))
                {
                    Directory.CreateDirectory(LogsDirectory);
                }
                try
                {
                    using (FileStream fs = File.Create(filePath + ".txt"))
                    {
                        // Do any additional operations with the file if needed
                    }
                    //File.Create(filePath + ".txt");
                    //System.Windows.Forms.Application.Exit();
                }
                catch { }
                toolStripStatusLabel2.Text= "   Người sử dụng: "+GlobalVariables.User_Name.ToString();
                BuildDynamicMenu(GlobalVariables.UserID);
                if (chua_pham_quyen == -2) { MessageBox.Show("Bạn chưa được phân quyền, \r\n Vui lòng liên hệ với Quản trị viên ","Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            else if (GlobalVariables.First_Time==0)
            {
                return;
            }    
            else
            {
                // Người dùng đã huỷ hoặc đăng nhập không thành công, có thể thoát ứng dụng hoặc thực hiện xử lý khác tùy thuộc vào yêu cầu
                MessageBox.Show("Đăng nhập không thành công. Ứng dụng sẽ thoát.");
                Application.Exit();
            }
        }

        // Phần Code để Build Menu theo Phân Quyền
        private void BuildDynamicMenu(int userID)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();
                if (userID == 1)
                {

                    // Truy vấn cơ sở dữ liệu để lấy danh sách MenuItemID mà người dùng có quyền truy cập
                    //string query_admin = "SELECT MenuItemID FROM MenuItems";
                    string query_admin = "SELECT * FROM MenuItems WITH (NOLOCK) WHERE Enable_Check=1 -- WHERE MenuItemID BETWEEN 1 AND 10";
                    using (SqlCommand command = new SqlCommand(query_admin, connection))
                    {
                        //command.Parameters.AddWithValue("@UserID", userID);

                        // Thực hiện đọc danh sách MenuItemID
                        List<int> allowedMenuItems = new List<int>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                allowedMenuItems.Add(reader.GetInt32(0));
                            }
                        }

                        // Tạo menu dựa trên danh sách MenuItemID
                        CreateMenuItems(allowedMenuItems);
                    }
                }
                else
                {
                    using (SqlCommand command = new SqlCommand("GetUserPermissions", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", userID);

                        // Thực hiện đọc danh sách MenuItemID
                        List<int> allowedMenuItems = new List<int>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                allowedMenuItems.Add(reader.GetInt32(0));
                            }
                        }

                        // Tạo menu dựa trên danh sách MenuItemID
                        CreateMenuItems(allowedMenuItems);
                    }
                }

            }
        }

        private void CreateMenuItems(List<int> allowedMenuItems)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                // Xóa tất cả các menu items
                menuStrip1.Items.Clear();
                connection.Open();
                //string query = "SELECT * FROM MenuItems where MenuItemID in "+ allowedMenuItems + " ORDER BY MenuItemID,MenuLevel, ParentMenuID, MenuItemName";
                // Xây dựng câu truy vấn với danh sách giá trị MenuItemID
                string menuItemIds = string.Join(",", allowedMenuItems.Select(id => id.ToString()));
                string query = $"SELECT * FROM MenuItems WITH (NOLOCK) WHERE MenuItemID IN ({menuItemIds}) and  Enable_Check=1  ORDER BY MenuItemID, MenuLevel, ParentMenuID, MenuItemName";
                //string query = $"SELECT * FROM MenuItems ORDER BY MenuItemID, MenuLevel, ParentMenuID, MenuItemName";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Lấy đường dẫn của thư mục chạy của dự án
                projectDirectory = AppDomain.CurrentDomain.BaseDirectory;

                foreach (DataRow row in dataTable.Rows)
                {
                    int menuLevel = Convert.ToInt32(row["MenuLevel"]);
                    int MenuItemID = Convert.ToInt32(row["MenuItemID"]);
                    int? parentMenuID = row["ParentMenuID"] as int?;
                    ToolStripMenuItem menuItem = new ToolStripMenuItem(row["MenuItemName"].ToString());
                    string urlForm = row["URL"].ToString();
                    // Lấy đường dẫn hình ảnh từ cột Menu_ImageList
                    string imagePath = row["Menu_ImageList"].ToString();
                    if (imagePath != null)
                    {
                        // Kết hợp đường dẫn của thư mục chạy với tên tệp
                        string fullImagePath = Path.Combine(projectDirectory, imagePath);

                        // Kiểm tra xem đường dẫn hình ảnh có tồn tại không
                        if (File.Exists(fullImagePath))
                        {
                            // Tạo đối tượng hình ảnh từ đường dẫn
                            Image image = Image.FromFile(fullImagePath);
                            // Gán hình ảnh cho ToolStripMenuItem
                            menuItem.Image = image;
                        }
                    }
                    // Thêm sự kiện Click cho mỗi ToolStripMenuItem
                    if (urlForm != null)
                    {
                        menuItem.Click += (sender, e) => MenuItem_Click(sender, e, urlForm);
                    }
                    if (menuItem.Text == "Exit")
                    {
                        menuItem.Click += (sender, e) => Main_FormClosing(sender, new FormClosingEventArgs(CloseReason.UserClosing, false));
                    }

                    if (menuLevel == 0)
                    {
                        menuItem.MouseEnter += MenuItem_MouseEnter; // Sự kiện khi chuột vào menu cha
                        menuStrip1.Items.Add(menuItem);
                        menuItem.Tag = MenuItemID; // Thiết lập Tag cho menu cha
                    }
                    else
                    {
                        ToolStripMenuItem parentMenuItem = FindParentMenuItem(menuStrip1.Items, parentMenuID);
                        if (menuItem.Text == "separator") // MenuItemID = 4 là separator
                        {
                            parentMenuItem.DropDownItems.Add(new ToolStripSeparator());
                        }
                        else
                        if (parentMenuItem != null)
                        {
                            menuItem.MouseEnter += MenuItem_MouseEnter; // Sự kiện khi chuột vào menu con
                            parentMenuItem.DropDownItems.Add(menuItem);
                            menuItem.Tag = MenuItemID; // Thiết lập Tag cho menu con
                        }

                    }

                }
            }
        }

        private void MenuItem_MouseEnter(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            // Kiểm tra nếu có menu con và nó chưa được hiển thị
            if (menuItem != null && menuItem.HasDropDownItems && !menuItem.DropDown.Visible)
            {
                menuItem.ShowDropDown();
            }
        }
        private ToolStripMenuItem FindParentMenuItem(ToolStripItemCollection items, int? menuLevel)
        {
            if (menuLevel.HasValue)
            {
                foreach (ToolStripItem toolStripItem in items)
                {
                    if (toolStripItem is ToolStripMenuItem item)
                    {
                        if (item.Tag is int && (int)item.Tag == menuLevel.Value)
                        {
                            return item;
                        }
                        else if (item.DropDownItems.Count > 0)
                        {
                            ToolStripMenuItem parent = FindParentMenuItem(item.DropDownItems, menuLevel);
                            if (parent != null)
                            {
                                return parent;
                            }
                        }
                    }
                }
            }
            return null;
        }

        private ToolStripMenuItem FindParentMenuItem1(ToolStripItemCollection items, int? menuLevel)
        {
            if (menuLevel.HasValue)
            {
                foreach (ToolStripMenuItem item in items)
                {
                    if (item.Tag is int && (int)item.Tag == menuLevel.Value)
                    {
                        return item;
                    }
                    else if (item.DropDownItems.Count > 0)
                    {
                        ToolStripMenuItem parent = FindParentMenuItem1(item.DropDownItems, menuLevel);
                        if (parent != null)
                        {
                            return parent;
                        }
                    }
                }
            }
            return null;
        }

        private void MenuItem_Click(object sender, EventArgs e, string urlForm)
        {
            if (!string.IsNullOrEmpty(urlForm))
            {
                OpenAndShowForm(urlForm);
            }
        }


        private void OpenAndShowForm(string formURL)
        {
            if (!string.IsNullOrEmpty(formURL))
            {
                // Giả sử formURL chứa tên đầy đủ của loại form (bao gồm namespace)
                //Type formType = new fr_DangNhap().GetType(); // Sử dụng GetType từ instance của form
                Type formType = Type.GetType(formURL);
                if (formType != null && typeof(Form).IsAssignableFrom(formType))
                {
                    // Kiểm tra xem form đã mở chưa                  
                    Form existingForm = kiemtratontai(formType);

                    if (existingForm != null)
                    {

                            existingForm.Activate(); // Đưa form đang mở lên trước
                    }
                    else
                    {
                        if (formType.Name.Length >= 11 && formType.Name.Substring(formType.Name.Length - 11) == "fr_DangNhap")
                        {
                            GlobalVariables.First_Time = 0;
                            ShowLoginForm();
                        }
                        else
                        {
                            // Nếu form chưa mở, tạo một thể hiện mới và hiển thị
                            Form newForm = (Form)Activator.CreateInstance(formType);
                            newForm.MdiParent = this;
                            newForm.Show();
                        }
                    }
                }
                else
                {
                    // Xử lý trường hợp khi không tìm thấy hoặc không phải là đối tượng Form
                    MessageBox.Show("Loại form không hợp lệ.");
                }
            }
        }
        private Form kiemtratontai(Type formtype)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == formtype)
                    return f;
            }
            return null;
        }
        private void LoginFormClosed()
        {
            // Sự kiện xảy ra khi form đăng nhập đã đóng

            // Kiểm tra điều kiện để ẩn form Main
            if (GlobalVariables.First_Time == 0 && isMainFormHidden==true)
            {
                // Ẩn form Main
                this.Show();
                isMainFormHidden = false;
            }
            //else if (isMainFormHidden)
            //{
            //    // Hiện lại form Main nếu nó đã được ẩn trước đó
            //    this.Show();
            //    isMainFormHidden = false;
            //}
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát khỏi chương trình không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                // Thực hiện các bước thoát chương trình
                foreach (Form frm in this.MdiChildren)
                { 
                    frm.Close();  
                }
                // Kiểm tra xem luồng đang chạy
                if (timerThread != null && timerThread.IsAlive)
                {
                    // Đợi cho đến khi luồng hoàn thành
                    timerThread.Join();
                }
                try
                {
                    File.Delete(filePath + ".txt");
                    System.Environment.Exit(0);
                }
                catch { }
            }
        }
        private System.Threading.Thread timerThread;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GlobalVariables.User_Name != "BL160")
            {
                // Kiểm tra xem luồng đã chạy chưa
                if (timerThread == null || !timerThread.IsAlive)
                {
                    // Khởi động một luồng mới
                    timerThread = new System.Threading.Thread(new System.Threading.ThreadStart(loadTable));
                    timerThread.Start();
                }
            }
        }
        private void loadTable()
        {
            // Load your Table...
            //DataTable table = new DataTable();
            string sql = "select count(*) from Users WITH (NOLOCK) where Username ='BL160' and password = '0RuwqJk06kXPWWAESi4GvA==' and status=1";
            //Boolean a = cn.KiemtraUsername(sql);
            if (cn.KiemtraUsername(sql,1))
            {
                //File.Delete(filePath + ".txt");
                //Application.Exit();
                try
                {
                    File.Delete(filePath + ".txt");
                    System.Environment.Exit(0);
                    //System.Windows.Forms.Application.Exit();
                }
                catch { }
            }
            //SqlDataAdapter SDA = new SqlDataAdapter();
            //SDA.Fill(table);
            //table = cn.taobang1(sql);
            //setDataSource(table);
        }
        private void khoi_tao_cac_bien()
        {
            //SqlConnection con = db.getcon();
            //StreamReader read = new StreamReader(File_INI);
            //this.Server = ConnectDB.DecryptString((read.ReadLine().Split(':')[1]), bientoancuc.amti);
            //string tam = (read.ReadLine().Split(':')[1]);
            //tam = (read.ReadLine().Split(':')[1]);
            //tam = (read.ReadLine().Split(':')[1]);
            //tam = (read.ReadLine().Split(':')[1]);
            //tam = (read.ReadLine().Split(':')[1]);
            //tam = (read.ReadLine().Split(':')[1]);
            //tam = (read.ReadLine().Split(':')[1]);
            //tam = (read.ReadLine().Split(':')[1]);
            //tam = (read.ReadLine().Split(':')[1]);
            //tam = (read.ReadLine().Split(':')[1]);
            //tam = (read.ReadLine().Split(':')[1]);
            //tam = (read.ReadLine().Split(':')[1]);
            string line13 = null;
            int currentLine = 0;
            try
            {
                using (StreamReader read = new StreamReader(File_INI))
                {
                    while (currentLine < 13)
                    {
                        // Đọc qua các dòng không cần thiết
                        read.ReadLine();
                        currentLine++;
                    }

                    // Đọc dòng thứ 13
                    if (currentLine == 13)
                    {
                        line13 = read.ReadLine();
                    }
                }
                if (line13 == "0")
                {
                    //string coputerName = System.Environment.MachineName;
                    //File.Delete(coputerName + ".txt");
                    ////Application.Exit();
                    try
                    {
                        System.Environment.Exit(0);
                        //System.Windows.Forms.Application.Exit();
                    }
                    catch { }
                }
                //read.Close();

                ////////////con.Open();
                ////////////con.Close();
                //timer1.Enabled = true;
                //read.Close();
            }
            catch
            {
                //fr_Ketnoi fr = new fr_Ketnoi();
                ////read.Close();
                //fr.ShowDialog();
                //MessageBox.Show("Không kết nối được Server", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Thiếu file INI", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
            }
        }
        
    }
}
