using ClassLibrary1;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Torrent_Server_Side.Commom.Models;

namespace Torrent_Client_Side
{
    /// <summary>
    /// Interaction logic for FileTransfer.xaml
    /// </summary>
    public partial class FileTransfer : Window
    {
        public const string ADMIN_FILES_URI = "Admin-Files";
        public const string USER_PORT = "User-Port"; 
        public const string CONNECTIONSLOGOUTRINGS = "ConnectionLogoutStrings";
        public const string UPDATEFILESINSERVER = "ConnectionUpdateFilesPerUserStrings";
        public const string IPADRESSPERFILE = "IPAdressPerFile";
        public string chosenFileName;
        private List<FilesInfo> myFilesList;
        private List<DownloadFile> myDownloadFiles;
        private string downloadPath;
        private string uploadPath;
        private Proxy _proxy;

        private TcpClient client;
        private bool IsToListen = true;

        public FileTransfer(string downloadPath, string uploadPath,Proxy proxy)
        {

            InitializeComponent();
            _proxy = proxy;
            this.downloadPath = downloadPath;
            this.uploadPath = uploadPath;
            Task.Run(() =>
            {
                Listener();
            });
            UploadFilesToServer();
            myDownloadFiles = new List<DownloadFile>();
            this.Show();
   
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await InitialFilesTable();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
        private async Task<HttpResponseMessage> InitialFilesTable(string search_parameter = "")
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response;
                string responseBody;

                string requestURI = ConfigurationManager.ConnectionStrings[ADMIN_FILES_URI].ToString();
                if (search_parameter != "")
                {
                    response = await client.GetAsync(requestURI + "/?search_parameter=" + search_parameter.Trim());
                }
                else
                {
                    response = await client.GetAsync(requestURI);
                }
                responseBody = response.Content.ReadAsStringAsync().Result;
                myFilesList = JsonConvert.DeserializeObject<List<FilesInfo>>(responseBody);
                FilesTable.ItemsSource = myFilesList;
                return response;
            }
        }

        protected async void SearchButton_OnClickAsync(object sender, EventArgs e)
        {
            string parameter = txt_Search.Text.ToString();
            try
            {
                var response = await InitialFilesTable(parameter);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        private void listViewResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object t = FilesTable.SelectedItems[0];
            System.Reflection.PropertyInfo pi = t.GetType().GetProperty("FileName");
            chosenFileName = (String)(pi.GetValue(t, null));
        }
        private void Listener()
        {
            TcpListener server = null;
            // Set the TcpListener on port 13000.
            string port_asString = ConfigurationManager.ConnectionStrings[USER_PORT].ToString();
            Int32 port = Convert.ToInt32(port_asString);
            IPAddress localAddr = IPAddress.Parse(_proxy._user.IPAdress);

            // TcpListener server = new TcpListener(port);
            server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();

            // Buffer for reading data
            byte[] bytes = new Byte[256];
            byte[] dataFrom = new byte[4];
            byte[] dataTo = new byte[4];
            byte[] dataFileName = new byte[256];
            byte[] file;

            // Enter the listening loop.
            while (IsToListen)
            {
                // Perform a blocking call to accept requests.
                // You could also user server.AcceptSocket() here.
                client = server.AcceptTcpClient();
                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();
                int i;
                int from;
                int to;
                string filename;
                // Loop to receive all the data sent by the client.
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    Array.Copy(bytes, dataFrom, dataFrom.Length);
                    Array.Copy(bytes, 4, dataTo, 0, dataTo.Length);
                    Array.Copy(bytes, 8, dataFileName, 0, bytes.Length - 8);
                    from = BitConverter.ToInt32(dataFrom, 0);
                    to = BitConverter.ToInt32(dataTo, 0);
                    from = BitConverter.ToInt32(dataFrom, 0);
                    filename = System.Text.Encoding.ASCII.GetString(bytes, 8, i - 8);
                    file = GetFile(from, to, filename);
                    stream.Write(file, 0, file.Length);
                }
                client.Close();
            }
        }

        private byte[] GetFile(int from, int to, string fileName)
        {
            string full_path = uploadPath + "\\" + fileName;
            byte[] block = new byte[to-from];
            var tempResult = File.ReadAllBytes(full_path);
            for (int i = from; i < to; i++)
            {
                block[i - from] = tempResult[i];
            }
            
            return block;
        }

        private async Task<List<string>> GetListOfIPAdresses(string FileName)
        {
            using (var client = new HttpClient())
            {
                string connection_string = ConfigurationManager.ConnectionStrings[IPADRESSPERFILE].ToString();
                string url = connection_string + "/?FileName=" + FileName;
                HttpResponseMessage response;
                response = await client.GetAsync(url);
                string responseBody = response.Content.ReadAsStringAsync().Result;
                List<string> myFilesList = JsonConvert.DeserializeObject<List<string>>(responseBody);
                return myFilesList;
            }

          
        }
        private async void Window_Closing(object sender, CancelEventArgs e)
        {
            string connection_string = ConfigurationManager.ConnectionStrings[CONNECTIONSLOGOUTRINGS].ToString();
            string url = connection_string + "/?username=" + _proxy._user.UserName;
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
            }

            IsToListen = false;
            //client.Close();

        }
        private async void UploadFilesToServer()
        {
            if (_proxy._user == null || string.IsNullOrEmpty(uploadPath))
                return;
            string connectionString = ConfigurationManager.ConnectionStrings[UPDATEFILESINSERVER].ToString();
            var dir = new DirectoryInfo(uploadPath);
            System.IO.FileInfo[] files = dir.GetFiles();
            List<FilesInfo> fileInfos = new List<FilesInfo>();
            foreach (var fileInfo in files)
            {
                FilesInfo the_file = new FilesInfo();
                the_file.FileName = fileInfo.Name;
                the_file.Location = fileInfo.DirectoryName;
                the_file.Size = fileInfo.Length;
                fileInfos.Add(the_file);
            }
            FilesPerUser filesPerUser = new FilesPerUser();
            filesPerUser._User = _proxy._user;
            filesPerUser._FilesList = fileInfos;
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(connectionString, filesPerUser);
                //  return response.IsSuccessStatusCode;
            }

        }
        private void Conncet_Click(object sender, RoutedEventArgs e)
        {
            Task<DownloadFile> task = Task<DownloadFile>.Run(() =>
            {
                return DownloadFile();
            });
            myDownloadFiles.Add(task.Result);
            DownloadTable.ItemsSource = myDownloadFiles;


        }

        private async Task<DownloadFile> DownloadFile()
        {
            if (string.IsNullOrEmpty(chosenFileName))
            {
                MessageBox.Show("Please choose a file");
                return null;
            }

            List<string> IpAdressList = await GetListOfIPAdresses(chosenFileName);
            int peers = 0;
            double size = 0;
            foreach (FilesInfo fileInfo in myFilesList)
            {
                if (fileInfo.FileName.Equals(chosenFileName))
                {
                    peers = fileInfo.Amount_Of_Peers;
                    size = fileInfo.Size;
                }
            }

            DownloadFile download = new DownloadFile(chosenFileName, size);
            string port_asString = ConfigurationManager.ConnectionStrings[USER_PORT].ToString();
            Int32 port = Convert.ToInt32(port_asString);
            var list = new List<TaskPackage>();
            for (int i = 0; i < peers; i++)
            {
                TaskPackage taskPackage = new TaskPackage(i, peers, port, size, chosenFileName, IpAdressList[i]);
                list.Add(taskPackage);
                taskPackage.Task.Start();
            }

            int file_size = Convert.ToInt32(size) + 1;
            FileStream fileStream = null;
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Task.WaitAny(list[i].Task);
                }

                fileStream = File.Open(downloadPath + "\\" + chosenFileName, FileMode.Create);
                for (int i = 0; i < list.Count; i++)
                {
                    fileStream.Write(list[i].Bytes, 0, list[i].Bytes.Length);
                }
              

                download.SetEndTime();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                fileStream.Close();
            }

            try
            {
                fileStream = File.Open(uploadPath + "\\" + chosenFileName, FileMode.Create);
                for (int i = 0; i < list.Count; i++)
                {
                    fileStream.Write(list[i].Bytes, 0, list[i].Bytes.Length);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                fileStream.Close();
            }
            UploadFilesToServer();
            return download;
        }
    }
}
