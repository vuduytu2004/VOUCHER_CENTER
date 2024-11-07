//using DocumentFormat.OpenXml.Math;
using Lib.Utils.Package;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VOUCHER_CENTER.DataAccess;
using static VOUCHER_CENTER.Main;

namespace VOUCHER_CENTER.Presentation
{
    public partial class fr_VoucherSync_Print : Form
    {
        private string connectionString = bientoancuc.connectionString;
        private decimal totalMenhgia = 0;
        //bool cap_nhat = false;
        //private DateTime _lastInputTime;
        //private const int ScanInputThreshold = 50; // Thời gian tính bằng milliseconds, có thể điều chỉnh
        private DateTime lastKeystroke = DateTime.MinValue;
        private List<char> barcode = new List<char>();
        private TimeSpan barcodeTimeSpan = TimeSpan.FromMilliseconds(70); // Adjust this time span as needed
        //dateTimePicker1_check.Focus();
        ////////////private void Form1_Load(object sender, EventArgs e)
        ////////////{
        ////////////    // Gọi phương thức Leave khi form load xong
        ////////////    dateTimePicker1_check_Leave(sender, e);  // Bạn có thể truyền null nếu không cần giá trị thực tế của sender và e

        ////////////    // Focus vào dataGridView2 sau khi gọi phương thức
        ////////////    dataGridView2.Focus();
        ////////////}

        public fr_VoucherSync_Print()
        {
            InitializeComponent();
            InitializeDataGridView();
            ApplyEnterKeyToAllControls(this);
            dtpCreatedDate.CustomFormat = "dd/MM/yyyy";
            //frdate.CustomFormat = "dd/MM/yyyy";
            //todate.CustomFormat = "dd/MM/yyyy";
            dtpCreatedDate.Value = DateTime.Now;
            dtpCreatedDate.MaxDate = DateTime.Now;
            LoadUser_Location();
            lb_LocationsGroup.Text = GlobalVariables.Locations_Group.ToString();
            lb_LocationsDetail.Text = GlobalVariables.Locations_Detail.ToString();
            dateTimePicker1_check.CustomFormat = "dd/MM/yyyy";
            //frdate.CustomFormat = "dd/MM/yyyy";
            //todate.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1_check.Value = DateTime.Now;
            dateTimePicker1_check.MaxDate = DateTime.Now;
            dateTimePicker1_check.Focus();
            //dataGridView2.Focus();
            ////////////this.Load += new EventHandler(Form1_Load);

        }
        private void InitializeDataGridView()
        {

            ////////// Thêm cột nút xóa
            ////////DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            ////////deleteButtonColumn.Name = "DeleteButton";
            ////////deleteButtonColumn.HeaderText = "Del";
            ////////deleteButtonColumn.Text = "Del";
            ////////deleteButtonColumn.UseColumnTextForButtonValue = true;
            ////////deleteButtonColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ////////dataGridView1.Columns.Add(deleteButtonColumn);

            ////////// Đăng ký sự kiện CellClick
            ////////dataGridView1.CellClick += dataGridView1_CellClick;

            //// Gọi phương thức Leave khi giá trị thay đổi
            //dateTimePicker1_check_Leave(sender, e);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có nhấn vào cột nút xóa không
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["DeleteButton"].Index)
            {
                // Xóa dòng tương ứng
                dataGridView1.Rows.RemoveAt(e.RowIndex);

                // Cập nhật lại cột STT
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells["STT"].Value = i + 1;
                }
                // Cập nhật hoặc thêm dòng "Tổng cộng"
                UpdateTotalRow();
            }
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
            try
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    return; // No data in dataGridView1
                }
                ////if (string.IsNullOrWhiteSpace(phone_number.Text) && dataGridView1.Rows.Count > 0)
                ////{
                ////    MessageBox.Show("Không được để trống Số điện thoại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ////    phone_number.Focus();
                ////    return;

                ////}
                ////DialogResult dialogResult = MessageBox.Show("Bạn có thực hiện thu hồi voucher không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                ////if (dialogResult == DialogResult.Cancel)
                ////{
                ////    return; // User canceled operation
                ////}

                //////////////using (SqlConnection connection = new SqlConnection(connectionString))
                //////////////{
                //////////////    connection.Open();

                //////foreach (DataGridViewRow row in dataGridView1.Rows)
                //////{
                //////    var voucherSerial = "C" + row.Cells["voucher_serial"].Value.ToString();
                //////    var result = GetVoucher(connection, voucherSerial);

                //////    if (result.Exists)
                //////    {
                //////        switch (result.VoucherCheckValue)
                //////        {
                //////            case "0":
                //////                MessageBox.Show($"Voucher: {voucherSerial.Substring(1)} không có. Vui lòng nhập giá trị khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //////                row.DefaultCellStyle.BackColor = Color.Red;
                //////                return;
                //////            case "1":
                //////                MessageBox.Show($"Voucher: {voucherSerial.Substring(1)} đã hết hạn sử dụng. Vui lòng nhập giá trị khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //////                row.DefaultCellStyle.BackColor = Color.Red;
                //////                return;
                //////            case "3":
                //////                MessageBox.Show($"Voucher Serial: {voucherSerial.Substring(1)}\r\nĐã sử dụng tại {result.HcrcLocationType} - {result.HcrcLocationName}.\r\nCập nhật lần cuối vào {result.HcrcLastUpdate}.",
                //////                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //////                row.DefaultCellStyle.BackColor = Color.Red;
                //////                return;
                //////            case "4":
                //////                MessageBox.Show($"Voucher Serial: {voucherSerial.Substring(1)}\r\nĐã sử dụng tại {result.VoucherSyncLocationGroupName} - {result.VoucherSyncLocationDetailName}.\r\nCập nhật lần cuối vào {result.VoucherSyncLastUpdate}.",
                //////                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //////                row.DefaultCellStyle.BackColor = Color.Red;
                //////                return;
                //////            case "5":
                //////                MessageBox.Show($"Voucher Serial: {voucherSerial.Substring(1)}\r\nChưa được Active", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //////                row.DefaultCellStyle.BackColor = Color.Red;
                //////                return;
                //////            case "6":
                //////                MessageBox.Show($"Voucher Serial: {voucherSerial.Substring(1)}\r\nĐã bị thu hồi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //////                row.DefaultCellStyle.BackColor = Color.Red;
                //////                return;
                //////        }
                //////    }
                //////}
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
                ////////////foreach (DataGridViewRow row in dataGridView1.Rows)
                ////////////{
                ////////////    // Nếu đây là hàng cuối cùng (Tổng cộng), thì bỏ qua
                ////////////    if (row.Index == dataGridView1.Rows.Count - 1) continue;

                ////////////    var voucherSerial = "C" + row.Cells["voucher_serial"].Value.ToString();
                ////////////    decimal VALUE_AMT = Convert.ToDecimal(row.Cells["menhgia"].Value);
                ////////////    InsertVoucherSync(connection, voucherSerial, UniqueID_Group, VALUE_AMT);
                ////////////}

                if (radioButton1.Checked == true)
                {
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
                        string phone_num = phone_number.Text;

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
                        ev.Graphics.DrawString($"Số giao dịch :  {transNum} -       Ngày GD : {createdDate}", printFont, myBrush, leftMargin, yPos, new StringFormat());

                        count += 1; // Cách 1 dòng

                        // Giả sử bạn muốn chỉ lấy 50 ký tự của biến playerName
                        string truncatedPlayerName = playerName.Length > 50 ? playerName.Substring(0, 50) : playerName;
                        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                        //ev.Graphics.DrawString($"Khách hàng :  {playerName}", printFont, myBrush, leftMargin, yPos, new StringFormat());
                        ev.Graphics.DrawString($"Khách hàng :  {truncatedPlayerName}    -       Số ĐT : {phone_num}", printFont, myBrush, leftMargin, yPos, new StringFormat());

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
                            ev.Graphics.DrawString(dgvRow.Cells["Voucher_serial"].Value?.ToString(), printFont, myBrush, leftMargin + sttWidth + 5, yPos, new StringFormat());
                            // Căn phải và định dạng số cho cột "Mệnh giá"
                            decimal menhGiaValue = 0;
                            if (dgvRow.Cells["menhgia"].Value != null && decimal.TryParse(dgvRow.Cells["menhgia"].Value.ToString(), out menhGiaValue))
                            {
                                ev.Graphics.DrawString(menhGiaValue.ToString("N0"), printFont, myBrush, new RectangleF(leftMargin + sttWidth + voucherWidth - 10, yPos, Menh_giaWidth, cellHeight), new StringFormat { Alignment = StringAlignment.Far });
                            }
                            else
                            {
                                ev.Graphics.DrawString("", printFont, myBrush, new RectangleF(leftMargin + sttWidth + voucherWidth - 10, yPos, Menh_giaWidth, cellHeight), new StringFormat { Alignment = StringAlignment.Far });
                            }

                            // Vẽ các đường viền cho dữ liệu
                            ev.Graphics.DrawRectangle(blackPen, leftMargin, yPos, sttWidth, cellHeight);
                            ev.Graphics.DrawRectangle(blackPen, leftMargin + sttWidth, yPos, voucherWidth, cellHeight);
                            ev.Graphics.DrawRectangle(blackPen, leftMargin + sttWidth + voucherWidth, yPos, Menh_giaWidth, cellHeight);

                            count += 1; // Cách 1 dòng
                        }

                        //// Thêm dòng "Tổng cộng" vào cuối bảng
                        //yPos = topMargin + count * cellHeight;
                        //ev.Graphics.DrawString("Tổng cộng:", printFont, myBrush, leftMargin + sttWidth, yPos, new StringFormat { Alignment = StringAlignment.Far });
                        ////ev.Graphics.DrawString(totalMenhgia.ToString("N0"), printFont, myBrush, leftMargin + sttWidth + voucherWidth, yPos, new StringFormat { Alignment = StringAlignment.Far });
                        //ev.Graphics.DrawString(totalMenhgia.ToString("N0"), printFont, myBrush, new RectangleF(leftMargin + sttWidth + voucherWidth - 10, yPos, Menh_giaWidth, cellHeight), new StringFormat { Alignment = StringAlignment.Far });

                        //// Vẽ đường viền cho dòng "Tổng cộng"
                        //ev.Graphics.DrawRectangle(blackPen, leftMargin, yPos, sttWidth + voucherWidth, cellHeight);
                        //ev.Graphics.DrawRectangle(blackPen, leftMargin + sttWidth + voucherWidth, yPos, Menh_giaWidth, cellHeight);

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
                    ((Form)printPreviewDialog).WindowState = FormWindowState.Maximized;
                    printPreviewDialog.Load += (s, ev) =>
                    {
                        // Thiết lập mức thu phóng của PrintPreviewControl bên trong PrintPreviewDialog
                        ((PrintPreviewDialog)s).PrintPreviewControl.Zoom = 1.0;
                    };
                    printPreviewDialog.ShowDialog();
                }
                if (radioButton2.Checked == true)
                {
                    // Hiển thị print preview
                    PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                    PrintDocument printDocument = new PrintDocument();

                    printDocument.PrintPage += (s, ev) =>
                    {
                        // Thiết lập font và độ rộng của trang
                        float yPos = 0;
                        int count = 0;
                        float leftMargin = ev.PageBounds.Left + 10;
                        float rightMargin = ev.PageBounds.Right + 10;
                        float printableWidth = 80 * 3.93701f; // 80mm to points
                        float topMargin = ev.PageBounds.Top + 10;
                        float cellHeight = 25; // Chiều cao mỗi ô
                        Font printFont = new Font("Arial", 9);
                        SolidBrush myBrush = new SolidBrush(Color.Black);
                        Pen blackPen = new Pen(Color.Black);

                        // Đặt chiều rộng các cột theo số ký tự yêu cầu
                        float sttWidth = 5 * 10;
                        float transNumWidth = 50;
                        float voucherWidth = 14 * 10;
                        float dateWidth = 40;
                        float customerWidth = 80;
                        float menhGiaWidth = 8 * 10;

                        // Lấy các thông tin từ form lb_LocationGroupName
                        string locationGroupName = lb_LocationGroupName.Text.Length > 50 ? lb_LocationGroupName.Text.Substring(0, 50) : lb_LocationGroupName.Text;
                        string locationDetailName = lb_LocationDetailName.Text.Length > 50 ? lb_LocationDetailName.Text.Substring(0, 50) : lb_LocationDetailName.Text;
                        string currentDateFormatted = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        string transNum = txtTransNum.Text;
                        string createdDate = dtpCreatedDate.Value.ToString("dd/MM/yyyy");
                        string playerName = txtPlayerName.Text;
                        string description = txtDescription.Text;
                        string phone_num = phone_number.Text;

                        // Vẽ thông tin lên trang
                        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                        ev.Graphics.DrawString(locationGroupName, printFont, myBrush, leftMargin, yPos, new StringFormat());
                        count += 2; // Cách 2 dòng

                        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                        ev.Graphics.DrawString(locationDetailName, printFont, myBrush, leftMargin, yPos, new StringFormat());
                        count += 2; // Cách 2 dòng

                        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                        ev.Graphics.DrawString("BIÊN BẢN THU HỒI VOUCHER", printFont, myBrush, leftMargin + (printableWidth - ev.Graphics.MeasureString("BIÊN BẢN THU HỒI VOUCHER", printFont).Width) / 2, yPos, new StringFormat());
                        count += 1; // Cách 1 dòng

                        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                        ev.Graphics.DrawString(currentDateFormatted, printFont, myBrush, leftMargin + (printableWidth - ev.Graphics.MeasureString(currentDateFormatted, printFont).Width) / 2, yPos, new StringFormat());
                        count += 2; // Cách 2 dòng

                        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                        ev.Graphics.DrawString($"Số giao dịch: {transNum}", printFont, myBrush, leftMargin, yPos, new StringFormat());
                        count += 1; // Cách 1 dòng
                        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                        ev.Graphics.DrawString($"Ngày GD: {createdDate}", printFont, myBrush, leftMargin, yPos, new StringFormat());
                        count += 1; // Cách 1 dòng

                        // Giả sử bạn muốn chỉ lấy 50 ký tự của biến playerName
                        string truncatedPlayerName = playerName.Length > 50 ? playerName.Substring(0, 50) : playerName;
                        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                        ev.Graphics.DrawString($"Khách hàng: {truncatedPlayerName}", printFont, myBrush, leftMargin, yPos, new StringFormat());
                        count += 1; // Cách 1 dòng
                        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
                        ev.Graphics.DrawString($"Số ĐT: {phone_num}", printFont, myBrush, leftMargin, yPos, new StringFormat());
                        //count += 2; // Cách 2 dòng

                        // Tạo header cho bảng
                        yPos = topMargin + (count - 2) * cellHeight;

                        // Sử dụng StringFormat để căn giữa
                        StringFormat centerFormat = new StringFormat();
                        centerFormat.Alignment = StringAlignment.Center;
                        centerFormat.LineAlignment = StringAlignment.Center;

                        ev.Graphics.DrawString("STT", printFont, myBrush, new RectangleF(leftMargin, yPos, sttWidth, cellHeight), centerFormat);
                        ev.Graphics.DrawString("Mã Voucher", printFont, myBrush, new RectangleF(leftMargin + sttWidth, yPos, voucherWidth, cellHeight), centerFormat);
                        ev.Graphics.DrawString("Mệnh giá", printFont, myBrush, new RectangleF(leftMargin + sttWidth + voucherWidth, yPos, menhGiaWidth, cellHeight), centerFormat);
                        //count += 1;

                        // Vẽ các đường viền cho header
                        ev.Graphics.DrawRectangle(blackPen, leftMargin, yPos, sttWidth, cellHeight);
                        ev.Graphics.DrawRectangle(blackPen, leftMargin + sttWidth, yPos, voucherWidth, cellHeight);
                        ev.Graphics.DrawRectangle(blackPen, leftMargin + sttWidth + voucherWidth, yPos, menhGiaWidth, cellHeight);

                        // Lặp qua từng hàng trong dataGridView1 để in dữ liệu
                        foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
                        {
                            yPos = topMargin + (count - 1) * cellHeight;

                            // Căn giữa cột STT
                            ev.Graphics.DrawString(dgvRow.Cells["STT"].Value?.ToString(), printFont, myBrush, new RectangleF(leftMargin, yPos, sttWidth, cellHeight), centerFormat);
                            ev.Graphics.DrawString(dgvRow.Cells["Voucher_serial"].Value?.ToString(), printFont, myBrush, leftMargin + sttWidth + 5, yPos, new StringFormat());

                            // Căn phải và định dạng số cho cột "Mệnh giá"
                            decimal menhGiaValue = 0;
                            if (dgvRow.Cells["menhgia"].Value != null && decimal.TryParse(dgvRow.Cells["menhgia"].Value.ToString(), out menhGiaValue))
                            {
                                ev.Graphics.DrawString(menhGiaValue.ToString("N0"), printFont, myBrush, new RectangleF(leftMargin + sttWidth + voucherWidth - 10, yPos, menhGiaWidth, cellHeight), new StringFormat { Alignment = StringAlignment.Far });
                            }
                            else
                            {
                                ev.Graphics.DrawString("", printFont, myBrush, new RectangleF(leftMargin + sttWidth + voucherWidth - 10, yPos, menhGiaWidth, cellHeight), new StringFormat { Alignment = StringAlignment.Far });
                            }

                            // Vẽ các đường viền cho dữ liệu
                            ev.Graphics.DrawRectangle(blackPen, leftMargin, yPos, sttWidth, cellHeight);
                            ev.Graphics.DrawRectangle(blackPen, leftMargin + sttWidth, yPos, voucherWidth, cellHeight);
                            ev.Graphics.DrawRectangle(blackPen, leftMargin + sttWidth + voucherWidth, yPos, menhGiaWidth, cellHeight);

                            count += 1; // Cách 1 dòng
                        }

                        // Vẽ phần "Diễn giải" dưới bảng
                        yPos = topMargin + (count - 2) * cellHeight + 2 * printFont.GetHeight(ev.Graphics);
                        ev.Graphics.DrawString($"Diễn giải: {description}", printFont, myBrush, leftMargin, yPos, new StringFormat());
                        //count += 2; // Cách 2 dòng

                        // Vẽ phần "Người Lập"
                        yPos = topMargin + count * cellHeight + 2 * printFont.GetHeight(ev.Graphics);
                        string indentedText1 = "Người Lập";
                        ev.Graphics.DrawString(indentedText1, printFont, myBrush, leftMargin, yPos, new StringFormat());
                        //ev.Graphics.DrawString(indentedText1, printFont, myBrush, rightMargin - ev.Graphics.MeasureString(indentedText1, printFont).Width, yPos, new StringFormat());

                        ev.HasMorePages = false; // Chỉ in một trang
                    };

                    printPreviewDialog.Document = printDocument;
                    ((Form)printPreviewDialog).WindowState = FormWindowState.Maximized;
                    printPreviewDialog.Load += (s, ev) =>
                    {
                        // Thiết lập mức thu phóng của PrintPreviewControl bên trong PrintPreviewDialog
                        ((PrintPreviewDialog)s).PrintPreviewControl.Zoom = 1.0;
                    };
                    printPreviewDialog.ShowDialog();
                }

                //////////////}

                //ClearInputFields();
                //dataGridView1.Rows.Clear();
                MessageBox.Show("Record saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        //private void btnSave_Click1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (dataGridView1.Rows.Count == 0)
        //        {
        //            return; // No data in dataGridView1
        //        }

        //        DialogResult dialogResult = MessageBox.Show("Bạn có thực hiện thu hồi voucher không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        //        if (dialogResult == DialogResult.Cancel)
        //        {
        //            return; // User canceled operation
        //        }

        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            foreach (DataGridViewRow row in dataGridView1.Rows)
        //            {
        //                var voucherSerial = row.Cells["voucher_serial"].Value.ToString();
        //                var result = GetVoucher(connection, voucherSerial);

        //                if (result.Exists)
        //                {
        //                    switch (result.VoucherCheckValue)
        //                    {
        //                        case "0":
        //                            MessageBox.Show($"Voucher: {voucherSerial} không có. Vui lòng nhập giá trị khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                            row.DefaultCellStyle.BackColor = Color.Red;
        //                            return;
        //                        case "1":
        //                            MessageBox.Show($"Voucher: {voucherSerial} đã hết hạn sử dụng. Vui lòng nhập giá trị khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                            row.DefaultCellStyle.BackColor = Color.Red;
        //                            return;
        //                        case "3":
        //                            MessageBox.Show($"Voucher Serial: {voucherSerial}\r\nĐã sử dụng tại {result.HcrcLocationType} - {result.HcrcLocationName}.\r\nCập nhật lần cuối vào {result.HcrcLastUpdate}.",
        //                                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                            row.DefaultCellStyle.BackColor = Color.Red;
        //                            return;
        //                        case "4":
        //                            MessageBox.Show($"Voucher Serial: {voucherSerial}\r\nĐã sử dụng tại {result.VoucherSyncLocationGroupName} - {result.VoucherSyncLocationDetailName}.\r\nCập nhật lần cuối vào {result.VoucherSyncLastUpdate}.",
        //                                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                            row.DefaultCellStyle.BackColor = Color.Red;
        //                            return;
        //                        case "5":
        //                            MessageBox.Show($"Voucher Serial: {voucherSerial}\r\nChưa được Active", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                            row.DefaultCellStyle.BackColor = Color.Red;
        //                            return;
        //                        case "6":
        //                            MessageBox.Show($"Voucher Serial: {voucherSerial}\r\nĐã bị thu hồi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                            row.DefaultCellStyle.BackColor = Color.Red;
        //                            return;
        //                    }
        //                }
        //            }
        //            // Tạo chuỗi UniqueID_Group tương đương với đoạn code SQL
        //            string UniqueID_Group;
        //            // Lấy ngày giờ hiện tại
        //            DateTime currentDate = DateTime.Now;
        //            // Format ngày giờ hiện tại thành chuỗi theo định dạng yyyymmddHHMMss
        //            string dateString = currentDate.ToString("yyyyMMddHHmmss");
        //            // Tạo chuỗi ngẫu nhiên với độ dài là 5 ký tự
        //            Random random = new Random();
        //            string randomString = random.Next(100000).ToString("D5");
        //            // Kết hợp các chuỗi lại với nhau để tạo UniqueID_Group
        //            UniqueID_Group = GlobalVariables.User_Name + dateString + randomString;
        //            foreach (DataGridViewRow row in dataGridView1.Rows)
        //            {
        //                var voucherSerial = row.Cells["voucher_serial"].Value.ToString();
        //                InsertVoucherSync(connection, voucherSerial, UniqueID_Group);
        //            }


        //            // Hiển thị print preview
        //            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
        //            PrintDocument printDocument = new PrintDocument();

        //            printDocument.PrintPage += (s, ev) =>
        //            {
        //                // Thiết lập font và độ rộng của trang
        //                float linesPerPage = 0;
        //                float yPos = 0;
        //                int count = 0;
        //                float leftMargin = ev.MarginBounds.Left;
        //                //float rightMargin = ev.MarginBounds.Right;
        //                float printableWidth = ev.MarginBounds.Width; // Độ rộng của vùng in có thể in được
        //                float rightMargin = leftMargin + printableWidth - 100;
        //                float topMargin = ev.MarginBounds.Top;
        //                string line = null;
        //                Font printFont = new Font("Arial", 12);
        //                SolidBrush myBrush = new SolidBrush(Color.Black);

        //                // Lấy các thông tin từ form
        //                string locationGroupName = lb_LocationGroupName.Text;
        //                string locationDetailName = lb_LocationDetailName.Text;
        //                string currentDateFormatted = currentDate.ToString("dd/MM/yyyy HH:mm:ss");
        //                string transNum = txtTransNum.Text;
        //                string createdDate = dtpCreatedDate.Value.ToString("dd/MM/yyyy");
        //                string playerName = txtPlayerName.Text;
        //                string description = txtDescription.Text;

        //                // Vẽ thông tin lên trang
        //                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
        //                ev.Graphics.DrawString(locationGroupName, printFont, myBrush, leftMargin, yPos, new StringFormat());
        //                count += 2; // Cách 2 dòng

        //                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
        //                ev.Graphics.DrawString(locationDetailName, printFont, myBrush, leftMargin, yPos, new StringFormat());
        //                count += 2; // Cách 2 dòng

        //                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
        //                ev.Graphics.DrawString("BIÊN BẢN THU HỒI VOUCHER", printFont, myBrush, leftMargin + (ev.MarginBounds.Width - ev.Graphics.MeasureString("Bên Bản Thu thôi Voucher", printFont).Width) / 2, yPos, new StringFormat());
        //                count += 1; // Cách 1 dòng

        //                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
        //                ev.Graphics.DrawString(currentDateFormatted, printFont, myBrush, leftMargin + (ev.MarginBounds.Width - ev.Graphics.MeasureString(currentDateFormatted, printFont).Width) / 2, yPos, new StringFormat());
        //                count += 2; // Cách 2 dòng

        //                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
        //                ev.Graphics.DrawString("Mã Voucher :", printFont, myBrush, leftMargin, yPos, new StringFormat());
        //                count += 1; // Cách 1 dòng

        //                // Lặp qua từng hàng trong dataGridView1 để in mã voucher
        //                foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
        //                {
        //                    yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
        //                    // Thêm lề bằng cách thêm khoảng trống vào đầu dòng
        //                    string indentedText = "    " + dgvRow.Cells["voucher_serial"].Value.ToString();
        //                    ev.Graphics.DrawString(indentedText, printFont, myBrush, leftMargin, yPos, new StringFormat());
        //                    count += 1; // Cách 1 dòng
        //                }

        //                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
        //                ev.Graphics.DrawString("Số giao dịch: " + transNum, printFont, myBrush, leftMargin, yPos, new StringFormat());
        //                count += 1; // Cách 1 dòng

        //                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
        //                ev.Graphics.DrawString("Ngày thu hồi: " + createdDate, printFont, myBrush, leftMargin, yPos, new StringFormat());
        //                count += 1; // Cách 1 dòng

        //                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
        //                ev.Graphics.DrawString("Khách hàng: " + playerName, printFont, myBrush, leftMargin, yPos, new StringFormat());
        //                count += 1; // Cách 1 dòng

        //                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
        //                ev.Graphics.DrawString("Diễn giải: " + description, printFont, myBrush, leftMargin, yPos, new StringFormat());
        //                count += 1; // Cách 1 dòng

        //                yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
        //                string indentedText1 = "Người Lập" + "               " + "              ";
        //                ev.Graphics.DrawString(indentedText1, printFont, myBrush, rightMargin, yPos, new StringFormat());
        //                count += 1; // Cách 1 dòng
        //                ev.HasMorePages = false; // Chỉ in một trang
        //            };

        //            printPreviewDialog.Document = printDocument;
        //            printPreviewDialog.ShowDialog();


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

        private void InsertVoucherSync(SqlConnection connection, string voucherSerial, string UniqueID_Group, decimal VALUE_AMT)
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
                command.Parameters.AddWithValue("@VALUE_AMT", VALUE_AMT);
                command.Parameters.AddWithValue("@phone_number", phone_number.Text);
                //command.Parameters.AddWithValue("@Last_update", DateTime.Now);
                //command.Parameters.AddWithValue("@Sync", "N");
                //command.Parameters.AddWithValue("@Sync_update", DBNull.Value);

                command.ExecuteNonQuery();
            }
        }
        //------------------------------------------------------




        //--------------------------------------------------------


        //-------------------------------------------------------------------------------------------------------------
        //private void txtVoucherSerial_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    // Chặn tất cả các phím gõ từ bàn phím
        //    //if (GlobalVariables.User_Name.ToUpper() != "TEST")
        //    //{
        //    //e.Handled = true;
        //    //}
        //    // Kiểm tra xem người dùng có đang nhập từ bàn phím không
        //    if (!IsScannerInput(e.KeyChar) && GlobalVariables.User_Name.ToUpper() != "TEST")
        //    {
        //        e.Handled = true; // Ngăn chặn nhập liệu từ bàn phím
        //    }
        //}
        //private bool IsScannerInput(char keyChar)
        //{
        //    // Thực hiện kiểm tra xem liệu ký tự đầu vào có đến từ máy quét hay không
        //    // Đây là một ví dụ đơn giản, bạn có thể điều chỉnh theo yêu cầu của mình
        //    return char.IsDigit(keyChar) || char.IsLetter(keyChar);
        //}
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
        //-----------------------------------------------------------------------------------------------------------------------
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
        //private void txtVoucherSerial_Leave(object sender, EventArgs e)

        //{
        //    try
        //    {
        //        // Kiểm tra xem txtVoucherSerial có dữ liệu không
        //        if (string.IsNullOrWhiteSpace(txtVoucherSerial.Text))
        //        {
        //            return; // Nếu không có dữ liệu, thoát khỏi phương thức
        //        }
        //        // Kiểm tra xem ký tự đầu tiên của txtVoucherSerial có phải là "C" không
        //        if (txtVoucherSerial.Text[0] != 'C')
        //        {
        //            MessageBox.Show($"Không có dữ liệu của Voucher : {txtVoucherSerial.Text}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            txtVoucherSerial.Focus();
        //            return; // Nếu ký tự đầu tiên không phải là "C", thoát khỏi phương thức
        //        }

        //        // Kiểm tra xem txtVoucherSerial.Text đã có trong cột VoucherSerial chưa
        //        bool exists_vc = dataGridView1.Rows.Cast<DataGridViewRow>()
        //            .Any(row => row.Cells["Voucher_Serial"].Value != null && row.Cells["Voucher_Serial"].Value.ToString() == txtVoucherSerial.Text.Substring(1));

        //        if (exists_vc)
        //        {
        //            MessageBox.Show($"Voucher : {txtVoucherSerial.Text.Substring(1)} đã nhập rồi. \r\nVui lòng nhập giá trị khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            txtVoucherSerial.Focus();
        //            txtVoucherSerial.Clear();
        //            return;
        //        }

        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            var result = GetVoucher(connection, txtVoucherSerial.Text);
        //            txtVoucherSerial.Text = txtVoucherSerial.Text.Substring(1);
        //            // Kiểm tra xem có dữ liệu trả về từ stored procedure hay không
        //            if (!result.Exists)
        //            {
        //                MessageBox.Show($"Không có dữ liệu cho Voucher Serial: {txtVoucherSerial.Text}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                txtVoucherSerial.Clear();
        //                txtVoucherSerial.Focus();
        //                return;
        //            }

        //            // Kiểm tra các trường hợp của VoucherCheckValue
        //            switch (result.VoucherCheckValue)
        //            {
        //                case "0":
        //                    MessageBox.Show($"Voucher : {txtVoucherSerial.Text} không có. \r\nVui lòng nhập giá trị khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    txtVoucherSerial.Focus();
        //                    txtVoucherSerial.Clear();
        //                    return;
        //                case "1":
        //                    MessageBox.Show($"Voucher : {txtVoucherSerial.Text} đã hết hạn sử dụng. \r\nVui lòng nhập giá trị khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    txtVoucherSerial.Focus();
        //                    txtVoucherSerial.Clear();
        //                    return;
        //                case "3" when GlobalVariables.UserID != result.user_id:
        //                    MessageBox.Show($"Voucher Serial: {txtVoucherSerial.Text}\r\nĐã sử dụng tại {result.HcrcLocationType} - {result.HcrcLocationName}.\r\nCập nhật lần cuối vào {result.HcrcLastUpdate}.",
        //                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    txtVoucherSerial.Clear();
        //                    txtVoucherSerial.Focus();
        //                    return;
        //                case "4":
        //                    MessageBox.Show($"Voucher Serial: {txtVoucherSerial.Text}\r\nĐã sử dụng tại {result.VoucherSyncLocationGroupName} - {result.VoucherSyncLocationDetailName}.\r\nCập nhật lần cuối vào {result.VoucherSyncLastUpdate}.",
        //                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    txtVoucherSerial.Clear();
        //                    txtVoucherSerial.Focus();
        //                    return;
        //                case "5":
        //                    MessageBox.Show($"Voucher Serial: {txtVoucherSerial.Text}\r\n Chưa được Active", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    txtVoucherSerial.Focus();
        //                    txtVoucherSerial.Clear();
        //                    return;
        //                case "6":
        //                    MessageBox.Show($"Voucher Serial: {txtVoucherSerial.Text}\r\n Đã bị khóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                    txtVoucherSerial.Focus();
        //                    txtVoucherSerial.Clear();
        //                    return;

        //            }
        //            // Kiểm tra xem txtVoucherSerial.Text đã có trong cột VoucherSerial chưa
        //            bool exists = dataGridView1.Rows.Cast<DataGridViewRow>()
        //                .Any(row => row.Cells["Voucher_Serial"].Value != null && row.Cells["Voucher_Serial"].Value.ToString() == txtVoucherSerial.Text);

        //            if (!exists)
        //            {
        //                // Thêm txtVoucherSerial.Text vào dataGridView1
        //                int newRowIndex = dataGridView1.Rows.Add();
        //                DataGridViewRow newRow = dataGridView1.Rows[newRowIndex];
        //                //newRow.Cells["STT"].Value = newRowIndex + 1; // Tăng STT từ 1
        //                //// Kiểm tra nếu dataGridView1 chưa có bản ghi nào
        //                //if (dataGridView1.Rows.Count == 1)
        //                //{
        //                //    newRow.Cells["STT"].Value = 1; // Tăng STT từ 1 nếu chưa có bản ghi nào
        //                //}
        //                //else
        //                //{
        //                //    newRow.Cells["STT"].Value = newRowIndex; // Giữ nguyên giá trị STT nếu đã có bản ghi
        //                //}
        //                newRow.Cells["Voucher_Serial"].Value = txtVoucherSerial.Text;
        //                newRow.Cells["menhgia"].Value = result.valueAmt;

        //                // Xóa dòng "Tổng cộng" nếu tồn tại
        //                var totalRow = dataGridView1.Rows.Cast<DataGridViewRow>()
        //                    .FirstOrDefault(row => row.Cells["Voucher_Serial"].Value != null && row.Cells["Voucher_Serial"].Value.ToString() == "Tổng cộng:");

        //                if (totalRow != null)
        //                {
        //                    dataGridView1.Rows.Remove(totalRow);
        //                }

        //                // Cập nhật lại cột STT
        //                for (int i = 0; i < dataGridView1.Rows.Count; i++)
        //                {
        //                    dataGridView1.Rows[i].Cells["STT"].Value = i + 1;
        //                }
        //                // Tính tổng của cột menhgia
        //                //decimal totalMenhgia = CalculateTotalMenhgia();

        //                // Cập nhật hoặc thêm dòng "Tổng cộng"
        //                UpdateTotalRow();
        //            }
        //            txtVoucherSerial.Clear();
        //            txtVoucherSerial.Focus();
        //            //// Thêm txtVoucherSerial.Text vào listView1
        //            //ListViewItem item = new ListViewItem(txtVoucherSerial.Text);
        //            //listView1.Items.Add(item);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error checking Voucher serial: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        txtVoucherSerial.Focus();
        //    }
        //}
        private decimal CalculateTotalMenhgia()
        {
            decimal totalMenhgia = dataGridView1.Rows.Cast<DataGridViewRow>()
                .Where(row => row.Cells["menhgia"].Value != null)
                .Sum(row => Convert.ToDecimal(row.Cells["menhgia"].Value));

            return totalMenhgia;
        }

        private void UpdateTotalRow()
        {
            // Xóa dòng "Tổng cộng" nếu tồn tại
            var totalRow = dataGridView1.Rows.Cast<DataGridViewRow>()
                .FirstOrDefault(row => row.Cells["Voucher_Serial"].Value != null && row.Cells["Voucher_Serial"].Value.ToString() == "Tổng cộng:");

            if (totalRow != null)
            {
                dataGridView1.Rows.Remove(totalRow);
            }

            totalMenhgia = dataGridView1.Rows.Cast<DataGridViewRow>()
                .Where(row => row.Cells["menhgia"].Value != null)
                .Sum(row => Convert.ToDecimal(row.Cells["menhgia"].Value));
            if (totalMenhgia > 0)
            {
                // Thêm dòng "Tổng cộng" mới ở cuối bảng
                int totalRowIndex = dataGridView1.Rows.Add();
                totalRow = dataGridView1.Rows[totalRowIndex];
                totalRow.Cells["Voucher_Serial"].Value = "Tổng cộng:";
                totalRow.Cells["menhgia"].Value = totalMenhgia;

                // Đặt màu nền của dòng "Tổng cộng"
                totalRow.DefaultCellStyle.BackColor = Color.LightGray; // Đổi thành màu xám

                //// Sử dụng sự kiện CellFormatting để ẩn nút "Del" cho dòng "Tổng cộng"
                //dataGridView1.CellFormatting += (sender, e) =>
                //{
                //    if (e.RowIndex == totalRowIndex && e.ColumnIndex == dataGridView1.Columns["DeleteButton"].Index)
                //    {
                //        e.Value = null; // Ẩn nút "Del"
                //    }
                //    else
                //    {
                //        // Hiện nút "Del"
                //        if (e.ColumnIndex == dataGridView1.Columns["DeleteButton"].Index)
                //        {
                //            e.Value = "Del";
                //        }
                //    }
                //};
            }
        }
        private (bool Exists, int user_id, string VoucherCheckValue, string VoucherSyncVoucherSerial, decimal valueAmt, string VoucherSyncLocationGroupName, string VoucherSyncLocationDetailName, DateTime VoucherSyncLastUpdate, string VoucherSyncComputerName, string VoucherSyncTransNum, DateTime VoucherSyncCreatedDate, string VoucherSyncPlayerName, string VoucherSyncDescription, string HcrcVoucherSerial, string HcrcLocationType, string HcrcLocationName, DateTime HcrcLastUpdate) GetVoucher(SqlConnection connection, string voucherSerial)
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
                        decimal valueAmt = reader.IsDBNull(reader.GetOrdinal("VALUE_AMT")) ? 0 : reader.GetDecimal(reader.GetOrdinal("VALUE_AMT"));

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

                        return (true, user_id, voucherCheckValue, voucherSyncVoucherSerial, valueAmt, voucherSyncLocationGroupName, voucherSyncLocationDetailName, voucherSyncLastUpdate, voucherSyncComputerName, voucherSyncTransNum, voucherSyncCreatedDate, voucherSyncPlayerName, voucherSyncDescription, hcrcVoucherSerial, hcrcLocationType, hcrcLocationName, hcrcLastUpdate);
                        //, cardId, activate, status, dueDate

                    }
                }
            }
            return (false, 0, null, null, 0, null, null, DateTime.MinValue, null, null, DateTime.MinValue, null, null, null, null, null, DateTime.MinValue);
            //, 0, 0, DateTime.MinValue
        }




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
            txtDescription.Clear();
            txtPlayerName.Clear();
            txtTransNum.Clear();
            phone_number.Clear();
        }


        private void clear_test_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(bientoancuc.connectionString))
            {
                connection.Open();

                string query = $"UPDATE dsmart12.dbo.PMCRDINF SET ACTIVATE = 1, STATUS = 1, DUE_DATE = GETDATE() + 10 WHERE CARD_ID IN ('C44000000001', 'C44000000003', 'C24000014172', 'C24000014174', 'C24000000007', 'C24000000009', 'C14000000010', 'C14000000006', 'C4000028022', 'C54000028023'); DELETE FROM voucher_SYNC WHERE Voucher_Code IN ('C44000000001', 'C44000000003', 'C24000014172', 'C24000014174', 'C24000000007', 'C24000000009', 'C14000000010', 'C14000000006', 'C54000028022', 'C54000028023');DELETE FROM hcrc_voucher.dbo.HCRC_SYNC WHERE Voucher_Code IN ('C44000000001', 'C44000000003', 'C24000014172', 'C24000014174', 'C24000000007', 'C24000000009', 'C14000000010', 'C14000000006', 'C54000028022', 'C54000028023');";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Dữ liệu đã được khởi tạo lại!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtVoucherSerial_TextChanged(object sender, EventArgs e)
        {

        }
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Biểu thức chính quy kiểm tra số điện thoại
            string pattern = @"^(\+?\d{1,4}?[\s-]?)?\(?\d{1,4}?\)?[\s-]?\d{1,4}[\s-]?\d{1,9}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }

        private void label1_Leave(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_check_Leave(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị ngày từ dateTimePicker
                //DateTime selectedDate = dateTimePicker1_check.Value.Date;

                // Xóa các dòng cũ trong dataGridView2
                dataGridView2.Rows.Clear();

                // Kết nối tới cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Câu lệnh SQL truy vấn với tham số Created_Date
                    string query = "SELECT DISTINCT [UniqueID_Group], [TRANS_NUM] FROM [VOUCHER_CENTER].[dbo].[VOUCHER_SYNC] WHERE CONVERT(DATE, Created_Date) = @Created_Date AND Cancelled_Date IS NULL and Locations_Group = @Locations_Group and Locations_Detail = @Locations_Detail";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số ngày vào câu lệnh SQL
                        //command.Parameters.AddWithValue("@Created_Date", selectedDate);
                        command.Parameters.AddWithValue("@Created_Date", dateTimePicker1_check.Value.ToString("yyyyMMdd"));
                        command.Parameters.AddWithValue("@Locations_Group", GlobalVariables.Locations_Group.ToString());
                        command.Parameters.AddWithValue("@Locations_Detail", GlobalVariables.Locations_Detail.ToString());
                        //lb_LocationGroupName
                        //    lb_LocationDetailName

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Duyệt qua các kết quả trả về
                            while (reader.Read())
                            {
                                // Thêm dòng mới vào dataGridView2
                                int newRowIndex = dataGridView2.Rows.Add();
                                DataGridViewRow newRow = dataGridView2.Rows[newRowIndex];

                                // Lấy giá trị của UniqueID_Group và TRANS_NUM từ kết quả truy vấn
                                newRow.Cells["UniqueID_Group"].Value = reader["UniqueID_Group"].ToString();
                                newRow.Cells["TRANS_NUM"].Value = reader["TRANS_NUM"] != DBNull.Value ? reader["TRANS_NUM"].ToString() : "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker1_check_ValueChanged(object sender, EventArgs e)
        {
            // Gọi phương thức Leave khi giá trị thay đổi
            dateTimePicker1_check_Leave(sender, e);
        }
        private void ProcessCellSelection(int rowIndex)
        {
            try
            {
                if (rowIndex >= 0 && rowIndex < dataGridView2.Rows.Count)
                {
                    var uniqueID = dataGridView2.Rows[rowIndex].Cells["UniqueID_Group"].Value;

                    if (uniqueID != null)
                    {
                        // Xóa dữ liệu hiện tại trong dataGridView1
                        dataGridView1.Rows.Clear();

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            // Câu lệnh SQL truy vấn với UniqueID_Group
                            string query = "SELECT Voucher_code, VALUE_AMT,Created_Date,TRANS_NUM,Player_Name,Phone_Number,Description FROM VOUCHER_SYNC WHERE UniqueID_Group = @UniqueID_Group";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                // Gán tham số UniqueID_Group
                                command.Parameters.AddWithValue("@UniqueID_Group", uniqueID);

                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    // Duyệt qua các kết quả trả về
                                    while (reader.Read())
                                    {
                                        int newRowIndex = dataGridView1.Rows.Add();
                                        DataGridViewRow newRow = dataGridView1.Rows[newRowIndex];
                                        newRow.Cells["STT"].Value = newRowIndex + 1; // Tăng STT từ 1
                                        newRow.Cells["Voucher_Serial"].Value = reader["Voucher_code"].ToString();
                                        newRow.Cells["menhgia"].Value = reader["VALUE_AMT"].ToDecimal();
                                        dtpCreatedDate.Value = Convert.ToDateTime(reader["Created_Date"]);
                                        txtTransNum.Text = reader["TRANS_NUM"].ToString();
                                        txtPlayerName.Text = reader["Player_Name"].ToString();
                                        phone_number.Text = reader["Phone_Number"].ToString();
                                        txtDescription.Text = reader["Description"].ToString();
                                    }
                                }
                            }
                        }
                        // Cập nhật hoặc thêm dòng "Tổng cộng"
                        UpdateTotalRow();
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProcessCellSelection(e.RowIndex);
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                ProcessCellSelection(dataGridView2.CurrentRow.Index);
            }
        }

        private void dataGridView2_CellContentClick_luu(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra xem hàng có chỉ số hợp lệ không
                if (e.RowIndex >= 0 && e.RowIndex < dataGridView2.Rows.Count)
                {
                    // Lấy giá trị của cột "UniqueID_Group" từ hàng được chọn
                    var selectedRow = dataGridView2.Rows[e.RowIndex];
                    string uniqueIDGroup = selectedRow.Cells["UniqueID_Group"].Value?.ToString();

                    // Kiểm tra nếu giá trị của UniqueID_Group không null hoặc trống
                    if (!string.IsNullOrEmpty(uniqueIDGroup))
                    {
                        // Xóa dữ liệu hiện tại trong dataGridView1
                        dataGridView1.Rows.Clear();

                        // Kết nối tới cơ sở dữ liệu
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            // Câu lệnh SQL truy vấn với tham số UniqueID_Group
                            string query = "SELECT Voucher_code, VALUE_AMT,Created_Date,TRANS_NUM,Player_Name,Phone_Number,Description FROM VOUCHER_SYNC WHERE UniqueID_Group = @UniqueID_Group";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                // Thêm tham số UniqueID_Group vào câu lệnh SQL                        
                                command.Parameters.AddWithValue("@UniqueID_Group", uniqueIDGroup);

                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    // Duyệt qua các kết quả trả về
                                    while (reader.Read())
                                    {
                                        int newRowIndex = dataGridView1.Rows.Add();
                                        DataGridViewRow newRow = dataGridView1.Rows[newRowIndex];
                                        newRow.Cells["STT"].Value = newRowIndex + 1; // Tăng STT từ 1
                                        newRow.Cells["Voucher_Serial"].Value = reader["Voucher_code"].ToString();

                                        // Kiểm tra giá trị VALUE_AMT và gán giá trị mặc định nếu null
                                        newRow.Cells["menhgia"].Value = reader["VALUE_AMT"] != DBNull.Value
                                            ? Convert.ToDecimal(reader["VALUE_AMT"])
                                            : 0;
                                        dtpCreatedDate.Value = Convert.ToDateTime(reader["Created_Date"]);
                                        txtTransNum.Text = reader["TRANS_NUM"].ToString();
                                        txtPlayerName.Text = reader["Player_Name"].ToString();
                                        phone_number.Text = reader["Phone_Number"].ToString();
                                        txtDescription.Text = reader["Description"].ToString();
                                    }
                                }
                            }
                            // Cập nhật hoặc thêm dòng "Tổng cộng"
                            UpdateTotalRow();
                        }
                    }
                    else
                    {
                        MessageBox.Show("UniqueID_Group không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

//if (radioButton2.Checked == true)
//{
//    // Hiển thị print preview
//    PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
//    PrintDocument printDocument = new PrintDocument();

//    printDocument.PrintPage += (s, ev) =>
//    {
//        // Thiết lập font và độ rộng của trang
//        float yPos = 0;
//        int count = 0;
//        float leftMargin = ev.PageBounds.Left + 10;
//        float topMargin = ev.PageBounds.Top + 10;
//        Font printFont = new Font("Arial", 9);
//        SolidBrush myBrush = new SolidBrush(Color.Black);

//        // Lấy các thông tin từ form lb_LocationGroupName
//        string locationGroupName = lb_LocationGroupName.Text.Length > 50 ? lb_LocationGroupName.Text.Substring(0, 50) : lb_LocationGroupName.Text;
//        string locationDetailName = lb_LocationDetailName.Text.Length > 50 ? lb_LocationDetailName.Text.Substring(0, 50) : lb_LocationDetailName.Text;
//        string currentDateFormatted = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
//        string transNum = txtTransNum.Text;
//        string createdDate = dtpCreatedDate.Value.ToString("dd/MM/yyyy");
//        string playerName = txtPlayerName.Text;
//        string description = txtDescription.Text;
//        string phone_num = phone_number.Text;

//        // Vẽ thông tin lên trang
//        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
//        ev.Graphics.DrawString(locationGroupName, printFont, myBrush, leftMargin, yPos, new StringFormat());
//        count += 1; // Cách 2 dòng

//        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
//        ev.Graphics.DrawString(locationDetailName, printFont, myBrush, leftMargin, yPos, new StringFormat());
//        count += 2; // Cách 2 dòng

//        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
//        ev.Graphics.DrawString("    BIÊN BẢN THU HỒI VOUCHER", printFont, myBrush, leftMargin, yPos, new StringFormat());
//        count += 1; // Cách 1 dòng

//        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
//        ev.Graphics.DrawString("    " + currentDateFormatted, printFont, myBrush, leftMargin, yPos, new StringFormat());
//        count += 2; // Cách 2 dòng

//        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
//        ev.Graphics.DrawString($"Số giao dịch: {transNum}", printFont, myBrush, leftMargin, yPos, new StringFormat());
//        count += 1; // Cách 1 dòng
//        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
//        ev.Graphics.DrawString($"   Ngày GD: {createdDate}", printFont, myBrush, leftMargin, yPos, new StringFormat());
//        count += 1; // Cách 1 dòng

//        // Giả sử bạn muốn chỉ lấy 50 ký tự của biến playerName
//        string truncatedPlayerName = playerName.Length > 50 ? playerName.Substring(0, 50) : playerName;
//        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
//        ev.Graphics.DrawString($"Khách hàng: {truncatedPlayerName}", printFont, myBrush, leftMargin, yPos, new StringFormat());
//        count += 1; // Cách 1 dòng                            
//        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
//        ev.Graphics.DrawString($"   Số ĐT: {phone_num}", printFont, myBrush, leftMargin, yPos, new StringFormat());
//        count += 2; // Cách 2 dòng
//        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
//        ev.Graphics.DrawString("TT-VC- Tien", printFont, myBrush, leftMargin, yPos, new StringFormat());
//        count += 1;
//        // Lặp qua từng hàng trong dataGridView1 để in dữ liệu
//        foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
//        {
//            yPos = topMargin + count * printFont.GetHeight(ev.Graphics);

//            ev.Graphics.DrawString(dgvRow.Cells["STT"].Value?.ToString(), printFont, myBrush, leftMargin, yPos, new StringFormat());
//            yPos += printFont.GetHeight(ev.Graphics);
//            ev.Graphics.DrawString(dgvRow.Cells["Voucher_serial"].Value?.ToString(), printFont, myBrush, leftMargin, yPos, new StringFormat());
//            yPos += printFont.GetHeight(ev.Graphics);
//            decimal menhGiaValue = 0;
//            if (dgvRow.Cells["menhgia"].Value != null && decimal.TryParse(dgvRow.Cells["menhgia"].Value.ToString(), out menhGiaValue))
//            {
//                ev.Graphics.DrawString(menhGiaValue.ToString("N0"), printFont, myBrush, leftMargin, yPos, new StringFormat());
//            }
//            else
//            {
//                ev.Graphics.DrawString("", printFont, myBrush, leftMargin, yPos, new StringFormat());
//            }

//            count += 3; // Cách 3 dòng để chuyển sang dòng mới
//        }

//        // Vẽ phần "Diễn giải" dưới bảng
//        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
//        ev.Graphics.DrawString($"Diễn giải: {description}", printFont, myBrush, leftMargin, yPos, new StringFormat());
//        count += 2; // Cách 2 dòng

//        // Vẽ phần "Người Lập"
//        yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
//        ev.Graphics.DrawString("Người Lập", printFont, myBrush, leftMargin, yPos, new StringFormat());

//        ev.HasMorePages = false; // Chỉ in một trang
//    };

//    printPreviewDialog.Document = printDocument;
//    ((Form)printPreviewDialog).WindowState = FormWindowState.Maximized;
//    printPreviewDialog.Load += (s, ev) =>
//    {
//        // Thiết lập mức thu phóng của PrintPreviewControl bên trong PrintPreviewDialog
//        ((PrintPreviewDialog)s).PrintPreviewControl.Zoom = 1.0;
//    };
//    printPreviewDialog.ShowDialog();
//}