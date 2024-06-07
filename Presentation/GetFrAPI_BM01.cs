using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Report_Center.Presentation
{
    public partial class GetFrAPI_BM01 : Form
    {
        // Khai báo biến apiUrl ở mức độ của lớp
        private string apiUrl = "https://uat-apps-api.hcrc.vn/api/contracts_bas/excel/bm02";
        private bool isDownloading = false; // Biến cờ để theo dõi trạng thái tải xuống
        public GetFrAPI_BM01()
        {
            InitializeComponent();
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
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

        private async void btnDownload_Click3(object sender, EventArgs e)
        {
            if (!isDownloading)
            {
                isDownloading = true;

                string apiUrl = "https://uat-apps-api.hcrc.vn/api/contracts_bas/excel/bm02";
                string filePath = "path/to/save/excel.xlsx"; // Đặt đường dẫn lưu file tại đây
                                                             // Thiết lập các thuộc tính của SaveFileDialog
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    // Thiết lập các thuộc tính của SaveFileDialog
                    saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                    saveFileDialog.Title = "Chọn vị trí để lưu tệp Excel";

                    // Hiển thị hộp thoại và kiểm tra nếu người dùng đã chọn một vị trí
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Lấy đường dẫn được chọn
                        filePath = saveFileDialog.FileName;
                    }
                    else
                    { return; }
                }

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
                MessageBox.Show("File đang được tải xuống. Vui lòng đợi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnDownload_Click2(object sender, EventArgs e)
        {
            string apiUrl = "https://uat-apps-api.hcrc.vn/api/contracts_bas/excel/bm02";
            string filePath = "path/to/save/excel.xlsx"; // Đặt đường dẫn lưu file tại đây
                                                         // Thiết lập các thuộc tính của SaveFileDialog
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Thiết lập các thuộc tính của SaveFileDialog
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                saveFileDialog.Title = "Chọn vị trí để lưu tệp Excel";

                // Hiển thị hộp thoại và kiểm tra nếu người dùng đã chọn một vị trí
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy đường dẫn được chọn
                    filePath = saveFileDialog.FileName;
                }
            }
            await DownloadAndSaveExcel(apiUrl, filePath);
        }

        private async Task DownloadAndSaveExcel(string apiUrl, string filePath)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //string requestData = "{ \"frDate\": \"2019-11-21T08:56:05.314Z\", \"toDate\": \"2023-11-21T08:56:05.314Z\", \"supp_ID\": \"string\", \"branchId\": \"string\", \"hcrC1_2FUJI_3All\": 0, \"contractType_1Mua_2Kg\": 0 }";
                    //string requestData = "{ \"frDate\": \"2019-11-21T08:56:05.314Z\", \"toDate\": \"2023-11-21T08:56:05.314Z\", \"supp_ID\": null, \"branchId\": null, \"hcrC1_2FUJI_3All\": 0, \"contractType_1Mua_2Kg\": 0 }";
                    string requestData = "{ \"frDate\": \"2019-11-21T08:56:05.314Z\", \"toDate\": \"2023-11-21T08:56:05.314Z\" }";

                    // Dữ liệu JSON để gửi lên API
                    //                    string requestData = @"
                    //{
                    //  ""frDate"": ""2019-11-21T08:56:05.314Z"",
                    //  ""toDate"": ""2023-11-21T08:56:05.314Z"",
                    //  ""supp_ID"": "",
                    //  ""branchId"": "",
                    //  ""hcrC1_2FUJI_3All"": 0,
                    //  ""contractType_1Mua_2Kg"": 0
                    //}";
                    // Gửi request POST đồng bộ
                    HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(requestData, Encoding.UTF8, "application/json"));

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
                        MessageBox.Show($"Lỗi khi tải file: {response.StatusCode}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDownload_Click1(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Thiết lập các thuộc tính của SaveFileDialog
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
                saveFileDialog.Title = "Chọn vị trí để lưu tệp Excel";

                // Hiển thị hộp thoại và kiểm tra nếu người dùng đã chọn một vị trí
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy đường dẫn được chọn
                    string selectedPath = saveFileDialog.FileName;

                    // Gọi hàm để tải tệp Excel từ API và lưu vào đường dẫn đã chọn
                    DownloadAndSaveExcel(selectedPath);

                    // Hiển thị thông báo hoặc thực hiện các bước khác nếu cần
                    MessageBox.Show($"Tệp Excel đã được tải và lưu tại:\n{selectedPath}", "Hoàn thành", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private async void DownloadAndSaveExcel(string filePath)
        {
            string apiUrl = "https://uat-apps-api.hcrc.vn/api/contracts_bas/excel/bm02";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    // Thực hiện yêu cầu GET đến API
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    // Kiểm tra xem yêu cầu có thành công không (200 OK)
                    if (response.IsSuccessStatusCode)
                    {
                        // Đọc nội dung của phản hồi và lưu vào tệp
                        byte[] content = await response.Content.ReadAsByteArrayAsync();
                        System.IO.File.WriteAllBytes(filePath, content);

                        Console.WriteLine("File tải về thành công.");
                    }
                    else
                    {
                        Console.WriteLine($"Lỗi: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi: {ex.Message}");
                }
            }
        }
        private void DownloadAndSaveExcel1(string filePath)  //Nếu API hỗ trợ phương thức GET, bạn có thể sử dụng WebClient.DownloadFile như sau:
        {
            // Gọi API để lấy dữ liệu Excel
            string apiUrl = "https://uat-apps-api.hcrc.vn/api/contracts_bas/excel/bm02";

            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(apiUrl, filePath);
            }
        }
    }
}
