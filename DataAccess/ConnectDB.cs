using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
//using NuGet;
using static Report_Center.Main;

namespace Report_Center.DataAccess
{
    public class bientoancuc
    {
        public static string userchung, passchung, amti = "BC123", b_Server2, b_Database2, b_user2, B_pass2, tenfilenhap, keylog, Server_nguon, Database_nguon, user_nguon, pass_nguon;
        public static string connectionString;
        //public static string amti = "BC123";
    }
    class ConnectDB
    {

        SqlConnection con;
        SqlCommand sqlcom;
        SqlDataReader sqldr;
        /// //--------------------------------------------------------------------
        protected static string strEx_Logs = "";
        protected static string on_IP = "";
        protected static bool run1 = false;
        //--------------------------------------------------------------------
        public string Server1 { get; set; }
        public string Database1 { get; set; }
        public string user1 { get; set; }
        public string pass1 { get; set; }
        public string Server2 { get; set; }
        public string Database2 { get; set; }
        // Chỉnh lần 3
        public string Server3 { get; set; }
        public string Database3 { get; set; }
        public string user3 { get; set; }
        public string pass3 { get; set; }
        //internal static string EncryptString(string text)
        //{
        //    throw new NotImplementedException();
        //}

        public string user2 { get; set; }
        public string pass2 { get; set; }

        public ConnectDB()
        {
            try
            {
                using (StreamReader read = new StreamReader(File_INI))
                {
                    this.Server1 = read.ReadLine().Split(':')[1];
                    this.Database1 = read.ReadLine().Split(':')[1];
                    this.user1 = DecryptString((read.ReadLine().Split(':')[1]), bientoancuc.amti);
                    this.pass1 = DecryptString((read.ReadLine().Split(':')[1]), bientoancuc.amti);
                    this.Server2 = ((read.ReadLine().Split(':')[1]));
                    this.Database2 = ((read.ReadLine().Split(':')[1]));
                    this.user2 = DecryptString((read.ReadLine().Split(':')[1]), bientoancuc.amti);
                    this.pass2 = DecryptString((read.ReadLine().Split(':')[1]), bientoancuc.amti);
                    bientoancuc.b_Server2 = this.Server2;
                    bientoancuc.b_Database2 = this.Database2;
                    bientoancuc.b_user2 = this.user2;
                    bientoancuc.B_pass2 = this.pass2;

                    bientoancuc.Server_nguon = this.Server1;
                    bientoancuc.Database_nguon = this.Database1;
                    bientoancuc.user_nguon = this.user1;
                    bientoancuc.pass_nguon = this.pass1;

                    // Chỉnh lần 3
                    this.Server3 = ((read.ReadLine().Split(':')[1]));
                    this.Database3 = ((read.ReadLine().Split(':')[1]));
                    this.user3 = DecryptString((read.ReadLine().Split(':')[1]), bientoancuc.amti);
                    this.pass3 = DecryptString((read.ReadLine().Split(':')[1]), bientoancuc.amti);
                }
                bientoancuc.connectionString = $"Data Source={bientoancuc.Server_nguon};Initial Catalog={bientoancuc.Database_nguon};User ID={bientoancuc.user_nguon};Password={bientoancuc.pass_nguon};Trusted_Connection=False;";
            }
            catch
            {

            }
        }

        public SqlConnection get_connection(int server_disk)
        {
            if (server_disk == 1)
            {
                return new SqlConnection("Data Source=" + this.Server1 + ";Initial Catalog =" + this.Database1 + "; User ID=" + this.user1 + ";Password=" + this.pass1 + ";Trusted_Connection=False;");
            }
            else if (server_disk == 2)
            {
                return new SqlConnection("Data Source=" + this.Server2 + ";Initial Catalog =" + this.Database2 + "; User ID=" + this.user2 + ";Password=" + this.pass2 + ";Trusted_Connection=False;");
            }
            else
            {
                return new SqlConnection("Data Source=" + this.Server3 + ";Initial Catalog =" + this.Database3 + "; User ID=" + this.user3 + ";Password=" + this.pass3 + ";Trusted_Connection=False;");
            }
        }



        public SqlConnection getcon()
        {
            return new SqlConnection("Data Source=" + this.Server1 + ";Initial Catalog =" + this.Database1 + "; User ID=" + this.user1 + ";Password=" + this.pass1 + ";Trusted_Connection=False;");
            //return new SqlConnection("Data Source=" + this.Server1 + ";Initial Catalog =" + this.Database1 + "; User ID=daisy;Password=hanoi;Trusted_Connection=False;");
            //return new SqlConnection("Data Source=192.168.1.54;Initial Catalog =QL_Quancaphe; User ID=daisy;Password=hanoi;Trusted_Connection=False;");
            //"Data Source=SKTUADMIN;Initial Catalog=BRGReports;User ID=daisy;Password=hanoi;Trusted_Connection=False;"

        }
        public SqlConnection getcon1()
        {

            //return new SqlConnection("Data Source=" + this.Server + ";Initial Catalog =" + this.Database + "; User ID=daisy;Password=hanoi;Trusted_Connection=False;");

            return new SqlConnection("Data Source=" + this.Server2 + ";Initial Catalog =" + this.Database2 + "; User ID=" + this.user2 + ";Password=" + this.pass2 + ";Trusted_Connection=False;");
            //return new SqlConnection("Data Source=" + this.Server2 + ";Initial Catalog =" + this.Database2 + "; User ID=daisy;Password=hanoi;Trusted_Connection=False;");
            //return new SqlConnection("Data Source=172.16.70.20;Initial Catalog =dsmart12; User ID=duong.nt;Password=123456;Trusted_Connection=False;");


            //"Data Source=SKTUADMIN;Initial Catalog=BRGReports;User ID=daisy;Password=hanoi;Trusted_Connection=False;"

        }
        // Chỉnh lần 3
        public SqlConnection getcon_Center()
        {
            return new SqlConnection("Data Source=" + this.Server3 + ";Initial Catalog =" + this.Database3 + "; User ID=" + this.user3 + ";Password=" + this.pass3 + ";Trusted_Connection=False;");
            //return new SqlConnection("Data Source=" + this.Server1 + ";Initial Catalog =" + this.Database1 + "; User ID=daisy;Password=hanoi;Trusted_Connection=False;");
            //return new SqlConnection("Data Source=192.168.1.54;Initial Catalog =QL_Quancaphe; User ID=daisy;Password=hanoi;Trusted_Connection=False;");
            //"Data Source=SKTUADMIN;Initial Catalog=BRGReports;User ID=daisy;Password=hanoi;Trusted_Connection=False;"

        }
        #region ưeqrwe

        public SqlDataReader WiteToBang(string sql)
        {
            con = getcon();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }


        //public static void writefile(string server, string db)
        //{
        //    StreamWriter write = new StreamWriter(File_INI);
        //    //write.WriteLine("SV=:" + server);
        //    //write.WriteLine("DB=:" + db);
        //    //write.WriteLine("L1=:" + server);
        //    write.WriteLine("A1=:" + Server1);
        //    write.WriteLine("A2=:" + txtdb1.Text);
        //    write.WriteLine("A3=:" + user1.Text);
        //    write.WriteLine("A4=:" + pass1.Text);
        //    write.WriteLine("B1=:" + txtserver2.Text);
        //    write.WriteLine("B2=:" + txtdb2.Text);
        //    write.WriteLine("B3=:" + user2.Text);
        //    write.WriteLine("B4=:" + pass2.Text);
        //    write.Close();

        //}
        public DataTable taobang(string sql)
        {
            con = getcon();

            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }
        public DataTable taobang_NO_Para_All(string sql, int ketnoi)
        {
            if (ketnoi == 3)
            {
                con = getcon_Center();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            else if (ketnoi == 2)
            {
                con = getcon1();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            else if (ketnoi == 1)
            {
                con = getcon();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }

            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }
        public DataTable taobang_from_Procedure(string sql)
        {
            con = getcon();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            //SqlDataAdapter ad = cmd.ExecuteReader();
            SqlDataReader ad = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(ad);
            return dt;
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
            //    frDate.Value = Convert.ToDateTime(txtTimMaSP.Text);
            //    cmd.Parameters.Add(frDate);
            //    todate.Value = Convert.ToDateTime(txtTimTenSP.Text);
            //    cmd.Parameters.Add(todate);
            //    SqlDataReader dr = cmd.ExecuteReader();
            //    DataTable dt = new DataTable();
            //    dt.Load(dr);//gắn dữ liệu vào DataSet
        }
        public DataTable taobang_from_Procedure1(string sql)
        {
            con = getcon();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);//gắn dữ liệu vào DataSet
            return dt;

        }
        public DataTable taobang_from_Procedure2(string sql)
        {
            con = getcon();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            DataColumn Col = new DataColumn("STT", typeof(int));
            dt.Columns.Add(Col);
            dt.Load(dr);//gắn dữ liệu vào DataSet
            return dt;

        }

        public DataTable taobang_from_Procedure_Parameter(string sql, DateTimePicker fr_dae)
        {
            con = getcon();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            SqlParameter frDate = new SqlParameter("@frDate", SqlDbType.Date);
            frDate.Value = Convert.ToDateTime(fr_dae.Value);
            cmd.Parameters.Add(frDate);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);//gắn dữ liệu vào DataSet
            return dt;

        }

        public DataTable taobang_from_Procedure_Parameter_Fuji(string sql, DateTimePicker fr_dae)
        {
            con = getcon();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            SqlParameter frDate = new SqlParameter("@frDate", SqlDbType.Date);
            frDate.Value = Convert.ToDateTime(fr_dae.Value);
            cmd.Parameters.Add(frDate);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);//gắn dữ liệu vào DataSet
            return dt;

        }


        public DataTable taobang_from_Procedure_Parameter1(string sql, DateTimePicker fr_date, DateTimePicker to_date, string node_id)
        {
            con = getcon();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            SqlParameter frDate = new SqlParameter("@frDate", SqlDbType.Date);
            SqlParameter toDate = new SqlParameter("@toDate", SqlDbType.Date);
            SqlParameter nodeid = new SqlParameter("@node_id", SqlDbType.VarChar);
            frDate.Value = Convert.ToDateTime(fr_date.Value);
            toDate.Value = Convert.ToDateTime(to_date.Value);
            nodeid.Value = node_id.ToString();
            cmd.Parameters.Add(frDate);
            cmd.Parameters.Add(toDate);
            cmd.Parameters.Add(nodeid);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            DataColumn Col = new DataColumn("STT", typeof(int));
            dt.Columns.Add(Col);
            dt.Load(dr);//gắn dữ liệu vào DataSet
            return dt;
            con.Close();
            con.Dispose();

        }
        public DataTable taobang_from_Procedure_Ton_Kho_Now(string sql, DateTimePicker fr_date, DateTimePicker to_date, string node_id, int ketnoi)
        {
            if (ketnoi == 3)
            {
                con = getcon_Center();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            else if (ketnoi == 2)
            {
                con = getcon1();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            else if (ketnoi == 1)
            {
                con = getcon();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            SqlParameter frDate = new SqlParameter("@frDate", SqlDbType.Date);
            SqlParameter toDate = new SqlParameter("@toDate", SqlDbType.Date);
            SqlParameter nodeid = new SqlParameter("@node_id", SqlDbType.VarChar);
            frDate.Value = Convert.ToDateTime(fr_date.Value);
            toDate.Value = Convert.ToDateTime(to_date.Value);
            nodeid.Value = node_id.ToString();
            cmd.Parameters.Add(frDate);
            cmd.Parameters.Add(toDate);
            cmd.Parameters.Add(nodeid);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            DataColumn Col = new DataColumn("STT", typeof(int));
            dt.Columns.Add(Col);
            dt.Load(dr);//gắn dữ liệu vào DataSet
            return dt;
            con.Close();
            con.Dispose();

        }
        public DataTable taobang_from_Procedure_Parameter1_Center(string sql, DateTimePicker fr_date, DateTimePicker to_date, string node_id)
        {
            con = getcon_Center();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            SqlParameter frDate = new SqlParameter("@frDate", SqlDbType.Date);
            SqlParameter toDate = new SqlParameter("@toDate", SqlDbType.Date);
            SqlParameter nodeid = new SqlParameter("@Node_id", SqlDbType.VarChar);
            frDate.Value = Convert.ToDateTime(fr_date.Value);
            toDate.Value = Convert.ToDateTime(to_date.Value);
            nodeid.Value = node_id.ToString();
            cmd.Parameters.Add(frDate);
            cmd.Parameters.Add(toDate);
            cmd.Parameters.Add(nodeid);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            DataColumn Col = new DataColumn("STT", typeof(int));
            dt.Columns.Add(Col);
            dt.Load(dr);//gắn dữ liệu vào DataSet
            return dt;
            con.Close();
            con.Dispose();

        }
        public DataTable taobang_from_Procedure_Parameter1_Center_USER(string sql, DateTimePicker fr_date, DateTimePicker to_date, string node_id, int ketnoi)
        {
            if (ketnoi == 3)
            {
                con = getcon_Center();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            else if (ketnoi == 2)
            {
                con = getcon1();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            else if (ketnoi == 1)
            {
                con = getcon();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            SqlParameter frDate = new SqlParameter("@frDate", SqlDbType.Date);
            SqlParameter toDate = new SqlParameter("@toDate", SqlDbType.Date);
            SqlParameter nodeid = new SqlParameter("@Node_id", SqlDbType.VarChar);
            SqlParameter grpid = new SqlParameter("@GRP_ID", SqlDbType.VarChar);
            frDate.Value = Convert.ToDateTime(fr_date.Value);
            toDate.Value = Convert.ToDateTime(to_date.Value);
            nodeid.Value = node_id.ToString();
            cmd.Parameters.Add(frDate);
            cmd.Parameters.Add(toDate);
            cmd.Parameters.Add(nodeid);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            //DataColumn Col = new DataColumn("STT", typeof(int));
            //dt.Columns.Add(Col);
            dt.Load(dr);//gắn dữ liệu vào DataSet
            return dt;
            con.Close();
            con.Dispose();

        }
        public DataTable taobang_from_Procedure_Parameter1_Center_GRP(string sql, DateTimePicker fr_date, DateTimePicker to_date, string node_id, string grp_id, int ketnoi)
        {
            if (ketnoi == 3)
            {
                con = getcon_Center();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            else if (ketnoi == 2)
            {
                con = getcon1();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            else if (ketnoi == 1)
            {
                con = getcon();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            SqlParameter frDate = new SqlParameter("@frDate", SqlDbType.Date);
            SqlParameter toDate = new SqlParameter("@toDate", SqlDbType.Date);
            SqlParameter nodeid = new SqlParameter("@Node_id", SqlDbType.VarChar);
            SqlParameter grpid = new SqlParameter("@GRP_ID", SqlDbType.VarChar);
            frDate.Value = Convert.ToDateTime(fr_date.Value);
            toDate.Value = Convert.ToDateTime(to_date.Value);
            nodeid.Value = node_id.ToString();
            grpid.Value = grp_id.ToString();
            cmd.Parameters.Add(frDate);
            cmd.Parameters.Add(toDate);
            cmd.Parameters.Add(nodeid);
            cmd.Parameters.Add(grpid);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            DataColumn Col = new DataColumn("STT", typeof(int));
            dt.Columns.Add(Col);
            dt.Load(dr);//gắn dữ liệu vào DataSet
            return dt;
            con.Close();
            con.Dispose();

        }
        public DataTable taobang_from_Procedure_Parameter1_Center_SKU(string sql, DateTimePicker fr_date, DateTimePicker to_date, string node_id, string grp_id, string sku_id, int ketnoi)
        {
            if (ketnoi == 3)
            {
                con = getcon_Center();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            else if (ketnoi == 2)
            {
                con = getcon1();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            else if (ketnoi == 1)
            {
                con = getcon();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
            }
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            SqlParameter frDate = new SqlParameter("@frDate", SqlDbType.Date);
            SqlParameter toDate = new SqlParameter("@toDate", SqlDbType.Date);
            SqlParameter nodeid = new SqlParameter("@Node_id", SqlDbType.VarChar);
            SqlParameter grpid = new SqlParameter("@GRP_ID", SqlDbType.VarChar);
            SqlParameter skuid = new SqlParameter("@SKU_ID", SqlDbType.VarChar);
            frDate.Value = Convert.ToDateTime(fr_date.Value);
            toDate.Value = Convert.ToDateTime(to_date.Value);
            nodeid.Value = node_id.ToString();
            grpid.Value = grp_id.ToString();
            skuid.Value = sku_id.ToString();
            cmd.Parameters.Add(frDate);
            cmd.Parameters.Add(toDate);
            cmd.Parameters.Add(nodeid);
            cmd.Parameters.Add(grpid);
            cmd.Parameters.Add(skuid);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            DataColumn Col = new DataColumn("STT", typeof(int));
            dt.Columns.Add(Col);
            dt.Load(dr);//gắn dữ liệu vào DataSet
            return dt;
            con.Close();
            con.Dispose();

        }
        public DataSet taobang_from_Procedure_Parameter2(string sql, DateTimePicker fr_date, DateTimePicker to_date, string node_id)
        {
            con = getcon();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            SqlParameter frDate = new SqlParameter("@frDate", SqlDbType.Date);
            SqlParameter toDate = new SqlParameter("@toDate", SqlDbType.Date);
            SqlParameter nodeid = new SqlParameter("@node_id", SqlDbType.VarChar);
            frDate.Value = Convert.ToDateTime(fr_date.Value);
            toDate.Value = Convert.ToDateTime(to_date.Value);
            nodeid.Value = node_id.ToString();
            cmd.Parameters.Add(frDate);
            cmd.Parameters.Add(toDate);
            cmd.Parameters.Add(nodeid);
            SqlDataReader dr = cmd.ExecuteReader();
            DataSet dt = new DataSet();


            dt.Tables.Add("met");
            //DataColumn Col = new DataColumn("STT", typeof(int));
            dt.Tables[0].Columns.Add("STT", typeof(int));
            dt.Tables.Add("qua");
            dt.Tables[1].Columns.Add("STT", typeof(int));
            dt.Tables[0].Load(dr);//gắn dữ liệu vào DataSet
                                  //rpt_StkISQty_test1
                                  //dr.NextResult();

            //-----------------------------------------------------------------------------------------------
            con.Close();
            con.Dispose();
            con = getcon();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con;
            cmd1.CommandTimeout = 0;
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "rpt_StkISQty_Order_SUM";
            SqlParameter frDate1 = new SqlParameter("@frDate", SqlDbType.Date);
            SqlParameter toDate1 = new SqlParameter("@toDate", SqlDbType.Date);
            SqlParameter nodeid1 = new SqlParameter("@node_id", SqlDbType.VarChar);
            frDate1.Value = Convert.ToDateTime(fr_date.Value);
            toDate1.Value = Convert.ToDateTime(to_date.Value);
            nodeid1.Value = node_id.ToString();
            cmd1.Parameters.Add(frDate1);
            cmd1.Parameters.Add(toDate1);
            cmd1.Parameters.Add(nodeid1);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            //-----------------------------------------------------------------------------------------------
            dt.Tables[1].Load(dr1);//gắn dữ liệu vào DataSet


            return dt;
            con.Close();
            con.Dispose();

        }
        public DataTable taobang_from_Procedure_Parameter3(string sql, DateTimePicker fr_date, DateTimePicker to_date, string stk_id, string grp_id, int sapxep)
        {
            con = getcon();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            SqlParameter frDate = new SqlParameter("@frDate", SqlDbType.Date);
            SqlParameter toDate = new SqlParameter("@toDate", SqlDbType.Date);
            SqlParameter stkid = new SqlParameter("@stk_id", SqlDbType.VarChar);
            SqlParameter GrpID = new SqlParameter("@Grp_ID", SqlDbType.VarChar);
            SqlParameter SAPXEP1 = new SqlParameter("@SAPXEP", SqlDbType.Int);

            frDate.Value = Convert.ToDateTime(fr_date.Value);
            toDate.Value = Convert.ToDateTime(to_date.Value);
            stkid.Value = stk_id.ToString();
            GrpID.Value = grp_id.ToString();
            SAPXEP1.Value = Convert.ToInt32(sapxep.ToString());
            cmd.Parameters.Add(frDate);
            cmd.Parameters.Add(toDate);
            cmd.Parameters.Add(stkid);
            cmd.Parameters.Add(GrpID);
            cmd.Parameters.Add(SAPXEP1);

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            //DataColumn Col = new DataColumn("STT", typeof(int));
            //dt.Columns.Add(Col);
            dt.Load(dr);//gắn dữ liệu vào DataSet
            return dt;
            con.Close();
            con.Dispose();

        }
        public DataSet taobang_from_Procedure_Parameter4(string sql, DateTimePicker fr_date, DateTimePicker to_date, string node_id)
        {
            con = getcon();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            SqlParameter frDate = new SqlParameter("@frDate", SqlDbType.Date);
            SqlParameter toDate = new SqlParameter("@toDate", SqlDbType.Date);
            SqlParameter nodeid = new SqlParameter("@node_id", SqlDbType.VarChar);
            frDate.Value = Convert.ToDateTime(fr_date.Value);
            toDate.Value = Convert.ToDateTime(to_date.Value);
            nodeid.Value = node_id.ToString();
            cmd.Parameters.Add(frDate);
            cmd.Parameters.Add(toDate);
            cmd.Parameters.Add(nodeid);

            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);

            DataSet dt = new DataSet();


            adapter.Fill(dt);//gắn dữ liệu vào DataSet


            return dt;
            con.Close();
            con.Dispose();

        }
        public DataSet taobang_from_Procedure_Parameter5(string sql, DateTimePicker fr_date, DateTimePicker to_date, string node_id, string core_all)
        {
            con = getcon();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            //SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sql;
            SqlParameter frDate = new SqlParameter("@frDate", SqlDbType.Date);
            SqlParameter toDate = new SqlParameter("@toDate", SqlDbType.Date);
            SqlParameter nodeid = new SqlParameter("@node_id", SqlDbType.VarChar);
            SqlParameter core1 = new SqlParameter("@core", SqlDbType.VarChar);
            frDate.Value = Convert.ToDateTime(fr_date.Value);
            toDate.Value = Convert.ToDateTime(to_date.Value);
            nodeid.Value = node_id.ToString();
            core1.Value = core_all.ToString();
            cmd.Parameters.Add(frDate);
            cmd.Parameters.Add(toDate);
            cmd.Parameters.Add(nodeid);
            cmd.Parameters.Add(core1);

            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);

            DataSet dt = new DataSet();


            adapter.Fill(dt);//gắn dữ liệu vào DataSet


            return dt;
            con.Close();
            con.Dispose();

        }
        public DataTable taobang1(string sql)
        {
            con = getcon1();
            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            DataColumn Col = new DataColumn("STT", typeof(int));
            dt.Columns.Add(Col);
            ad.Fill(dt);
            return dt;
        }
        public DataTable taobang2(string sql)
        {
            using (con = getcon1())
            {
                SqlDataAdapter ad = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                DataColumn Col = new DataColumn("STT", typeof(int));
                dt.Columns.Add(Col);
                ad.SelectCommand.CommandTimeout = 0; // 0 means no timeout
                ad.Fill(dt);
                return dt;
            }
        }
        public void ExcuteNonQuery(string sql)
        {
            con = getcon();
            sqlcom = new SqlCommand(sql, con);
            con.Open();
            sqlcom.ExecuteNonQuery();
            con.Close();
            con.Dispose();
        }

        public bool kiemtra(string sql)
        {
            con = getcon();
            con.Open();
            sqlcom = new SqlCommand(sql, con);
            int n = (int)sqlcom.ExecuteScalar();
            con.Close();
            if (n > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public int returnscalarnumber(string sql)
        {
            con = getcon();
            con.Open();
            sqlcom = new SqlCommand(sql, con);
            int n = (int)sqlcom.ExecuteScalar();
            con.Close();
            return n;
        }
        public string LayQuen(string sql)
        {
            string q = "";
            con = getcon();
            con.Open();
            sqlcom = new SqlCommand(sql, con);
            sqldr = sqlcom.ExecuteReader();
            while (sqldr.Read())
            {
                q = sqldr[0].ToString();
            }
            con.Close();
            return q;
        }
        public string LoadLable(string sql)
        {
            string ketqua = "";
            con = getcon();
            con.Open();
            sqlcom = new SqlCommand(sql, con);
            sqldr = sqlcom.ExecuteReader();
            while (sqldr.Read())
            {
                ketqua = sqldr[0].ToString();
            }
            con.Close();
            return ketqua;
        }
        public void LoadLenCombobox(ComboBox cb, string SQL, int chiso)
        {
            cb.Items.Clear();
            con = getcon();
            con.Open();
            sqlcom = new SqlCommand(SQL, con);
            sqldr = sqlcom.ExecuteReader();
            while (sqldr.Read())
            {
                cb.Items.Add(sqldr[chiso].ToString());
            }
            con.Close();
        }

        public void getData(AutoCompleteStringCollection dataCollection, string SQL)
        {
            //string connetionString = null;
            //SqlConnection connection;
            SqlCommand command;

            con = getcon1();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            //DataTable ds = new DataTable();
            //connetionString = "Data Source=" + bientoancuc.b_Server2 + ";Initial Catalog =" + bientoancuc.b_Database2 + "; User ID=" + bientoancuc.b_user2 + ";Password=" + bientoancuc.B_pass2 + ";Trusted_Connection=False;";
            //string sql = "SELECT SKU_ID, full_name  from SKU_DEF";
            //connection = new SqlConnection(connetionString);
            try
            {
                con.Open();
                command = new SqlCommand(SQL, con);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                adapter.Dispose();
                command.Dispose();
                con.Close();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dataCollection.Add(row[0].ToString());
                    //dataCollection.Add(row[1].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
        }

        public void LoadLenCombobox1(ComboBox cb, string SQL, int chiso)
        {
            cb.Items.Clear();
            con = getcon1();
            con.Open();
            sqlcom = new SqlCommand(SQL, con);
            sqldr = sqlcom.ExecuteReader();
            while (sqldr.Read())
            {
                cb.Items.Add(sqldr[chiso].ToString());
            }
            con.Close();
        }
        public void Load_TextBox(TextBox cb, string SQL)
        {
            cb.Clear();
            con = getcon1();
            con.Open();
            sqlcom = new SqlCommand(SQL, con);
            sqldr = sqlcom.ExecuteReader();
            while (sqldr.Read())
            {
                cb.Text += "," + (sqldr[0].ToString());
            }
            con.Close();
        }
        public void Load_TextBox_Center(TextBox cb, string SQL)
        {
            cb.Clear();
            con = getcon_Center();
            con.Open();
            sqlcom = new SqlCommand(SQL, con);
            sqldr = sqlcom.ExecuteReader();
            while (sqldr.Read())
            {
                cb.Text += "," + (sqldr[0].ToString());
            }
            con.Close();
        }
        public void Load_string(string cb, string SQL)
        {
            //cb.cl .Clear();
            con = getcon1();
            con.Open();
            sqlcom = new SqlCommand(SQL, con);
            sqldr = sqlcom.ExecuteReader();
            while (sqldr.Read())
            {
                cb = (sqldr[0].ToString());
            }

            con.Close();

        }
        public static string ToTitleCase(string mText)
        {
            string rText = "";
            try
            {
                System.Globalization.CultureInfo cultureInfo =
                System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Globalization.TextInfo TextInfo = cultureInfo.TextInfo;
                rText = TextInfo.ToTitleCase(mText);
            }
            catch
            {
                rText = mText;
            }
            return rText;
        }

        public bool kiemtrauser(string sql, string user, string pass)
        {
            con = getcon();
            bool a = true;
            sqlcom = new SqlCommand(sql, con);
            sqldr = sqlcom.ExecuteReader();
            while (sqldr.Read())
            {
                if (user == sqldr[0].ToString() && pass == sqldr[1].ToString())
                {
                    a = false;
                }
                else
                {
                    a = true;
                }
            }
            return a;
        }

        public bool KiemtraUsername(string strsql, int sv_disk)
        {
            con = get_connection(sv_disk);
            con.Open();
            sqlcom = new SqlCommand(strsql, con);
            int tontai = (int)(sqlcom.ExecuteScalar());
            con.Close();
            if (tontai > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        public int Gan_max_progressbar(string sql)
        {
            con = getcon1();
            con.Open();
            sqlcom = new SqlCommand(sql, con);
            int n = (int)sqlcom.ExecuteScalar();
            con.Close();
            return n;
        }
        public int Gan_max_progressbar_Center(string sql)
        {
            con = getcon_Center();
            con.Open();
            sqlcom = new SqlCommand(sql, con);
            int n = (int)sqlcom.ExecuteScalar();
            con.Close();
            return n;
        }
        //public int Get_mounth_close(string sql)
        //{
        //    //string ketqua1 = "";
        //    //con = getcon();
        //    //con.Open();
        //    //sqlcom = new SqlCommand(sql, con);
        //    //ketqua1 = sqlcom.ExecuteReader();

        //    //con.Close();
        //    //return ketqua1;
        //}
        //Hàm mã hóa chuỗi

        public static string EncryptString(string Message, string Passphrase)

        {
            //string rText1 = "";

            byte[] Results;

            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Buoc 1: Bam chuoi su dung MD5

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();

            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Tao doi tuong TripleDESCryptoServiceProvider moi

            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Cai dat bo ma hoa

            TDESAlgorithm.Key = TDESKey;

            TDESAlgorithm.Mode = CipherMode.ECB;

            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert chuoi (Message) thanh dang byte[]

            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Ma hoa chuoi

            try

            {

                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();

                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);

            }

            finally

            {

                // Xoa moi thong tin ve Triple DES va HashProvider de dam bao an toan

                TDESAlgorithm.Clear();

                HashProvider.Clear();

            }



            // Step 6. Tra ve chuoi da ma hoa bang thuat toan Base64

            //rText1= Convert.ToBase64String(Results);
            //return rText1;
            return Convert.ToBase64String(Results);

        }

        //Hàm giải mã chuỗi

        public static string DecryptString(string Message, string Passphrase)

        {
            //string rText2 = "";
            byte[] Results;

            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. Bam chuoi su dung MD5

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();

            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Tao doi tuong TripleDESCryptoServiceProvider moi

            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Cai dat bo giai ma

            TDESAlgorithm.Key = TDESKey;

            TDESAlgorithm.Mode = CipherMode.ECB;

            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert chuoi (Message) thanh dang byte[]

            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Bat dau giai ma chuoi

            try

            {

                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();

                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);

            }

            finally

            {

                // Xoa moi thong tin ve Triple DES va HashProvider de dam bao an toan

                TDESAlgorithm.Clear();

                HashProvider.Clear();

            }

            // Step 6. Tra ve ket qua bang dinh dang UTF8

            return UTF8.GetString(Results);
            //rText1 = Convert.ToBase64String(Results);
            //return rText1;

        }
        //--------------------------------------------------------------------

        #endregion





    }
}
