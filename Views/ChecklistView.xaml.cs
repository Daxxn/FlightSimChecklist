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

      private void IndexTextBox_TextChanged(object sender, TextChangedEventArgs e)
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

               checklist.Tags.Add(new() { Value = "NewTag" });
               VM.UpdateTags();
            }
         }
      }

      private void TextBox_GotFocus(object sender, RoutedEventArgs e)
      {
         if (sender is TextBox box)
         {
            box.Focus();
            box.SelectAll();
         }
         e.Handled = true;
      }

      private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
      {
         if (sender is TextBox box)
         {
            box.Focus();
            box.SelectAll();
            box.ScrollToEnd();
         }
         e.Handled = true;
      }

      private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
      {
         if (sender is TextBox box)
         {
            box.Focus();
            box.SelectAll();
            box.ScrollToEnd();
         }
         e.Handled = true;
      }

      private void TagTextBox_TextChanged(object sender, TextChangedEventArgs e)
      {
         if (e.Changes.Count == 0) return;

         if (sender is TextBox box)
         {
            VM.UpdateTag(box.DataContext as Tag);
         }
      }

      private void DeleteTag_Click(object sender, RoutedEventArgs e)
      {
         if (sender is MenuItem menu)
         {
            VM.DeleteTag(menu.DataContext as Tag);
         }
      }

      private void FontSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
      {
         if (ChecklistDataView is not null)
         {
            ChecklistDataView.FontSize = (int)e.NewValue;
         }
      }
   }
}
