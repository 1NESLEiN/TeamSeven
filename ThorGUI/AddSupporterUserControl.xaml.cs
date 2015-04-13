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
using Control;

namespace DevelopmentProject
{
   /// <summary>
   /// Interaction logic for AddSupporterUserControl.xaml
   /// </summary>
   public partial class AddSupporterUserControl : UserControl
   {
      public AddSupporterUserControl()
      {
         InitializeComponent();
         _handler = Handler.GetInstance();
      }

      private readonly Handler _handler;

      private void AddSupporter_OnClick(object sender, RoutedEventArgs e)
      {
         //try
         //{
         //   if (TxtBoxName.Text == String.Empty || TxtBoxInitials.Text == String.Empty)
         //   {
         //      MessageBox.Show("Udfyld alle felter", "Udfyld felter", MessageBoxButton.OK, MessageBoxImage.Exclamation);
         //   }
         //   else
         //   {
         //      bool success = _handler.AddSupporter(TxtBoxName.Text, TxtBoxInitials.Text, txtBoxPassword.Text, ComboboxAccess.SelectedIndex);
         //      if (success)
         //      {
         //         MessageBox.Show("Supporter added succesfully");
         //         TxtBoxName.Clear();
         //         TxtBoxInitials.Clear();
         //      }
         //   }
         //}
         //catch (Exception exception)
         //{
         //   MessageBox.Show("Der opstod en fejl: " + exception.Message, "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
         //}
      }
   }
}
