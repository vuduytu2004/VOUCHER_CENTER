//using COMExcel = Microsoft.Office.Interop.Excel;
//using System.Threading.Tasks;
//using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
//using System.Collections;
//using Action = System.Action;

using OfficeOpenXml;
using Report_Center.DataAccess;
using System;
//using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
//using System.Drawing;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Threading;
//using Font = System.Drawing.Font;
using Application = System.Windows.Forms.Application;
using DataTable = System.Data.DataTable;

namespace Report_Center.Presentation
{
    public partial class fr_Zone_Prices_STK : Form
    {

        public fr_Zone_Prices_STK()
        {
            InitializeComponent();
            Shown += new EventHandler(fr_Zone_Prices_STK_Shown);
            // To report progress from the background worker we need to set this property
            backgroundWorker1.WorkerReportsProgress = true;
            // This event will be raised on the worker thread when the worker starts
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            //// This event will be raised when we call ReportProgress
            //backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            //this.dataGridView_full.ColumnWidthChanged += new
            //DataGridViewColumnEventHandler(dataGridView1_ColumnWidthChanged);
            //this.dataGridView_full.CellValueChanged += new
            //DataGridViewCellEventHandler(dataGridView1_CellValueChanged);


            //this.Ma_NCC.GotFocus += Ma_NCC_GotFocus;
            //this.Ma_NCC.Click += Ma_NCC_Click;

        }
        ConnectDB cn = new ConnectDB();

        //public object AutoCompleteSuggestMode { get; private set; }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                SendKeys.Send("{TAB}");
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                //MessageBox.Show("Đang load dữ liệu ...", "Chú Ý !");
                return;
            }
            try
            {
                fr_Zone_Prices_STK f3 = (fr_Zone_Prices_STK)Application.OpenForms["fr_Zone_Prices_STK"];
                f3.Close();
            }
            catch (NullReferenceException ne)
            {
                if (backgroundWorker1.IsBusy)
                {
                    //MessageBox.Show("Đang load dữ liệu ...", "Chú Ý !");
                    return;
                }
            }
        }
        void fr_Zone_Prices_STK_Shown(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            // Start the background worker
            backgroundWorker1.RunWorkerAsync();

        }
        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //button2.Enabled = false;
            //progressBar11.Visible = true;
            //progressBar11.Style = ProgressBarStyle.Marquee;
            DataTable table = new DataTable();
            string tim_sql;
            tim_sql = @"SELECT sql_str FROM var_sql_str where [form_name]= '" + this.Name.Trim() + "' AND [id]=1";
            string sql = cn.LayQuen(tim_sql).Trim();
            /// -----------------------------test//----------------------------------------------------------------------------------
            //            string sql = @"select  ROW_NUMBER() OVER (ORDER BY a.SKU_ID) AS [STT],a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định' ,(b.SUPP_NAME) as 'Tên NCC' 
            //                    ,a.SKU_ID as 'Mã Hàng', c.BARCODE                     ,c.SKU_CODE,(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT'
            //                    ,c.GRP_ID as 'Nhóm' , (c.grp_name) as 'Tên Nhóm'                     ,c.rtPRICE as 'Giá Bán', c.MDPRICE as 'Giá nội bộ'
            //                    , c.TAX_RATE as 'Thuế Bán', SPPRICE as 'Giá Nhập chỉ định'                    , PREFPR as 'Giá Vốn chỉ định' , iif( f.tax_rate is null ,'Not Set', CAST(f.tax_rate as varchar(10))) as 'Thuế Nhập'    --, iif(  LEN(ISNULL(a.tax_code,''))=0, 'Not Set',a.tax_code ) as 'Thuế Nhập'  --f.tax_code as 'Thuế Nhập'--
            //                    ,PCPR_CODE as 'Vùng Giá'                     ,c.STATUS as 'Trạng Thái'                    ,c.ITEM_TYPE as 'Loại hàng'
            //                    ,iif(e.OPEN_DATE IS NULL,'20180101',e.OPEN_DATE)  AS OPEN_DATE ,iif(e.MODI_DATE IS NULL,'20180101',e.MODI_DATE) AS MODI_DATE
            //,CU.ZONE_CODE AS 'Vùng giá bán', CU.RTPRICE AS 'Giá bán theo vùng'
            //,CU2.STK_ID AS 'Vùng Kho', CU2.WSPRICE AS 'Giá bán theo KHO'
            //					from  DSMART12.dbo.SPPRICE a WITH (NOLOCK)
            //                    left join  DSMART12.dbo.SUPPLIER as b WITH (NOLOCK) on a.supp_id=b.supp_id
            //                    left join  DSMART12.dbo.SKU_DEF as c WITH (NOLOCK) on a.SKU_ID=c.SKU_ID
            //					 left join  DSMART12.dbo.GOODS as e WITH (NOLOCK) on right(left(a.SKU_ID,8),6)=e.GOODS_ID
            //					 left join  DSMART12.dbo.TAX_TYPE as f WITH (NOLOCK) on  a.tax_code = f.tax_code
            //LEFT join  DSMART12.dbo.RTPRICE AS CU WITH (NOLOCK) ON CU.SKU_ID = a.SKU_ID
            //lEFT join  DSMART12.dbo.STKSPRICE AS CU2 WITH (NOLOCK) ON CU2.SKU_ID = a.SKU_ID
            //                    where c.status NOT IN ( '02','05') order by a.SKU_ID";

            table = cn.taobang2(sql);
            setDataSource(table);

        }

        internal delegate void SetDataSourceDelegate(DataTable table);
        private void setDataSource(DataTable table)
        {
            // Invoke method if required:
            if (this.InvokeRequired)
            {
                this.Invoke(new SetDataSourceDelegate(setDataSource), table);
            }
            else
            {
                dataGridView_full.DataSource = table;
                progressBar1.Visible = false;
                dataGridView_full.Columns[4].Frozen = true;
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void fr_Zone_Prices_STK_Load(object sender, EventArgs e)
        {


        }

        //----------------------------------------
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            sql = @"select count(*) from SPPRICE with(nolock) where sku_id in (select SKU_ID from SKU_DEF with(nolock) where status  NOT IN ( '02','05'))";
            //p_max = cn.Gan_max_progressbar(sql);
            //            sql = @"select a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định',dbo.tcvn2unicode(b.SUPP_NAME) as 'Tên NCC' ,a.SKU_ID as 'Mã Hàng',c.SKU_CODE,dbo.tcvn2unicode(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT', c.TAX_RATE , SPPRICE as 'Giá Nhập chỉ định', LASTIMPPR as 'Giá nhập lần trước'
            //,PCPR_CODE as 'Vùng Giá' from SPPRICE a
            //left join SUPPLIER as b on a.supp_id=b.supp_id
            //left join SKU_DEF as c on a.SKU_ID=c.SKU_ID
            //where a.sku_id in (select SKU_ID from SKU_DEF where status <> '02') order by a.SKU_ID";
            progressBar1.Maximum = cn.Gan_max_progressbar(sql);

            //Lấy số ngẫu nhiên
            Random _r = new Random();
            string n = _r.Next(1, 10000).ToString();

            //sql = @"select SUPP_ID as 'Mã NCC', SKU_ID as 'Mã Hàng', SPPRICE as 'Giá Nhập chỉ định', PCPR_CODE as 'Vùng Giá' from SPPRICE where sku_id in (select SKU_ID from SKU_DEF where status <> '02') order by SKU_ID";
            //select* from SPPRICE where sku_id in (select SKU_ID from SKU_DEF where status <> '02')
            sql = @"select  a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định' ,(b.SUPP_NAME) as 'Tên NCC' 
                    ,a.SKU_ID as 'Mã Hàng', c.BARCODE                     ,c.SKU_CODE,(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT'
                    ,c.GRP_ID as 'Nhóm' , (c.grp_name) as 'Tên Nhóm'                     ,c.rtPRICE as 'Giá Bán', c.MDPRICE as 'Giá nội bộ'
                    , c.TAX_RATE as 'Thuế Bán', SPPRICE as 'Giá Nhập chỉ định'                    , PREFPR as 'Giá Vốn chỉ định' , iif( f.tax_rate is null ,'Not Set', CAST(f.tax_rate as varchar(10))) as 'Thuế Nhập'    --, iif(  LEN(ISNULL(a.tax_code,''))=0, 'Not Set',a.tax_code ) as 'Thuế Nhập'  --f.tax_code as 'Thuế Nhập'--
                    ,PCPR_CODE as 'Vùng Giá'                     ,c.STATUS as 'Trạng Thái'                    ,c.ITEM_TYPE as 'Loại hàng'
                    ,e.OPEN_DATE ,e.MODI_DATE
,CU.ZONE_CODE AS 'Vùng giá bán', CU.RTPRICE AS 'Giá bán theo vùng'
,CU2.STK_ID AS 'Vùng Kho', CU2.WSPRICE AS 'Giá bán theo KHO'
					from  DSMART12.dbo.SPPRICE a WITH (NOLOCK)
                    left join  DSMART12.dbo.SUPPLIER as b WITH (NOLOCK) on a.supp_id=b.supp_id
                    left join  DSMART12.dbo.SKU_DEF as c WITH (NOLOCK) on a.SKU_ID=c.SKU_ID
					 left join  DSMART12.dbo.GOODS as e WITH (NOLOCK) on right(left(a.SKU_ID,8),6)=e.GOODS_ID
					 left join  DSMART12.dbo.TAX_TYPE as f WITH (NOLOCK) on  a.tax_code = f.tax_code
LEFT join  DSMART12.dbo.RTPRICE AS CU WITH (NOLOCK) ON CU.SKU_ID = a.SKU_ID
lEFT join  DSMART12.dbo.STKSPRICE AS CU2 WITH (NOLOCK) ON CU2.SKU_ID = a.SKU_ID
                    where c.status NOT IN ( '02','05') order by a.SKU_ID";


            //  Ngày tạo, loại hàng, trạng thái ----Giá bán,Barcode, Mã nhóm, tên nhóm, 


            SQLtoExcel(sql, "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx");
            System.Diagnostics.Process.Start("D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx");
        }

        private void SQLtoExcel(string query, string Output)
        {
            //Lấy số ngẫu nhiên
            Random _r = new Random();
            string n = _r.Next(1, 10000).ToString();

            string Filename = "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".csv";
            //SqlConnection conn = new SqlConnection("Server=192.168.1.18;Database=dsmart12;User Id=daisy;Password=hanoi");
            SqlConnection conn = cn.getcon1();
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(Filename, false, Encoding.UTF8))
            {
                // Loop through the fields and add headers
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    //Converter.TCVN3ToUnicode (dr.GetName(i));
                    string name = dr.GetName(i);
                    if (name.Contains(","))
                        name = "\"" + name + "\"";
                    fs.Write(name + ",");

                }
                fs.WriteLine();

                // Loop through the rows and output the data
                int dong = 0;
                while (dr.Read())
                {

                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string value = Converter.TCVN3ToUnicode(dr[i].ToString());

                        if (value.Contains(","))
                            value = "\"" + value + "\"";

                        fs.Write(value + ",");

                    }
                    fs.WriteLine();
                    progressBar1.Value = dong + 1;
                    dong++;
                }

                fs.Close();
            }

            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = app.Workbooks.Open(Filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //wb.TableStyles.

            wb.SaveAs(Output, XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wb.Close();
            app.Quit();
            File.Delete(Filename);
        }
        private void dataGridView_full_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //--------------------------------------------------------
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //string value =
                if (e.ColumnIndex == 1)
                {
                    Ma_NCC.Text = dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
                }
                if (e.ColumnIndex == 3)
                {
                    Ma_NCC.Text = Converter.TCVN3ToUnicode(dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());
                }
                if (e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6)
                {
                    Ma_hang.Text = dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
                }
                if (e.ColumnIndex == 9)
                {
                    Ma_nhom.Text = dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
                }
                if (e.ColumnIndex == 10)
                {
                    Ma_nhom.Text = Converter.TCVN3ToUnicode(dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());
                }
                if (e.ColumnIndex == 7)
                {
                    Ma_hang.Text = Converter.TCVN3ToUnicode(dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());
                }
                ////Lưu lại dòng dữ liệu vừa kích chọn
                //DataGridViewRow rowss = this.dataGridView_full.Rows[e.RowIndex];
                ////DataGridViewCell cell = this.dataGridView_full.CellClick();
                //DataGridViewColumn columnss =this.dataGridView_full.Columns[e.ColumnIndex];
                ////Đưa dữ liệu vào textbox
                //Ma_NCC.Text = rowss.Cells[columnss.ValueType].Value.ToString();
                //string value =
                //dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
                //txtHoVaTen.Text = row.Cells[1].Value.ToString();
                //txtQueQuan.Text = row.Cells[2].Value.ToString();

                //Không cho phép sửa trường STT
                //txtSTT.Enabled = false;
            }
        }
        //----------------------------------------------------------



        private void Ma_NCC_TextChanged(object sender, EventArgs e)
        {

        }

        private void tk_Full_Click(object sender, EventArgs e)
        {
            // Tạo cột STT, add cột vào tb trước khi đổ dữ liệu vào table >> STT cột đầu tiên
            //DataColumn Col = new DataColumn("STT", typeof(int));
            //dataGridView_full.Columns.Add(Col);

            //......DataGridView1.DataSource = dt;
            if (backgroundWorker1.IsBusy)
            {
                //MessageBox.Show("Đang load dữ liệu ...", "Chú Ý !");
                return;
            }


            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            string sql = @"select  a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định' ,(b.SUPP_NAME) as 'Tên NCC' 
                    ,a.SKU_ID as 'Mã Hàng', c.BARCODE                     ,c.SKU_CODE,(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT'
                    ,c.GRP_ID as 'Nhóm' , (c.grp_name) as 'Tên Nhóm'                     ,c.rtPRICE as 'Giá Bán', c.MDPRICE as 'Giá nội bộ'
                    , c.TAX_RATE as 'Thuế Bán', SPPRICE as 'Giá Nhập chỉ định'                    , PREFPR as 'Giá Vốn chỉ định' 
                    ,iif( f.tax_rate is null ,'Not Set', CAST(f.tax_rate as varchar(10))) as 'Thuế Nhập'    --, iif(  LEN(ISNULL(a.tax_code,''))=0, 'Not Set',a.tax_code ) as 'Thuế Nhập'  --f.tax_code as 'Thuế Nhập'--
                    ,PCPR_CODE as 'Vùng Giá'                     ,c.STATUS as 'Trạng Thái'                    ,c.ITEM_TYPE as 'Loại hàng'
                    ,e.OPEN_DATE ,e.MODI_DATE
,CU.ZONE_CODE AS 'Vùng giá bán', CU.RTPRICE AS 'Giá bán theo vùng'
,CU2.STK_ID AS 'Vùng Kho', CU2.WSPRICE AS 'Giá bán theo KHO'
					from  DSMART12.dbo.SPPRICE a WITH (NOLOCK)
                    left join  DSMART12.dbo.SUPPLIER as b WITH (NOLOCK) on a.supp_id=b.supp_id
                    left join  DSMART12.dbo.SKU_DEF as c WITH (NOLOCK) on a.SKU_ID=c.SKU_ID
					 left join  DSMART12.dbo.GOODS as e WITH (NOLOCK) on right(left(a.SKU_ID,8),6)=e.GOODS_ID
					 left join  DSMART12.dbo.TAX_TYPE as f WITH (NOLOCK) on  a.tax_code = f.tax_code
                    LEFT join  DSMART12.dbo.RTPRICE AS CU WITH (NOLOCK) ON CU.SKU_ID = a.SKU_ID
lEFT join  DSMART12.dbo.STKSPRICE AS CU2 WITH (NOLOCK) ON CU2.SKU_ID = a.SKU_ID
                    where c.status NOT IN ( '02','05') ";
            if ((Ma_NCC.Text == "") && (Ma_nhom.Text == "") && (Ma_hang.Text == ""))
            {
                progressBar1.Visible = false;
                return;
            }
            if ((Ma_NCC.Text != ""))
            { sql += " and a.SUPP_ID= N'" + Ma_NCC.Text + "' or b.SUPP_NAME like N'%" + Unicode2TCVN.UnicodeToTCVN3(Ma_NCC.Text) + "%'"; }
            if ((Ma_hang.Text != ""))
            { sql += " and (a.SKU_ID like N'%" + Ma_hang.Text + "%' or c.BARCODE like N'%" + Ma_hang.Text + "%' or c.SKU_CODE like N'%" + Ma_hang.Text + "%' or c.FULL_NAME like N'%" + Unicode2TCVN.UnicodeToTCVN3(Ma_hang.Text) + "%')"; }
            if ((Ma_nhom.Text != ""))
            { sql += " and c.GRP_ID like N'%" + Ma_nhom.Text + "%' or c.grp_name like N'%" + Unicode2TCVN.UnicodeToTCVN3(Ma_nhom.Text) + "%'"; }



            dataGridView_full.DataSource = cn.taobang1(sql);

            for (int i = 0; i < dataGridView_full.Rows.Count; i++)
            {
                dataGridView_full.Rows[i].Cells[0].Value = i + 1;
            }
            progressBar1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                //MessageBox.Show("Đang load dữ liệu ...", "Chú Ý !");
                return;
            }
            else
            {

                progressBar1.Visible = true;
                progressBar1.Style = ProgressBarStyle.Marquee;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void export_full_Click_old(object sender, EventArgs e)
        {
            progressBar1.Visible = true;


            //Lấy số ngẫu nhiên
            Random _r = new Random();
            string n = _r.Next(1, 10000).ToString();


            //System.Diagnostics.Process.Start("D:\\Zone_Price_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx");

            string filePath = "D:\\Zone_Price_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx";
            ExcelExporter.ExportDataGridViewToExcel(dataGridView_full, filePath);
            System.Diagnostics.Process.Start(filePath);
            progressBar1.Visible = false;

        }

        private void dataGridView_full_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Ma_hang_TextChanged(object sender, EventArgs e)
        {

        }



        public class ExcelExporter
        {
            public static void ExportDataGridViewToExcel(DataGridView dataGridView, string filePath)
            {
                // Tạo một DataTable mới
                DataTable dataTable = new DataTable();

                // Tạo các cột trong DataTable dựa trên tên cột của DataGridView
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    dataTable.Columns.Add(column.HeaderText, column.ValueType);
                }

                // Lấy dữ liệu từ DataGridView và thêm vào DataTable
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    DataRow dataRow = dataTable.NewRow();

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        //dataRow[cell.ColumnIndex] = cell.Value;


                        if (cell.ColumnIndex == 3 || cell.ColumnIndex == 8 || cell.ColumnIndex == 7 || cell.ColumnIndex == 10)
                        {
                            dataRow[cell.ColumnIndex] = Converter.TCVN3ToUnicode(cell.Value.ToString());
                        }
                        else
                        {
                            dataRow[cell.ColumnIndex] = cell.Value.ToString();
                        }
                    }

                    dataTable.Rows.Add(dataRow);
                }

                // Xuất dữ liệu ra tệp Excel bằng EPPlus
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Zone Prices");
                    ///*worksheet*/.Worksheet(1).Columns().AdjustToContents();
                    //Worksheet(1).Columns().AdjustToContents();

                    // Ghi dữ liệu từ DataTable vào worksheet
                    worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);

                    // Điều chỉnh tự động chiều rộng cột
                    worksheet.Cells.AutoFitColumns();


                    // Lưu tệp Excel
                    FileInfo fileInfo = new FileInfo(filePath);
                    package.SaveAs(fileInfo);
                }
            }
        }

        private async void export_full_Click(object sender, EventArgs e)
        {
            // Giải phóng bộ nhớ trước khi bắt đầu
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (backgroundWorker1.IsBusy)
            {
                MessageBox.Show("Đang load dữ liệu, xin đợi ...", "Chú Ý !");
                return;
            }

            progressBar1.Visible = true;
            export_full.Enabled = false;

            int totalRows = dataGridView_full.Rows.Count;
            int maxRowsPerSheet = 800000; // Số dòng tối đa trên mỗi sheet

            await Task.Run(() =>
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add($"Sheet1");
                    int sheetNumber = 1;
                    int currentRow = 1; // Vị trí hiện tại trên sheet

                    // Ghi tiêu đề cột
                    foreach (DataGridViewColumn column in dataGridView_full.Columns)
                    {
                        worksheet.Cells[currentRow, column.Index + 1].Value = column.HeaderText;
                    }

                    currentRow++; // Bắt đầu ghi dữ liệu từ dòng thứ hai

                    for (int rowIndex = 0; rowIndex < totalRows; rowIndex++)
                    {
                        DataGridViewRow row = dataGridView_full.Rows[rowIndex];
                        int columnIndex = 1;

                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                            {
                                string cellValue = cell.Value.ToString();
                                if (cell.ColumnIndex == 3 || cell.ColumnIndex == 8 || cell.ColumnIndex == 7 || cell.ColumnIndex == 10)
                                {
                                    cellValue = Converter.TCVN3ToUnicode(cellValue);
                                }
                                worksheet.Cells[currentRow, columnIndex].Value = cellValue;
                            }
                            else
                            {
                                worksheet.Cells[currentRow, columnIndex].Value = DBNull.Value;
                            }
                            columnIndex++;
                        }

                        currentRow++;

                        //if (currentRow > maxRowsPerSheet)
                        //{
                        //    // Nếu số dòng vượt quá ngưỡng, chuyển sang sheet mới
                        //    sheetNumber++;
                        //    worksheet = excelPackage.Workbook.Worksheets.Add($"Sheet{sheetNumber}");

                        //    currentRow = 1;

                        //    // Ghi tiêu đề cột trên sheet mới
                        //    foreach (DataGridViewColumn column in dataGridView_full.Columns)
                        //    {
                        //        worksheet.Cells[currentRow, column.Index + 1].Value = column.HeaderText;
                        //    }

                        //    currentRow++; // Bắt đầu ghi dữ liệu từ dòng thứ hai
                        //}
                    }

                    string randomNum = new Random().Next(1, 10000).ToString();
                    string fileName = $"D:\\Zone_Price_STK_{DateTime.Today.Day}{DateTime.Today.Month}{DateTime.Today.Year}_{randomNum}.xlsx";
                    FileInfo excelFile = new FileInfo(fileName);
                    excelPackage.SaveAs(excelFile);

                    // Mở file Excel sau khi xuất xong
                    System.Diagnostics.Process.Start(fileName);

                    // Giải phóng bộ nhớ sau khi hoàn thành
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            });

            progressBar1.Visible = false;
            export_full.Enabled = true;
        }

        private async void export_full_Click_9992(object sender, EventArgs e)
        {
            // Giải phóng bộ nhớ trước khi bắt đầu
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (backgroundWorker1.IsBusy)
            {
                MessageBox.Show("Đang load dữ liệu, xin đợi ...", "Chú Ý !");
                return;
            }

            progressBar1.Visible = true;
            export_full.Enabled = false;

            int maxRowsPerSheet = 400000; // Số dòng tối đa trên mỗi sheet

            await Task.Run(() =>
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    //ExcelWorksheet worksheet = null;
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                    int sheetNumber = 1;
                    int currentRow = 1; // Vị trí hiện tại trên sheet

                    foreach (DataGridViewColumn column in dataGridView_full.Columns)
                    {
                        if (column != null && column.HeaderText != null)
                        {
                            worksheet.Cells[1, column.Index + 1].Value = column.HeaderText;
                        }
                    }


                    foreach (DataGridViewRow row in dataGridView_full.Rows)
                    {
                        int rowIndex = row.Index;
                        int columnIndex = 1;

                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                            {
                                string cellValue = cell.Value.ToString();
                                if (cell.ColumnIndex == 3 || cell.ColumnIndex == 8 || cell.ColumnIndex == 7 || cell.ColumnIndex == 10)
                                {
                                    cellValue = Converter.TCVN3ToUnicode(cellValue);
                                }
                                worksheet.Cells[rowIndex + 2, columnIndex].Value = cellValue;
                            }
                            else
                            {
                                worksheet.Cells[rowIndex + 2, columnIndex].Value = DBNull.Value;
                            }

                            columnIndex++;
                        }

                        currentRow++;

                        if (currentRow > maxRowsPerSheet)
                        {
                            // Nếu số dòng vượt quá ngưỡng, chuyển sang sheet mới
                            sheetNumber++;
                            worksheet = excelPackage.Workbook.Worksheets.Add($"Sheet{sheetNumber}");
                            
                            currentRow = 1;

                            foreach (DataGridViewColumn column in dataGridView_full.Columns)
                            {
                                worksheet.Cells[currentRow, column.Index + 1].Value = column.HeaderText;
                            }
                        }
                    }

                    string randomNum = new Random().Next(1, 10000).ToString();
                    string fileName = $"D:\\Zone_Price_STK_{DateTime.Today.Day}{DateTime.Today.Month}{DateTime.Today.Year}_{randomNum}.xlsx";
                    FileInfo excelFile = new FileInfo(fileName);
                    excelPackage.SaveAs(excelFile);

                    // Mở file Excel sau khi xuất xong
                    System.Diagnostics.Process.Start(fileName);

                    // Giải phóng bộ nhớ trước khi bắt đầu
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            });

            progressBar1.Visible = false;
            export_full.Enabled = true;
        }


        private async void export_full_Click_9991(object sender, EventArgs e)
        {
            // Giải phóng bộ nhớ trước khi bắt đầu
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (backgroundWorker1.IsBusy)
            {
                MessageBox.Show("Đang load dữ liệu, xin đợi ...", "Chú Ý !");
                return;
            }

            progressBar1.Visible = true;
            export_full.Enabled = false;

            int totalRows = dataGridView_full.Rows.Count;
            int maxRowsPerSheet = 800000; // Số dòng tối đa trên mỗi sheet

            await Task.Run(() =>
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = null;
                    int sheetNumber = 1;
                    int currentRow = 1; // Vị trí hiện tại trên sheet

                    DataTable dt_chunk = new DataTable();
                    foreach (DataGridViewColumn column in dataGridView_full.Columns)
                    {
                        dt_chunk.Columns.Add(column.HeaderText, column.ValueType);
                    }

                    for (int startIndex = 0; startIndex < totalRows; startIndex += maxRowsPerSheet)
                    {
                        dt_chunk.Clear();
                        int endIndex = Math.Min(startIndex + maxRowsPerSheet, totalRows);

                        for (int rowIndex = startIndex; rowIndex < endIndex; rowIndex++)
                        {
                            DataGridViewRow row = dataGridView_full.Rows[rowIndex];
                            DataRow newRow = dt_chunk.NewRow();

                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                                {
                                    if (cell.ColumnIndex == 3 || cell.ColumnIndex == 8 || cell.ColumnIndex == 7 || cell.ColumnIndex == 10)
                                    {
                                        newRow[cell.ColumnIndex] = Converter.TCVN3ToUnicode(cell.Value.ToString());
                                    }
                                    else
                                    {
                                        newRow[cell.ColumnIndex] = cell.Value.ToString();
                                    }
                                }
                                else
                                {
                                    newRow[cell.ColumnIndex] = DBNull.Value; // Set giá trị NULL vào newRow
                                }
                            }

                            dt_chunk.Rows.Add(newRow);
                        }

                        if (worksheet == null || currentRow + dt_chunk.Rows.Count > maxRowsPerSheet)
                        {
                            worksheet = excelPackage.Workbook.Worksheets.Add($"Sheet{sheetNumber}");
                            sheetNumber++;
                            currentRow = 1;

                            for (int i = 0; i < dt_chunk.Columns.Count; i++)
                            {
                                worksheet.Cells[currentRow, i + 1].Value = dt_chunk.Columns[i].ColumnName;
                            }

                            currentRow++; // Chuyển xuống hàng tiếp theo sau header
                        }

                        for (int i = 0; i < dt_chunk.Rows.Count; i++)
                        {
                            for (int j = 0; j < dt_chunk.Columns.Count; j++)
                            {
                                worksheet.Cells[currentRow + i, j + 1].Value = dt_chunk.Rows[i][j];
                            }
                        }

                        currentRow += dt_chunk.Rows.Count;
                    }

                    string randomNum = new Random().Next(1, 10000).ToString();
                    string fileName = $"D:\\Zone_Price_STK_{DateTime.Today.Day}{DateTime.Today.Month}{DateTime.Today.Year}_{randomNum}.xlsx";

                    FileInfo excelFile = new FileInfo(fileName);
                    excelPackage.SaveAs(excelFile);

                    // Mở file Excel sau khi xuất xong
                    System.Diagnostics.Process.Start(fileName);

                    // Giải phóng bộ nhớ sau khi hoàn thành
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            });

            progressBar1.Visible = false;
            export_full.Enabled = true;
        }


        //private async void export_full_Click_999(object sender, EventArgs e)
        //{

        //    // Giải phóng bộ nhớ trước khi bắt đầu
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //    GC.Collect();

        //    if (backgroundWorker1.IsBusy)
        //    {
        //        MessageBox.Show("Đang load dữ liệu, xin đợi ...", "Chú Ý !");
        //        return;
        //    }

        //    progressBar1.Visible = true;
        //    export_full.Enabled = false;

        //    int totalRows = dataGridView_full.Rows.Count;
        //    int maxRowsPerSheet = 800000; // Số dòng tối đa trên mỗi sheet

        //    await Task.Run(() =>
        //    {
        //        using (ExcelPackage excelPackage = new ExcelPackage())
        //        {
        //            ExcelWorksheet worksheet = null;
        //            int sheetNumber = 1;
        //            int currentRow = 1; // Vị trí hiện tại trên sheet

        //            for (int startIndex = 0; startIndex < totalRows; startIndex += maxRowsPerSheet)
        //            {
        //                int endIndex = Math.Min(startIndex + maxRowsPerSheet, totalRows);

        //                DataTable dt_chunk = new DataTable();

        //                foreach (DataGridViewColumn column in dataGridView_full.Columns)
        //                {
        //                    dt_chunk.Columns.Add(column.HeaderText, column.ValueType);
        //                }

        //                for (int rowIndex = startIndex; rowIndex < endIndex; rowIndex++)
        //                {
        //                    DataGridViewRow row = dataGridView_full.Rows[rowIndex];
        //                    DataRow newRow = dt_chunk.NewRow();

        //                    foreach (DataGridViewCell cell in row.Cells)
        //                    {
        //                        if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
        //                        {
        //                            if (cell.ColumnIndex == 3 || cell.ColumnIndex == 8 || cell.ColumnIndex == 7 || cell.ColumnIndex == 10)
        //                            {
        //                                newRow[cell.ColumnIndex] = Converter.TCVN3ToUnicode(cell.Value.ToString());
        //                            }
        //                            else
        //                            {
        //                                newRow[cell.ColumnIndex] = cell.Value.ToString();
        //                            }
        //                        }
        //                        else
        //                        {
        //                            newRow[cell.ColumnIndex] = DBNull.Value; // Set giá trị NULL vào newRow
        //                        }
        //                    }

        //                    dt_chunk.Rows.Add(newRow);
        //                }

        //                if (currentRow + dt_chunk.Rows.Count > maxRowsPerSheet)
        //                {
        //                    // Nếu số dòng vượt quá ngưỡng, chuyển sang sheet mới
        //                    worksheet = excelPackage.Workbook.Worksheets.Add($"Sheet{sheetNumber}");
        //                    sheetNumber++;
        //                    currentRow = 1;
        //                }

        //                if (worksheet == null)
        //                {
        //                    worksheet = excelPackage.Workbook.Worksheets.Add($"Sheet{sheetNumber}");
        //                    sheetNumber++;
        //                }

        //                for (int i = 0; i < dt_chunk.Columns.Count; i++)
        //                {
        //                    worksheet.Cells[currentRow, i + 1].Value = dt_chunk.Columns[i].ColumnName;
        //                }

        //                for (int i = 0; i < dt_chunk.Rows.Count; i++)
        //                {
        //                    for (int j = 0; j < dt_chunk.Columns.Count; j++)
        //                    {
        //                        worksheet.Cells[currentRow + i + 1, j + 1].Value = dt_chunk.Rows[i][j];
        //                    }
        //                }

        //                currentRow += dt_chunk.Rows.Count;
        //            }

        //            string randomNum = new Random().Next(1, 10000).ToString();
        //            string fileName = $"D:\\Zone_Price_STK_{DateTime.Today.Day}{DateTime.Today.Month}{DateTime.Today.Year}_{randomNum}.xlsx";
        //            //string filePath = "D:\\Zone_Price_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx";

        //            FileInfo excelFile = new FileInfo(fileName);
        //            excelPackage.SaveAs(excelFile);


        //            // Mở file Excel sau khi xuất xong
        //            System.Diagnostics.Process.Start(fileName);

        //            // Giải phóng bộ nhớ sau khi hoàn thành
        //            GC.Collect();
        //            GC.WaitForPendingFinalizers();

        //        }
        //    });

        //    progressBar1.Visible = false;
        //    export_full.Enabled = true;

        //}




    }

}
