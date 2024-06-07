using Report_Center.DataAccess;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using DataTable = System.Data.DataTable;

namespace Report_Center.Presentation
{
    public partial class fr_CheckConnects : Form
    {
        public fr_CheckConnects()
        {
            InitializeComponent();
            progressBar1.Visible = false;
            //Shown += new EventHandler(fr_Zone_Prices_Shown);
            // To report progress from the background worker we need to set this property
            backgroundWorker1.WorkerReportsProgress = true;
            // This event will be raised on the worker thread when the worker starts
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker2.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
            frdate.CustomFormat = "dd/MM/yyyy";
            frdate.Value = DateTime.Today.AddDays(-1);
        }
        ConnectDB cn = new ConnectDB();
        DateTime da;
        //public object AutoCompleteSuggestMode { get; private set; }
        //frdate.CustomFormat = "MMM-dd-yyyy";
        //frdate.Format = DateTimePickerFormat.Custom;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                SendKeys.Send("{TAB}");
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void changecorlor111(object sender, EventArgs e)
        {

            //-----------------------------------------------------

            ////-------------------------------------------------
            //foreach (DataGridViewRow row in this.List_Connected.RowCount)
            //{
            //    if ((row.Cells["TT"].Value.to / 2) == "0")
            //    //List_Connected.Rows/2==0
            //    //&& row.Cells["TagStatus"].Value.ToString() == "Lost"
            //    //|| row.Cells["TagStatus"].Value != null
            //    //&& row.Cells["TagStatus"].Value.ToString() == "Damaged"
            //    //|| row.Cells["TagStatus"].Value != null
            //    //&& row.Cells["TagStatus"].Value.ToString() == "Discarded")
            //    {
            //        row.DefaultCellStyle.BackColor = Color.LightGray;
            //        row.DefaultCellStyle.Font = new Font("Tahoma", 8, FontStyle.Bold);
            //    }
            //    else
            //    {
            //        row.DefaultCellStyle.BackColor = Color.Ivory;
            //    }
            //}
        }

        private void comboBox1_LostFocus(object sender, System.EventArgs e)
        {
            //string message = "Name22222: " + comboBox1.Text;
            ////message += Environment.NewLine;
            ////message += "CustomerId: " + cbCustomers.SelectedValue;
            //MessageBox.Show(message);
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            //progressBar1.Visible = false;

            //List_Connected.View = View.Details;
            //List_Connected.Columns.Add("Mã ST", 50, HorizontalAlignment.Center);
            //List_Connected.Columns.Add("Tên ST/CH", 300, HorizontalAlignment.Center);
            //List_Connected.Columns.Add("Check Online", 100, HorizontalAlignment.Center);
            //List_Connected.Columns.Add("Check Lệch ngày", 90, HorizontalAlignment.Center);
            //List_Connected.Columns.Add("Địa chỉ IP", 100, HorizontalAlignment.Center);
            //List_Connected.Columns.Add("IT Phụ Trách", 100, HorizontalAlignment.Center);
            //List_Connected.Columns.Add("Điện Thoại", 100, HorizontalAlignment.Center);
            //List_Connected.GridLines = true;
            //List_Connected.FullRowSelect = true;

            //dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns[1].HeaderText = "Mã ST";
            //dataGridView1.Columns[1].Frozen = true;
            //dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns[1].Width = 50;
            //dataGridView1.Columns[2].HeaderText = "Tên ST/CH";
            //dataGridView1.Columns[2].Width = 300;
            //dataGridView1.Columns[3].HeaderText = "Check Online";
            //dataGridView1.Columns[3].Width = 100;
            //dataGridView1.Columns[4].HeaderText = "Check Lệch ngày";
            //dataGridView1.Columns[4].Width = 90;
            //dataGridView1.Columns[5].HeaderText = "Địa chỉ IP";
            //dataGridView1.Columns[5].Width = 100;
            //dataGridView1.Columns[6].HeaderText = "IT Phụ Trách";
            //dataGridView1.Columns[6].Width = 100;
            //dataGridView1.Columns[7].HeaderText = "Điện Thoại";
            //dataGridView1.Columns[7].Width = 100;

            //comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //AutoCompleteStringCollection combData = new AutoCompleteStringCollection();
            //string sql = "SELECT SKU_ID, full_name  from SKU_DEF";
            //cn.getData(combData,sql);
            //comboBox1.AutoCompleteCustomSource = combData;
            ////comboBox1.AutoCompleteSuggestMode = AutoCompleteSuggestMode.Contains;
            ////---------------------------------------------------------------
            //comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //comboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //AutoCompleteStringCollection combData1 = new AutoCompleteStringCollection();
            //cn.getData(combData1,sql);
            //comboBox2.AutoCompleteCustomSource = combData1;
            //-------------------------------------------------------------


            //comboBox1.Text() = '';
            //comboBox1.DisplayMember = "full_name";
            //comboBox1.ValueMember = "full_name";
            //comboBox1.Refresh();
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string message = "Name: " + comboBox1.Text;
            ////message += Environment.NewLine;
            ////message += "CustomerId: " + cbCustomers.SelectedValue;
            //MessageBox.Show(message);
        }
        private void comboBox1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //string message = "Name11111: " + comboBox1.Text;
            ////message += Environment.NewLine;
            ////message += "CustomerId: " + cbCustomers.SelectedValue;
            //MessageBox.Show(message);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            da = DateTime.Now;
            timer1.Start();
            //button1.Text = "Stop";

            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            // Start the background worker
            backgroundWorker1.RunWorkerAsync();

            //if (MessageBox.Show =true)

            check_all.Enabled = false;
            check_not_connect.Enabled = false;

        }
        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //button2.Enabled = false;
            //progressBar11.Visible = true;
            //progressBar11.Style = ProgressBarStyle.Marquee;
            DataTable table = new DataTable();
            /// -----------------------------test//----------------------------------------------------------------------------------

            //----------------------------------------------------
            string sql1 = @"rptCheckCurrentDate_new1";
            //dt = cn.taobang_from_Procedure(sql);
            //List_Connected.DataSource = cn.taobang_from_Procedure(sql1);
            table = cn.taobang_from_Procedure_Parameter(sql1, frdate);
            setDataSource(table);

            //--------------------------------------------------------------------------------


            changecorlor();
            //dosomething();
            //dgv_CellFormatting(List_Connected);


            //List_Connected.Refresh();
            List_Connected.Columns["DS_TT"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            List_Connected.Columns["DS_TT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            List_Connected.Columns["ds_tv"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            List_Connected.Columns["ds_tv"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            List_Connected.Columns["DS_TT"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            List_Connected.Columns["DS_TT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            List_Connected.Columns["Lệch DT"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            List_Connected.Columns["Lệch DT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            List_Connected.Columns["bills"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            List_Connected.Columns["bills"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            List_Connected.Columns["Bills_LW"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            List_Connected.Columns["Bills_LW"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            List_Connected.Columns["Lệch Bill"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            List_Connected.Columns["Lệch Bill"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            List_Connected.Columns["Lãi gộp"].DefaultCellStyle.Format = "#,##0;#,##0;0";
            List_Connected.Columns["Lãi gộp"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            List_Connected.Columns[1].Frozen = true;
            //List_Not_Connect.Columns[1].Frozen = true;
            //List_Not_Connect.RightToLeft = Enabled;
            //////List_Connected.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //////List_Connected.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            //////List_Not_Connect.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //////List_Not_Connect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            ////////////////check_not_connect.Enabled = true;
            ////////////////check_all.Enabled = true;
            //---------------------------------------------------------------------------
            ////////////      string sql = @"select  a.SUPP_ID as 'Mã NCC', ISDEFAULT as 'NCC Chỉ định' ,(b.SUPP_NAME) as 'Tên NCC' 
            ////////////              ,a.SKU_ID as 'Mã Hàng', c.BARCODE 
            ////////////              ,c.SKU_CODE,(c.FULL_NAME) as 'Tên Hàng'  ,c.UNIT_DESC as 'ĐVT'
            ////////////              ,c.GRP_ID as 'Nhóm' , (c.grp_name) as 'Tên Nhóm' 
            ////////////              ,c.rtPRICE as 'Giá Bán', c.MDPRICE as 'Giá nội bộ'
            ////////////              , c.TAX_RATE , SPPRICE as 'Giá Nhập chỉ định', LASTIMPPR as 'Giá nhập lần trước'
            ////////////              ,PCPR_CODE as 'Vùng Giá' 
            ////////////              ,c.STATUS as 'Trạng Thái'
            ////////////              ,c.ITEM_TYPE as 'Loại hàng'
            ////////////              ,e.OPEN_DATE ,e.MODI_DATE 
            ////////////              from SPPRICE a
            ////////////              left join SUPPLIER as b on a.supp_id=b.supp_id
            ////////////              left join SKU_DEF as c on a.SKU_ID=c.SKU_ID
            ////////////left join GOODS as e on right(left(a.SKU_ID,8),6)=e.GOODS_ID
            ////////////              where c.status <> '02' order by a.SKU_ID";

            ////////////      table = cn.taobang1(sql);
            ////////////      setDataSource(table);

            ////////////      for (int i = 0; i < List_Connected.Rows.Count; i++)
            ////////////      {
            ////////////          List_Connected.Rows[i].Cells[0].Value = i + 1;
            ////////////      }

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
        void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //button2.Enabled = false;
            //progressBar11.Visible = true;
            //progressBar11.Style = ProgressBarStyle.Marquee;
            DataTable table = new DataTable();
            /// -----------------------------test//----------------------------------------------------------------------------------
            string sql1 = @"rptCheckCurrentDate_old";
            //dt = cn.taobang_from_Procedure(sql);
            //List_Connected.DataSource = cn.taobang_from_Procedure(sql1);
            //table = cn.taobang_from_Procedure(sql1);
            table = cn.taobang_from_Procedure_Parameter(sql1, frdate);
            setDataSource1(table);

            //--------------------------------------------------------------------------------


            changecorlor();


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
                List_Connected.DataSource = table;
                progressBar1.Visible = false;
                //List_Connected.Columns[4].Frozen = true;
                //List_Connected.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //List_Connected.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                //List_Not_Connect.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //List_Not_Connect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                check_not_connect.Enabled = true;
                check_all.Enabled = true;
                string sql = @"select b.STK_ID , (c.bu_desc) as 'ST/CH' 	,CONVERT(VARCHAR(10), a.CurDate, 103) as 'Check_Online'
	                    ,a.inventory as 'Lệch Ngày',c.SRV_IP,it_name
                        --rtrim(LTRIM(RIGHT(+it_name, CHARINDEX(' ', REVERSE(+it_name))))) AS 'IT'
                        ,d.IT_PHONE
                        ,DS_TT,ds_tv,(DS_TT-ds_tv) as'Lệch DT', bills, Bills_LW,(bills- Bills_LW) as 'Lệch Bill' ,sales_lw as 'Lãi gộp'
                        FROM doanhso a with(nolock) 
                        inner join [172.16.70.20].dsmart12.dbo.stock as b with(nolock) on a.Stk_id = b.STK_ID
                        inner join  BRG_Info as c with(nolock) on a.Stk_id = c.STK_ID
                        inner join  IT_BRG_INFO as d with(nolock) on a.Stk_id = d.STK_ID
                        where b.DIMENSION <> 0 and(a.CurDate) is null or a.inventory <> 0 
                        or (DS_TT-ds_tv)is null or (bills- Bills_LW)<>0
	                    or (bills- Bills_LW)is null or (DS_TT-ds_tv)>=1000";
                List_Not_Connect.DataSource = cn.taobang(sql);
                List_Connected.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                List_Connected.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                List_Not_Connect.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                List_Not_Connect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                List_Connected.Columns[4].Frozen = true;
                List_Not_Connect.Columns[4].Frozen = true;
                //setDataSource(table);
                timer1.Stop();
            }
        }

        private void setDataSource1(DataTable table)
        {
            // Invoke method if required:
            if (this.InvokeRequired)
            {
                this.Invoke(new SetDataSourceDelegate(setDataSource1), table);
            }
            else
            {
                List_Not_Connect.DataSource = table;
                progressBar1.Visible = false;
                //List_Connected.Columns[4].Frozen = true;
                //List_Connected.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //List_Connected.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                //List_Not_Connect.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //List_Not_Connect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                check_not_connect.Enabled = true;
                check_all.Enabled = true;
                string sql = @"select b.STK_ID , (c.bu_desc) as 'ST/CH' 	,CONVERT(VARCHAR(10), a.CurDate, 103) as 'Check_Online'
                         ,a.inventory as 'Lệch Ngày',c.SRV_IP,it_name
                            --rtrim(LTRIM(RIGHT(+it_name, CHARINDEX(' ', REVERSE(+it_name))))) AS 'IT'
                            ,d.IT_PHONE
                            ,DS_TT,ds_tv,(DS_TT-ds_tv) as'Lệch DT', bills, Bills_LW,(bills- Bills_LW) as 'Lệch Bill' ,sales_lw as 'Lãi gộp'
                            FROM doanhso a with(nolock) 
                            inner join[172.16.70.20].dsmart12.dbo.stock as b with(nolock) on a.Stk_id = b.STK_ID
                            inner join  BRG_Info as c with(nolock)  on a.Stk_id = c.STK_ID
                            inner join  IT_BRG_INFO as d with(nolock) on a.Stk_id = d.STK_ID
                            where b.DIMENSION <> 0";
                List_Connected.DataSource = cn.taobang(sql);
                //List_Connected.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //List_Connected.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                //List_Not_Connect.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //List_Not_Connect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                //List_Connected.Columns[4].Frozen = true;
                //List_Not_Connect.Columns[4].Frozen = true;
                //setDataSource(table);
                timer1.Stop();
            }
        }
        private void changecorlor()
        {
            foreach (DataGridViewRow row in List_Connected.Rows)
            {
                if ((row.Cells["Lệch Ngày"].Value is DBNull) || (Convert.ToInt32(row.Cells["Lệch Ngày"].Value) != 0))
                //Convert.ToInt32(row.Cells["Lệch Ngày"].Value) != 0 ||
                {
                    row.Cells["Lệch Ngày"].Style.BackColor = Color.Red;
                }
                else
                {
                    row.Cells["Lệch Ngày"].Style.BackColor = Color.Blue;

                }

                if ((row.Cells["Lệch DT"].Value is DBNull) || (Convert.ToInt32(row.Cells["Lệch DT"].Value) >= 1000))
                {
                    row.Cells["Lệch DT"].Style.BackColor = Color.Red;
                }
                else
                {
                    row.Cells["Lệch DT"].Style.BackColor = Color.Blue;
                }

                if ((row.Cells["Lệch Bill"].Value is DBNull) || (Convert.ToInt32(row.Cells["Lệch Bill"].Value) != 0))
                {
                    row.Cells["Lệch Bill"].Style.BackColor = Color.Red;
                }
                else
                {
                    row.Cells["Lệch Bill"].Style.BackColor = Color.Blue;
                }

                if (row.Cells["STK_ID"].Value != null)
                {
                    //{ }
                    //else
                    //{
                    if ((row.Cells["STK_ID"].Value.ToString().Replace(" ", "") == "11011") || (row.Cells["STK_ID"].Value.ToString().Replace(" ", "") == "11021") || (row.Cells["STK_ID"].Value.ToString().Replace(" ", "") == "11031"))
                    {
                        row.Cells["ST/CH"].Style.BackColor = Color.LightSeaGreen;
                    }
                }
                //List_Connected.Columns["ColumnName"].DefaultCellStyle.Format = "+#,##0;-#,##0;0";
            }
            //List_Connected.Refresh();
            //this.List_Connected.Columns[1].Visible = false;
            //for (int i = 0; i < List_Connected.RowCount; i++)
            //{
            //    if (i / 2 == 0)
            //    {
            //        //this is where your LAST LINE code goes
            //        //row.DefaultCellStyle.BackColor = Color.Yellow;
            //        List_Connected.Rows[i].DefaultCellStyle.BackColor = Color.Red;
            //    }
            //    else
            //    {
            //        //this is your normal code NOT LAST LINE
            //        //row.DefaultCellStyle.BackColor = Color.Red;
            //        List_Connected.Rows[i].DefaultCellStyle.BackColor = Color.White;
            //    }
            //}
        }

        //private async Task LoadData()
        //{
        //    // show progress bar
        //    progressBar1.Visible = true;
        //    await LoadDataAsync();
        //    // hide progress bar
        //    progressBar1.Visible = false;
        //}

        private void dosomething()
        {
            foreach (DataGridViewRow row in List_Connected.Rows)
            {
                if (Convert.ToInt32(row.Cells["TT"].Value) / 2 == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
        private void List_Connected_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // once per format
            if (e.ColumnIndex == 0 && e.RowIndex == 0)
            {
                foreach (DataGridViewRow row in List_Connected.Rows)
                    if (row != null)
                        row.DefaultCellStyle.BackColor = Color.Red;
            }
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }



        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    progressBar1.Value = e.ProgressPercentage;
        //    System.Windows.Forms.Application.DoEvents();
        //    //textBox1.Text = a + " %";
        //}

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            check_all.Enabled = true;
            check_not_connect.Enabled = true;
            //MessageBox.Show("Hoàn thành tiến trình", "tiến trình kết thức", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void check_not_connect_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            // Start the background worker
            backgroundWorker2.RunWorkerAsync();
            da = DateTime.Now;
            timer1.Start();
            check_all.Enabled = false;
            check_not_connect.Enabled = false;
            //if (MessageBox.Show =true)

            ////////////////////check_all.Enabled = false;
            ////////////////////check_not_connect.Enabled = false;
            ////////////////////check_all.Enabled = false;
            ////////////////////check_not_connect.Enabled = false;
            ////////////////////string sql = @"rptCheckCurrentDate_old";
            //////////////////////dt = cn.taobang_from_Procedure(sql);
            ////////////////////List_Not_Connect.DataSource = cn.taobang_from_Procedure(sql);
            //////////////////////--------------------------------------------------------------------------------
            ////////////////////sql = @"select b.STK_ID , (c.bu_desc) as 'ST/CH' 	,CONVERT(VARCHAR(10), a.CurDate, 103) as 'Check_Online'
            ////////////////////         ,a.inventory as 'Lệch Ngày',c.SRV_IP,rtrim(LTRIM(RIGHT(+it_name, CHARINDEX(' ', REVERSE(+it_name))))) AS 'IT',d.IT_PHONE
            ////////////////////            ,DS_TT,ds_tv,(DS_TT-ds_tv) as'Lệch DT', bills, Bills_LW,(bills- Bills_LW) as 'Lệch Bill' 
            ////////////////////            FROM doanhso a
            ////////////////////            inner join[172.16.70.20].dsmart12.dbo.stock as b on a.Stk_id = b.STK_ID
            ////////////////////            inner join  BRG_Info as c on a.Stk_id = c.STK_ID
            ////////////////////            inner join  IT_BRG_INFO as d on a.Stk_id = d.STK_ID
            ////////////////////            where b.DIMENSION <> 0";
            ////////////////////List_Connected.DataSource = cn.taobang(sql);
            ////////////////////changecorlor();
            //////////////////////dosomething();
            //////////////////////dgv_CellFormatting(List_Connected);

            ////////////////////List_Connected.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            ////////////////////List_Connected.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            ////////////////////List_Not_Connect.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            ////////////////////List_Not_Connect.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            //////////////////////List_Connected.Refresh();
            ////////////////////check_not_connect.Enabled = true;
            ////////////////////check_all.Enabled = true;
        }

        private void dgvMatHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void export_all_Click(object sender, EventArgs e)
        {

        }

        private void export_not_connect_Click(object sender, EventArgs e)
        {

        }

        private void List_Connected_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void List_Connected_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            changecorlor();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                fr_CheckConnects f3 = (fr_CheckConnects)Application.OpenForms["fr_CheckConnects"];
                f3.Close();
            }
            catch (NullReferenceException ne)
            {
                //One of the forms is not opened
            }
        }

        private void List_Connected_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    if (timer1.Enabled)
        //    {
        //        timer1.Stop();
        //        button1.Text = "Start";
        //    }
        //    else
        //    {
        //        da = DateTime.Now;
        //        timer1.Start();
        //        button1.Text = "Stop";
        //    }
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan span = DateTime.Now.Subtract(da);
            label1.Text = span.Hours.ToString() + " : " + span.Minutes.ToString() + " : " + span.Seconds.ToString() + " : "
                + span.Milliseconds.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            da = DateTime.Now;
            timer1.Start();
            //button1.Text = "Stop";

            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            // Start the background worker
            backgroundWorker1.RunWorkerAsync();

            //if (MessageBox.Show =true)

            check_all.Enabled = false;
            check_not_connect.Enabled = false;
            //SqlParameter frDate = new SqlParameter("@frDate", SqlDbType.Date);
        }

        private void List_Not_Connect_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void List_Connected_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            changecorlor();
        }
    }

}
// Khóa cột trong dataGridView
//dataGridView1.Columns["columnname"].Frozen = true;
//dataGridView1.RightToLeft = Enabled;


//You might use one of the DataGridViewElementStates enumeration values.

//Either use an index:

//dataGridView1.Columns[0].Frozen = true;
//or use the column name:

//dataGridView1.Columns["columnName"].Frozen = true;
//You may also use the DataGridViewColumnCollection.GetFirstColumn() method:

//dataGridView1.Columns.GetFirstColumn(DataGridViewElementStates.Frozen);




//private void frmMatHang_Load(object sender, EventArgs e)
//{
//    //Khoi tạo đối tượng Connection
//    con = new SqlConnection();
//    con.ConnectionString = Properties.Settings.Default.QLBanHangConnectionString;
//    //
//    HienThiDL();
//}
//private void HienThiDL()
//{
//    ////Khoi tao DataSet và DataAdapter
//    //ds = new DataSet();
//    //String sql = "Select * from tblMatHang";
//    //dap = new SqlDataAdapter(sql, con);
//    //dap.Fill(ds);//gắn dữ liệu vào DataSet
//    ////Gắn lên datagridView
//    //dgvMatHang.DataSource = ds.Tables[0];
//    //dgvMatHang.Refresh();
//}
//private void btnThem_Click(object sender, EventArgs e)
//{
//    lblTieuDe.Text = "THÊM MẶT HÀNG";
//    //Reset các control
//    txtMaSP.Text = "";
//    txtTenSP.Text = "";
//    dtpNgaySX.Value = DateTime.Today;
//    dtpNgayHH.Value = DateTime.Today;
//    txtDonGia.Text = "";
//    txtDonVi.Text = "";
//    txtGhiChu.Text = "";
//    //Cấm click
//    btnSua.Enabled = false;
//    btnXoa.Enabled = false;
//    //
//    btnLuu.Visible = true;
//    btnHuy.Visible = true;
//}

//private void btnLuu_Click(object sender, EventArgs e)
//{
//    //trường hợp thêm mới
//    if (btnThem.Enabled == true)
//    {
//        string sql = "INSERT INTO tblMatHang(MaSP,TenSP,NgaySX)";
//        sql += "VALUES(N'" + txtMaSP.Text + "','" + txtTenSP.Text + "','" + dtpNgaySX.Value.Date + "')";
//        //Mở kết nối
//        if (con.State != ConnectionState.Open)
//            con.Open();
//        //Khoi tạo đối tượng cmd
//        cmd = new SqlCommand();
//        cmd.CommandText = sql;
//        cmd.Connection = con;
//        //Thực thi câu lệnh truy vấn
//        cmd.ExecuteNonQuery();
//        //
//        HienThiDL();
//    }
//}

//private void btnTimKiem_Click(object sender, EventArgs e)
//{

//    string sql = "select * from tblMatHang where ";
//    if ((txtTimTenSP.Text == "") && (txtTimMaSP.Text == ""))
//    { return; }
//    if ((txtTimMaSP.Text != "") && (txtTimTenSP.Text == ""))
//    { sql += "MaSP= N'" + txtTimMaSP.Text + "'"; }
//    if ((txtTimTenSP.Text != "") && (txtTimMaSP.Text == ""))
//    { sql += "TenSP= N'" + txtTenSP.Text + "'"; }
//    if ((txtTimTenSP.Text != "") && (txtTimMaSP.Text != ""))
//    { sql += "MaSP= N'" + txtTimTenSP.Text + "' and TenSP=N'" + txtTimMaSP.Text + "','"; }


//    //Mở kết nối
//    if (con.State != ConnectionState.Open)
//        con.Open();
//    //Khoi tạo đối tượng cmd
//    cmd = new SqlCommand();
//    cmd.CommandText = sql;
//    cmd.Connection = con;
//    //Thực thi câu lệnh truy vấn
//    cmd.ExecuteNonQuery();
//    //
//    //HienThiDL();
//    //Khoi tao DataSet và DataAdapter
//    ds = new DataSet();
//    //String sql = "Select * from tblMatHang";
//    dap = new SqlDataAdapter(sql, con);
//    dap.Fill(ds);//gắn dữ liệu vào DataSet
//                 //Gắn lên datagridView
//    dgvMatHang.DataSource = ds.Tables[0];
//    dgvMatHang.Refresh();

//}

//private void btnXoa_Click(object sender, EventArgs e)
//{
//    HienThiDL();
//    dgvMatHang.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
//    dgvMatHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
//}

//private void btnThoat_Click(object sender, EventArgs e)
//{
//    if (con.State == ConnectionState.Open)
//        con.Close();
//    Application.Exit();
//}

//private void dgvMatHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
//{

//}

//private void btnSua_Click(object sender, EventArgs e)
//{
//    // Connect BC bán hàng
//    SqlConnection conn = new SqlConnection("Server=sktuadmin; Database = BRGReports; Uid = daisy; Pwd = hanoi");
//    if (conn.State != ConnectionState.Open)
//    {
//        conn.Open();
//    }
//    SqlCommand cmd = new SqlCommand();
//    cmd.Connection = conn;
//    cmd.CommandType = CommandType.StoredProcedure;
//    cmd.CommandText = "rptStkQty";
//    SqlParameter frDate = new SqlParameter("@frDate", SqlDbType.Date);
//    SqlParameter todate = new SqlParameter("@toDate", SqlDbType.Date);

//    //string iDate = "05/05/2005";
//    //DateTime oDate = Convert.ToDateTime(txtTimMaSP.Text);
//    //MessageBox.Show(oDate.Day + " " + oDate.Month + "  " + oDate.Year);

//    frDate.Value = Convert.ToDateTime(txtTimMaSP.Text);
//    cmd.Parameters.Add(frDate);
//    todate.Value = Convert.ToDateTime(txtTimTenSP.Text);
//    cmd.Parameters.Add(todate);
//    //DataSet ds = new DataSet();
//    SqlDataReader dr = cmd.ExecuteReader();
//    DataTable dt = new DataTable();
//    dt.Load(dr);//gắn dữ liệu vào DataSet

//    //MessageBox.Show(dr[0].ToString());
//    //MessageBox.Show(dr[1].ToString());

//    //String sql = "Select * from tblMatHang";
//    //dap = new SqlDataAdapter(sql, con);

//    //Gắn lên datagridView
//    dgvMatHang.DataSource = dt;


//    dgvMatHang.Refresh();
//    dr.Close();
//    cmd.Dispose();
//    conn.Close();
//}