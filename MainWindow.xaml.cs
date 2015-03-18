using System.Windows;

namespace DevelopmentProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void BtnAddSupporter_OnClick(object sender, RoutedEventArgs e)
        {
            AddSupporterWindow addSupporterWindow = new AddSupporterWindow();
            addSupporterWindow.Show();
        }

        private void BtnAddDocumentation_OnClick(object sender, RoutedEventArgs e)
        {
            AddDocumentationWindow addDocumentationWindow = new AddDocumentationWindow();
            addDocumentationWindow.Show();
        }
    }
}
