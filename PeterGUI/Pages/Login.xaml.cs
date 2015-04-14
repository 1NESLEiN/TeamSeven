using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Control;
using Model;

namespace DevelopmentProject.PeterGUI.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private readonly Handler _handler;
        private DataTable _login;
        public ContentVisibility ContentVisibility { get; set; }

        public Login(ContentVisibility contentVisibility)
        {
            InitializeComponent();
            _handler = Handler.GetInstance();

            DataContext = contentVisibility;
        }

        private void Login_OnClick(object sender, RoutedEventArgs e)
        {
            if (TextBoxSupporterName.Text == "" || TextBoxSupporterPass.Text == "")
            {
                MessageBox.Show("You need to type both your username and password");
            }
            else
            {
                try
                {
                    _login = _handler.Login(TextBoxSupporterName.Text, TextBoxSupporterPass.Text);

                    if (_login.Rows.Count != 0)
                    {
                        var contentVisibility = DataContext as ContentVisibility;
                        var vis = contentVisibility != null && contentVisibility.LoginVisibility;

                        var visibility = DataContext as ContentVisibility;
                        if (visibility != null)
                            visibility.LoginVisibility = !vis;

                        if (NavigationService != null) NavigationService.Navigate(new SearchDocumentationPage());
                    }
                    else
                    {
                        MessageBox.Show("Your Username or Password were incorrect");
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
