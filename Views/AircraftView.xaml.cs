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
   /// Interaction logic for AircraftView.xaml
   /// </summary>
   public partial class AircraftView : UserControl
   {
      public AircraftView()
      {
         DataContext = new AircraftViewModel();
         InitializeComponent();
      }
   }
}
