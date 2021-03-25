using ChecklistApp.Models;
using ChecklistApp.ViewModels;
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

namespace ChecklistApp.Views
{
   /// <summary>
   /// Interaction logic for ChecklistView.xaml
   /// </summary>
   public partial class ChecklistView : UserControl
   {
      private ChecklistViewModel VM { get; init; }
      public ChecklistView()
      {
         VM = new ChecklistViewModel();
         DataContext = VM;
         InitializeComponent();
      }

      private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
      {
         VM.SelectedChecklist?.OrganizeItems();
      }

      private void NewTag_Click(object sender, RoutedEventArgs e)
      {
         if (e.Source is Button btn)
         {
            if (btn.DataContext is Checklist checklist)
            {
               if (checklist.Tags is null) checklist.Tags = new();

               checklist.Tags.Add("New-Tag");
            }
         }
      }

      private void TextBox_GotFocus(object sender, RoutedEventArgs e)
      {
         if (sender is TextBox box)
         {
            box.SelectAll();
         }
      }
   }
}
