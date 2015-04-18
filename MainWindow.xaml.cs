using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DevelopmentProject.PeterGUI.Pages;
using Model;

namespace DevelopmentProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ContentVisibility ContentVisibility { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ContentVisibility = new ContentVisibility();
            NavigationDockPanel.DataContext = ContentVisibility;
            ManageSupportersButton.DataContext = ContentVisibility;

            Login login = new Login(ContentVisibility);
            MainWondowFrame.Content = login;
        }
        /// <summary>
        /// Open AddDocumentationPage method
        /// </summary>
        private void AddDocumentation_Click(object sender, RoutedEventArgs e)
        {
            AddDocumentationPage addDocumentationPage = new AddDocumentationPage();
            MainWondowFrame.Content = addDocumentationPage;
        }
        /// <summary>
        /// Open AddSupporterPage method
        /// </summary>
        private void AddSupporter_Click(object sender, RoutedEventArgs e)
        {
            AddSupporterPage addSupporterPage = new AddSupporterPage();
            MainWondowFrame.Content = addSupporterPage;
        }
        /// <summary>
        /// Open SearchDocumentationPage method
        /// </summary>
        private void SearchDocumentation_Click(object sender, RoutedEventArgs e)
        {
            SearchDocumentationPage searchDocumentationPage = new SearchDocumentationPage(ContentVisibility);
            MainWondowFrame.Content = searchDocumentationPage;
        }
        /// <summary>
        /// logout method
        /// </summary>
        private void Logout_OnClick(object sender, RoutedEventArgs e)
        {
            //Opens up login page
            Login login = new Login(ContentVisibility);
            MainWondowFrame.Content = login;

            //resets the login visibility property
            var contentVisibility = NavigationDockPanel.DataContext as ContentVisibility;
            var vis = contentVisibility != null && contentVisibility.LoginVisibility;

            var visibility = NavigationDockPanel.DataContext as ContentVisibility;
            if (visibility != null)
                visibility.LoginVisibility = !vis;

            //resets the user visibility property
            var contentVisibility2 = ManageSupportersButton.DataContext as ContentVisibility;
            var vis2 = contentVisibility2 != null && contentVisibility2.UserVisibility;

            var visibility2 = ManageSupportersButton.DataContext as ContentVisibility;
            if (visibility2.UserVisibility)
                visibility2.UserVisibility = !vis2;
        }
    }
}
