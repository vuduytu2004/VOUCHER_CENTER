//using System.Collections;
using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;
using System;
//using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using COMExcel = Microsoft.Office.Interop.Excel;
//using System.Threading.Tasks;
//using Microsoft.Office.Interop;
//using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Text;
using System.Threading;
//using System.Data.SqlClient;
//using System.Drawing;
//using System.Linq;
//using System.Text;
using System.Windows.Forms;
using Report_Center.DataAccess;
//using System.Threading;
//using Font = System.Drawing.Font;
using Application = System.Windows.Forms.Application;
using DataTable = System.Data.DataTable;
//using Action = System.Action;
//using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;



namespace Report_Center.Presentation
{
    public partial class fr_StkISQty_Doc : Form
    {

        public fr_StkISQty_Doc()
        {
            InitializeComponent();
            //Shown += new EventHandler(fr_StkISQty_Shown);
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

            frdate.CustomFormat = "dd/MM/yyyy";
            todate.CustomFormat = "dd/MM/yyyy";
            //frdate.Value = DateTime.Today.AddDays(-1);
            //Process currentProcess = Process.GetCurrentProcess();
            //long usedMemory = currentProcess.PrivateMemorySize64;
        }
        ConnectDB cn = new ConnectDB();
        DateTime da;
        //this.Ma_NCC.GotFocus += Ma_NCC_GotFocus;
        //this.Ma_NCC.Click += Ma_NCC_Click;
        DataTable table = new DataTable();
        DataTable table1 = new DataTable();
        //ConnectDB cn = new ConnectDB();
        string dk;
        int dong, dong1 = 0;
        int cot, cot1 = 0;
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

                fr_StkISQty_Doc f3 = (fr_StkISQty_Doc)Application.OpenForms["fr_StkISQty_Doc"];
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
        void fr_StkISQty_Doc_Shown(object sender, EventArgs e)
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
            //DataTable table1 = new DataTable();
            //DataTable table1 = new DataTable();

            string sql1 = "rpt_StkISQty_test1_Doc";
            //dt = cn.taobang_from_Procedure(sql);
            //List_Connected.DataSource = cn.taobang_from_Procedure(sql1);
            table1 = cn.taobang_from_Procedure_Parameter1(sql1, frdate, todate, Node_Id.Text.ToString());

            //Response.clear()
            //    Creating DataTable.
            //DataTable dt_grid_nhap = new DataTable();

            //foreach (DataRow dong_row in table1.Rows)
            //{
            //    dong_row[3] = Converter.TCVN3ToUnicode(dong_row[3].ToString());
            //    dong_row[4] = Converter.TCVN3ToUnicode(dong_row[4].ToString());
            //}
            //foreach (DataColumn cot_cot in table1.Columns)
            //{
            //    cot_cot.ColumnName = Converter.TCVN3ToUnicode(cot_cot.ToString());
            //}
            //System.GC.Collect();
            //progressBar1.Value = dong + 1;
            //dong++;

            //setDataSource(table);
            //For example, for the DataTable  provided as Datasource 
            //DataTable dtReturn = Pivot_table.GetInversedDataTable(table, "ten_cot", "sku_id", "value", "-", true);



            //DataTable dtReturn = Pivot_table.Pivot(table,  "ten_cot" , "value");

            //For example, for the DataTable  provided as Datasource 
            //DataTable dtReturn = cn.GetInversedDataTable(table, "Date", "EmployeeID",
            //                                          "Cost", "-", true);

            setDataSource(table1);
            //ClearTable(table);
            //ClearTable(table1);
            GC.Collect();
            //Clipboard.Clear();
            //////////////////////////for (int i = 0; i < dataGridView_full.Rows.Count; i++)
            //////////////////////////{
            //////////////////////////    dataGridView_full.Rows[i].Cells[0].Value = i + 1;
            //////////////////////////}

            //if (progressBar11.InvokeRequired)

            //    progressBar11.Visible = false;

            //dataGridView_full.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            //dataGridView_full.ColumnHeadersVisible = false;

            //this.dataGridView_full.DataSource = cn.taobang1(sql);

            //for (int i = 0; i < dataGridView_full.Rows.Count; i++)
            //{
            //    dataGridView_full.Rows[i].Cells[0].Value = i + 1;
            //}

            //dataGridView_full.Columns[4].Frozen = true;
            ////----------------------------------------------------------------------------------
            //// Your background task goes here
            //for (int i = 0; i <= 100; i++)
            //{
            //    // Report progress to 'UI' thread
            //    backgroundWorker1.ReportProgress(i);

            //    // Simulate long task
            //    System.Threading.Thread.Sleep(100);
            //}
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

                if (dataGridView_full.Columns.Count > 5) { dataGridView_full.Columns[5].Frozen = true; }
                timer1.Stop();
                tk_Full.Enabled = true;
                export_full.Enabled = true;

                //for (int i = 0; i < dataGridView_full.Rows.Count; i++)
                //{
                //    dataGridView_full.Rows[i].Cells[0].Value = i + 1;
                //}

            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void fr_StkISQty_Doc_Load(object sender, EventArgs e)
        {

            //backgroundWorker1.RunWorkerAsync();
            //////      //Task task = Task.Run((Action) MyFunction);
            //////      string sql = @"select top (2) a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định' ,(b.SUPP_NAME) as 'Tên NCC' 
            //////              ,a.SKU_ID as 'Mã Hàng', c.BARCODE 
            //////              ,c.SKU_CODE,(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT'
            //////              ,c.GRP_ID as 'Nhóm' , (c.grp_name) as 'Tên Nhóm' 
            //////              ,c.rtPRICE as 'Giá Bán', c.MDPRICE as 'Giá nội bộ'
            //////              , c.TAX_RATE , SPPRICE as 'Giá Nhập chỉ định', LASTIMPPR as 'Giá nhập lần trước'
            //////              ,PCPR_CODE as 'Vùng Giá' 
            //////              ,c.STATUS as 'Trạng Thái'
            //////              ,c.ITEM_TYPE as 'Loại hàng'
            //////              ,e.OPEN_DATE ,e.MODI_DATE 
            //////              from SPPRICE a
            //////              left join SUPPLIER as b on a.supp_id=b.supp_id
            //////              left join SKU_DEF as c on a.SKU_ID=c.SKU_ID
            //////left join GOODS as e on right(left(a.SKU_ID,8),6)=e.GOODS_ID
            //////              where c.status <> '02' order by a.SKU_ID";

            //////      //dataGridView_full.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            //////      //dataGridView_full.ColumnHeadersVisible = false;

            //////      dataGridView_full.DataSource = cn.taobang1(sql);

            //////      for (int i = 0; i < dataGridView_full.Rows.Count; i++)
            //////      {
            //////          dataGridView_full.Rows[i].Cells[0].Value = i + 1;
            //////      }

            //////      dataGridView_full.Columns[4].Frozen = true;

            //FastAutoSizeColumns(dataGridView_full);

            //_worker1.RunWorkerAsync();

            //dataGridView_full.DataSource = _worker1.RunWorkerAsync();


            //dataGridView_full.Columns["DS_TT"].DefaultCellStyle.Format = Converter.TCVN3ToUnicode(dataGridView_full);


            //_worker1_DoWork();
            //dataGridView_full.AutoResizeColumns();

            //dataGridView_full.ColumnHeadersVisible = true;
            //dataGridView_full.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //changecorlor();
            //dosomething();
            //dgv_CellFormatting(List_Connected);


            //List_Connected.Refresh();
            //dataGridView_full.Columns["DS_TT"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            //dataGridView_full.Columns["DS_TT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //List_Not_Connect.RightToLeft = Enabled;


            //dataGridView_full.DataSource = DS.Tables("LV1");

            //FastAutoSizeColumns(dataGridView_full);
            //dataGridView_full.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //dataGridView_full.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

        }


        //----------------------------------------
        void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (this.dataGridView_full.AutoSizeColumnsMode ==
            //DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader &&
            //this.dataGridView_full.Columns[e.ColumnIndex].AutoSizeMode ==
            //DataGridViewAutoSizeColumnMode.None)
            //{
            //    this.dataGridView_full.Columns[e.ColumnIndex].AutoSizeMode =
            //    DataGridViewAutoSizeColumnMode.NotSet;
            //}
        }

        void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            //if (e.Column.Width > 200 &&
            //this.dataGridView_full.AutoSizeColumnsMode ==
            //DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader &&
            //e.Column.AutoSizeMode == DataGridViewAutoSizeColumnMode.NotSet)
            //{
            //    e.Column.AutoSizeMode =
            //    DataGridViewAutoSizeColumnMode.None;
            //    this.dataGridView_full.ColumnWidthChanged -= new
            //    DataGridViewColumnEventHandler(dataGridView1_ColumnWidthChanged);
            //    e.Column.Width = 200;
            //    this.dataGridView_full.ColumnWidthChanged += new
            //    DataGridViewColumnEventHandler(dataGridView1_ColumnWidthChanged);
            //}
        }
        //----------------------------------------
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now.Subtract(da);
            label6.Text = span.Hours.ToString() + " : " + span.Minutes.ToString() + " : " + span.Seconds.ToString() + " : "
                + span.Milliseconds.ToString();
        }

        private void SQLtoExcel(DataGridView grView, string Output)
        {
            //Lấy số ngẫu nhiên
            Random _r = new Random();
            string n = _r.Next(1, 10000).ToString();

            string Filename = "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".csv";


            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(Filename, false, Encoding.UTF8))
            {
                int dong = 0;
                // Loop through the fields and add headers
                //Adding the Columns.
                foreach (DataGridViewColumn column in grView.Columns)
                {
                    //dt_grid_nhap.Columns.Add(column.HeaderText, column.ValueType);
                    //dt_grid_nhap.Columns.Add(Converter.TCVN3ToUnicode(column.HeaderText), column.ValueType);
                    string name = column.HeaderText;
                    if (name.Contains(","))
                        name = "\"" + name + "\"";
                    fs.Write(name + ",");

                }
                fs.WriteLine();
                //Adding the Rows.
                //int dong = 0;
                foreach (DataGridViewRow row in grView.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        string value = cell.Value.ToString();
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

            //Excel.Worksheet xlWorkSheetToUpload = default(Excel.Worksheet);
            //xlWorkSheetToUpload = xlAppToUpload.Sheets["Sheet1"];

            Excel._Worksheet workSheet = (Excel.Worksheet)app.ActiveSheet;
            workSheet.Name = "aaaaa";
            workSheet.Columns.AutoFit();
            //workSheet.Range["A1", "H5"].AutoFormat(
            //        Excel.XlRangeAutoFormat.xlRangeAutoFormatList1); //xlRangeAutoFormat3DEffects1); //, xlRangeAutoFormatClassic2) ;
            workSheet.Range["F:F"].NumberFormat = "0";

            //workSheet.Range["A6", "H10"].AutoFormat(
            //        Excel.XlRangeAutoFormat.xlRangeAutoFormatList3);              // Thích cái Format này

            //workSheet.Range["A11", "H15"].AutoFormat(
            //        Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1);
            //workSheet.Range["A16", "H22"].AutoFormat(
            //        Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic2);

            //workSheet.Range["A23", "H30"].AutoFormat(
            //        Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic3);
            //workSheet.Range["A31", "H35"].AutoFormat(
            //        Excel.XlRangeAutoFormat.xlRangeAutoFormatLocalFormat1);

            //workSheet.Range["A1", "H10"].AutoFormat(
            //     Excel.XlCalcMemNumberFormatType.xlNumberFormatTypeNumber();
            ////workSheet.Range["A1", "H100"].AutoFormat(Excel.XlRangeAutoFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatClassic1, object Number, object Font, object Alignment, object Border, object Pattern, object Width);


            //Worksheets a = (Worksheets)app.Worksheets;
            ////wb.Worksheets.
            //a.. [2].NumberFormat = "@";      // column as a text
            wb.SaveAs(Output, XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //XlFileFormat. . .AutoFit();
            wb.Close();
            app.Quit();
            File.Delete(Filename);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Blocks;
            int sttmax = dataGridView_full.Rows.Count + 1;
            progressBar1.Maximum = sttmax;
            //////////string sql;
            //////////sql = @"select count(*) from SPPRICE where sku_id in (select SKU_ID from SKU_DEF where status <> '02')";
            ////////////p_max = cn.Gan_max_progressbar(sql);
            ////////////            sql = @"select a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định',dbo.tcvn2unicode(b.SUPP_NAME) as 'Tên NCC' ,a.SKU_ID as 'Mã Hàng',c.SKU_CODE,dbo.tcvn2unicode(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT', c.TAX_RATE , SPPRICE as 'Giá Nhập chỉ định', LASTIMPPR as 'Giá nhập lần trước'
            ////////////,PCPR_CODE as 'Vùng Giá' from SPPRICE a
            ////////////left join SUPPLIER as b on a.supp_id=b.supp_id
            ////////////left join SKU_DEF as c on a.SKU_ID=c.SKU_ID
            ////////////where a.sku_id in (select SKU_ID from SKU_DEF where status <> '02') order by a.SKU_ID";
            //////////progressBar1.Maximum = cn.Gan_max_progressbar(sql);

            //Lấy số ngẫu nhiên
            Random _r = new Random();
            string n = _r.Next(1, 10000).ToString();

            //////////      //sql = @"select SUPP_ID as 'Mã NCC', SKU_ID as 'Mã Hàng', SPPRICE as 'Giá Nhập chỉ định', PCPR_CODE as 'Vùng Giá' from SPPRICE where sku_id in (select SKU_ID from SKU_DEF where status <> '02') order by SKU_ID";
            //////////      //select* from SPPRICE where sku_id in (select SKU_ID from SKU_DEF where status <> '02')
            //////////      sql = @"select a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định' ,(b.SUPP_NAME) as 'Tên NCC' 
            //////////              ,a.SKU_ID as 'Mã Hàng', c.BARCODE 
            //////////              ,c.SKU_CODE,(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT'
            //////////              ,c.GRP_ID as 'Nhóm' , (c.grp_name) as 'Tên Nhóm' 
            //////////              ,c.rtPRICE as 'Giá Bán', c.MDPRICE as 'Giá nội bộ'
            //////////              , c.TAX_RATE , SPPRICE as 'Giá Nhập chỉ định', LASTIMPPR as 'Giá nhập lần trước'
            //////////              ,PCPR_CODE as 'Vùng Giá' 
            //////////              ,c.STATUS as 'Trạng Thái'
            //////////              ,c.ITEM_TYPE as 'Loại hàng'
            //////////              ,e.OPEN_DATE ,e.MODI_DATE 
            //////////              from SPPRICE a
            //////////              left join SUPPLIER as b on a.supp_id=b.supp_id
            //////////              left join SKU_DEF as c on a.SKU_ID=c.SKU_ID
            //////////left join GOODS as e on right(left(a.SKU_ID,8),6)=e.GOODS_ID
            //////////              where c.status <> '02' order by a.SKU_ID";


            //////////      //  Ngày tạo, loại hàng, trạng thái ----Giá bán,Barcode, Mã nhóm, tên nhóm, 


            SQLtoExcel(dataGridView_full, "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx");
            System.Diagnostics.Process.Start("D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx");
            GC.Collect();
            progressBar1.Visible = false;
            progressBar1.Style = ProgressBarStyle.Marquee;
        }


        private void dataGridView_full_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //--------------------------------------------------------
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ////////if (e.RowIndex >= 0)
            ////////{
            ////////    //string value =
            ////////    if (e.ColumnIndex == 1)
            ////////    {
            ////////        Ma_NCC.Text = dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
            ////////    }
            ////////    if (e.ColumnIndex == 3)
            ////////    {
            ////////        Ma_NCC.Text = Converter.TCVN3ToUnicode(dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());
            ////////    }
            ////////    if (e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 6)
            ////////    {
            ////////        Ma_hang.Text = dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
            ////////    }
            ////////    if (e.ColumnIndex == 9)
            ////////    {
            ////////        Ma_nhom.Text = dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
            ////////    }
            ////////    if (e.ColumnIndex == 10)
            ////////    {
            ////////        Ma_nhom.Text = Converter.TCVN3ToUnicode(dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());
            ////////    }
            ////////    if (e.ColumnIndex == 7)
            ////////    {
            ////////        Ma_hang.Text = Converter.TCVN3ToUnicode(dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString());
            ////////    }
            ////////    ////Lưu lại dòng dữ liệu vừa kích chọn
            ////////    //DataGridViewRow rowss = this.dataGridView_full.Rows[e.RowIndex];
            ////////    ////DataGridViewCell cell = this.dataGridView_full.CellClick();
            ////////    //DataGridViewColumn columnss =this.dataGridView_full.Columns[e.ColumnIndex];
            ////////    ////Đưa dữ liệu vào textbox
            ////////    //Ma_NCC.Text = rowss.Cells[columnss.ValueType].Value.ToString();
            ////////    //string value =
            ////////    //dataGridView_full.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
            ////////    //txtHoVaTen.Text = row.Cells[1].Value.ToString();
            ////////    //txtQueQuan.Text = row.Cells[2].Value.ToString();

            ////////    //Không cho phép sửa trường STT
            ////////    //txtSTT.Enabled = false;
            ////////}
        }
        //----------------------------------------------------------
        private void dataGridView_full_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private void Ma_NCC_GotFocus(object sender, RoutedEventArgs e)
        //private void Ma_NCC_Click(object sender, EventArgs e)
        //{
        //    this.Ma_NCC.SelectAll();
        //    this.Ma_NCC.Click -= Ma_NCC_Click;
        //}

        //private void Ma_NCC_GotFocus(object sender, EventArgs e)
        //{
        //    this.Ma_NCC.Select(this.Ma_NCC.Text.Length, 0);
        //}

        private void Ma_NCC_TextChanged(object sender, EventArgs e)
        {

        }
        private void Ma_NCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != ','))
            {
                e.Handled = true;
                MessageBox.Show("Chỉ nhập ký tự số .", "Chú ý !");
            }

            if ((e.KeyChar == ',') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }
        private void tk_Full_Click(object sender, EventArgs e)
        {
            // Kiểm tra điều kiện
            string phrase = Node_Id.Text;
            phrase = phrase.Replace(" ", "");
            string[] words = phrase.Split(',');
            if (phrase.Length != 0)
            {
                int i = 0;
                foreach (var word in words)
                {
                    if (i == 0)
                    { dk = $"'{word}'"; }

                    else
                    { dk += "," + $"'{word}'"; }
                    i++;
                    //System.Console.WriteLine($"<{word}>");
                }
                string sql1 = @"select count(*)  from STK_INFO with(nolock) where right(left(stk_id,4),3) in (" + dk + ")";
                i = cn.Gan_max_progressbar(sql1);
                if (i == 0)
                {
                    MessageBox.Show("Điều kiện Siêu Thị/Vùng.. không có." + "\n" + "Hãy kiểm tra lại .", "Chú ý !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            ClearTable(table1);
            ClearTable(table);
            dataGridView_full.Columns.Clear();
            dataGridView_full.Refresh();
            GC.Collect();
            da = DateTime.Now;
            timer1.Start();
            // Tạo cột STT, add cột vào tb trước khi đổ dữ liệu vào table >> STT cột đầu tiên
            //DataColumn Col = new DataColumn("STT", typeof(int));
            //dataGridView_full.Columns.Add(Col);

            //......DataGridView1.DataSource = dt;
            if (backgroundWorker1.IsBusy)
            {
                //MessageBox.Show("Đang load dữ liệu ...", "Chú Ý !");
                return;
            }
            backgroundWorker1.RunWorkerAsync();

            tk_Full.Enabled = false;
            export_full.Enabled = false;
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            //       string sql = @"select a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định' ,(b.SUPP_NAME) as 'Tên NCC' 
            //               ,a.SKU_ID as 'Mã Hàng', c.BARCODE 
            //               ,c.SKU_CODE,(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT'
            //               ,c.GRP_ID as 'Nhóm' , (c.grp_name) as 'Tên Nhóm' 
            //               ,c.rtPRICE as 'Giá Bán', c.MDPRICE as 'Giá nội bộ'
            //               , c.TAX_RATE , SPPRICE as 'Giá Nhập chỉ định', LASTIMPPR as 'Giá nhập lần trước'
            //               ,PCPR_CODE as 'Vùng Giá' 
            //               ,c.STATUS as 'Trạng Thái'
            //               ,c.ITEM_TYPE as 'Loại hàng'
            //               ,e.OPEN_DATE ,e.MODI_DATE 
            //               from SPPRICE a
            //               left join SUPPLIER as b on a.supp_id=b.supp_id
            //               left join SKU_DEF as c on a.SKU_ID=c.SKU_ID
            //left join GOODS as e on right(left(a.SKU_ID,8),6)=e.GOODS_ID
            //               where c.status <> '02' ";
            //       if ((Ma_NCC.Text == "") && (Ma_nhom.Text == "") && (Ma_hang.Text == ""))
            //               { progressBar1.Visible = false;
            //                   return; }
            //       if ((Ma_NCC.Text != ""))
            //               { sql += " and a.SUPP_ID= N'" + Ma_NCC.Text + "' or b.SUPP_NAME like N'%" + Unicode2TCVN.UnicodeToTCVN3(Ma_NCC.Text) + "%'"; }
            //       if ((Ma_hang.Text != ""))
            //               { sql += " and (a.SKU_ID like N'%" + Ma_hang.Text + "%' or c.BARCODE like N'%" + Ma_hang.Text + "%' or c.SKU_CODE like N'%" + Ma_hang.Text + "%' or c.FULL_NAME like N'%" + Unicode2TCVN.UnicodeToTCVN3(Ma_hang.Text) + "%')"; }
            //       if ((Ma_nhom.Text != ""))
            //               { sql += " and c.GRP_ID like N'%" + Ma_nhom.Text + "%' or c.grp_name like N'%" + Unicode2TCVN.UnicodeToTCVN3(Ma_nhom.Text) + "%'"; }


            //dataGridView_full.DataSource = cn.taobang1(sql);

            //for (int i = 0; i < dataGridView_full.Rows.Count ; i++)
            //{
            //    dataGridView_full.Rows[i].Cells[0].Value = i + 1;
            //}
            //progressBar1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ////if (backgroundWorker1.IsBusy)
            ////{
            ////    //MessageBox.Show("Đang load dữ liệu ...", "Chú Ý !");
            ////    return;
            ////}
            ////else
            ////{

            ////    progressBar1.Visible = true;
            ////    progressBar1.Style = ProgressBarStyle.Marquee;
            ////    backgroundWorker1.RunWorkerAsync();
            ////}
            ///////////////////////////////////////////////////////////////////
            //string sql = @"select * from table_test ";


            //dataGridView_full.DataSource = cn.taobang(sql);


        }

        private void ClearTable(DataTable table)
        {
            try
            {
                table.Clear();
            }
            catch (DataException e)
            {
                // Process exception and return.
                Console.WriteLine("Exception of type {0} occurred.",
                    e.GetType());
            }
        }

        private void Exprot_Table2Excl()
        {
            //Response.clear()
            //Creating DataTable.
            //--------------------------------------------------------------
            //////////////////////DataTable dt_grid_nhap = new DataTable();

            ////////////////////////Adding the Columns.
            //////////////////////foreach (DataGridViewColumn column in dataGridView_full.Columns)
            //////////////////////{
            //////////////////////    //dt_grid_nhap.Columns.Add(column.HeaderText, column.ValueType);
            //////////////////////    //dt_grid_nhap.Columns.Add(Converter.TCVN3ToUnicode(column.HeaderText), column.ValueType);
            //////////////////////    dt_grid_nhap.Columns.Add((column.HeaderText), column.ValueType);

            //////////////////////}

            ////////////////////////Adding the Rows.
            ////////////////////////int dong = 0;
            //////////////////////foreach (DataGridViewRow row in dataGridView_full.Rows)
            //////////////////////{
            //////////////////////    dt_grid_nhap.Rows.Add();
            //////////////////////    foreach (DataGridViewCell cell in row.Cells)
            //////////////////////    {
            //////////////////////        //if (cell.ColumnIndex == 3 || cell.ColumnIndex == 4)// || cell.ColumnIndex == 10)
            //////////////////////        //{
            //////////////////////        //    dt_grid_nhap.Rows[dt_grid_nhap.Rows.Count - 1][cell.ColumnIndex] = Converter.TCVN3ToUnicode(cell.Value.ToString());
            //////////////////////        //}
            //////////////////////        ////else if (cell.ColumnIndex == "")
            //////////////////////        ////{

            //////////////////////        ////}    
            //////////////////////        //else
            //////////////////////        //{
            //////////////////////        //    dt_grid_nhap.Rows[dt_grid_nhap.Rows.Count - 1][cell.ColumnIndex] = cell.Value;
            //////////////////////        //}
            //////////////////////        dt_grid_nhap.Rows[dt_grid_nhap.Rows.Count - 1][cell.ColumnIndex] = cell.Value;

            //////////////////////    }
            //////////////////////    //System.GC.Collect();
            //////////////////////    //progressBar1.Value = dong + 1;
            //////////////////////    //dong++;
            //////////////////////}
            /////--------------------------------------------------------------------------------------------
            //Exporting to Excel.
            //string folderPath = "D:\\Fuji\\";
            //if (!Directory.Exists(folderPath))
            //{
            //    Directory.CreateDirectory(folderPath);
            //}
            System.GC.Collect();
            using (XLWorkbook wb = new XLWorkbook())
            {

                wb.Worksheets.Add(table1, "Tồn_Bán");


                //Adjust widths of Columns.
                wb.Worksheet(1).Columns().AdjustToContents();

                //Lấy số ngẫu nhiên
                Random _r = new Random();
                string n = _r.Next(1, 10000).ToString();

                string Filename = "D:\\Ton_ban_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx";

                //dt_grid_nhap.Clear();
                //ClearTable(dt_grid_nhap);
                //Save the Excel file.
                wb.SaveAs(Filename);

                //System.Diagnostics.Process.Start("D:\\Zone_Price_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + ".xlsx");
                System.Diagnostics.Process.Start(Filename);
            }
            System.GC.Collect();

            //progressBar1.Visible = false;
        }
        private void export_full_Click(object sender, EventArgs e)
        {
            //Response.Clear();
            progressBar1.Visible = true;
            //progressBar1.Style = ProgressBarStyle.Blocks;
            //int sttmax = dataGridView_full.Rows.Count + 1;
            //progressBar1.Maximum = sttmax;
            System.GC.Collect();
            System.Threading.Thread thread =
           new System.Threading.Thread(new System.Threading.ThreadStart(Exprot_Table2Excl));
            thread.Start();

            //progressBar1.Visible = false;
            //progressBar1.Style = ProgressBarStyle.Marquee;

        }

        private void dataGridView_full_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void copyAlltoClipboard()
        {
            dataGridView_full.SelectAll();
            DataObject dataObj = dataGridView_full.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occurred while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
        private void Exp2Excl_Click(object sender, EventArgs e)
        {
            ClearTable(table);
            GC.Collect();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Inventory_Sell_Export.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Copy DataGridView results to clipboard
                    copyAlltoClipboard();

                    object misValue = System.Reflection.Missing.Value;  // sau khi copy vào Clipboard bộ nhớ chiếm : 787 MB
                    Excel.Application xlexcel = new Excel.Application();

                    xlexcel.DisplayAlerts = false; // Without this you will get two confirm overwrite prompts
                    Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                    Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    // Format column D as text before pasting results, this was required for my data
                    Excel.Range rng = xlWorkSheet.get_Range("D:D").Cells;
                    rng.NumberFormat = "@";
                    Excel.Range rng1 = xlWorkSheet.get_Range("G:G").Cells;
                    rng1.NumberFormat = "@";


                    // Paste clipboard results to worksheet range
                    Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
                    CR.Select();
                    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

                    // For some reason column A is always blank in the worksheet. ¯\_(ツ)_/¯
                    // Delete blank column A and select cell A1
                    Excel.Range delRng = xlWorkSheet.get_Range("A:A").Cells;
                    delRng.Delete(Type.Missing);
                    xlWorkSheet.get_Range("A1").Select();

                    // Save the excel file under the captured location from the SaveFileDialog
                    xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkSheet.Columns.AutoFit();
                    xlexcel.DisplayAlerts = true;
                    xlWorkBook.Close(true, misValue, misValue);
                    xlexcel.Quit();

                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlexcel);

                    // Clear Clipboard and DataGridView selection
                    Clipboard.Clear();
                    dataGridView_full.ClearSelection();

                    // Open the newly saved excel file
                    if (File.Exists(sfd.FileName))
                        System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "File đang mở or Data quá lớn ! Bấm Export Data");
                    //throw new Exception("ExportToExcel: \n" + ex.Message);
                    Clipboard.Clear();
                    dataGridView_full.ClearSelection();
                    return;
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        private void Node_Id_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(Node_Id.Text.ToString(), "Danh sách Vùng");
        }

        private void check_all_CheckedChanged(object sender, EventArgs e)
        {
            //Node_Id.Text = "Toàn bộ Mart/MiniMart";
            ////Node_Id.Enabled = false;
            //Node_Id.ReadOnly = true;
            //Node_Id.BackColor = System.Drawing.SystemColors.Window;
            Node_Id.Enabled = true;
            Node_Id.ReadOnly = false;
            Node_Id.Text = "";
            Node_Id.Focus();
        }

        private void check_mart_CheckedChanged(object sender, EventArgs e)
        {
            if (check_mart.Checked == true)
                Node_Id.Text = "";
            cn.Load_TextBox(Node_Id, @"SELECT  (a.NODE_ID)  from stock   a with(nolock) 
                            inner join node_def as b with(nolock) on a.stk_id=b.stk_id
                            where left(b.GRP_ID,2)='01' and a.type='01' and a.STATUS ='1'");
            //Node_Id.Enabled = false;
            Node_Id.ReadOnly = true;
            Node_Id.BackColor = System.Drawing.SystemColors.Window;
        }


        private void check_minimart_CheckedChanged(object sender, EventArgs e)
        {
            if (check_minimart.Checked == true)
                Node_Id.Text = "";
            cn.Load_TextBox(Node_Id, @"SELECT  (a.NODE_ID)  from stock   a with(nolock) 
                            inner join node_def as b with(nolock) on a.stk_id=b.stk_id
                            where left(b.GRP_ID,2)='02' and a.type='01' and a.STATUS ='1'");
            //Node_Id.Enabled = false;
            Node_Id.ReadOnly = true;
            Node_Id.BackColor = System.Drawing.SystemColors.Window;
        }

        private void check_minimart_hn_CheckedChanged(object sender, EventArgs e)
        {
            if (check_minimart_hn.Checked == true)
                Node_Id.Text = "";
            cn.Load_TextBox(Node_Id, @"SELECT  (a.NODE_ID)   from dbo.stock   a with(nolock) 
                              inner join node_def as b with(nolock) on a.stk_id=b.stk_id
                              where left(b.GRP_ID,2)='02' and a.type='01' and a.STATUS ='1' and a.PCPR_CODE='901'");
            //Node_Id.Enabled = false;
            Node_Id.ReadOnly = true;
            Node_Id.BackColor = System.Drawing.SystemColors.Window;
        }

        private void check_minimart_sg_CheckedChanged(object sender, EventArgs e)
        {
            if (check_minimart_sg.Checked == true)
                Node_Id.Text = "";

            cn.Load_TextBox(Node_Id, @"SELECT  (a.NODE_ID)   from dbo.stock   a with(nolock) 
                              inner join node_def as b with(nolock) on a.stk_id=b.stk_id
                              where left(b.GRP_ID,2)='02' and a.type='01' and a.STATUS ='1' and a.PCPR_CODE<>'901'");
            //Node_Id.Enabled = false;
            Node_Id.ReadOnly = true;
            Node_Id.BackColor = System.Drawing.SystemColors.Window;
        }
        private void btm_Fillter_Click(object sender, EventArgs e)
        {
            if ((dataGridView_full.DataSource as DataTable) != null)
            {
                if (txt_Fillter.Text.Length != 0)
                {
                    //(dataGridView_full.DataSource as DataTable).DefaultView.RowFilter = string.Format("sku_id LIKE '%{0}%'", txt_Fillter.Text);
                    string rowFilter = string.Format("sku_id LIKE '%{0}%'", txt_Fillter.Text);
                    rowFilter += string.Format("or dept_id LIKE '%{0}%'", txt_Fillter.Text);
                    rowFilter += string.Format("or grp_id LIKE '%{0}%'", txt_Fillter.Text);
                    rowFilter += string.Format("or grp_name LIKE '%{0}%'", txt_Fillter.Text);
                    rowFilter += string.Format("or full_name LIKE '%{0}%'", txt_Fillter.Text);
                    (dataGridView_full.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                }
                else
                {
                    (dataGridView_full.DataSource as DataTable).DefaultView.RowFilter = string.Format("sku_id LIKE '%'", txt_Fillter.Text);
                    //dataGridView_full.DataSource = table1;
                    //dataGridView_full.Refresh();
                }
            }
        }

        private void Show_all_Click(object sender, EventArgs e)
        {
            if ((dataGridView_full.DataSource as DataTable) is null)
            { return; }
            txt_Fillter.Text = "";
            (dataGridView_full.DataSource as DataTable).DefaultView.RowFilter = string.Format("sku_id LIKE '%'", txt_Fillter.Text);
        }

        private void dataGridView_full_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable table2 = new DataTable();
            /// -----------------------------test//----------------------------------------------------------------------------------
            string sql1 = @"Select * from StkISQty_test1 with(nolock) ";
            //dt = cn.taobang_from_Procedure(sql);
            //List_Connected.DataSource = cn.taobang_from_Procedure(sql1);
            //table = cn.taobang_from_Procedure(sql1);
            table2 = cn.taobang(sql1);
            setDataSource_DataTable1(table2, dataGridView_full);
        }
        private void setDataSource_DataTable1(DataTable table, DataGridView dtgrv)
        {
            // Invoke method if required:
            if (this.InvokeRequired)
            {
                this.Invoke(new SetDataSourceDelegate(setDataSource), table);
            }
            else
            {


                dtgrv.DataSource = table;

                progressBar1.Visible = false;
                //dataGridView_full.Columns[4].Frozen = true;
                //dataGridView_recap.Columns[6].Frozen = true;
                timer1.Stop();
                tk_Full.Enabled = true;
                export_full.Enabled = true;

                //for (int i = 0; i < dataGridView_full.Rows.Count; i++)
                //{
                //    dataGridView_full.Rows[i].Cells[0].Value = i + 1;
                //}

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView_full.RowCount == 0)
            { return; }

            GC.Collect();

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Inventory_Sell_Export_Doc.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //F003_Splash.ShowSplash();
                    //progressBar1.Visible = true;
                    //progressBar1.Style = ProgressBarStyle.Marquee;

                    //-----------------------------------------------------------------------------------------------------------------------------------------------------
                    // creating Excel Application  
                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    // creating new WorkBook within Excel application  
                    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                    // creating new Excelsheet in workbook  
                    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                    // see the excel sheet behind the program  


                    //app.Visible = true;


                    app.DisplayAlerts = false;


                    // get the reference of first sheet. By default its name is Sheet1.  
                    // store its reference to worksheet  
                    for (int x = 0; x < dataGridView_full.Rows.Count; x++)
                    {
                        //worksheet = workbook.Sheets[1].delete();
                        int ngat_trang = 0;
                        workbook.Sheets.Add();
                        //workbook.Sheets.Delete();

                        //worksheet = workbook.Sheets["Sheet" + x + ""];
                        worksheet = workbook.ActiveSheet;
                        //// changing the name of active sheet  
                        worksheet.Name = "Exported" + x + "";
                        // storing header part in Excel  
                        for (int i = 1; i < dataGridView_full.Columns.Count + 1; i++)
                        {
                            worksheet.Cells[1, i] = dataGridView_full.Columns[i - 1].HeaderText;
                        }
                        // storing Each row and column value to excel sheet  
                        for (int i = 0; i < dataGridView_full.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j < dataGridView_full.Columns.Count; j++)
                            {
                                worksheet.Cells[i + 2, j + 1] = dataGridView_full.Rows[x].Cells[j].Value;
                            }
                            ngat_trang++;
                            if (ngat_trang == 1000000)
                            {
                                //x--;
                                break;
                            }
                        }
                    }

                    try
                    {
                        // save the application  
                        //workbook.SaveAs("d:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        workbook.SaveAs(sfd.FileName, XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, ConflictResolution: Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                        // Exit from the application  
                        //app.Quit();  
                        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(new Form() { TopMost = true }, "Chú ý!");
                        //F003_Splash.CloseSplash();
                        MessageBox.Show("File đang mở...\n" + "Đóng file rồi xuất BC lại...\n" + ex.Message, "Chú ý!");

                        timer1.Stop();
                        ////F003_Splash.CloseSplash();
                        Invoke(new System.Action(() => this.progressBar1.Visible = false));
                        return;
                    }


                    app.Quit();
                    timer1.Stop();
                    //F003_Splash.CloseSplash();
                    Invoke(new System.Action(() => this.progressBar1.Visible = false));
                    System.Diagnostics.Process.Start(sfd.FileName);
                    GC.Collect();
                }
                catch (Exception ex)
                {
                    //F003_Splash.CloseSplash();
                    MessageBox.Show(ex.ToString(), "Chả biết lỗi gì ? Chụp ảnh gửi mềnh ! ");
                    timer1.Stop();
                    Invoke(new System.Action(() => this.progressBar1.Visible = false));
                    return;

                }
            }




        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView_full.RowCount == 0)
            { return; }
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            da = DateTime.Now;
            timer1.Start();
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(New_Export_Click2));

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView_full.RowCount == 0)
            { return; }
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            da = DateTime.Now;
            timer1.Start();
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(New_Export_Click1));

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        private void New_Export_Click1()
        {
            if (dataGridView_full.RowCount == 0)
            { return; }

            GC.Collect();

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Inventory_Sell_Export_Doc.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //F003_Splash.ShowSplash();
                    //progressBar1.Visible = true;
                    //progressBar1.Style = ProgressBarStyle.Marquee;

                    dong = 0;
                    cot = 0;
                    dong1 = 0;
                    cot1 = 0;

                    //------------------------------------------------------------------
                    //Lấy số ngẫu nhiên
                    Random _r = new Random();
                    string n = _r.Next(1, 10000).ToString();

                    string Filename = "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + "_1.csv";
                    //string Filename1 = "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + "_2.csv";

                    using (System.IO.StreamWriter fs = new System.IO.StreamWriter(Filename, false, Encoding.UTF8))
                    {

                        // Loop through the fields and add headers
                        //Adding the Columns.
                        foreach (DataGridViewColumn column in dataGridView_full.Columns)
                        {

                            string name = column.HeaderText;
                            if (name.Contains(","))
                                name = "\"" + name + "\"";
                            fs.Write(name + ",");
                            cot++;
                        }
                        fs.WriteLine();

                        foreach (DataGridViewRow row in dataGridView_full.Rows)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                string value = cell.Value.ToString();
                                if (value.Contains(","))
                                    value = "\"" + value + "\"";

                                fs.Write(value + ",");
                            }


                            fs.WriteLine();
                            dong++;
                        }
                        fs.Close();
                        dong++;
                    }

                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Open(Filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    app.DisplayAlerts = false;
                    Excel._Worksheet workSheet = (Excel.Worksheet)app.ActiveSheet;
                    //workSheet.Name = "aaaaa";
                    //workSheet = (Worksheet)wb.Sheets[1];
                    //    workSheet.Activate();
                    workSheet.Name = "Sell-Inventory";
                    workSheet.Rows[1].WrapText = true;
                    workSheet.Range["G:G"].NumberFormat = "0";
                    //workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].VerticalAlignment = XlVAlign.xlVAlignCenter;
                    //workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[dong, cot]].AutoFormat(XlRangeAutoFormat.xlRangeAutoFormatList3);
                    //workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].Borders.LineStyle = XlLineStyle.xlContinuous;
                    workSheet.Range[workSheet.Cells[2, "A"], workSheet.Cells[dong, cot]].font.size = 10;

                    workSheet.Rows[1].Insert();

                    Excel.Range newRng = workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]];
                    newRng.Interior.Color = XlRgbColor.rgbBlue;
                    newRng.Font.Color = XlRgbColor.rgbWhite;

                    //workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].Interior.Color = XlRgbColor.rgbBlue;
                    //    workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].VerticalAlignment = XlVAlign.xlVAlignCenter;
                    //    workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    //    workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].font.Color = XlRgbColor.rgbWhite;

                    workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[dong + 2, cot]].Borders.LineStyle = XlLineStyle.xlContinuous;

                    Excel.Range newRng1 = workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[2, cot]];
                    newRng1.Font.Size = 9;
                    newRng1.RowHeight = 24;
                    newRng1.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    newRng1.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    workSheet.Cells[1, 4] = "Thời gian : " + frdate.Value.ToString("dd/M/yyyy") + " - " + todate.Value.ToString("dd/M/yyyy");
                    //int next = 0;
                    //for (int i = 1; i <= cot; i++)
                    //{
                    //    try
                    //    {
                    //        string name = workSheet.Cells[2, i].Value.ToString();


                    //        if (name.Contains("_3_"))
                    //        {
                    //            // Do Something // 
                    //            workSheet.Columns[i].font.Color = XlRgbColor.rgbBlue;
                    //            workSheet.Cells[1, i - 2] = name.Substring(13);
                    //            workSheet.Range[workSheet.Cells[1, i - 2], workSheet.Cells[1, i]].Merge();
                    //            if (next == 0)
                    //            {
                    //                workSheet.Range[workSheet.Cells[2, i - 2], workSheet.Cells[2, i]].Interior.Color = XlRgbColor.rgbPeachPuff;
                    //                next = 1;
                    //                workSheet.Cells[2, i - 2] = " SL Tồn ";
                    //                workSheet.Cells[2, i - 1] = " SL Bán ";
                    //                workSheet.Cells[2, i] = " TB Bán ";
                    //            }
                    //            else
                    //            {
                    //                workSheet.Range[workSheet.Cells[2, i - 2], workSheet.Cells[2, i]].Interior.Color = XlRgbColor.rgbLightYellow;
                    //                next = 0;
                    //                workSheet.Cells[2, i - 2] = " SL Tồn ";
                    //                workSheet.Cells[2, i - 1] = " SL Bán ";
                    //                workSheet.Cells[2, i] = " TB Bán ";
                    //            }
                    //        }


                    //    }

                    //    catch (Exception exe)
                    //    {

                    //    }
                    //}

                    try
                    {
                        //wb.SaveAs(sfd.FileName, XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        wb.SaveAs(sfd.FileName, XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, ConflictResolution: Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(new Form() { TopMost = true }, "Chú ý!");
                        //F003_Splash.CloseSplash();
                        MessageBox.Show("File đang mở...\n" + "Đóng file rồi xuất BC lại...\n" + ex.Message, "Chú ý!");
                        wb.Close(false, Type.Missing, Type.Missing);
                        releaseObject(workSheet);
                        releaseObject(wb);
                        releaseObject(newRng);
                        releaseObject(newRng1);
                        File.Delete(Filename);
                        timer1.Stop();
                        ////F003_Splash.CloseSplash();
                        Invoke(new System.Action(() => this.progressBar1.Visible = false));
                        return;
                    }
                    wb.Close(false, Type.Missing, Type.Missing);
                    releaseObject(workSheet);
                    releaseObject(wb);
                    releaseObject(newRng);
                    releaseObject(newRng1);

                    File.Delete(Filename);

                    app.Quit();
                    timer1.Stop();
                    //F003_Splash.CloseSplash();
                    Invoke(new System.Action(() => this.progressBar1.Visible = false));
                    System.Diagnostics.Process.Start(sfd.FileName);
                    GC.Collect();
                }
                catch (Exception ex)
                {
                    //F003_Splash.CloseSplash();
                    MessageBox.Show(ex.ToString(), "Chả biết lỗi gì ? Chụp ảnh gửi mềnh ! ");
                    timer1.Stop();
                    Invoke(new System.Action(() => this.progressBar1.Visible = false));
                    return;

                }
            }
        }
        private void New_Export_Click2()
        {
            if (dataGridView_full.RowCount == 0)
            { return; }

            GC.Collect();

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "Inventory_Sell_Export_Doc.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //F003_Splash.ShowSplash();

                    dong = 0;
                    cot = 0;
                    dong1 = 0;
                    cot1 = 0;

                    Random _r = new Random();
                    string n = _r.Next(1, 10000).ToString();

                    string Filename = "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + "_1.csv";
                    //string Filename1 = "D:\\data_" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year + "_" + n + "_2.csv";

                    using (System.IO.StreamWriter fs = new System.IO.StreamWriter(Filename, false, Encoding.UTF8))
                    {

                        // Loop through the fields and add headers
                        //Adding the Columns.
                        foreach (DataGridViewColumn column in dataGridView_full.Columns)
                        {

                            string name = column.HeaderText;
                            if (name.Contains(","))
                                name = "\"" + name + "\"";
                            fs.Write(name + ",");
                            cot++;
                        }
                        fs.WriteLine();

                        foreach (DataGridViewRow row in dataGridView_full.Rows)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                string value = cell.Value.ToString();
                                if (value.Contains(","))
                                    value = "\"" + value + "\"";

                                fs.Write(value + ",");
                            }


                            fs.WriteLine();
                            dong++;
                        }
                        fs.Close();
                        dong++;
                    }
                    ////////////------------------------------------------------------------------------------------------------------


                    //double i, j;
                    //string UserFileName;
                    //string strTextLine;
                    //int iFile; iFile = FileSystem.FreeFile;

                    //UserFileName = Application.GetOpenFilename;
                    //Open(UserFileName);
                    //i = 1;
                    //j = 1;
                    //Check = false;

                    //while (!FileSystem.EOF(1))
                    //{
                    //    Line(Input, strTextLine);
                    //    if (i >= 1048576)
                    //    {
                    //        i = 1;
                    //        j = j + 1;
                    //    }
                    //    else
                    //    {
                    //        Sheets(1).Cells(i, j) = strTextLine;
                    //        i = i + 1;
                    //    }
                    //}

                    //Close();


                    ////////////------------------------------------------------------------------------------------------------------
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Open(Filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    app.DisplayAlerts = false;
                    Excel._Worksheet workSheet = (Excel.Worksheet)app.ActiveSheet;
                    //workSheet.Name = "aaaaa";
                    //workSheet = (Worksheet)wb.Sheets[1];
                    //    workSheet.Activate();
                    workSheet.Name = "Sell-Inventory";
                    workSheet.Rows[1].WrapText = true;
                    workSheet.Range["G:G"].NumberFormat = "0";

                    workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[dong, cot]].AutoFormat(XlRangeAutoFormat.xlRangeAutoFormatList3);
                    //workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]].Borders.LineStyle = XlLineStyle.xlContinuous;
                    workSheet.Range[workSheet.Cells[2, "A"], workSheet.Cells[dong, cot]].font.size = 10;

                    workSheet.Rows[1].Insert();

                    Excel.Range newRng = workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[1, cot]];
                    newRng.Interior.Color = XlRgbColor.rgbBlue;
                    newRng.Font.Color = XlRgbColor.rgbWhite;

                    workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[dong + 2, cot]].Borders.LineStyle = XlLineStyle.xlContinuous;

                    Excel.Range newRng1 = workSheet.Range[workSheet.Cells[1, "A"], workSheet.Cells[2, cot]];
                    newRng1.Font.Size = 9;
                    newRng1.RowHeight = 24;
                    newRng1.VerticalAlignment = XlVAlign.xlVAlignCenter;
                    newRng1.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    workSheet.Cells[1, 4] = "Thời gian : " + frdate.Value.ToString("dd/M/yyyy") + " - " + todate.Value.ToString("dd/M/yyyy");

                    try
                    {
                        //wb.SaveAs(sfd.FileName, XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        wb.SaveAs(sfd.FileName, XlFileFormat.xlOpenXMLWorkbook, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive, ConflictResolution: Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(new Form() { TopMost = true }, "Chú ý!");
                        //F003_Splash.CloseSplash();
                        MessageBox.Show("File đang mở...\n" + "Đóng file rồi xuất BC lại...\n" + ex.Message, "Chú ý!");
                        wb.Close(false, Type.Missing, Type.Missing);
                        releaseObject(workSheet);
                        releaseObject(wb);
                        releaseObject(newRng);
                        releaseObject(newRng1);
                        File.Delete(Filename);
                        timer1.Stop();
                        ////F003_Splash.CloseSplash();
                        Invoke(new System.Action(() => this.progressBar1.Visible = false));
                        return;
                    }
                    wb.Close(false, Type.Missing, Type.Missing);
                    releaseObject(workSheet);
                    releaseObject(wb);
                    releaseObject(newRng);
                    releaseObject(newRng1);

                    File.Delete(Filename);

                    app.Quit();
                    timer1.Stop();
                    //F003_Splash.CloseSplash();
                    Invoke(new System.Action(() => this.progressBar1.Visible = false));
                    System.Diagnostics.Process.Start(sfd.FileName);
                    GC.Collect();
                }
                catch (Exception ex)
                {
                    //F003_Splash.CloseSplash();
                    MessageBox.Show(ex.ToString(), "Chả biết lỗi gì ? Chụp ảnh gửi mềnh ! ");
                    timer1.Stop();
                    Invoke(new System.Action(() => this.progressBar1.Visible = false));
                    return;

                }
            }
        }
        //private void button6_Click(object sender, EventArgs e)
        //{

        //    // creating Excel Application  
        //    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
        //    // creating new WorkBook within Excel application  
        //    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
        //    // creating new Excelsheet in workbook  
        //    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
        //    // see the excel sheet behind the program  
        //    app.Visible = true;
        //    // get the reference of first sheet. By default its name is Sheet1.  
        //    // store its reference to worksheet  
        //    for (int x = 0; x < 3; x++)
        //    {
        //        workbook.Sheets.Add();
        //        workbook.Sheets.Delete();
        //        //worksheet = workbook.Sheets["Sheet" + x + ""];
        //        worksheet = workbook.ActiveSheet;
        //        //// changing the name of active sheet  
        //        worksheet.Name = "Exported from gridview" + x + "";
        //        // storing header part in Excel  
        //        for (int i = 1; i < dataGridView_full.Columns.Count + 1; i++)
        //        {
        //            worksheet.Cells[1, i] = dataGridView_full.Columns[i - 1].HeaderText;
        //        }
        //        // storing Each row and column value to excel sheet  
        //        for (int i = 0; i < dataGridView_full.Rows.Count - 1; i++)
        //        {
        //            for (int j = 0; j < dataGridView_full.Columns.Count; j++)
        //            {
        //                worksheet.Cells[i + 2, j + 1] = dataGridView_full.Rows[i].Cells[j].Value;
        //            }
        //        }
        //    }

        //    // save the application  
        //    workbook.SaveAs("d:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //    // Exit from the application  
        //    //app.Quit();  


        //}
    }

}
