using System;
using System.Windows;
using Control;

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
    }
}
