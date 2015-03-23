using System.Windows;

namespace DevelopmentProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

       private void SupporterNew_OnClick(object sender, RoutedEventArgs e)
       {
          //AddSupporterWindow addSupporterWindow = new AddSupporterWindow();
          //addSupporterWindow.Show();

          UserControlPanel.Children.Clear();

          AddSupporterUserControl AddSupporter = new AddSupporterUserControl();
          UserControlPanel.Children.Add(AddSupporter);
       }
       private void DocumentationNew_OnClick(object sender, RoutedEventArgs e)
       {
          UserControlPanel.Children.Clear();

          AddDocumentationUserControl addDocumentation = new AddDocumentationUserControl();
          UserControlPanel.Children.Add(addDocumentation);
       }
       private void DocumentationSearch_OnClick(object sender, RoutedEventArgs e)
       {
          MessageBox.Show("Documentation Search user Control here");
       }
    }
}
