using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using Torrent_Server_Side.Commom.Models;
using FileInfo = Torrent_Server_Side.Commom.Models.FilesInfo;

namespace Torrent_Client_Side
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,IDisposable
    {
        public const string USER_ID = "User_Id";
        public const string USER_PASSWORD = "User_Password";
        public const string CONNECTIONSLOGINTRINGS = "ConnectionLoginStrings";
        public const string DIRECTORYUPLOADFILES = "DirectoryUploadFiles";       
        public const string DOWNLOADPATHFOLDER = "DownloadPathFolder";
        public const string UPLOADPATHFOLDER = "UploadPathFolder";
        public const string CONNECTIONSLOGOUTRINGS = "ConnectionLogoutStrings";
        public const string USER_IP = "USER-IP";
        public static string connection_string;
        private string downloadPath;
        private string uploadPath;
        private Proxy _proxy;


        public MainWindow()
        {
            InitializeComponent();
            try
            {
                this.User_Name.Text = ConfigurationManager.ConnectionStrings[USER_ID].ToString();
                this.User_Password.Password = ConfigurationManager.ConnectionStrings[USER_PASSWORD].ToString();
                connection_string = ConfigurationManager.ConnectionStrings[CONNECTIONSLOGINTRINGS].ToString();
                downloadPath = ConfigurationManager.ConnectionStrings[DOWNLOADPATHFOLDER].ToString();
                uploadPath = ConfigurationManager.ConnectionStrings[UPLOADPATHFOLDER].ToString();
                _proxy = new Proxy();
            }
            catch (Exception e)
            {
                    Console.WriteLine($"Can Not Initiate , error message: {e.Message}");       
            }
        }

        private async void Button_Log_In_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(uploadPath) || string.IsNullOrEmpty(downloadPath))
            {
                MessageBox.Show("Please choose upload and download folders");
                return;
            }
            string IPAdress = ConfigurationManager.ConnectionStrings[USER_IP].ToString();
            bool is_succeed = await _proxy.Login(this.User_Name.Text, this.User_Password.Password, IPAdress,connection_string);
            if (!is_succeed) return;
            FileTransfer sign_up_window = new FileTransfer(downloadPath, uploadPath, _proxy);
            this.Close();           
        }

        private void Button_UploadFolder(object sender, RoutedEventArgs e)
        {
            uploadPath = GetFileFromBrowser("Choose your upload folder");
        }

        private string GetFileFromBrowser(string Description)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();

            dlg.Description = Description;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return dlg.SelectedPath;
            }

            return null;
        }

        private void Button_DownloadFolder(object sender, RoutedEventArgs e)
        {
            downloadPath = GetFileFromBrowser("Choose your downloads folder");
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
