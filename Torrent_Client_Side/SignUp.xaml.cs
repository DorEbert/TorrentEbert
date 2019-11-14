using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Common.Models;

namespace Torrent_Client_Side
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(ifc);

        }
        private IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "0vkUIyQqDLMGQienml0tNqMrDJZtSgub32sy4wzF",
            BasePath = "https://torrentebert.firebaseio.com/"
        };
        private IFirebaseClient client;

        private void Button_SignUp_Click(object sender, RoutedEventArgs e)
        {
            #region Condition
            if (string.IsNullOrWhiteSpace(txt_User_FullName.Text) ||
                string.IsNullOrWhiteSpace(txt_User_Password.Password) ||
                string.IsNullOrWhiteSpace(txt_User_Gender.Text) ||
                string.IsNullOrWhiteSpace(txt_User_FullName.Text) ||
                string.IsNullOrWhiteSpace(txt_User_Nic_Number.Text)) {
                MessageBox.Show("Please fill all the fields");
                return;
            }

        #endregion
            FireBaseUser user = new FireBaseUser( txt_User_FullName.Text,
                                                  txt_User_Password.Password,
                                                  txt_User_FullName.Text,
                                                  txt_User_Gender.Text,
                                                  txt_User_Nic_Number.Text);
            SetResponse set = client.Set(@"User/" + txt_User_FullName.Text, user);
            MessageBox.Show("Successfully registered!");
        }
    }
}
