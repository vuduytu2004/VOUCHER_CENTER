using OfficeOpenXml;
using OfficeOpenXml.Style;
using Report_Center.DataAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Report_Center.Main;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Report_Center.Presentation
{
    public partial class fr_Reports : Form
    {

        DataTable dataTable = new DataTable();
        // Khai báo biến apiUrl ở mức độ của lớp
        private string apiUrl = "";
        private bool isDownloading = false; // Biến cờ để theo dõi trạng thái tải xuống

        public fr_Reports()
        {
            InitializeComponent();
            PopulateTreeView();
            frdate.CustomFormat = "dd/MM/yyyy";
            todate.CustomFormat = "dd/MM/yyyy";
        }

        private void PopulateTreeView()
        {

            var test = GlobalVariables.User_Name;
            //if (GlobalVariables.User_Name == "ADMIN" || GlobalVariables.User_Name == "Bl160")
            //{
            //    Node_id.Visible = true;
            //    Pro_name.Visible = true;
            //    gr_para_name.Visible = true;
            //    para_name.Visible = true;
            //    lbl_API.Visible = true;

            //}    
            //else
            //{
            //    Node_id.Visible = false;
            //    Pro_name.Visible = false;
            //    gr_para_name.Visible = false;
            //    para_name.Visible = false;
            //    lbl_API.Visible = false;
            //}    
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();
                //string query = "SELECT [NodeID],[ParentID],[NodeName],[Proc_name],[Gr_Parameter],[Parameter],[Enable_Check] FROM [TreeNodes_Report] WHERE [Enable_Check]=1;";
                string query = "";
                if (GlobalVariables.User_Name == "ADMIN")// || GlobalVariables.User_Name == "Bl160")
                {
                    query = "SELECT * FROM [TreeNodes_Report] WHERE [Enable_Check]=1;";

                }
                else
                {
                    query = $"SELECT * FROM [TreeNodes_Report] WHERE [Enable_Check]=1 and Department in ( select distinct RoleGroupID from UserPermissionRoleGroups where UserPermissionID={GlobalVariables.UserID} );";
                }


                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                //DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Tạo cây nút
                TreeNodeCollection nodes = treeView1.Nodes;
                CreateTree(dataTable, nodes, "");

                // Mở rộng nút gốc
                treeView1.ExpandAll();
                // Gọi hàm để tô màu các nút trong cây
                ColorTreeViewNodes(treeView1.Nodes);
            }
        }
        private void ColorTreeViewNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                // Gọi hàm để tô màu nút hiện tại
                ColorNode(node);


                // Đệ quy gọi hàm cho các nút con
                ColorTreeViewNodes(node.Nodes);
            }
        }

        private void ColorNode(TreeNode node)
        {
            // Lấy thông tin từ Tag của nút
            var nodeInfo = node.Tag;

            // Kiểm tra thông tin không null và thực hiện tô màu dựa trên điều kiện nào đó
            if (nodeInfo != null)
            {
                // Ví dụ: Tô màu đỏ cho các nút có ProcName chứa chuỗi "DIO"
                if (nodeInfo.ToString() == "0")
                {
                    node.BackColor = Color.Aqua;
                }
                // Ví dụ: Tô màu xanh cho các nút có Parameter chứa chuỗi "Tháng"
                else if (nodeInfo.ToString() == "1")
                {
                    node.BackColor = Color.Lavender;
                }
                else
                {
                    node.BackColor = Color.LemonChiffon;
                }
                // Các điều kiện khác có thể thêm tùy theo yêu cầu
            }
        }
        private void CreateTree(DataTable dataTable, TreeNodeCollection nodes, string parentID)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                if (row["ParentID"].ToString() == parentID)
                {
                    //ReportNodeInfo nodeInfo = new ReportNodeInfo(nodeName, procName, parameter);
                    TreeNode node = new TreeNode(row["NodeName"].ToString());
                    node.Tag = row["Node_color"].ToString();
                    nodes.Add(node);

                    // Gọi đệ quy để thêm các nút con
                    CreateTree(dataTable, node.Nodes, row["NodeID"].ToString());
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Xử lý sự kiện khi người dùng chọn một nút trong TreeView
            //MessageBox.Show($"Selected Node: {e.Node.Text}", "Node Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Làm mới các Label
            report_name.Text = "";
            Node_id.Text = "";
            Pro_name.Text = "";
            gr_para_name.Text = "";
            para_name.Text = "";
            lbl_API.Text = "";

            // Tìm kiếm trong DataTable
            DataRow[] foundRows = dataTable.Select($"NodeName = '{e.Node.Text}'");

            // Kiểm tra xem có dòng nào được tìm thấy không
            if (foundRows.Length > 0)
            {
                // Lấy thông tin từ dòng đầu tiên tìm thấy (có thể có nhiều dòng, bạn có thể xử lý theo cách khác nếu cần)
                DataRow row = foundRows[0];

                // Cập nhật các Label
                report_name.Text = row["NodeName"].ToString();
                Node_id.Text = row["NodeID"].ToString();
                Pro_name.Text = row["Proc_name"].ToString();
                gr_para_name.Text = row["Gr_Parameter"].ToString();
                para_name.Text = row["Parameter"].ToString();
                lbl_API.Text = row["API"].ToString();

                object dayValue = row["day"];
                int daysToAdd = Convert.IsDBNull(dayValue) ? 0 : Convert.ToInt32(dayValue);
                frdate.MaxDate = DateTime.Now.AddDays(daysToAdd);
                todate.MaxDate = DateTime.Now.AddDays(daysToAdd);
            }
            //gr_fr_to_date.Visible = false;
            //gr_stk_id.Visible = false;
            //gr_dept_id.Visible = false;
            //string[] groupNames1 = gr_para_name.Text.Split(',');
            foreach (Control control in this.Controls.OfType<Control>().OrderBy(c => c.TabIndex))
            {
                // Kiểm tra xem control có phải là GroupBox và có tên nằm trong danh sách groupNames không
                if (control is GroupBox groupBox)
                {

                    // Ẩn GroupBox và thiết lập vị trí dọc
                    groupBox.Visible = false;

                }
            }
            // Tách các tên trong gr_para_name.Text bằng dấu phẩy
            string[] groupNames = gr_para_name.Text.Split(',');

            // Thiết lập vị trí dọc ban đầu
            int currentTop = 126;

            // Duyệt qua tất cả các controls trên form
            //foreach (Control control in this.Controls)
            //{
            foreach (Control control in this.Controls.OfType<Control>().OrderBy(c => c.TabIndex))
            {
                // Kiểm tra xem control có phải là GroupBox và có tên nằm trong danh sách groupNames không
                if (control is GroupBox groupBox)
                {
                    //// In giá trị của groupBox.Text ra output (Console hoặc Debug)
                    ////Console.WriteLine($"GroupBox Text: {groupBox.Name}");

                    if (groupNames.Contains(groupBox.Name))
                    {
                        // Hiển thị GroupBox và thiết lập vị trí dọc
                        groupBox.Visible = true;
                        groupBox.Location = new Point(400, currentTop);

                        // Cập nhật vị trí dọc cho control tiếp theo
                        currentTop += groupBox.Height + 5; // 5 là khoảng cách giữa các GroupBox
                    }
                }
            }
            //if (para_name.Text.Contains("todate"))
            //{
            //    // Nếu chuỗi chứa "todate", đặt thuộc tính Enabled của Date thành True
            //    todate.Enabled = true;
            //}
            //else
            //{
            //    // Ngược lại, đặt thuộc tính Enabled của Date thành False
            //    todate.Enabled = false;
            //}
            todate.Enabled = para_name.Text.Contains("todate");

        }


        private void fr_Reports_Load(object sender, EventArgs e)
        {
            // Gọi hàm PopulateTreeView trong sự kiện Load của Form
            //PopulateTreeView();
        }

        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gr_para_name_TextChanged(object sender, EventArgs e)
        {
            gr_fr_to_date.Visible = false;
            gr_stk_id.Visible = false;
            gr_dept_id.Visible = false;
            // Tách các tên trong gr_para_name.Text bằng dấu phẩy
            string[] groupNames = gr_para_name.Text.Split(',');

            // Thiết lập vị trí dọc ban đầu
            int currentTop = 126;

            // Duyệt qua tất cả các controls trên form
            //foreach (Control control in this.Controls)
            //{
            foreach (Control control in this.Controls.OfType<Control>().OrderBy(c => c.TabIndex))
            {
                // Kiểm tra xem control có phải là GroupBox và có tên nằm trong danh sách groupNames không
                if (control is GroupBox groupBox && groupNames.Contains(groupBox.Text))
                {
                    // Hiển thị GroupBox và thiết lập vị trí dọc
                    groupBox.Visible = true;
                    groupBox.Location = new Point(559, currentTop);

                    // Cập nhật vị trí dọc cho control tiếp theo
                    currentTop += groupBox.Height + 5; // 5 là khoảng cách giữa các GroupBox
                }
            }
        }

        private async void bt_BC_Click(object sender, EventArgs e)
        {
            string Dirpath = Directory.GetCurrentDirectory();
            string Template = Dirpath + @"/Media/Template/";
            string file_temp = "";
            //if (Pro_name.Text == "rpt_Do_Phu_ASM")
            //{
            //     file_temp= "Template-BC-do_phu_ASM.xlsx";
            //}
            //else if (Pro_name.Text == "rpt_Nonmoving")
            //{
            //     file_temp = "Template-BC_NonMoving.xlsx";
            //}
            //else if (Pro_name.Text == "rpt_SUPP_IMPORT")
            //{
            //    file_temp = "Template-BC-NhapHang.xlsx";
            //}
            file_temp = $"Template-{Pro_name.Text}.xlsx";
            //string dateAndRandom = DateTime.Now.ToString("yyyyMMdd") + "_" + new Random().Next(1000, 9999);
            string dateAndRandom = frdate.Value.ToString("yyyyMMddHHmm") + "_" + new Random().Next(100, 999);
            string templatePath = Path.Combine(Template, file_temp);

            //var templatePath = RootPathConfig.TemplatePath.Template + "Template-BC-do_phu_ASM.xlsx";
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "xlsx";
                saveFileDialog.AddExtension = true;
                //if (Pro_name.Text == "rpt_Do_Phu_ASM")
                //{
                //    saveFileDialog.FileName = "BC_do_phu_ASM";
                //}
                //else if (Pro_name.Text == "rpt_Nonmoving")
                //{
                //    saveFileDialog.FileName = "BC_Nonmoving";
                //}
                //else if (Pro_name.Text == "rpt_SUPP_IMPORT")
                //{
                //    saveFileDialog.FileName = "BC_Nhap_hang";
                //}
                //else if (Pro_name.Text == "rpt_DioByNCC")
                //{
                //    saveFileDialog.FileName = "BC_DIOByNCC";
                //}
                //saveFileDialog.FileName = Pro_name.Text;
                saveFileDialog.FileName = $"Template-{Pro_name.Text}_{dateAndRandom}.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
                    string uniqueFileName = GetUniqueFileName(Path.GetFileName(saveFileDialog.FileName), selectedDirectory);
                    //// Lấy tên tệp đã chọn
                    //string originalFileName = Path.GetFileName(saveFileDialog.FileName);

                    //// Tạo chuỗi ngày tháng và số ngẫu nhiên
                    //string dateAndRandom = DateTime.Now.ToString("yyyyMMddHHmmss") +" "+ new Random().Next(1000, 9999);

                    //// Kết hợp tên tệp gốc, ngày tháng và số ngẫu nhiên để tạo tên tệp duy nhất
                    //string uniqueFileName = originalFileName + " " + dateAndRandom;

                    ////string outputPath = saveFileDialog.FileName;

                    // Chạy thủ tục trong một luồng riêng biệt
                    if (Pro_name.Text == "rpt_TotalRetailWholesaleByIndustry")
                    {
                        await Task.Run(() => RunReportAsync_Total(templatePath, uniqueFileName));
                    }
                    else if (Pro_name.Text == "rpt_TotalRetailWholesaleByMonth")
                    {
                        await Task.Run(() => RunReportAsync_ByMonth(templatePath, uniqueFileName));
                    }
                    else
                    {
                        await Task.Run(() => RunReportAsync(templatePath, uniqueFileName));
                    }
                    Console.WriteLine("File saved to: " + uniqueFileName);
                }
                else
                {
                    Console.WriteLine("Operation canceled by the user.");
                }
            }

        }
        private string GetUniqueFileName(string baseFileName, string directory)
        {
            string fileName = Path.Combine(directory, baseFileName);
            int counter = 1;

            while (File.Exists(fileName))
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(baseFileName);
                string fileExtension = Path.GetExtension(baseFileName);

                fileName = Path.Combine(directory, $"{fileNameWithoutExtension}_{counter}{fileExtension}");
                counter++;
            }

            return fileName;
        }
        private async Task RunReportAsync(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số vào thủ tục
                    if (stk_id.Visible == true)
                    {
                        command.Parameters.AddWithValue("@stk_id", stk_id.Text);
                    }
                    if (frdate.Visible == true)
                    {
                        command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    }
                    if (todate.Visible == true && todate.Enabled == true)
                    {
                        command.Parameters.AddWithValue("@todate", todate.Value.ToString("yyyyMMdd"));
                    }
                    if (dept_id.Visible == true)
                    {
                        command.Parameters.AddWithValue("@dept_id", dept_id.Text);
                    }
                    if (sku_code.Visible == true)
                    {
                        command.Parameters.AddWithValue("@sku_id", sku_code.Text);
                    }
                    if (supp_id.Visible == true)
                    {
                        command.Parameters.AddWithValue("@supp_id", supp_id.Text);
                    }
                    //command.Parameters.AddWithValue("@stk_id", stk_id.Text);
                    //command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    //command.Parameters.AddWithValue("@todate", todate.Value.ToString("yyyyMMdd"));

                    command.CommandTimeout = 0; // Set timeout as needed
                                                // Giả sử bạn có một DataTable để lưu trữ kết quả
                    DataTable dataTable = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                    // Kiểm tra xem có dữ liệu trong dataTable không
                    if (dataTable.Rows.Count == 0)
                    {
                        // Thông báo hoặc xử lý khi không có dữ liệu
                        MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine("Không có dữ liệu để xuất Excel.");
                        return; // hoặc thực hiện các hành động khác theo yêu cầu của bạn
                    }
                    // Set the license context to use EPPlus
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial
                                                                                // Load tệp Excel mẫu
                    FileInfo templateFile = new FileInfo(templatePath);

                    // Tạo một gói Excel mới dựa trên mẫu
                    using (ExcelPackage package = new ExcelPackage(templateFile, true))
                    {
                        // Truy cập vào tờ công việc đầu tiên
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        int startRow = 7;
                        if (Pro_name.Text == "rpt_Do_Phu_ASM")
                        {
                            worksheet.Cells["A5"].Value = $"Từ ngày: {frdate.Value} - Đến ngày : {todate.Value} ";
                        }
                        else if (Pro_name.Text == "rpt_TotalRetailWholesaleByIndustry_lv3" || Pro_name.Text == "rpt_TotalRetailWholesaleByIndustry_lv2")
                        {
                            startRow = 5;
                            worksheet.Cells["B2"].Value = $"Ngày: {frdate.Value}";// - Đến ngày : {todate.Value} ";
                        }
                        else //if (Pro_name.Text == "rpt_Nonmoving" || Pro_name.Text == "rpt_SUPP_IMPORT")
                        {
                            worksheet.Cells["A4"].Value = $"Từ ngày: {frdate.Value} - Đến ngày : {todate.Value} ";
                        }

                        int columns_dem = 0;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            int i;
                            for (i = 0; i < dataTable.Columns.Count; i++)
                            {
                                // Giả sử các cột trong DataTable tương ứng với các cột trong Excel
                                //worksheet.Cells[startRow, i + 1].Value = row[i].ToString();
                                worksheet.Cells[startRow, i + 1].Value = row[i];
                                columns_dem = i;
                            }
                            if (Pro_name.Text == "rpt_Do_Phu_ASM")
                            {
                                worksheet.Cells[startRow, i + 1].Formula = $"=IFERROR({worksheet.Cells[startRow, i].Address} / {worksheet.Cells[startRow, i - 1].Address}, 0)";
                            }
                            else if (Pro_name.Text == "rpt_Nonmoving")
                            {
                                worksheet.Cells[startRow, i + 3].Formula = $"=IF({worksheet.Cells[startRow, i - 3].Address}={worksheet.Cells[startRow, i - 1].Address}-{worksheet.Cells[startRow, i + 1].Address},\"Nonmoving\",\"Hàng có GD\")";
                            }
                            else if (Pro_name.Text == "rpt_DioByNCC")
                            {
                                worksheet.Cells[startRow, i + 1].Formula = $"={worksheet.Cells[startRow, i - 1].Address} + {worksheet.Cells[startRow, i - 3].Address}-{worksheet.Cells[startRow, i - 5].Address}";
                                worksheet.Cells[startRow, i + 2].Formula = $"={worksheet.Cells[startRow, i].Address} + {worksheet.Cells[startRow, i - 2].Address}-{worksheet.Cells[startRow, i - 4].Address}";
                                worksheet.Cells[startRow, i + 3].Formula = $"=IFERROR((({worksheet.Cells[startRow, i + 2].Address} / {worksheet.Cells[startRow, i + 1].Address})*90)/3, 0)";
                                //// Định dạng điều kiện: Nếu giá trị của ô là số và < 0, thì đổi màu sắc
                                //var range = worksheet.Cells[1, 1, 10, 1]; // Phạm vi từ A1 đến A10
                                //var rule = range.ConditionalFormatting.AddExpression(range);
                                //rule.Formula = $"AND(ISNUMBER({range.Address}), {range.Address}<0)";
                                //rule.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                //rule.Style.Fill.BackgroundColor.Color.SetColor(System.Drawing.Color.Red); // Màu sắc bạn muốn áp dụng
                            }
                            else if (Pro_name.Text == "rpt_TotalRetailWholesaleByIndustry_lv3" || Pro_name.Text == "rpt_TotalRetailWholesaleByIndustry_lv2")
                            {
                                worksheet.Cells[startRow, i + 1].Formula = $"=IFERROR(({worksheet.Cells[startRow, i].Address} / {worksheet.Cells[startRow, i - 1].Address})-1, 0)";
                                worksheet.Cells[startRow, i + 2].Formula = $"=+{worksheet.Cells[startRow, i].Address}-{worksheet.Cells[startRow, i - 1].Address}";
                                worksheet.Cells[startRow, i + 3].Formula = $"=IFERROR(({worksheet.Cells[startRow, i].Address} / {worksheet.Cells[startRow, i - 2].Address})-1, 0)";
                                worksheet.Cells[startRow, i + 4].Formula = $"=+{worksheet.Cells[startRow, i].Address}-{worksheet.Cells[startRow, i - 2].Address}";
                            }
                            else if (Pro_name.Text == "rpt_SUPP_IMPORT")
                            {
                                //// Định dạng điều kiện: Nếu giá trị của ô là số và < 0, thì đổi màu sắc
                                //var range = worksheet.Cells[1, 1, 10, 1]; // Phạm vi từ A1 đến A10
                                //var rule = range.ConditionalFormatting.AddExpression(range);
                                //rule.Formula = $"AND(ISNUMBER({range.Address}), {range.Address}<0)";
                                //rule.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                //rule.Style.Fill.BackgroundColor.Color.SetColor(System.Drawing.Color.Red); // Màu sắc bạn muốn áp dụng
                            }
                            startRow++;
                        }
                        if (Pro_name.Text == "rpt_Do_Phu_ASM")
                        {
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 2)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        }
                        else if (Pro_name.Text == "rpt_Nonmoving")
                        {
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"G7:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Numberformat.Format = "_-* #,##0_-;-* #,##0_-;_-* \"-\"??_-;_-@_-";
                        }
                        else if (Pro_name.Text == "rpt_SUPP_IMPORT")
                        {
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 1)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 1)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 1)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 1)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 1)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"E7:{GetExcelColumnName(columns_dem + 1)}{startRow}"].Style.Numberformat.Format = "_-* #,##0_-;-* #,##0_-;_-* \"-\"??_-;_-@_-";
                        }
                        else if (Pro_name.Text == "rpt_DioByNCC")
                        {
                            //var a = GetExcelColumnName(columns_dem + 4);
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A6:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"G7:{GetExcelColumnName(columns_dem + 4)}{startRow}"].Style.Numberformat.Format = "_-* #,##0_-;-* #,##0_-;_-* \"-\"??_-;_-@_-";
                        }
                        else if (Pro_name.Text == "rpt_TotalRetailWholesaleByIndustry_lv3")
                        {
                            // Áp dụng quy tắc định dạng
                            ExcelAddress address = new ExcelAddress($"M5:P{startRow}");
                            var formattingRule = worksheet.ConditionalFormatting.AddLessThan(address);
                            formattingRule.Formula = "0";
                            formattingRule.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Pink); //= "#FFC7CE"
                            formattingRule.Style.Font.Color.SetColor(System.Drawing.Color.Red);
                            //formattingRule.Style.Fill.BackgroundColor.Color = System.Drawing.Color.from(255, 205, 92, 92); // IndianRed color
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"J5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Numberformat.Format = "#,##0.0,,\"tr\"";

                            worksheet.Cells[$"M5:M{startRow}"].Style.Numberformat.Format = "0%";
                            worksheet.Cells[$"O5:O{startRow}"].Style.Numberformat.Format = "0%";

                            //worksheet.Cells[$"J5:L{startRow}"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.BlanchedAlmond);
                            var range = worksheet.Cells[$"J5:L{startRow}"];
                            var fill = range.Style.Fill;
                            // Thiết lập kiểu mẫu trước khi đặt màu nền
                            fill.PatternType = ExcelFillStyle.Solid;
                            // Bây giờ đặt màu nền
                            //fill.BackgroundColor.SetColor(System.Drawing.Color.Cornsilk);
                            fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#fce4d6"));
                            ////--------------------------------------------------------------------------------------------------------
                            ////var range = worksheet.Cells[$"J5:L{startRow}"];

                            //// Thiết lập kiểu mẫu trước khi đặt màu nền
                            //range.Style.Fill.PatternType = ExcelFillStyle.Solid;

                            //// Bây giờ đặt màu nền
                            //range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.BlanchedAlmond);

                            //// Tạo điều kiện định dạng cho giá trị âm
                            //var negativeFormat = range.ConditionalFormatting.AddExpression();
                            //negativeFormat.Style.Font.Color.SetColor(System.Drawing.Color.Red);
                            //negativeFormat.Formula = "[$J5]<0";  // Áp dụng cho cột J, bạn có thể thay đổi thành cột khác nếu cần.
                            ////--------------------------------------------------------------------------------------------------------
                            worksheet.Cells["L4"].Value = frdate.Value.ToString("dd/MM/yyyy");
                            DateTime frdateValue = frdate.Value;
                            DateTime previousMonth = frdateValue.AddMonths(-1);
                            worksheet.Cells["K4"].Value = previousMonth.ToString("dd/MM/yyyy");
                            DateTime previousYear = frdateValue.AddYears(-1);
                            worksheet.Cells["J4"].Value = previousYear.ToString("dd/MM/yyyy");
                        }
                        else if (Pro_name.Text == "rpt_TotalRetailWholesaleByIndustry_lv2")
                        {
                            // Áp dụng quy tắc định dạng
                            ExcelAddress address = new ExcelAddress($"K5:N{startRow}");
                            var formattingRule = worksheet.ConditionalFormatting.AddLessThan(address);
                            formattingRule.Formula = "0";
                            formattingRule.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Pink); //= "#FFC7CE"
                            formattingRule.Style.Font.Color.SetColor(System.Drawing.Color.Red);
                            //formattingRule.Style.Fill.BackgroundColor.Color = System.Drawing.Color.from(255, 205, 92, 92); // IndianRed color
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"H5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Numberformat.Format = "#,##0.0,,\"tr\"";

                            worksheet.Cells[$"M5:M{startRow}"].Style.Numberformat.Format = "0%";
                            worksheet.Cells[$"K5:K{startRow}"].Style.Numberformat.Format = "0%";

                            //worksheet.Cells[$"J5:L{startRow}"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.BlanchedAlmond);
                            var range = worksheet.Cells[$"H5:J{startRow}"];
                            var fill = range.Style.Fill;
                            // Thiết lập kiểu mẫu trước khi đặt màu nền
                            fill.PatternType = ExcelFillStyle.Solid;
                            // Bây giờ đặt màu nền
                            //fill.BackgroundColor.SetColor(System.Drawing.Color.Cornsilk);
                            fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#fce4d6"));

                            worksheet.Cells["J4"].Value = frdate.Value.ToString("dd/MM/yyyy");
                            DateTime frdateValue = frdate.Value;
                            DateTime previousMonth = frdateValue.AddMonths(-1);
                            worksheet.Cells["I4"].Value = previousMonth.ToString("dd/MM/yyyy");
                            DateTime previousYear = frdateValue.AddYears(-1);
                            worksheet.Cells["H4"].Value = previousYear.ToString("dd/MM/yyyy");
                        }

                        // Lưu gói Excel đã được sửa đổi vào tệp đầu ra.
                        //string uniqueFileName = GetUniqueFileName(Path.GetFileName(saveFileDialog.FileName), selectedDirectory);
                        package.SaveAs(new FileInfo(outputPath));
                        // Mở tệp Excel sau khi đã lưu
                        System.Diagnostics.Process.Start(outputPath);
                    }
                }
            }
        }
        private async Task RunReportAsync_Total(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (frdate.Visible == true)
                    {
                        command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    }

                    if (dept_id.Visible == true)
                    {
                        command.Parameters.AddWithValue("@dept_id", dept_id.Text);
                    }

                    command.CommandTimeout = 0; // Đặt thời gian chờ theo cần thiết

                    DataTable dataTable = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine("Không có dữ liệu để xuất Excel.");
                        return;
                    }

                    // Đặt ngữ cảnh giấy phép để sử dụng EPPlus
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    // Nạp mẫu Excel
                    FileInfo templateFile = new FileInfo(templatePath);

                    using (ExcelPackage package = new ExcelPackage(templateFile, true))
                    {
                        // Truy cập vào tờ công việc đầu tiên
                        ExcelWorksheet mainWorksheet = package.Workbook.Worksheets[0];

                        // Lấy các nhóm duy nhất từ DataTable
                        var distinctGroups = dataTable.AsEnumerable().Select(row => row.Field<string>("Groups")).Distinct();
                        //var distinctGroups = dataTable.AsEnumerable()
                        //                    .Select(row => row.Field<int>("Groups").ToString())
                        //                    .Distinct()
                        //                    .OrderBy(group => group)
                        //                    .ToList();
                        foreach (var group in distinctGroups)
                        {
                            // Sao chép tờ công việc chính cho mỗi nhóm
                            //ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(group, mainWorksheet);
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[group];
                            if (worksheet == null)
                            {
                                // Nếu tờ không tồn tại, tạo một tờ mới
                                worksheet = package.Workbook.Worksheets.Add(group, mainWorksheet);

                                // Cấu hình tờ mới nếu cần
                                // ...
                            }

                            // Lọc dữ liệu cho nhóm hiện tại
                            //var groupData = dataTable.Select($"Groups = '{group}'").CopyToDataTable();
                            // Lọc dữ liệu cho nhóm hiện tại và sắp xếp theo cột dept_id
                            var groupData = dataTable.Select($"Groups = '{group}'", "Dept_id").CopyToDataTable();
                            //var groupData = dataTable.Select($"Groups = {int.Parse(group)}", "Dept_id").CopyToDataTable();

                            int startRow = 5;

                            // Đặt dữ liệu cụ thể cho nhóm trên tờ công việc
                            worksheet.Cells["B2"].Value = $"Ngày: {frdate.Value}";// - Đến ngày : {todate.Value} ";

                            int columns_dem = 0;

                            foreach (DataRow row in groupData.Rows)
                            {
                                int i;
                                for (i = 0; i < groupData.Columns.Count - 1; i++)
                                {
                                    worksheet.Cells[startRow, i + 1].Value = row[i];
                                    columns_dem = i;
                                }


                                worksheet.Cells[startRow, i + 1].Formula = $"=IFERROR(({worksheet.Cells[startRow, i].Address} / {worksheet.Cells[startRow, i - 1].Address})-1, 0)";
                                worksheet.Cells[startRow, i + 2].Formula = $"=+{worksheet.Cells[startRow, i].Address}-{worksheet.Cells[startRow, i - 1].Address}";
                                worksheet.Cells[startRow, i + 3].Formula = $"=IFERROR(({worksheet.Cells[startRow, i].Address} / {worksheet.Cells[startRow, i - 2].Address})-1, 0)";
                                worksheet.Cells[startRow, i + 4].Formula = $"=+{worksheet.Cells[startRow, i].Address}-{worksheet.Cells[startRow, i - 2].Address}";
                                // Công thức và xử lý bổ sung cho mỗi hàng (giống như mã hiện tại của bạn)

                                startRow++;
                            }
                            //startRow--;
                            // Tính tổng cho từng cột từ C đến E
                            for (int colIndex = 3; colIndex <= 5; colIndex++) // Cột C đến E
                            {
                                worksheet.Cells[startRow, colIndex].Formula = $"SUM({GetExcelColumnName(colIndex)}5:{GetExcelColumnName(colIndex)}{startRow - 1})";
                            }

                            worksheet.Cells[startRow, 6].Formula = $"=IFERROR(({worksheet.Cells[startRow, 5].Address} / {worksheet.Cells[startRow, 4].Address})-1, 0)";
                            worksheet.Cells[startRow, 7].Formula = $"=+{worksheet.Cells[startRow, 5].Address}-{worksheet.Cells[startRow, 4].Address}";
                            worksheet.Cells[startRow, 8].Formula = $"=IFERROR(({worksheet.Cells[startRow, 5].Address} / {worksheet.Cells[startRow, 3].Address})-1, 0)";
                            worksheet.Cells[startRow, 9].Formula = $"=+{worksheet.Cells[startRow, 5].Address}-{worksheet.Cells[startRow, 3].Address}";
                            // Gán giá trị "Tổng" cho ô đã merge
                            worksheet.Cells[startRow, 1].Value = "Tổng cộng";
                            // Merge cột A5 đến I5
                            worksheet.Cells[startRow, 1, startRow, 2].Merge = true;

                            worksheet.Cells[startRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            // Đặt định dạng chữ đậm cho ô đã merge
                            worksheet.Cells[startRow, 1, startRow, columns_dem + 5].Style.Font.Bold = true;

                            // Áp dụng các quy tắc định dạng và kiểu cho nhóm hiện tại
                            ExcelAddress address = new ExcelAddress($"F5:I{startRow}");
                            var formattingRule = worksheet.ConditionalFormatting.AddLessThan(address);
                            formattingRule.Formula = "0";
                            formattingRule.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Pink);
                            formattingRule.Style.Font.Color.SetColor(System.Drawing.Color.Red);

                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"C5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Numberformat.Format = "#,##0.0,,\"tr\"";

                            worksheet.Cells[$"F5:F{startRow}"].Style.Numberformat.Format = "0%";
                            worksheet.Cells[$"H5:H{startRow}"].Style.Numberformat.Format = "0%";

                            var range = worksheet.Cells[$"C5:E{startRow}"];
                            var fill = range.Style.Fill;
                            fill.PatternType = ExcelFillStyle.Solid;
                            //fill.BackgroundColor.SetColor(System.Drawing.Color.Cornsilk);
                            fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#fce4d6"));

                            //fill.Style.Fill.BackgroundColor.Color = System.Drawing.Color.from(252, 228, 214, 255); // IndianRed color
                            worksheet.Cells["E4"].Value = frdate.Value.ToString("dd/MM/yyyy");
                            DateTime frdateValue = frdate.Value;
                            DateTime previousMonth = frdateValue.AddMonths(-1);
                            worksheet.Cells["D4"].Value = previousMonth.ToString("dd/MM/yyyy");
                            DateTime previousYear = frdateValue.AddYears(-1);
                            worksheet.Cells["C4"].Value = previousYear.ToString("dd/MM/yyyy");

                            //// Tính tổng cho cột cuối cùng (ví dụ: cột 5)
                            //worksheet.Cells[startRow, 3].Formula = $"SUM({GetExcelColumnName(1)}{startRow}:{GetExcelColumnName(groupData.Columns.Count)}{startRow + groupData.Rows.Count - 1})";

                        }
                        // Chọn tờ cuối cùng
                        int numberOfWorksheets = package.Workbook.Worksheets.Count;
                        ExcelWorksheet lastWorksheet = package.Workbook.Worksheets[0];
                        lastWorksheet.Select();
                        // Lưu gói Excel đã được sửa đổi vào tệp đầu ra
                        package.SaveAs(new FileInfo(outputPath));

                        // Mở tệp Excel sau khi đã lưu
                        System.Diagnostics.Process.Start(outputPath);
                    }
                }
            }
        }
        private async Task RunReportAsync_ByMonth(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (frdate.Visible == true)
                    {
                        command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    }

                    if (stk_id.Visible == true)
                    {
                        command.Parameters.AddWithValue("@stk_id", stk_id.Text);
                    }

                    command.CommandTimeout = 0; // Đặt thời gian chờ theo cần thiết

                    DataTable dataTable = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine("Không có dữ liệu để xuất Excel.");
                        return;
                    }

                    // Đặt ngữ cảnh giấy phép để sử dụng EPPlus
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    // Nạp mẫu Excel
                    FileInfo templateFile = new FileInfo(templatePath);

                    using (ExcelPackage package = new ExcelPackage(templateFile, true))
                    {
                        // Truy cập vào tờ công việc đầu tiên
                        ExcelWorksheet mainWorksheet = package.Workbook.Worksheets[0];

                        // Lấy các nhóm duy nhất từ DataTable
                        var distinctGroups = dataTable.AsEnumerable().Select(row => row.Field<string>("Groups")).Distinct();

                        foreach (var group in distinctGroups)
                        {
                            // Sao chép tờ công việc chính cho mỗi nhóm
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[group];
                            if (worksheet == null)
                            {
                                // Nếu tờ không tồn tại, tạo một tờ mới
                                worksheet = package.Workbook.Worksheets.Add(group, mainWorksheet);
                            }
                            // Lọc dữ liệu cho nhóm hiện tại 
                            var groupData = dataTable.Select($"Groups = '{group}'").CopyToDataTable();
                            //var groupData = dataTable.Select($"Groups = '{group}'").OrderBy(r => r.Field<string>("grp_id").Substring(0, 2)).ThenBy(r => Convert.ToInt64(r.Field<long>("TT"))).CopyToDataTable();
                            // Lấy các nhóm duy nhất từ groupData
                            var distinctGroups_small = groupData.AsEnumerable().Select(row => row.Field<string>("nhom")).Distinct();
                            int startRow = 7;
                            // Giả sử bạn có một DateTimePicker tên là dateTimePicker1
                            DateTime selectedDate = frdate.Value;
                            int numberOfDaysInMonth = DateTime.DaysInMonth(selectedDate.Year, selectedDate.Month);

                            string year = selectedDate.Year.ToString();
                            string thang = selectedDate.Month.ToString("00");
                            string ngay = group.Substring(0, 2);

                            worksheet.Cells["Z3"].Value = $"NĂM {year}";
                            // Đặt dữ liệu cụ thể cho nhóm trên tờ công việc
                            worksheet.Cells["I3"].Value = $"Ngày: {ngay}/{thang}/{year}";// - Đến ngày : {todate.Value} ";
                            worksheet.Cells["O3"].Value = $"Tháng {thang}/{year}";
                            var RowTT1 = 0;
                            var RowTT2 = 0;
                            var RowTT3 = 0;
                            int columns_dem = 0;
                            foreach (var group_small in distinctGroups_small)
                            {

                                // Lọc dữ liệu cho nhóm hiện tại 
                                var groupData_small = groupData.Select($"nhom = '{group_small}'").CopyToDataTable();
                                string ten_nhom = groupData_small.Rows[0]["Ten_nhom"].ToString();
                                worksheet.Cells[startRow, 4].Value = ten_nhom;
                                worksheet.Cells[startRow, 4, startRow, 5].Merge = true;

                                worksheet.Cells[startRow, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                // Đặt định dạng chữ đậm cho ô đã merge
                                worksheet.Cells[startRow, 4, startRow, 5].Style.Font.Bold = true;

                                startRow++;
                                int strartGroup_row = startRow;
                                foreach (DataRow row in groupData_small.Rows)
                                {
                                    int i;
                                    for (i = 0; i < groupData_small.Columns.Count - 3; i++)
                                    {
                                        worksheet.Cells[startRow, i + 1].Value = row[i];
                                        columns_dem = i;
                                    }
                                    // DT bán lẻ bình quân
                                    worksheet.Cells[startRow, 13].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 9].Address}, 0)";
                                    worksheet.Cells[startRow, 14].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 6].Address}, 0)";
                                    worksheet.Cells[startRow, 24].Formula = $"=IFERROR({worksheet.Cells[startRow, 22].Address} / {worksheet.Cells[startRow, 20].Address}, 0)";
                                    worksheet.Cells[startRow, 25].Formula = $"=IFERROR({worksheet.Cells[startRow, 22].Address} / {worksheet.Cells[startRow, 6].Address}, 0)";
                                    worksheet.Cells[startRow, 30].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 28].Address}, 0)";
                                    worksheet.Cells[startRow, 31].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 6].Address}, 0)";

                                    worksheet.Cells[startRow, 17].Formula = $"=IFERROR({worksheet.Cells[startRow, 15].Address} / {worksheet.Cells[startRow, 16].Address}, 0)";
                                    worksheet.Cells[startRow, 18].Formula = $"=IFERROR({worksheet.Cells[startRow, 23].Address} / {worksheet.Cells[startRow, 15].Address}, 0)";
                                    worksheet.Cells[startRow, 19].Formula = $"=IFERROR(({worksheet.Cells[startRow, 22].Address}/ {ngay}*{numberOfDaysInMonth} + {worksheet.Cells[startRow, 21].Address})/{worksheet.Cells[startRow, 15].Address}, 0)";
                                    worksheet.Cells[startRow, 33].Formula = $"=IFERROR({worksheet.Cells[startRow, 29].Address} / {worksheet.Cells[startRow, 32].Address}, 0)";


                                    startRow++;
                                }
                                // Gán giá trị "Tổng" cho ô đã merge
                                worksheet.Cells[startRow, 4].Value = $"Tổng {ten_nhom}";
                                // Merge cột A5 đến I5
                                worksheet.Cells[startRow, 4, startRow, 5].Merge = true;

                                worksheet.Cells[startRow, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                // Đặt định dạng chữ đậm cho ô đã merge
                                worksheet.Cells[startRow, 1, startRow, columns_dem + 2].Style.Font.Bold = true;
                                for (int colIndex = 6; colIndex <= 33; colIndex++) /// Cột F đến AG
                                {
                                    worksheet.Cells[startRow, colIndex].Formula = $"SUM({GetExcelColumnName(colIndex)}{strartGroup_row}:{GetExcelColumnName(colIndex)}{startRow - 1})";
                                }
                                // DT bán lẻ bình quân
                                worksheet.Cells[startRow, 13].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 9].Address}, 0)";
                                worksheet.Cells[startRow, 14].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 6].Address}, 0)";
                                worksheet.Cells[startRow, 24].Formula = $"=IFERROR({worksheet.Cells[startRow, 22].Address} / {worksheet.Cells[startRow, 20].Address}, 0)";
                                worksheet.Cells[startRow, 25].Formula = $"=IFERROR({worksheet.Cells[startRow, 22].Address} / {worksheet.Cells[startRow, 6].Address}, 0)";
                                worksheet.Cells[startRow, 30].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 28].Address}, 0)";
                                worksheet.Cells[startRow, 31].Formula = $"=IFERROR({worksheet.Cells[startRow, 11].Address} / {worksheet.Cells[startRow, 6].Address}, 0)";

                                worksheet.Cells[startRow, 17].Formula = $"=IFERROR({worksheet.Cells[startRow, 15].Address} / {worksheet.Cells[startRow, 16].Address}, 0)";
                                worksheet.Cells[startRow, 18].Formula = $"=IFERROR({worksheet.Cells[startRow, 23].Address} / {worksheet.Cells[startRow, 15].Address}, 0)";
                                worksheet.Cells[startRow, 19].Formula = $"=IFERROR(({worksheet.Cells[startRow, 22].Address} / {ngay}*{numberOfDaysInMonth} + {worksheet.Cells[startRow, 21].Address})/{worksheet.Cells[startRow, 15].Address}, 0)";
                                worksheet.Cells[startRow, 33].Formula = $"=IFERROR({worksheet.Cells[startRow, 29].Address} / {worksheet.Cells[startRow, 32].Address}, 0)";

                                if (ten_nhom == "MART")
                                {
                                    RowTT1 = startRow;
                                }
                                if (ten_nhom == "MiniMART")
                                {
                                    RowTT2 = startRow;
                                }
                                if (ten_nhom == "FujiMART")
                                {
                                    RowTT3 = startRow;
                                }
                                startRow++;
                            }
                            for (int colIndex = 6; colIndex <= 33; colIndex++) // Cột F đến AG
                            {
                                worksheet.Cells[startRow, colIndex].Formula = $"({GetExcelColumnName(colIndex)}{RowTT1}+{GetExcelColumnName(colIndex)}{RowTT2}+{GetExcelColumnName(colIndex)}{RowTT3})";
                            }
                            // Gán giá trị "Tổng" cho ô đã merge
                            worksheet.Cells[startRow, 1].Value = "TỔNG HỆ THỐNG";
                            worksheet.Cells[startRow, 1, startRow, 2].Merge = true;
                            worksheet.Cells[startRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;                            // Đặt định dạng chữ đậm cho ô đã merge
                            worksheet.Cells[startRow, 1, startRow, columns_dem + 2].Style.Font.Bold = true;

                            worksheet.Cells[$"A7:{GetExcelColumnName(columns_dem + 2)}{startRow}"].AutoFitColumns();
                            worksheet.Cells[$"A7:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A7:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A7:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"A7:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[$"F7:{GetExcelColumnName(columns_dem + 2)}{startRow}"].Style.Numberformat.Format = "_(* #,##0_);_(* (#,##0);_(* \"-\"??_);_(@_)";

                            worksheet.Cells[$"{GetExcelColumnName(33)}7:{GetExcelColumnName(33)}{startRow}"].Style.Numberformat.Format = "0%";
                            worksheet.Cells[$"{GetExcelColumnName(18)}7:{GetExcelColumnName(19)}{startRow}"].Style.Numberformat.Format = "0%";
                        }
                        // Kiểm tra xem tờ đã tồn tại hay chưa
                        if (package.Workbook.Worksheets.Any(ws => ws.Name == "Templ"))
                        {
                            // Nếu tờ đã tồn tại, xóa nó đi
                            package.Workbook.Worksheets.Delete("Templ");
                        }
                        // Kiểm tra xem sheet theo ngày xem đã có chưa

                        var sheet_name_sear = frdate.Value.ToString("dd") + "." + frdate.Value.ToString("MM");
                        int sheetIndex = -1; // Khởi tạo giá trị mặc định

                        for (int i = 0; i < package.Workbook.Worksheets.Count; i++)
                        {
                            if (package.Workbook.Worksheets[i].Name == sheet_name_sear)
                            {
                                sheetIndex = i; // Lưu vị trí tờ khi tìm thấy tờ có tên giống
                                break; // Thoát khỏi vòng lặp sau khi tìm thấy tờ
                            }
                        }

                        if (sheetIndex != -1)
                        {
                            // Tồn tại tờ có tên giống với sheet_name_sear
                            // Sử dụng sheetIndex để thực hiện các thao tác tiếp theo với tờ đó
                            ExcelWorksheet foundWorksheet = package.Workbook.Worksheets[sheetIndex];
                            foundWorksheet.Select();
                        }
                        // Lưu gói Excel đã được sửa đổi vào tệp đầu ra
                        package.SaveAs(new FileInfo(outputPath));

                        // Mở tệp Excel sau khi đã lưu
                        System.Diagnostics.Process.Start(outputPath);
                    }
                }
            }
        }
        private async Task RunReportAsync_Total_old(string templatePath, string outputPath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(Pro_name.Text, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (frdate.Visible == true)
                    {
                        command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    }

                    if (dept_id.Visible == true)
                    {
                        command.Parameters.AddWithValue("@dept_id", dept_id.Text);
                    }

                    command.CommandTimeout = 0; // Set timeout as needed
                                                // Giả sử bạn có một DataTable để lưu trữ kết quả
                    DataTable dataTable = new DataTable();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                    // Kiểm tra xem có dữ liệu trong dataTable không
                    if (dataTable.Rows.Count == 0)
                    {
                        // Thông báo hoặc xử lý khi không có dữ liệu
                        MessageBox.Show("Không có dữ liệu", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine("Không có dữ liệu để xuất Excel.");
                        return; // hoặc thực hiện các hành động khác theo yêu cầu của bạn
                    }
                    // Set the license context to use EPPlus
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial
                                                                                // Load tệp Excel mẫu
                    FileInfo templateFile = new FileInfo(templatePath);

                    // Tạo một gói Excel mới dựa trên mẫu
                    using (ExcelPackage package = new ExcelPackage(templateFile, true))
                    {
                        // Truy cập vào tờ công việc đầu tiên
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                        int startRow = 5;

                        worksheet.Cells["B2"].Value = $"Từ ngày: {frdate.Value} - Đến ngày : {todate.Value} ";


                        int columns_dem = 0;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            int i;
                            for (i = 0; i < dataTable.Columns.Count; i++)
                            {
                                // Giả sử các cột trong DataTable tương ứng với các cột trong Excel
                                //worksheet.Cells[startRow, i + 1].Value = row[i].ToString();
                                worksheet.Cells[startRow, i + 1].Value = row[i];
                                columns_dem = i;
                            }

                            worksheet.Cells[startRow, i + 1].Formula = $"=IFERROR(({worksheet.Cells[startRow, i].Address} / {worksheet.Cells[startRow, i - 1].Address})-1, 0)";
                            worksheet.Cells[startRow, i + 2].Formula = $"=+{worksheet.Cells[startRow, i].Address}-{worksheet.Cells[startRow, i - 1].Address}";
                            worksheet.Cells[startRow, i + 3].Formula = $"=IFERROR(({worksheet.Cells[startRow, i].Address} / {worksheet.Cells[startRow, i - 2].Address})-1, 0)";
                            worksheet.Cells[startRow, i + 4].Formula = $"=+{worksheet.Cells[startRow, i].Address}-{worksheet.Cells[startRow, i - 2].Address}";


                            startRow++;
                        }

                        // Áp dụng quy tắc định dạng
                        ExcelAddress address = new ExcelAddress($"F5:I{startRow}");
                        var formattingRule = worksheet.ConditionalFormatting.AddLessThan(address);
                        formattingRule.Formula = "0";
                        formattingRule.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Pink); //= "#FFC7CE"
                        formattingRule.Style.Font.Color.SetColor(System.Drawing.Color.Red);
                        //formattingRule.Style.Fill.BackgroundColor.Color = System.Drawing.Color.from(255, 205, 92, 92); // IndianRed color
                        worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].AutoFitColumns();
                        worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"A5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[$"C5:{GetExcelColumnName(columns_dem + 5)}{startRow}"].Style.Numberformat.Format = "#,##0.0,,\"tr\"";

                        worksheet.Cells[$"G5:G{startRow}"].Style.Numberformat.Format = "0%";
                        worksheet.Cells[$"I5:I{startRow}"].Style.Numberformat.Format = "0%";

                        //worksheet.Cells[$"J5:L{startRow}"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.BlanchedAlmond);
                        var range = worksheet.Cells[$"H5:J{startRow}"];
                        var fill = range.Style.Fill;
                        // Thiết lập kiểu mẫu trước khi đặt màu nền Cho cột Doanh THu MTD
                        fill.PatternType = ExcelFillStyle.Solid;
                        // Bây giờ đặt màu nền Cho cột Doanh THu MTD
                        fill.BackgroundColor.SetColor(System.Drawing.Color.Cornsilk);

                        worksheet.Cells["E4"].Value = frdate.Value.ToString("dd/MM/yyyy");
                        DateTime frdateValue = frdate.Value;
                        DateTime previousMonth = frdateValue.AddMonths(-1);
                        worksheet.Cells["D4"].Value = previousMonth.ToString("dd/MM/yyyy");
                        DateTime previousYear = frdateValue.AddYears(-1);
                        worksheet.Cells["C4"].Value = previousYear.ToString("dd/MM/yyyy");


                        // Lưu gói Excel đã được sửa đổi vào tệp đầu ra
                        package.SaveAs(new FileInfo(outputPath));
                        // Mở tệp Excel sau khi đã lưu
                        System.Diagnostics.Process.Start(outputPath);
                    }
                }
            }
        }
        private async void bt_BC_Click_Gọi_API(object sender, EventArgs e)
        {
            if (lbl_API.Text.ToLower() == "true")
            {
                apiUrl = Pro_name.Text.Trim();
                if (!isDownloading)
                {
                    isDownloading = true;

                    // Đường dẫn mặc định cho SaveFileDialog
                    string defaultSavePath = "path/to/save/excel.xlsx";

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                        saveFileDialog.Title = "Chọn vị trí để lưu tệp Excel";
                        saveFileDialog.FileName = "excel.xlsx"; // Tên mặc định của tệp

                        // Hiển thị hộp thoại và kiểm tra nếu người dùng đã chọn một vị trí
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Lấy đường dẫn được chọn
                            string filePath = saveFileDialog.FileName;

                            try
                            {
                                await DownloadAndSaveExcel(apiUrl, filePath);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Lỗi khi tải xuống file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                isDownloading = false;
                            }
                        }
                        else
                        {
                            // Người dùng đã hủy hoặc đóng hộp thoại, bạn có thể xử lý tùy thuộc vào yêu cầu của ứng dụng
                            isDownloading = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("File đang được tải xuống. Vui lòng đợi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private async Task DownloadAndSaveExcel(string apiUrl, string filePath)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //string requestData = "{ \"frDate\": \"2019-11-21T08:56:05.314Z\", \"toDate\": \"2023-11-21T08:56:05.314Z\", \"supp_ID\": \"string\", \"branchId\": \"string\", \"hcrC1_2FUJI_3All\": 0, \"contractType_1Mua_2Kg\": 0 }";
                    //string requestData = "{ \"frDate\": \"2019-11-21T08:56:05.314Z\", \"toDate\": \"2023-11-21T08:56:05.314Z\", \"supp_ID\": null, \"branchId\": null, \"hcrC1_2FUJI_3All\": 0, \"contractType_1Mua_2Kg\": 0 }";
                    //para_name
                    //string requestData = $"stk_id ={stk_id.Text},frDate= {frdate.Value} ,toDate={todate.Value}";
                    //apiUrl = "https://uat-reports_center-api.hcrc.vn/api/Ngam_Cuu/BC-Do_Phu_ASM";

                    string stkId = stk_id.Text;
                    DateTime frDate1 = frdate.Value;
                    DateTime toDate1 = todate.Value;

                    // Tạo đối tượng chứa các tham số
                    var parameters = new
                    {
                        stk_id = stkId,
                        FrDate = frDate1,
                        ToDate = toDate1
                    };
                    // Chuyển đối tượng thành chuỗi JSON
                    //string requestData = JsonConvert.SerializeObject(parameters);
                    string requestData = "?stk_id=001%2C002&FrDate=12%2F05%2F2023&ToDate=12%2F10%2F2023";
                    // Gửi request POST đồng bộ
                    HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(requestData, Encoding.UTF8, "application/json"));
                    // Thực hiện yêu cầu GET đến API
                    //HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    // Kiểm tra xem request có thành công không
                    // Kiểm tra xem request có thành công không
                    if (response.IsSuccessStatusCode)
                    {
                        // Đọc dữ liệu từ response và lưu vào file
                        byte[] content = await response.Content.ReadAsByteArrayAsync();
                        System.IO.File.WriteAllBytes(filePath, content);

                        MessageBox.Show("Tải file thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //MessageBox.Show($"Lỗi khi tải file: {response.StatusCode}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        string responseContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Lỗi khi tải file: {response.StatusCode}\n{responseContent}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void report_name_Click(object sender, EventArgs e)
        {
            Node_id.Visible = true;
            Pro_name.Visible = true;
            gr_para_name.Visible = true;
            para_name.Visible = true;
            lbl_API.Visible = true;
        }
        private string GetExcelColumnName(int columnIndex)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int dividend = columnIndex;
            string columnName = "";

            while (dividend > 0)
            {
                int modulo = (dividend - 1) % 26;
                columnName = letters[modulo] + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }
    }
}
