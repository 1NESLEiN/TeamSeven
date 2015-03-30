using System.Windows;
using System.Windows.Controls;

namespace DevelopmentProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Page[] _pages = new Page[] { new AddDocumentationPage(), new AddSupporterPage(), new SearchDocumentationPage() };

        #region Window GUI
        //private void BtnAddSupporter_OnClick(object sender, RoutedEventArgs e)
        //{
        //    AddSupporterWindow addSupporterWindow = new AddSupporterWindow();
        //    addSupporterWindow.Show();
        //}

        //private void BtnAddDocumentation_OnClick(object sender, RoutedEventArgs e)
        //{
        //    AddDocumentationWindow addDocumentationWindow = new AddDocumentationWindow();
        //    addDocumentationWindow.Show();
        //}
        #endregion

        #region Old GUI controls
        //private void SupporterNew_OnClick(object sender, RoutedEventArgs e)
       //{

       //   UserControlPanel.Children.Clear();

       //   AddSupporterUserControl addSupporter = new AddSupporterUserControl();
       //   UserControlPanel.Children.Add(addSupporter);
       //}
       //private void DocumentationNew_OnClick(object sender, RoutedEventArgs e)
       //{
       //   UserControlPanel.Children.Clear();

       //   AddDocumentationUserControl addDocumentation = new AddDocumentationUserControl();
       //   UserControlPanel.Children.Add(addDocumentation);
       //}
       //private void DocumentationSearch_OnClick(object sender, RoutedEventArgs e)
       //{
       //    UserControlPanel.Children.Clear();
       //   SearchDocumentationUserControl SearchDocumentation = new SearchDocumentationUserControl();
       //   UserControlPanel.Children.Add(SearchDocumentation);
        //}
        #endregion

        private void AddDocumentation_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = _pages[0];
        }

        private void AddSupporter_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = _pages[1];
        }

        private void SearchDocumentation_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = _pages[2];
        }
    }
}
