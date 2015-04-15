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

            Login login = new Login(ContentVisibility);
            MainWondowFrame.Content = login;
        }
        private void AddDocumentation_Click(object sender, RoutedEventArgs e)
        {
            AddDocumentationPage addDocumentationPage = new AddDocumentationPage();
            MainWondowFrame.Content = addDocumentationPage;
        }
        private void AddSupporter_Click(object sender, RoutedEventArgs e)
        {
            AddSupporterPage addSupporterPage = new AddSupporterPage();
            MainWondowFrame.Content = addSupporterPage;
        }
        private void SearchDocumentation_Click(object sender, RoutedEventArgs e)
        {
            SearchDocumentationPage searchDocumentationPage = new SearchDocumentationPage();
            MainWondowFrame.Content = searchDocumentationPage;
        }
        private void Logout_OnClick(object sender, RoutedEventArgs e)
        {
            Login login = new Login(ContentVisibility);
            MainWondowFrame.Content = login;

            var contentVisibility = NavigationDockPanel.DataContext as ContentVisibility;
            var vis = contentVisibility != null && contentVisibility.LoginVisibility;

            var visibility = NavigationDockPanel.DataContext as ContentVisibility;
            if (visibility != null)
                visibility.LoginVisibility = !vis;
        }
    }
}
