using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VOUCHER_CENTER.DataAccess;
using static VOUCHER_CENTER.Main;

namespace VOUCHER_CENTER.Presentation
{
    public partial class fr_VoucherSync_Test : Form
    {
        private string connectionString = bientoancuc.connectionString;
        string UniqueID_Group1 = "";
        private decimal totalMenhgia = 0;
        //bool cap_nhat = false;
        //private DateTime _lastInputTime;
        //private const int ScanInputThreshold = 50; // Thời gian tính bằng milliseconds, có thể điều chỉnh
        private DateTime lastKeystroke = DateTime.MinValue;
        private List<char> barcode = new List<char>();
        private TimeSpan barcodeTimeSpan = TimeSpan.FromMilliseconds(70); // Adjust this time span as needed
        public fr_VoucherSync_Test()
        {
            InitializeComponent();

            this.Paint += new PaintEventHandler(this.MainForm_Paint);
            this.Resize += new EventHandler(this.MainForm_Resize);

            //InitializeDataGridView();
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

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            DrawWatermark(e.Graphics);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.Invalidate(); // Redraw the form when resized
        }

        private void DrawWatermark(Graphics g)
        {
            string watermarkText = "HCRC UAT";
            Font watermarkFont = new Font("Arial", 74, FontStyle.Bold, GraphicsUnit.Pixel);
            Color watermarkColor = Color.FromArgb(128, 255, 0, 0); // Semi-transparent red

            // Tính toán kích thước của văn bản watermark
            SizeF textSize = g.MeasureString(watermarkText, watermarkFont);

            // Tính toán vị trí của watermark sao cho ở giữa Form
            PointF location = new PointF(
                (this.ClientSize.Width - textSize.Width) / 2,
                (this.ClientSize.Height - textSize.Height) / 2
            );

            // Lưu trạng thái hiện tại của Graphics
            GraphicsState state = g.Save();

            // Dịch chuyển điểm gốc của Graphics tới giữa của Form
            g.TranslateTransform(location.X + textSize.Width / 2, location.Y + textSize.Height / 2);

            // Xoay Graphics 45 độ
            g.RotateTransform(-45);

            // Vẽ watermark
            g.DrawString(watermarkText, watermarkFont, new SolidBrush(watermarkColor), -textSize.Width / 2, -textSize.Height / 2);

            // Khôi phục trạng thái ban đầu của Graphics
            g.Restore(state);
        }
        //private void InitializeDataGridView()
        //{

        //    // Thêm cột nút xóa
        //    DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
        //    deleteButtonColumn.Name = "DeleteButton";
        //    deleteButtonColumn.HeaderText = "Del";
        //    deleteButtonColumn.Text = "Del";
        //    deleteButtonColumn.UseColumnTextForButtonValue = true;
        //    deleteButtonColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    dataGridView1.Columns.Add(deleteButtonColumn);

        //    // Đăng ký sự kiện CellClick
        //    dataGridView1.CellClick += dataGridView1_CellClick;
        //}
        //private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    // Kiểm tra xem người dùng có nhấn vào cột nút xóa không
        //    if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["DeleteButton"].Index)
        //    {
        //        // Xóa dòng tương ứng
        //        dataGridView1.Rows.RemoveAt(e.RowIndex);

        //        // Cập nhật lại cột STT
        //        for (int i = 0; i < dataGridView1.Rows.Count; i++)
        //        {
        //            dataGridView1.Rows[i].Cells["STT"].Value = i + 1;
        //        }
        //    }
        //}

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
                    //btnSave.Enabled = false;
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
            try
            {
                // Tạo chuỗi UniqueID_Group tương đương với đoạn code SQL
                string UniqueID_Group;
                // Lấy ngày giờ hiện tại
                DateTime currentDate = DateTime.Now;
                // Format ngày giờ hiện tại thành chuỗi theo định dạng yyyymmddHHMMss
                string dateString = currentDate.ToString("yyyyMMddHHmmss");
                // Tạo chuỗi ngẫu nhiên với độ dài là 5 ký tự
                Random random = new Random();
                string randomString = random.Next(100000).ToString("D5");
                // Kết hợp các chuỗi lại với nhau để tạo UniqueID_Group
                UniqueID_Group = GlobalVariables.User_Name + dateString + randomString;

                // Hiển thị print preview
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                PrintDocument printDocument = new PrintDocument();

                printDocument.PrintPage += (s, ev) =>
                {
                    // Thiết lập font và độ rộng của trang
                    float yPos = 0;
                    int count = 0;
                    float leftMargin = ev.MarginBounds.Left - 40; // 1 cm ~ 40 points
                                                                  //float rightMargin = ev.MarginBounds.Right; // 1 cm ~ 40 points
                    float printableWidth = ev.MarginBounds.Width; // Độ rộng của vùng in có thể in được
                    float rightMargin = leftMargin + printableWidth - 100;
                    float topMargin = ev.MarginBounds.Top;
                    float cellHeight = 25; // Chiều cao mỗi ô
                    Font printFont = new Font("Arial", 11);
                    SolidBrush myBrush = new SolidBrush(Color.Black);
                    Pen blackPen = new Pen(Color.Black);

                    // Đặt chiều rộng các cột theo số ký tự yêu cầu
                    float transNumWidth = 18 * 10; // 18 ký tự * 10 điểm (mỗi ký tự ước tính khoảng 10 điểm)
                    float voucherWidth = 14 * 10; // 14 ký tự * 10 điểm
                    float dateWidth = 11 * 10; // 11 ký tự * 10 điểm
                    float customerWidth = 30 * 10; // 30 ký tự * 10 điểm

                    // Lấy các thông tin từ form
                    string locationGroupName = lb_LocationGroupName.Text;
                    string locationDetailName = lb_LocationDetailName.Text;
                    string currentDateFormatted = currentDate.ToString("dd/MM/yyyy HH:mm:ss");
                    string transNum = txtTransNum.Text;
                    string createdDate = dtpCreatedDate.Value.ToString("dd/MM/yyyy");
                    string playerName = txtPlayerName.Text;
                    string description = txtDescription.Text;

                    // Vẽ thông tin lên trang
                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString(locationGroupName, printFont, myBrush, leftMargin, yPos, new StringFormat());
                    count += 2; // Cách 2 dòng

                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString(locationDetailName, printFont, myBrush, leftMargin, yPos, new StringFormat());
                    count += 2; // Cách 2 dòng

                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString("BIÊN BẢN THU HỒI VOUCHER", printFont, myBrush, leftMargin + (ev.MarginBounds.Width - ev.Graphics.MeasureString("BIÊN BẢN THU HỒI VOUCHER", printFont).Width) / 2, yPos, new StringFormat());
                    count += 1; // Cách 1 dòng

                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString(currentDateFormatted, printFont, myBrush, leftMargin + (ev.MarginBounds.Width - ev.Graphics.MeasureString(currentDateFormatted, printFont).Width) / 2, yPos, new StringFormat());

                    // Tạo header cho bảng
                    yPos = topMargin + count * cellHeight;

                    // Sử dụng StringFormat để căn giữa
                    StringFormat centerFormat = new StringFormat();
                    centerFormat.Alignment = StringAlignment.Center;
                    centerFormat.LineAlignment = StringAlignment.Center;

                    ev.Graphics.DrawString("Số giao dịch", printFont, myBrush, new RectangleF(leftMargin, yPos, transNumWidth, cellHeight), centerFormat);
                    ev.Graphics.DrawString("Mã Voucher", printFont, myBrush, new RectangleF(leftMargin + transNumWidth, yPos, voucherWidth, cellHeight), centerFormat);
                    ev.Graphics.DrawString("Ngày thu hồi", printFont, myBrush, new RectangleF(leftMargin + transNumWidth + voucherWidth, yPos, dateWidth, cellHeight), centerFormat);
                    ev.Graphics.DrawString("Khách hàng", printFont, myBrush, new RectangleF(leftMargin + transNumWidth + voucherWidth + dateWidth, yPos, customerWidth, cellHeight), centerFormat);
                    count += 1;

                    // Vẽ các đường viền cho header
                    ev.Graphics.DrawRectangle(blackPen, leftMargin, yPos, transNumWidth, cellHeight);
                    ev.Graphics.DrawRectangle(blackPen, leftMargin + transNumWidth, yPos, voucherWidth, cellHeight);
                    ev.Graphics.DrawRectangle(blackPen, leftMargin + transNumWidth + voucherWidth, yPos, dateWidth, cellHeight);
                    ev.Graphics.DrawRectangle(blackPen, leftMargin + transNumWidth + voucherWidth + dateWidth, yPos, customerWidth, cellHeight);

                    // Lặp qua từng hàng trong dataGridView1 để in dữ liệu
                    foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                    {
                        yPos = topMargin + count * cellHeight;

                        ev.Graphics.DrawString(transNum, printFont, myBrush, leftMargin, yPos, new StringFormat());
                        ev.Graphics.DrawString(dgvRow.Cells["Voucher_serial"].Value?.ToString(), printFont, myBrush, leftMargin + transNumWidth, yPos, new StringFormat());
                        ev.Graphics.DrawString(createdDate, printFont, myBrush, new RectangleF(leftMargin + transNumWidth + voucherWidth, yPos, dateWidth, cellHeight), centerFormat);
                        //ev.Graphics.DrawString(createdDate, printFont, myBrush, leftMargin + transNumWidth + voucherWidth, yPos, new StringFormat());
                        ev.Graphics.DrawString(playerName, printFont, myBrush, leftMargin + transNumWidth + voucherWidth + dateWidth, yPos, new StringFormat());

                        //ev.Graphics.DrawString(transNum, printFont, myBrush, leftMargin, yPos, new StringFormat());
                        //ev.Graphics.DrawString(dgvRow.Cells["Voucher_serial"].Value?.ToString(), printFont, myBrush, new RectangleF(leftMargin + transNumWidth, yPos, voucherWidth, cellHeight), centerFormat);
                        //ev.Graphics.DrawString(createdDate, printFont, myBrush, new RectangleF(leftMargin + transNumWidth + voucherWidth, yPos, dateWidth, cellHeight), centerFormat);
                        //ev.Graphics.DrawString(playerName, printFont, myBrush, new RectangleF(leftMargin + transNumWidth + voucherWidth + dateWidth, yPos, customerWidth, cellHeight), centerFormat);

                        // Vẽ các đường viền cho dữ liệu
                        ev.Graphics.DrawRectangle(blackPen, leftMargin, yPos, transNumWidth, cellHeight);
                        ev.Graphics.DrawRectangle(blackPen, leftMargin + transNumWidth, yPos, voucherWidth, cellHeight);
                        ev.Graphics.DrawRectangle(blackPen, leftMargin + transNumWidth + voucherWidth, yPos, dateWidth, cellHeight);
                        ev.Graphics.DrawRectangle(blackPen, leftMargin + transNumWidth + voucherWidth + dateWidth, yPos, customerWidth, cellHeight);

                        count += 1; // Cách 1 dòng
                    }
                    count -= 1;
                    // Vẽ phần "Diễn giải" dưới bảng
                    yPos = topMargin + count * cellHeight + 2 * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString("Diễn giải:", printFont, myBrush, leftMargin, yPos, new StringFormat());
                    yPos += printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString(description, printFont, myBrush, leftMargin, yPos, new StringFormat());

                    count += 2; // Cách 1 dòng
                    yPos = topMargin + count * cellHeight + 2 * printFont.GetHeight(ev.Graphics);
                    string indentedText1 = "Người Lập" + "               " + "              ";
                    ev.Graphics.DrawString(indentedText1, printFont, myBrush, rightMargin, yPos, new StringFormat());
                    count += 1; // Cách 1 dòng
                    ev.HasMorePages = false; // Chỉ in một trang
                };

                printPreviewDialog.Document = printDocument;
                // Thiết lập mức thu phóng (zoom) của PrintPreviewDialog
                ((Form)printPreviewDialog).WindowState = FormWindowState.Maximized;
                printPreviewDialog.Load += (s, ev) =>
                {
                    // Thiết lập mức thu phóng của PrintPreviewControl bên trong PrintPreviewDialog
                    ((PrintPreviewDialog)s).PrintPreviewControl.Zoom = 2.0;
                };
                printPreviewDialog.ShowDialog();

                txtVoucherSerial.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click2222(object sender, EventArgs e)
        {
            try
            {
                // Tạo chuỗi UniqueID_Group tương đương với đoạn code SQL
                string UniqueID_Group;
                // Lấy ngày giờ hiện tại
                DateTime currentDate = DateTime.Now;
                // Format ngày giờ hiện tại thành chuỗi theo định dạng yyyymmddHHMMss
                string dateString = currentDate.ToString("yyyyMMddHHmmss");
                // Tạo chuỗi ngẫu nhiên với độ dài là 5 ký tự
                Random random = new Random();
                string randomString = random.Next(100000).ToString("D5");
                // Kết hợp các chuỗi lại với nhau để tạo UniqueID_Group
                UniqueID_Group = GlobalVariables.User_Name + dateString + randomString;

                // Hiển thị print preview
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                PrintDocument printDocument = new PrintDocument();

                printDocument.PrintPage += (s, ev) =>
                {
                    // Thiết lập font và độ rộng của trang
                    float yPos = 0;
                    int count = 0;
                    float leftMargin = ev.MarginBounds.Left - 40; // 1 cm ~ 40 points
                    //float rightMargin = ev.MarginBounds.Right; // 1 cm ~ 40 points
                    float printableWidth = ev.MarginBounds.Width; // Độ rộng của vùng in có thể in được
                    float rightMargin = leftMargin + printableWidth - 100;
                    float topMargin = ev.MarginBounds.Top;
                    float cellHeight = 25; // Chiều cao mỗi ô
                    Font printFont = new Font("Arial", 11);
                    SolidBrush myBrush = new SolidBrush(Color.Black);
                    Pen blackPen = new Pen(Color.Black);

                    // Đặt chiều rộng các cột theo số ký tự yêu cầu
                    float transNumWidth = 18 * 10; // 18 ký tự * 10 điểm (mỗi ký tự ước tính khoảng 10 điểm)
                    float voucherWidth = 14 * 10; // 13 ký tự * 10 điểm
                    float dateWidth = 11 * 10; // 11 ký tự * 10 điểm
                    float customerWidth = 30 * 10; // 20 ký tự * 10 điểm

                    // Lấy các thông tin từ form
                    string locationGroupName = lb_LocationGroupName.Text;
                    string locationDetailName = lb_LocationDetailName.Text;
                    string currentDateFormatted = currentDate.ToString("dd/MM/yyyy HH:mm:ss");
                    string transNum = txtTransNum.Text;
                    string createdDate = dtpCreatedDate.Value.ToString("dd/MM/yyyy");
                    string playerName = txtPlayerName.Text;
                    string description = txtDescription.Text;

                    // Vẽ thông tin lên trang
                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString(locationGroupName, printFont, myBrush, leftMargin, yPos, new StringFormat());
                    count += 2; // Cách 2 dòng

                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString(locationDetailName, printFont, myBrush, leftMargin, yPos, new StringFormat());
                    count += 2; // Cách 2 dòng

                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString("BIÊN BẢN THU HỒI VOUCHER", printFont, myBrush, leftMargin + (ev.MarginBounds.Width - ev.Graphics.MeasureString("BIÊN BẢN THU HỒI VOUCHER", printFont).Width) / 2, yPos, new StringFormat());
                    count += 1; // Cách 1 dòng

                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString(currentDateFormatted, printFont, myBrush, leftMargin + (ev.MarginBounds.Width - ev.Graphics.MeasureString(currentDateFormatted, printFont).Width) / 2, yPos, new StringFormat());

                    // Tạo header cho bảng
                    yPos = topMargin + count * cellHeight;
                    ev.Graphics.DrawString("Số giao dịch", printFont, myBrush, leftMargin, yPos, new StringFormat());
                    ev.Graphics.DrawString("Mã Voucher", printFont, myBrush, leftMargin + transNumWidth, yPos, new StringFormat());
                    ev.Graphics.DrawString("Ngày thu hồi", printFont, myBrush, leftMargin + transNumWidth + voucherWidth, yPos, new StringFormat());
                    ev.Graphics.DrawString("Khách hàng", printFont, myBrush, leftMargin + transNumWidth + voucherWidth + dateWidth, yPos, new StringFormat());
                    count += 1;

                    // Vẽ các đường viền cho header
                    ev.Graphics.DrawRectangle(blackPen, leftMargin, yPos, transNumWidth, cellHeight);
                    ev.Graphics.DrawRectangle(blackPen, leftMargin + transNumWidth, yPos, voucherWidth, cellHeight);
                    ev.Graphics.DrawRectangle(blackPen, leftMargin + transNumWidth + voucherWidth, yPos, dateWidth, cellHeight);
                    ev.Graphics.DrawRectangle(blackPen, leftMargin + transNumWidth + voucherWidth + dateWidth, yPos, customerWidth, cellHeight);

                    // Lặp qua từng hàng trong dataGridView1 để in dữ liệu
                    foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                    {
                        yPos = topMargin + count * cellHeight;

                        ev.Graphics.DrawString(transNum, printFont, myBrush, leftMargin, yPos, new StringFormat());
                        ev.Graphics.DrawString(dgvRow.Cells["Voucher_serial"].Value?.ToString(), printFont, myBrush, leftMargin + transNumWidth, yPos, new StringFormat());
                        ev.Graphics.DrawString(createdDate, printFont, myBrush, leftMargin + transNumWidth + voucherWidth, yPos, new StringFormat());
                        ev.Graphics.DrawString(playerName, printFont, myBrush, leftMargin + transNumWidth + voucherWidth + dateWidth, yPos, new StringFormat());

                        // Vẽ các đường viền cho dữ liệu
                        ev.Graphics.DrawRectangle(blackPen, leftMargin, yPos, transNumWidth, cellHeight);
                        ev.Graphics.DrawRectangle(blackPen, leftMargin + transNumWidth, yPos, voucherWidth, cellHeight);
                        ev.Graphics.DrawRectangle(blackPen, leftMargin + transNumWidth + voucherWidth, yPos, dateWidth, cellHeight);
                        ev.Graphics.DrawRectangle(blackPen, leftMargin + transNumWidth + voucherWidth + dateWidth, yPos, customerWidth, cellHeight);

                        count += 1; // Cách 1 dòng
                    }
                    count -= 1;
                    // Vẽ phần "Diễn giải" dưới bảng
                    yPos = topMargin + count * cellHeight + 2 * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString("Diễn giải:", printFont, myBrush, leftMargin, yPos, new StringFormat());
                    yPos += printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString(description, printFont, myBrush, leftMargin, yPos, new StringFormat());
                    
                    count += 2; // Cách 1 dòng
                    yPos = topMargin + count * cellHeight + 2 * printFont.GetHeight(ev.Graphics);
                    string indentedText1 = "Người Lập" + "               " + "              ";
                    ev.Graphics.DrawString(indentedText1, printFont, myBrush, rightMargin, yPos, new StringFormat());
                    count += 1; // Cách 1 dòng
                    ev.HasMorePages = false; // Chỉ in một trang
                };

                printPreviewDialog.Document = printDocument;
                printPreviewDialog.ShowDialog();

                txtVoucherSerial.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnSave_Click_luu(object sender, EventArgs e)
        {
            try
            {

                // Tạo chuỗi UniqueID_Group tương đương với đoạn code SQL
                string UniqueID_Group;
                // Lấy ngày giờ hiện tại
                DateTime currentDate = DateTime.Now;
                // Format ngày giờ hiện tại thành chuỗi theo định dạng yyyymmddHHMMss
                string dateString = currentDate.ToString("yyyyMMddHHmmss");
                // Tạo chuỗi ngẫu nhiên với độ dài là 5 ký tự
                Random random = new Random();
                string randomString = random.Next(100000).ToString("D5");
                // Kết hợp các chuỗi lại với nhau để tạo UniqueID_Group
                UniqueID_Group = GlobalVariables.User_Name + dateString + randomString;


                // Hiển thị print preview
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                PrintDocument printDocument = new PrintDocument();

                printDocument.PrintPage += (s, ev) =>
                {
                    // Thiết lập font và độ rộng của trang
                    float linesPerPage = 0;
                    float yPos = 0;
                    int count = 0;
                    float leftMargin = ev.MarginBounds.Left;
                    //float rightMargin = ev.MarginBounds.Right;
                    float printableWidth = ev.MarginBounds.Width; // Độ rộng của vùng in có thể in được
                    float rightMargin = leftMargin + printableWidth - 100;
                    float topMargin = ev.MarginBounds.Top;
                    string line = null;
                    Font printFont = new Font("Arial", 12);
                    SolidBrush myBrush = new SolidBrush(Color.Black);

                    // Lấy các thông tin từ form
                    string locationGroupName = lb_LocationGroupName.Text;
                    string locationDetailName = lb_LocationDetailName.Text;
                    string currentDateFormatted = currentDate.ToString("dd/MM/yyyy HH:mm:ss");
                    string transNum = txtTransNum.Text;
                    string createdDate = dtpCreatedDate.Value.ToString("dd/MM/yyyy");
                    string playerName = txtPlayerName.Text;
                    string description = txtDescription.Text;

                    // Vẽ thông tin lên trang
                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString(locationGroupName, printFont, myBrush, leftMargin, yPos, new StringFormat());
                    count += 2; // Cách 2 dòng

                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString(locationDetailName, printFont, myBrush, leftMargin, yPos, new StringFormat());
                    count += 2; // Cách 2 dòng

                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString("BIÊN BẢN THU HỒI VOUCHER", printFont, myBrush, leftMargin + (ev.MarginBounds.Width - ev.Graphics.MeasureString("Bên Bản Thu thôi Voucher", printFont).Width) / 2, yPos, new StringFormat());
                    count += 1; // Cách 1 dòng

                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString(currentDateFormatted, printFont, myBrush, leftMargin + (ev.MarginBounds.Width - ev.Graphics.MeasureString(currentDateFormatted, printFont).Width) / 2, yPos, new StringFormat());
                    count += 2; // Cách 2 dòng

                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString("Mã Voucher :", printFont, myBrush, leftMargin, yPos, new StringFormat());
                    count += 1; // Cách 1 dòng

                    // Lặp qua từng hàng trong dataGridView1 để in mã voucher
                    foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                    {
                        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                        // Thêm lề bằng cách thêm khoảng trống vào đầu dòng
                        string indentedText = "    " + dgvRow.Cells["voucher_serial"].Value.ToString();
                        ev.Graphics.DrawString(indentedText, printFont, myBrush, leftMargin, yPos, new StringFormat());
                        count += 1; // Cách 1 dòng
                    }

                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString("Số giao dịch: " + transNum, printFont, myBrush, leftMargin, yPos, new StringFormat());
                    count += 1; // Cách 1 dòng

                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString("Ngày thu hồi: " + createdDate, printFont, myBrush, leftMargin, yPos, new StringFormat());
                    count += 1; // Cách 1 dòng

                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString("Khách hàng: " + playerName, printFont, myBrush, leftMargin, yPos, new StringFormat());
                    count += 1; // Cách 1 dòng

                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    ev.Graphics.DrawString("Diễn giải: " + description, printFont, myBrush, leftMargin, yPos, new StringFormat());
                    count += 1; // Cách 1 dòng

                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    string indentedText1 = "Người Lập" + "               " + "              ";
                    ev.Graphics.DrawString(indentedText1, printFont, myBrush, rightMargin, yPos, new StringFormat());
                    count += 1; // Cách 1 dòng
                    ev.HasMorePages = false; // Chỉ in một trang
                };

                printPreviewDialog.Document = printDocument;
                printPreviewDialog.ShowDialog();






                txtVoucherSerial.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (dataGridView1.Rows.Count == 0)
        //        {
        //            return; // No data in dataGridView1
        //        }
        //        if (UniqueID_Group1 == "")
        //        {
        //            return; // No data in dataGridView1
        //        }


        //        ClearInputFields();
        //        dataGridView1.Rows.Clear();
        //        MessageBox.Show("Record saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        txtVoucherSerial.Enabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error saving record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


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

        private void InsertVoucherSync(SqlConnection connection, string voucherSerial, string UniqueID_Group)
        {
            using (SqlCommand command = new SqlCommand("InsertVoucherSync", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userid", GlobalVariables.UserID.ToString());
                command.Parameters.AddWithValue("@username", GlobalVariables.User_Name.ToString());
                command.Parameters.AddWithValue("@computername", GlobalVariables.Computer_Name.ToString());
                command.Parameters.AddWithValue("@TRANS_NUM", txtTransNum.Text);
                command.Parameters.AddWithValue("@Voucher_Serial", voucherSerial);
                //command.Parameters.AddWithValue("@Voucher_Code", Right(txtVoucherSerial.Text.Trim(), txtVoucherSerial.Text.Trim().Length - 1));
                command.Parameters.AddWithValue("@Created_Date", dtpCreatedDate.Value);
                //command.Parameters.AddWithValue("@Status", "USED");
                command.Parameters.AddWithValue("@Player_Name", txtPlayerName.Text);
                command.Parameters.AddWithValue("@Locations_Group", lb_LocationsGroup.Text);
                command.Parameters.AddWithValue("@Location_GroupName", lb_LocationGroupName.Text);
                command.Parameters.AddWithValue("@Locations_Detail", lb_LocationsDetail.Text);
                command.Parameters.AddWithValue("@Location_DetailName", lb_LocationDetailName.Text);
                command.Parameters.AddWithValue("@Description", txtDescription.Text);
                command.Parameters.AddWithValue("@UniqueID_Group", UniqueID_Group);
                //command.Parameters.AddWithValue("@Last_update", DateTime.Now);
                //command.Parameters.AddWithValue("@Sync", "N");
                //command.Parameters.AddWithValue("@Sync_update", DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        //------------------------------------------------------


        private void txtVoucherSerial_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private bool IsCompleteBarcode(char keyChar)
        {
            // Here you can define the logic to determine if the barcode is complete
            // For example, if the barcode ends with a newline character, or if it has reached a certain length
            return keyChar == '\r' || keyChar == '\n' || barcode.Count >= 13; // Adjust as needed
        }

        private void ProcessBarcode(string barcode)
        {
            // Process the scanned barcode
            txtVoucherSerial.Text = barcode;
            // Call your existing logic for handling the scanned barcode
            //txtVoucherSerial_Leave(null, null);
        }

        //--------------------------------------------------------

        //private void txtVoucherSerial_TextChanged(object sender, EventArgs e)
        //{
        //    if (!IsScannedInput() && GlobalVariables.User_Name.ToUpper() != "TEST")
        //    {
        //        txtVoucherSerial.Text = string.Empty;
        //    }
        //}
        //private bool IsScannedInput()
        //{
        //    DateTime currentTime = DateTime.Now;
        //    bool isScanned = (currentTime - _lastInputTime).TotalMilliseconds < ScanInputThreshold;
        //    _lastInputTime = currentTime;
        //    return isScanned;
        //}
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


                    //var result1 = GetVoucherSerialDetails_Local(connection, txtVoucherSerial.Text);
                    txtVoucherSerial.Text = txtVoucherSerial.Text.Substring(1);

                    // Kiểm tra xem Voucher_Serial đã tồn tại chưa trong local VOUCHER_SYNC

                    // Kiểm tra xem txtVoucherSerial.Text đã có trong cột VoucherSerial chưa
                    bool exists = dataGridView1.Rows.Cast<DataGridViewRow>()
                        .Any(row => row.Cells["Voucher_Serial"].Value != null && row.Cells["Voucher_Serial"].Value.ToString() == txtVoucherSerial.Text.Substring(1));

                    if (!exists)
                    {
                        // Thêm txtVoucherSerial.Text vào dataGridView1
                        int newRowIndex = dataGridView1.Rows.Add();
                        DataGridViewRow newRow = dataGridView1.Rows[newRowIndex];
                        newRow.Cells["STT"].Value = newRowIndex + 1; // Tăng STT từ 1
                        newRow.Cells["Voucher_Serial"].Value = txtVoucherSerial.Text;
                        newRow.Cells["menhgia"].Value ="200000";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking Voucher serial: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVoucherSerial.Focus();
            }
        }

        private (bool Exists, int user_id, string VoucherCheckValue, string VoucherSyncVoucherSerial, string VoucherSyncLocationGroupName, string VoucherSyncLocationDetailName, DateTime VoucherSyncLastUpdate, string VoucherSyncComputerName, string VoucherSyncTransNum, DateTime VoucherSyncCreatedDate, string VoucherSyncPlayerName, string VoucherSyncDescription, string HcrcVoucherSerial, string HcrcLocationType, string HcrcLocationName, DateTime HcrcLastUpdate) GetVoucher(SqlConnection connection, string voucherSerial)
        {//, string CardId, int Activate, int Status, DateTime DueDate
            using (SqlCommand command = new SqlCommand("CheckVoucher", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Voucher_Serial", voucherSerial);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string voucherCheckValue = reader.GetString(reader.GetOrdinal("Voucher_check_Value"));

                        //string cardId = reader.GetString(reader.GetOrdinal("CARD_ID"));
                        //int activate = reader.GetInt32(reader.GetOrdinal("ACTIVATE"));
                        //int status = reader.GetInt32(reader.GetOrdinal("STATUS"));
                        //DateTime dueDate = reader.GetDateTime(reader.GetOrdinal("DUE_DATE"));

                        int user_id = reader.IsDBNull(reader.GetOrdinal("VOUCHER_SYNC_userid")) ? 0 : reader.GetInt32(reader.GetOrdinal("VOUCHER_SYNC_userid"));
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

                        return (true, user_id, voucherCheckValue, voucherSyncVoucherSerial, voucherSyncLocationGroupName, voucherSyncLocationDetailName, voucherSyncLastUpdate, voucherSyncComputerName, voucherSyncTransNum, voucherSyncCreatedDate, voucherSyncPlayerName, voucherSyncDescription, hcrcVoucherSerial, hcrcLocationType, hcrcLocationName, hcrcLastUpdate);
                        //, cardId, activate, status, dueDate

                    }
                }
            }
            return (false, 0, null, null, null, null, DateTime.MinValue, null, null, DateTime.MinValue, null, null, null, null, null, DateTime.MinValue);
            //, 0, 0, DateTime.MinValue
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
        private (bool Exists, int user_id, string User_name, string LocationType, string LocationName, DateTime LastUpdate, string Computer_name, string TRANS_NUM, string Player_Name, string Description, DateTime Created_Date, string UniqueID_Group) GetVoucherSerialDetails_Local(SqlConnection connection, string voucherSerial)
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
                                int user_id = reader.IsDBNull(reader.GetOrdinal("userid")) ? 0 : reader.GetInt32(reader.GetOrdinal("userid"));
                                string User_name = reader.GetString(reader.GetOrdinal("User_name"));
                                string locationType = reader.GetString(reader.GetOrdinal("Location_GroupName"));
                                string locationName = reader.GetString(reader.GetOrdinal("Location_DetailName"));
                                DateTime lastUpdate = reader.GetDateTime(reader.GetOrdinal("Last_update"));
                                string Computer_name = reader.GetString(reader.GetOrdinal("Computer_name"));
                                string TRANS_NUM = reader.GetString(reader.GetOrdinal("TRANS_NUM"));
                                string Player_Name = reader.GetString(reader.GetOrdinal("Player_Name"));
                                string Description = reader.GetString(reader.GetOrdinal("Description"));
                                DateTime Created_Date = reader.GetDateTime(reader.GetOrdinal("Created_Date"));
                                string UniqueID_Group = reader.GetString(reader.GetOrdinal("UniqueID_Group"));
                                return (true, user_id, User_name, locationType, locationName, lastUpdate, Computer_name, TRANS_NUM, Player_Name, Description, Created_Date, UniqueID_Group);
                            }
                        }
                    }
                }
            }
            return (false, 0, null, null, null, DateTime.MinValue, null, null, null, null, DateTime.MinValue, null);
        }

        private void ClearInputFields()
        {
            txtVoucherSerial.Clear();
            txtDescription.Clear();
            txtPlayerName.Clear();
            txtTransNum.Clear();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            // Hiển thị print preview
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += (s, ev) =>
            {
                // Thiết lập font và độ rộng của trang
                float linesPerPage = 0;
                float yPos = 0;
                int count = 0;
                float leftMargin = ev.MarginBounds.Left;
                //float rightMargin = ev.MarginBounds.Right;
                float printableWidth = ev.MarginBounds.Width; // Độ rộng của vùng in có thể in được
                float rightMargin = leftMargin + printableWidth - 100;
                float topMargin = ev.MarginBounds.Top;
                string line = null;
                Font printFont = new Font("Arial", 12);
                SolidBrush myBrush = new SolidBrush(Color.Black);

                // Lấy các thông tin từ form
                string locationGroupName = lb_LocationGroupName.Text;
                string locationDetailName = lb_LocationDetailName.Text;
                string currentDateFormatted = currentDate.ToString("dd/MM/yyyy HH:mm:ss");
                string transNum = txtTransNum.Text;
                string createdDate = dtpCreatedDate.Value.ToString("dd/MM/yyyy");
                string playerName = txtPlayerName.Text;
                string description = txtDescription.Text;

                // Vẽ thông tin lên trang
                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString(locationGroupName, printFont, myBrush, leftMargin, yPos, new StringFormat());
                count += 2; // Cách 2 dòng

                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString(locationDetailName, printFont, myBrush, leftMargin, yPos, new StringFormat());
                count += 2; // Cách 2 dòng

                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString("BIÊN BẢN THU HỒI VOUCHER", printFont, myBrush, leftMargin + (ev.MarginBounds.Width - ev.Graphics.MeasureString("Bên Bản Thu thôi Voucher", printFont).Width) / 2, yPos, new StringFormat());
                count += 1; // Cách 1 dòng

                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString(currentDateFormatted, printFont, myBrush, leftMargin + (ev.MarginBounds.Width - ev.Graphics.MeasureString(currentDateFormatted, printFont).Width) / 2, yPos, new StringFormat());
                count += 2; // Cách 2 dòng

                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString("Mã Voucher :", printFont, myBrush, leftMargin, yPos, new StringFormat());
                count += 1; // Cách 1 dòng

                // Lặp qua từng hàng trong dataGridView1 để in mã voucher
                foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                {
                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                    // Thêm lề bằng cách thêm khoảng trống vào đầu dòng
                    string indentedText = "    " + dgvRow.Cells["voucher_serial"].Value.ToString();
                    ev.Graphics.DrawString(indentedText, printFont, myBrush, leftMargin, yPos, new StringFormat());
                    count += 1; // Cách 1 dòng
                }

                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString("Số giao dịch: " + transNum, printFont, myBrush, leftMargin, yPos, new StringFormat());
                count += 1; // Cách 1 dòng

                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString("Ngày thu hồi: " + createdDate, printFont, myBrush, leftMargin, yPos, new StringFormat());
                count += 1; // Cách 1 dòng

                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString("Khách hàng: " + playerName, printFont, myBrush, leftMargin, yPos, new StringFormat());
                count += 1; // Cách 1 dòng

                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString("Diễn giải: " + description, printFont, myBrush, leftMargin, yPos, new StringFormat());
                count += 1; // Cách 1 dòng

                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                string indentedText1 = "Người Lập" + "               " + "              ";
                ev.Graphics.DrawString(indentedText1, printFont, myBrush, rightMargin, yPos, new StringFormat());
                count += 1; // Cách 1 dòng
                ev.HasMorePages = false; // Chỉ in một trang
            };

            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;

            // Hiển thị print preview
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += (s, ev) =>
            {
                // Thiết lập font và độ rộng của trang
                float yPos = 0;
                int count = 0;
                float leftMargin = ev.MarginBounds.Left; //- 20; // 1 cm ~ 40 points
                                                              //float rightMargin = ev.MarginBounds.Right; // 1 cm ~ 40 points
                float printableWidth = ev.MarginBounds.Width; // Độ rộng của vùng in có thể in được
                float rightMargin = leftMargin + printableWidth - 100;
                float topMargin = ev.MarginBounds.Top;
                float cellHeight = 25; // Chiều cao mỗi ô
                Font printFont = new Font("Arial", 11);
                SolidBrush myBrush = new SolidBrush(Color.Black);
                Pen blackPen = new Pen(Color.Black);

                // Đặt chiều rộng các cột theo số ký tự yêu cầu
                float sttWidth = 10 * 10;
                float transNumWidth = 18 * 10; // 18 ký tự * 10 điểm (mỗi ký tự ước tính khoảng 10 điểm)
                float voucherWidth = 20 * 10; // 14 ký tự * 10 điểm
                float dateWidth = 11 * 10; // 11 ký tự * 10 điểm
                float customerWidth = 25 * 10; // 30 ký tự * 10 điểm
                float Menh_giaWidth = 25 * 10;

                // Lấy các thông tin từ form lb_LocationGroupName
                string locationGroupName = lb_LocationGroupName.Text.Length > 50 ? lb_LocationGroupName.Text.Substring(0, 50) : lb_LocationGroupName.Text;
                //playerName.Length > 50 ? playerName.Substring(0, 50) : playerName;  lb_LocationDetailName
                string locationDetailName = lb_LocationDetailName.Text.Length > 50 ? lb_LocationDetailName.Text.Substring(0, 50) : lb_LocationDetailName.Text;
                string currentDateFormatted = currentDate.ToString("dd/MM/yyyy HH:mm:ss");
                string transNum = txtTransNum.Text;
                string createdDate = dtpCreatedDate.Value.ToString("dd/MM/yyyy");
                string playerName = txtPlayerName.Text;
                string description = txtDescription.Text;

                // Vẽ thông tin lên trang
                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString(locationGroupName, printFont, myBrush, leftMargin, yPos, new StringFormat());
                count += 2; // Cách 2 dòng

                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString(locationDetailName, printFont, myBrush, leftMargin, yPos, new StringFormat());
                count += 2; // Cách 2 dòng

                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString("BIÊN BẢN THU HỒI VOUCHER", printFont, myBrush, leftMargin + (ev.MarginBounds.Width - ev.Graphics.MeasureString("BIÊN BẢN THU HỒI VOUCHER", printFont).Width) / 2, yPos, new StringFormat());
                count += 1; // Cách 1 dòng

                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString(currentDateFormatted, printFont, myBrush, leftMargin + (ev.MarginBounds.Width - ev.Graphics.MeasureString(currentDateFormatted, printFont).Width) / 2, yPos, new StringFormat());

                //ev.Graphics.DrawString("Số giao dịch", printFont, myBrush, new RectangleF(leftMargin, yPos, transNumWidth, cellHeight), centerFormat);
                // Vẽ phần "Diễn giải" dưới bảng
                count += 2; // Cách 1 dòng

                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString($"Số giao dịch :  {transNum}     -       Ngày GD : {createdDate}", printFont, myBrush, leftMargin, yPos, new StringFormat());

                count += 1; // Cách 1 dòng

                // Giả sử bạn muốn chỉ lấy 50 ký tự của biến playerName
                string truncatedPlayerName = playerName.Length > 50 ? playerName.Substring(0, 50) : playerName;
                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                //ev.Graphics.DrawString($"Khách hàng :  {playerName}", printFont, myBrush, leftMargin, yPos, new StringFormat());
                ev.Graphics.DrawString($"Khách hàng :  {truncatedPlayerName}", printFont, myBrush, leftMargin, yPos, new StringFormat());

                // Tạo header cho bảng
                yPos = topMargin + count * cellHeight;

                // Sử dụng StringFormat để căn giữa
                StringFormat centerFormat = new StringFormat();
                centerFormat.Alignment = StringAlignment.Center;
                centerFormat.LineAlignment = StringAlignment.Center;

                ev.Graphics.DrawString("STT", printFont, myBrush, new RectangleF(leftMargin, yPos, sttWidth, cellHeight), centerFormat);
                ev.Graphics.DrawString("Mã Voucher", printFont, myBrush, new RectangleF(leftMargin + sttWidth, yPos, voucherWidth, cellHeight), centerFormat);
                ev.Graphics.DrawString("Mệnh giá", printFont, myBrush, new RectangleF(leftMargin + sttWidth + voucherWidth, yPos, Menh_giaWidth, cellHeight), centerFormat);
                //ev.Graphics.DrawString("Khách hàng", printFont, myBrush, new RectangleF(leftMargin + transNumWidth + voucherWidth + dateWidth, yPos, customerWidth, cellHeight), centerFormat);
                count += 1;

                // Vẽ các đường viền cho header
                ev.Graphics.DrawRectangle(blackPen, leftMargin, yPos, sttWidth, cellHeight);
                ev.Graphics.DrawRectangle(blackPen, leftMargin + sttWidth, yPos, voucherWidth, cellHeight);
                ev.Graphics.DrawRectangle(blackPen, leftMargin + sttWidth + voucherWidth, yPos, Menh_giaWidth, cellHeight);
                //ev.Graphics.DrawRectangle(blackPen, leftMargin + sttWidth + voucherWidth + Menh_giaWidth, yPos, customerWidth, cellHeight);



                // Lặp qua từng hàng trong dataGridView1 để in dữ liệu
                foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                {
                    yPos = topMargin + count * cellHeight;

                    // Căn giữa cột STT
                    ev.Graphics.DrawString(dgvRow.Cells["STT"].Value?.ToString(), printFont, myBrush, new RectangleF(leftMargin, yPos, sttWidth, cellHeight), centerFormat);
                    ev.Graphics.DrawString(dgvRow.Cells["Voucher_serial"].Value?.ToString(), printFont, myBrush, leftMargin + sttWidth+5, yPos, new StringFormat());
                    // Căn phải và định dạng số cho cột "Mệnh giá"
                    decimal menhGiaValue = 0;
                    if (dgvRow.Cells["menhgia"].Value != null && decimal.TryParse(dgvRow.Cells["menhgia"].Value.ToString(), out menhGiaValue))
                    {
                        ev.Graphics.DrawString(menhGiaValue.ToString("N0"), printFont, myBrush, new RectangleF(leftMargin + sttWidth + voucherWidth-10, yPos, Menh_giaWidth, cellHeight), new StringFormat { Alignment = StringAlignment.Far });                        
                    }
                    else
                    {
                        ev.Graphics.DrawString("", printFont, myBrush, new RectangleF(leftMargin + sttWidth + voucherWidth-10, yPos, Menh_giaWidth, cellHeight), new StringFormat { Alignment = StringAlignment.Far });
                    }

                    // Vẽ các đường viền cho dữ liệu
                    ev.Graphics.DrawRectangle(blackPen, leftMargin, yPos, sttWidth, cellHeight);
                    ev.Graphics.DrawRectangle(blackPen, leftMargin + sttWidth, yPos, voucherWidth, cellHeight);
                    ev.Graphics.DrawRectangle(blackPen, leftMargin + sttWidth + voucherWidth, yPos, Menh_giaWidth, cellHeight);

                    count += 1; // Cách 1 dòng
                }

                // Thêm dòng "Tổng cộng" vào cuối bảng
                yPos = topMargin + count * cellHeight;
                ev.Graphics.DrawString("Tổng cộng:", printFont, myBrush, leftMargin + sttWidth, yPos, new StringFormat { Alignment = StringAlignment.Far });
                //ev.Graphics.DrawString(totalMenhgia.ToString("N0"), printFont, myBrush, leftMargin + sttWidth + voucherWidth, yPos, new StringFormat { Alignment = StringAlignment.Far });
                ev.Graphics.DrawString(totalMenhgia.ToString("N0"), printFont, myBrush, new RectangleF(leftMargin + sttWidth + voucherWidth - 10, yPos, Menh_giaWidth, cellHeight), new StringFormat { Alignment = StringAlignment.Far });

                // Vẽ đường viền cho dòng "Tổng cộng"
                ev.Graphics.DrawRectangle(blackPen, leftMargin, yPos, sttWidth+voucherWidth, cellHeight);
                ev.Graphics.DrawRectangle(blackPen, leftMargin + sttWidth + voucherWidth, yPos, Menh_giaWidth, cellHeight);

                count += 1; // Cách 1 dòng

                count -= 1;
                // Vẽ phần "Diễn giải" dưới bảng
                yPos = topMargin + count * cellHeight + 2 * printFont.GetHeight(ev.Graphics);
                ev.Graphics.DrawString($"Diễn giải:  {description}", printFont, myBrush, leftMargin, yPos, new StringFormat());
                //yPos += printFont.GetHeight(ev.Graphics);
                //ev.Graphics.DrawString(description, printFont, myBrush, leftMargin, yPos, new StringFormat());

                count += 2; // Cách 1 dòng
                yPos = topMargin + count * cellHeight + 2 * printFont.GetHeight(ev.Graphics);
                string indentedText1 = "Người Lập" + "               " + "              ";
                ev.Graphics.DrawString(indentedText1, printFont, myBrush, rightMargin, yPos, new StringFormat());
                count += 1; // Cách 1 dòng
                ev.HasMorePages = false; // Chỉ in một trang
            };

            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();
        }

        private void phone_number_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép các ký tự số và các ký tự đặc biệt hợp lệ
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != '-' && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void phone_number_Leave(object sender, EventArgs e)
        {
            if (!IsValidPhoneNumber(phone_number.Text))
            {
                MessageBox.Show("Không đúng định dạng của Số điện thoại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                phone_number.Focus();
            }
        }
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Biểu thức chính quy kiểm tra số điện thoại
            string pattern = @"^(\+?\d{1,4}?[\s-]?)?\(?\d{1,4}?\)?[\s-]?\d{1,4}[\s-]?\d{1,9}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }
    }
}
