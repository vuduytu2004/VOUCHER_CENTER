using OfficeOpenXml;
using Report_Center.DataAccess;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;


//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Report_Center.Presentation
{
    public partial class Fr_Mar_Imp : Form
    {

        DataTable dataTable = new DataTable();
        // Khai báo biến apiUrl ở mức độ của lớp
        private string apiUrl = "";
        private bool isDownloading = false; // Biến cờ để theo dõi trạng thái tải xuống

        public Fr_Mar_Imp()
        {
            InitializeComponent();
            InitializeCustomComponents();

            //PopulateTreeView();
            frdate.CustomFormat = "dd/MM/yyyy";
            todate.CustomFormat = "dd/MM/yyyy";
            frdate.MaxDate = DateTime.Now.AddDays(-1); // Chỉ cho phép chọn đến ngày hôm qua
            todate.MaxDate = DateTime.Now.AddDays(-1); // Chỉ cho phép chọn đến ngày hôm qua
        }
        private void InitializeCustomComponents()
        {
            // Gắn sự kiện cho Label
            label1.Click += new EventHandler(button1_Click);
            label1.MouseEnter += new EventHandler(label1_MouseEnter);
            label1.MouseLeave += new EventHandler(label1_MouseLeave);
        }
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            Label label = sender as Label;
            if (label != null)
            {
                label.Font = new Font(label.Font, FontStyle.Italic | FontStyle.Underline);
            }
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            Label label = sender as Label;
            if (label != null)
            {
                label.Font = new Font(label.Font, FontStyle.Regular | FontStyle.Underline);
            }
        }

        private void Fr_Mar_Imp_Load(object sender, EventArgs e)
        {
            // Gọi hàm PopulateTreeView trong sự kiện Load của Form
            //PopulateTreeView();
        }

        private void bt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy đường dẫn hiện tại của ứng dụng
            string Dirpath = Directory.GetCurrentDirectory();

            // Đường dẫn tới thư mục Template
            string Template = Path.Combine(Dirpath, "Media", "Template");

            // Tên file trong Template
            string file_temp = "Import_Marketting.xlsx";

            // Kết hợp đường dẫn thư mục Template với tên file để tạo đường dẫn đầy đủ tới file
            string templatePath = Path.Combine(Template, file_temp);

            // Khởi tạo SaveFileDialog để người dùng chọn nơi lưu file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.Title = "Save an Excel File";
            saveFileDialog.FileName = file_temp; // Tên file mặc định

            // Hiển thị hộp thoại SaveFileDialog và kiểm tra xem người dùng đã chọn một đường dẫn hợp lệ chưa
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn mà người dùng đã chọn
                string savePath = saveFileDialog.FileName;

                try
                {
                    // Copy file từ Template đến vị trí mới mà người dùng đã chọn
                    File.Copy(templatePath, savePath, true);
                    MessageBox.Show("File saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu có vấn đề xảy ra trong quá trình sao chép file
                    MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void import_data_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                // Truncate table trước khi nhập dữ liệu mới
                using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("TRUNCATE TABLE DATA_IMP", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheetByItems = package.Workbook.Worksheets["By items"];
                    var worksheetKeHoachVH = package.Workbook.Worksheets["Kế hoạch VH"];

                    int totalRows = (worksheetByItems.Dimension.End.Row - 2) + (worksheetKeHoachVH.Dimension.End.Row - 2);
                    int currentRow = 0;

                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = totalRows;
                    progressBar1.Value = 0;

                    using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
                    {
                        connection.Open();

                        // Insert dữ liệu từ sheet 'By items'
                        for (int row = 3; row <= worksheetByItems.Dimension.End.Row; row++)
                        {
                            string query = @"
                        INSERT INTO DATA_IMP (
                            Ma10So, Ma_Nganh_Hàng, Goods_id, TenSanPham, NganhHang, GiaBanLe, GiaKhuyenMaiMart, 
                            GiaKhuyenMaiMini, Key1, TotalKM, Thue, ApDung, ApDungMart, ApDungMiniMart, Note, 
                            Key_sheet
                        ) VALUES (
                            @Ma10So, @Ma_Nganh_Hàng, @Goods_id, @TenSanPham, @NganhHang, @GiaBanLe, @GiaKhuyenMaiMart, 
                            @GiaKhuyenMaiMini, @Key1, @TotalKM, @Thue, @ApDung, @ApDungMart, @ApDungMiniMart, @Note, 
                            @Key_sheet
                        )";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@Ma10So", GetCellValue(worksheetByItems.Cells[row, 1]));
                                command.Parameters.AddWithValue("@Ma_Nganh_Hàng", GetCellValue(worksheetByItems.Cells[row, 2]));
                                command.Parameters.AddWithValue("@Goods_id", GetCellValue(worksheetByItems.Cells[row, 3]));
                                command.Parameters.AddWithValue("@TenSanPham", GetCellValue(worksheetByItems.Cells[row, 4]));
                                command.Parameters.AddWithValue("@NganhHang", GetCellValue(worksheetByItems.Cells[row, 5]));
                                command.Parameters.AddWithValue("@GiaBanLe", GetCellValue(worksheetByItems.Cells[row, 6]));
                                command.Parameters.AddWithValue("@GiaKhuyenMaiMart", GetCellValue(worksheetByItems.Cells[row, 7]));
                                command.Parameters.AddWithValue("@GiaKhuyenMaiMini", GetCellValue(worksheetByItems.Cells[row, 8]));
                                command.Parameters.AddWithValue("@Key1", GetCellValue(worksheetByItems.Cells[row, 9]));
                                command.Parameters.AddWithValue("@TotalKM", GetCellValue(worksheetByItems.Cells[row, 10]));
                                command.Parameters.AddWithValue("@Thue", GetCellValue(worksheetByItems.Cells[row, 11]));
                                command.Parameters.AddWithValue("@ApDung", GetCellValue(worksheetByItems.Cells[row, 12]));
                                command.Parameters.AddWithValue("@ApDungMart", GetCellValue(worksheetByItems.Cells[row, 13]));
                                command.Parameters.AddWithValue("@ApDungMiniMart", GetCellValue(worksheetByItems.Cells[row, 14]));
                                command.Parameters.AddWithValue("@Note", GetCellValue(worksheetByItems.Cells[row, 15]));
                                command.Parameters.AddWithValue("@Key_sheet", "By_items");

                                command.ExecuteNonQuery();
                            }

                            currentRow++;
                            progressBar1.Value = currentRow;
                        }

                        // Insert dữ liệu từ sheet 'Kế hoạch VH'
                        for (int row = 3; row <= worksheetKeHoachVH.Dimension.End.Row; row++)
                        {
                            string query = @"
                        INSERT INTO DATA_IMP (
                            STK_ID, Goods_id, SL_DuKien_Ban, Key_sheet
                        ) VALUES (
                            @STK_ID, @Goods_id, @SL_DuKien_Ban, @Key_sheet
                        )";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@STK_ID", GetCellValue(worksheetKeHoachVH.Cells[row, 1]));
                                command.Parameters.AddWithValue("@Goods_id", GetCellValue(worksheetKeHoachVH.Cells[row, 2]));
                                command.Parameters.AddWithValue("@SL_DuKien_Ban", GetCellValue(worksheetKeHoachVH.Cells[row, 3]));
                                command.Parameters.AddWithValue("@Key_sheet", "Ke_hoach_VH");

                                command.ExecuteNonQuery();
                            }

                            currentRow++;
                            progressBar1.Value = currentRow;
                        }
                    }
                }
            }

            MessageBox.Show("Import completed successfully.");
        }

        private object GetCellValue(ExcelRange cell)
        {
            if (cell.Value == null)
            {
                return DBNull.Value;
            }

            if (cell.Value is ExcelErrorValue)
            {
                return DBNull.Value;
            }

            return cell.Value;
        }

        private async void Exp_data_Click(object sender, EventArgs e)
        {
            // Thiết lập progressBar1 vào chế độ Marquee
            progressBar1.Style = ProgressBarStyle.Marquee;

            string Dirpath = Directory.GetCurrentDirectory();
            string Template = Path.Combine(Dirpath, "Media", "Template");
            string file_temp = "Template_Marketting";
            string dateAndRandom = frdate.Value.ToString("yyyyMMddHHmm") + "_" + new Random().Next(100, 999);
            string templatePath = Path.Combine(Template, $"{file_temp}.xlsx");

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog.DefaultExt = "xlsx";
                saveFileDialog.AddExtension = true;
                saveFileDialog.FileName = $"{file_temp}_{dateAndRandom}.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
                    string uniqueFileName = GetUniqueFileName(Path.GetFileName(saveFileDialog.FileName), selectedDirectory);
                    Exp_data.Enabled = false;
                    await RunReportAsync_MKT(templatePath, uniqueFileName);
                    Exp_data.Enabled = true;
                    //MessageBox.Show($"File saved to: {uniqueFileName}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Operation canceled by the user.", "Export Canceled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async Task RunReportAsync_MKT(string templatePath, string savePath)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString_DWH))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("rpt_SUM_Bill_By_SKU_MarKeting", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@frdate", frdate.Value.ToString("yyyyMMdd"));
                    command.Parameters.AddWithValue("@todate", todate.Value.ToString("yyyyMMdd"));
                    command.CommandTimeout = 0;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        using (ExcelPackage package = new ExcelPackage(new FileInfo(templatePath)))
                        {
                            if (await reader.ReadAsync())
                            {
                                ExcelWorksheet sheet1 = package.Workbook.Worksheets["Bán, tồn"];
                                sheet1.Cells["A1"].Value = frdate.Value.ToString("dd-MM-yyyy");
                                sheet1.Cells["B1"].Value = todate.Value.ToString("dd-MM-yyyy");
                                int row = 4;
                                do
                                {
                                    for (int col = 0; col < reader.FieldCount; col++)
                                    {
                                        sheet1.Cells[row, col + 5].Value = reader.GetValue(col);
                                    }
                                    row++;
                                } while (await reader.ReadAsync());
                            }

                            await reader.NextResultAsync();
                            if (await reader.ReadAsync())
                            {
                                ExcelWorksheet sheet2 = package.Workbook.Worksheets["By items"];
                                int row = 10;
                                do
                                {
                                    for (int col = 0; col < reader.FieldCount; col++)
                                    {
                                        sheet2.Cells[row, col + 1].Value = reader.GetValue(col);
                                    }
                                    row++;
                                } while (await reader.ReadAsync());
                            }

                            await reader.NextResultAsync();
                            if (await reader.ReadAsync())
                            {
                                ExcelWorksheet sheet3 = package.Workbook.Worksheets["Kế hoạch VH"];
                                int row = 3;
                                do
                                {
                                    for (int col = 0; col < reader.FieldCount; col++)
                                    {
                                        sheet3.Cells[row, col + 3].Value = reader.GetValue(col);
                                    }
                                    row++;
                                } while (await reader.ReadAsync());
                            }

                            // Sau khi công việc dài hạn hoàn tất, bạn có thể đặt lại progressBar1 vào chế độ mặc định
                            progressBar1.Style = ProgressBarStyle.Blocks;

                            await package.SaveAsAsync(new FileInfo(savePath));
                            System.Diagnostics.Process.Start(new ProcessStartInfo(savePath) { UseShellExecute = true });
                        }
                    }
                }
            }
        }


    }
}