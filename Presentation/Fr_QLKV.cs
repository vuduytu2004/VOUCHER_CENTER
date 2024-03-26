using Report_Center.DataAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static Report_Center.Main;

namespace Report_Center.Presentation
{
    public partial class Fr_QLKV : Form
    {
        //private string connectionString = "Your_Connection_String"; // Thay thế bằng chuỗi kết nối của bạn

        public Fr_QLKV()
        {
            InitializeComponent();
            LoadData(); // Gọi hàm để tải dữ liệu từ SQL Server vào DataGridView khi Form khởi tạo
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();
                string query = "SELECT id, [Person_QLKV],[Size] ,[Bu_id] ,b.stk_name ,[Enable_check] FROM [HCRC_Report_Center].[dbo].[Person_QLKV] as a   left join [172.16.70.30].DATA_DETAIL.dbo.stock as b on a.Bu_id=b.node_id and b.comp_id='09' and b.type='01' order by a.id";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                // Lặp qua từng dòng trong DataGridView để thêm hoặc cập nhật dữ liệu vào SQL Server
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        // Kiểm tra giá trị null trước khi truy cập giá trị ô
                        string personId = row.Cells["id"].Value?.ToString();
                        string personQLKV = row.Cells["Person_QLKV"].Value?.ToString();
                        string size = row.Cells["Size"].Value?.ToString();
                        string buId = row.Cells["Bu_id"].Value?.ToString();

                        // Kiểm tra giá trị null và chuyển đổi sang kiểu boolean
                        bool enableCheck = row.Cells["Enable_check"].Value != null && Convert.ToBoolean(row.Cells["Enable_check"].Value);

                        // Thực hiện thêm hoặc cập nhật dữ liệu
                        string query = $"IF NOT EXISTS (SELECT 1 FROM Person_QLKV WHERE id = {personId}) " +
                                       $"INSERT INTO Person_QLKV (id, Person_QLKV, Size, Bu_id, Enable_check) VALUES " +
                                       $"({personId}, N'{personQLKV}', N'{size}', N'{buId}', '{enableCheck}') " +
                                       $"ELSE " +
                                       $"UPDATE Person_QLKV SET Size = N'{size}', Bu_id = N'{buId}', Enable_check = '{enableCheck}', Person_QLKV = N'{personQLKV}' " +
                                       $"WHERE id = {personId}";

                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                    }
                }


                MessageBox.Show("Dữ liệu đã được cập nhật thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(); // Sau khi cập nhật, tải lại dữ liệu để hiển thị trong DataGridView
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
