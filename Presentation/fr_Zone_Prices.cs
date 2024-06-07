//using COMExcel = Microsoft.Office.Interop.Excel;
//using System.Threading.Tasks;
//using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
//using System.Collections;
//using Action = System.Action;
using OfficeOpenXml.Style; // Thêm không gian tên cho ExcelFillStyle
using Report_Center.DataAccess;
using System;
//using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing; // Thêm không gian tên cho Color
using System.IO;
//using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Threading;
//using Font = System.Drawing.Font;
using Application = System.Windows.Forms.Application;
using DataTable = System.Data.DataTable;


namespace Report_Center.Presentation
{
    public partial class fr_Zone_Prices : Form
    {

        public fr_Zone_Prices()
        {
            InitializeComponent();
            Shown += new EventHandler(fr_Zone_Prices_Shown);
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
                fr_Zone_Prices f3 = (fr_Zone_Prices)Application.OpenForms["fr_Zone_Prices"];
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
        void fr_Zone_Prices_Shown(object sender, EventArgs e)
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
            //					from  DSMART12.dbo.SPPRICE a WITH (NOLOCK)
            //                    left join  DSMART12.dbo.SUPPLIER as b WITH (NOLOCK) on a.supp_id=b.supp_id
            //                    left join  DSMART12.dbo.SKU_DEF as c WITH (NOLOCK) on a.SKU_ID=c.SKU_ID
            //					 left join  DSMART12.dbo.GOODS as e WITH (NOLOCK) on right(left(a.SKU_ID,8),6)=e.GOODS_ID
            //					 left join  DSMART12.dbo.TAX_TYPE as f WITH (NOLOCK) on  a.tax_code = f.tax_code
            //LEFT join  DSMART12.dbo.RTPRICE AS CU WITH (NOLOCK) ON CU.SKU_ID = a.SKU_ID
            //                    where c.status NOT IN ( '02','05') order by a.SKU_ID";

            table = cn.taobang1(sql);
            setDataSource(table);
            System.GC.Collect();

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
                dataGridView_full.Columns[3].Frozen = true;
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void fr_Zone_Prices_Load(object sender, EventArgs e)
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
					from  DSMART12.dbo.SPPRICE a WITH (NOLOCK)
                    left join  DSMART12.dbo.SUPPLIER as b WITH (NOLOCK) on a.supp_id=b.supp_id
                    left join  DSMART12.dbo.SKU_DEF as c WITH (NOLOCK) on a.SKU_ID=c.SKU_ID
					 left join  DSMART12.dbo.GOODS as e WITH (NOLOCK) on right(left(a.SKU_ID,8),6)=e.GOODS_ID
					 left join  DSMART12.dbo.TAX_TYPE as f WITH (NOLOCK) on  a.tax_code = f.tax_code
LEFT join  DSMART12.dbo.RTPRICE AS CU WITH (NOLOCK) ON CU.SKU_ID = a.SKU_ID
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
                        //if (i == 19 )
                        //{
                        //    //CultureInfo frC = new CultureInfo("fr-FR");
                        //    value = string.Format("{0:MM/dd/yyyy}", value);
                        //    //value = String.Format("Number {0, 0:D5}", value);
                        //}


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
					from  DSMART12.dbo.SPPRICE a WITH (NOLOCK)
                    left join  DSMART12.dbo.SUPPLIER as b WITH (NOLOCK) on a.supp_id=b.supp_id
                    left join  DSMART12.dbo.SKU_DEF as c WITH (NOLOCK) on a.SKU_ID=c.SKU_ID
					 left join  DSMART12.dbo.GOODS as e WITH (NOLOCK) on right(left(a.SKU_ID,8),6)=e.GOODS_ID
					 left join  DSMART12.dbo.TAX_TYPE as f WITH (NOLOCK) on  a.tax_code = f.tax_code
                    LEFT join  DSMART12.dbo.RTPRICE AS CU WITH (NOLOCK) ON CU.SKU_ID = a.SKU_ID
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
        private void export_full_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
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


        public class ExcelExporter
        {
            public static void ExportDataGridViewToExcel(DataGridView dataGridView, string filePath)
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Zone Prices");

                    // Tạo header từ DataGridView columns
                    for (int col = 0; col < dataGridView.Columns.Count; col++)
                    {
                        worksheet.Cells[1, col + 1].Value = dataGridView.Columns[col].HeaderText;
                        worksheet.Cells[1, col + 1].Style.Font.Bold = true;
                        worksheet.Cells[1, col + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[1, col + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }

                    // Tạo nội dung từ DataGridView rows
                    for (int row = 0; row < dataGridView.Rows.Count; row++)
                    {
                        if (!dataGridView.Rows[row].IsNewRow) // Bỏ qua hàng trống cuối cùng (nếu có)
                        {
                            for (int col = 0; col < dataGridView.Columns.Count; col++)
                            {
                                var cellValue = dataGridView.Rows[row].Cells[col].Value;

                                if (cellValue != null)
                                {
                                    if (col == 3 || col == 7 || col == 8 || col == 10)
                                    {
                                        worksheet.Cells[row + 2, col + 1].Value = Converter.TCVN3ToUnicode(cellValue.ToString());
                                    }
                                    else
                                    {
                                        worksheet.Cells[row + 2, col + 1].Value = cellValue.ToString();
                                    }
                                }
                            }
                        }
                    }

                    // Điều chỉnh tự động chiều rộng cột
                    worksheet.Cells.AutoFitColumns();

                    // Lưu tệp Excel
                    FileInfo fileInfo = new FileInfo(filePath);
                    package.SaveAs(fileInfo);
                }
            }

            //public static void ExportDataGridViewToExcel(DataGridView dataGridView, string filePath)
            //{
            //    // Tạo một DataTable mới
            //    DataTable dataTable = new DataTable();

            //    // Tạo các cột trong DataTable dựa trên tên cột của DataGridView
            //    foreach (DataGridViewColumn column in dataGridView.Columns)
            //    {
            //        dataTable.Columns.Add(column.HeaderText, column.ValueType);
            //    }

            //    // Lấy dữ liệu từ DataGridView và thêm vào DataTable
            //    foreach (DataGridViewRow row in dataGridView.Rows)
            //    {
            //        DataRow dataRow = dataTable.NewRow();

            //        foreach (DataGridViewCell cell in row.Cells)
            //        {
            //            //dataRow[cell.ColumnIndex] = cell.Value;


            //            if (cell.ColumnIndex == 3 || cell.ColumnIndex == 8 || cell.ColumnIndex == 7 || cell.ColumnIndex == 10)
            //            {
            //                dataRow[cell.ColumnIndex] = Converter.TCVN3ToUnicode(cell.Value.ToString());
            //            }
            //            else
            //            {
            //                dataRow[cell.ColumnIndex] = cell.Value.ToString();
            //            }
            //        }

            //        dataTable.Rows.Add(dataRow);
            //    }

            //    // Xuất dữ liệu ra tệp Excel bằng EPPlus
            //    using (ExcelPackage package = new ExcelPackage())
            //    {
            //        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Zone Prices");
            //        ///*worksheet*/.Worksheet(1).Columns().AdjustToContents();
            //        //Worksheet(1).Columns().AdjustToContents();

            //        // Ghi dữ liệu từ DataTable vào worksheet
            //        worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);

            //        // Điều chỉnh tự động chiều rộng cột
            //        worksheet.Cells.AutoFitColumns();

            //        // Lưu tệp Excel
            //        FileInfo fileInfo = new FileInfo(filePath);
            //        package.SaveAs(fileInfo);
            //    }
            //}

        }



        //public class ExcelExporter
        //{
        //    public static void ExportToExcel(DataSet dataSet, string filePath)
        //    {
        //        using (var package = new ExcelPackage())
        //        {
        //            foreach (DataTable table in dataSet.Tables)
        //            {
        //                var worksheet = package.Workbook.Worksheets.Add(table.TableName);

        //                // Ghi dòng tiêu đề
        //                for (int i = 0; i < table.Columns.Count; i++)
        //                {
        //                    worksheet.Cells[1, i + 1].Value = table.Columns[i].ColumnName;
        //                }

        //                // Ghi dữ liệu từ DataTable
        //                var data = table.AsEnumerable()
        //                                .Select(row => row.ItemArray.Select(item => item.ToString()).ToArray());

        //                worksheet.Cells[2, 1].LoadFromArrays(data);
        //            }

        //            // Lưu file Excel
        //            package.SaveAs(new System.IO.FileInfo(filePath));
        //        }
        //    }
        //}



        //public class DataGridViewToDataSet
        //{
        //    public static DataSet GetDataSetFromDataGridView(DataGridView dataGridView)
        //    {
        //        DataSet dataSet = new DataSet();
        //        DataTable dataTable = new DataTable();


        //        // Tạo cột trong DataTable dựa trên tên cột của DataGridView
        //        foreach (DataGridViewColumn column in dataGridView.Columns)
        //        {
        //            dataTable.Columns.Add(column.Name);
        //        }

        //        // Thêm dữ liệu từ DataGridView vào DataTable
        //        foreach (DataGridViewRow row in dataGridView.Rows)
        //        {
        //            DataRow dataRow = dataTable.NewRow();

        //            foreach (DataGridViewCell cell in row.Cells)
        //            {
        //                // Kiểm tra kiểu dữ liệu của cell để chuyển đổi giá trị
        //                if (cell.Value != null)
        //                {
        //                    dataRow[cell.ColumnIndex] = cell.Value.ToString();
        //                }
        //            }

        //            dataTable.Rows.Add(dataRow);
        //        }

        //        dataSet.Tables.Add(dataTable);

        //        return dataSet;
        //    }
        //}


    }

}
