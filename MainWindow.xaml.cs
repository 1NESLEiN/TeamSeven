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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;

namespace DevelopmentProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void AddSupporter(object sender, RoutedEventArgs e)
        {
            using (var supctx = new SupporterContext())
            {
                Supporter supporter = new Supporter
                {
                    SupporterName = txtBoxName.Text,
                    SupporterInitials = txtBoxInitials.Text
                };

                supctx.Supporters.Add(supporter);
                supctx.SaveChanges();
            }

        }
    }
}
