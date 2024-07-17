using VOUCHER_CENTER.DataAccess;
using System;
using System.IO;
using System.Windows.Forms;
using static VOUCHER_CENTER.Main;

namespace VOUCHER_CENTER.Presentation
{
    public partial class fr_Ketnoi : Form
    {
        public fr_Ketnoi()
        {
            InitializeComponent();
        }
        //ConnectDB cn = new ConnectDB();

        private void cmddn_Click(object sender, EventArgs e)
        {
            if (mk.Text != "Vu Duy Tu")
            {
                MessageBox.Show("Không thiết lập được", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            try
            {
                if (File.Exists(File_INI))
                {
                    File.Delete(File_INI);
                }

                txtserver1.Text = (txtserver1.Text);
                txtdb1.Text = (txtdb1.Text);
                user1.Text = ConnectDB.EncryptString((user1.Text), bientoancuc.amti);
                pass1.Text = ConnectDB.EncryptString((pass1.Text), bientoancuc.amti);
                txtserver2.Text = (txtserver2.Text);
                txtdb2.Text = (txtdb2.Text);
                user2.Text = ConnectDB.EncryptString((user2.Text), bientoancuc.amti);
                pass2.Text = ConnectDB.EncryptString((pass2.Text), bientoancuc.amti);
                // Chỉnh lần 3
                txtserver3.Text = (txtserver3.Text);
                txtdb3.Text = (txtdb3.Text);
                user3.Text = ConnectDB.EncryptString((user3.Text), bientoancuc.amti);
                pass3.Text = ConnectDB.EncryptString((pass3.Text), bientoancuc.amti);
                using (StreamWriter write = new StreamWriter(File_INI))
                {
                    write.WriteLine("A1=:" + txtserver1.Text);
                    write.WriteLine("A2=:" + txtdb1.Text);
                    write.WriteLine("A3=:" + user1.Text);
                    write.WriteLine("A4=:" + pass1.Text);
                    write.WriteLine("B1=:" + txtserver2.Text);
                    write.WriteLine("B2=:" + txtdb2.Text);
                    write.WriteLine("B3=:" + user2.Text);
                    write.WriteLine("B4=:" + pass2.Text);
                    // Chỉnh lần 3
                    write.WriteLine("C1=:" + txtserver3.Text);
                    write.WriteLine("C2=:" + txtdb3.Text);
                    write.WriteLine("C3=:" + user3.Text);
                    write.WriteLine("C4=:" + pass3.Text);
                    write.WriteLine("A5=:1");
                }
                MessageBox.Show("Đã Thiết Lập xong", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Question);

                MessageBox.Show("Kết Nối Thành Công Tới Sever " + txtserver1.Text + ". Bạn sẻ phải khởi động lại chương trình đối với lần kết nối đầu tiên", "Chú Ý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
            catch
            {
                MessageBox.Show("Không thiết lập được", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmdthoat_Click(object sender, EventArgs e)
        {
            fr_Ketnoi f3 = (fr_Ketnoi)Application.OpenForms["fr_Ketnoi"];
            f3.Close();
        }

        private void fr_Ketnoi_Load(object sender, EventArgs e)
        {
            StreamReader read = new StreamReader(File_INI);
            this.txtserver1.Text = (read.ReadLine().Split(':')[1]);
            this.txtdb1.Text = (read.ReadLine().Split(':')[1]);
            this.user1.Text = (read.ReadLine().Split(':')[1]);
            this.pass1.Text = (read.ReadLine().Split(':')[1]);
            this.txtserver2.Text = (read.ReadLine().Split(':')[1]);
            this.txtdb2.Text = (read.ReadLine().Split(':')[1]);
            this.user2.Text = (read.ReadLine().Split(':')[1]);
            this.pass2.Text = (read.ReadLine().Split(':')[1]);
            // Chỉnh lần 3
            this.txtserver3.Text = (read.ReadLine().Split(':')[1]);
            this.txtdb3.Text = (read.ReadLine().Split(':')[1]);
            this.user3.Text = (read.ReadLine().Split(':')[1]);
            this.pass3.Text = (read.ReadLine().Split(':')[1]);
            //this.pass2 = EncryptString((read.ReadLine().Split(':')[1]), amti);
            read.Close();
        }

        private void check_Click(object sender, EventArgs e)
        {
            if (mk.Text == "Vu Duy Tu")
            {
                txtserver1.Text = ((txtserver1.Text));
                txtdb1.Text = ((txtdb1.Text));
                user1.Text = ConnectDB.DecryptString((user1.Text), bientoancuc.amti);
                pass1.Text = ConnectDB.DecryptString((pass1.Text), bientoancuc.amti);
                //-------------------
                txtserver2.Text = (txtserver2.Text);
                txtdb2.Text = (txtdb2.Text);
                user2.Text = ConnectDB.DecryptString((user2.Text), bientoancuc.amti);
                pass2.Text = ConnectDB.DecryptString((pass2.Text), bientoancuc.amti);
                // Chỉnh lần 3
                txtserver3.Text = (txtserver3.Text);
                txtdb3.Text = (txtdb3.Text);
                user3.Text = ConnectDB.DecryptString((user3.Text), bientoancuc.amti);
                pass3.Text = ConnectDB.DecryptString((pass3.Text), bientoancuc.amti);
                //--------------------------
                //ConnectDB.EncryptString((txtserver1.Text), bientoancuc.amti);

            }
            else
            {
                StreamReader read = new StreamReader(File_INI);
                this.txtserver1.Text = (read.ReadLine().Split(':')[1]);
                this.txtdb1.Text = (read.ReadLine().Split(':')[1]);
                this.user1.Text = (read.ReadLine().Split(':')[1]);
                this.pass1.Text = (read.ReadLine().Split(':')[1]);
                //---------------------------
                this.txtserver2.Text = (read.ReadLine().Split(':')[1]);
                this.txtdb2.Text = (read.ReadLine().Split(':')[1]);
                this.user2.Text = (read.ReadLine().Split(':')[1]);
                this.pass2.Text = (read.ReadLine().Split(':')[1]);
                // Chỉnh lần 3
                this.txtserver3.Text = (read.ReadLine().Split(':')[1]);
                this.txtdb3.Text = (read.ReadLine().Split(':')[1]);
                this.user3.Text = (read.ReadLine().Split(':')[1]);
                this.pass3.Text = (read.ReadLine().Split(':')[1]);
                //---------------------------
                read.Close();
            }
        }
    }
}
