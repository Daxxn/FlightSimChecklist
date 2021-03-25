using ChecklistApp.Models;
using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.ViewModels
{
   public class ChartViewModel : ViewModel
   {
      #region - Fields & Properties
      private Aircraft _currentAircraft;
      private ObservableCollection<Chart> _charts;
      private Chart _selectedChart;
      #endregion

      #region - Constructors
      public ChartViewModel() { }
      #endregion

      #region - Methods

      #endregion

      #region - Full Properties
      public Aircraft CurrentAircraft
      {
         get { return _currentAircraft; }
         set
         {
            _currentAircraft = value;
            OnPropertyChanged();
         }
      }

      public ObservableCollection<Chart> Charts
      {
         get { return _charts; }
         set
         {
            _charts = value;
            OnPropertyChanged();
         }
      }

      public Chart SelectedChart
      {
         get { return _selectedChart; }
         set
         {
            _selectedChart = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}
