using ChecklistApp.Models;
using JsonReaderLibrary;
using Microsoft.Win32;
using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChecklistApp.ViewModels
{
   public class ChartViewModel : ViewModel
   {
      #region - Fields & Properties
      private string _name;
      private string _chartsDir;
      private string _chartDataPath;
      private Aircraft _currentAircraft;
      private ObservableCollection<Chart> _charts;
      private Chart _selectedChart;

      public Command OpenChartsFileCmd { get; init; }
      public Command OpenChartsCmd { get; init; }
      public Command SaveChartsFileCmd { get; init; }
      #endregion

      #region - Constructors
      public ChartViewModel()
      {
         OpenChartsFileCmd = new((o) => OpenChartsFile());
         OpenChartsCmd = new((o) => OpenCharts());
         SaveChartsFileCmd = new((o) => SaveChartsFile());
      }
      #endregion

      #region - Methods
      public void OpenChartsFile()
      {
         OpenFileDialog dialog = new()
         {
            CustomPlaces = MainViewModel.CustomChartPlaces,
            Multiselect = false,
            AddExtension = false,
            Title = "Open Chart Data File"
         };

         if (dialog.ShowDialog() is not true) return;

         ChartsDir = dialog.FileName;
      }

      public void OpenCharts()
      {
         OpenFileDialog dialog = new()
         {
            CustomPlaces = MainViewModel.CustomChartPlaces,
            Multiselect = true,
            AddExtension = false,
            Title = "Open Chart Data File"
         };

         if (dialog.ShowDialog() is not true) return;

         BuildChartList(dialog.FileNames);
      }

      public void BuildChartList(string[] chartPaths)
      {
         Charts = new();
         foreach (var path in chartPaths)
         {
            Charts.Add(new() { FilePath = path });
         }
      }

      public void SaveChartsFile()
      {
         SaveFileDialog dialog = new()
         {
            Title = "Save Chart Data File",
            AddExtension = true,
            DefaultExt = ".chd",
            Filter = "Chart Data|*.chd|Json|*.json|All|*.*",
            CustomPlaces = MainViewModel.CustomChartPlaces,
            OverwritePrompt = true
         };

         if (dialog.ShowDialog() is not true) return;

         if (Charts is not null)
         {
            try
            {
               JsonReader.SaveJsonFile(dialog.FileName, new ChartsDataModel
               {
                  Charts = Charts.ToList(),
                  Aircraft = CurrentAircraft,
                  Name = ChartsFileName
               });
            }
            catch (Exception e)
            {
               MessageBox.Show(e.Message, "Save Error");
            }
         }
      }
      #endregion

      #region - Full Properties
      public string ChartsFileName
      {
         get { return _name; }
         set
         {
            _name = value;
            OnPropertyChanged();
         }
      }

      public string ChartsDir
      {
         get { return _chartsDir; }
         set
         {
            _chartsDir = value;
            OnPropertyChanged();
         }
      }

      public string ChartDataPath
      {
         get { return _chartDataPath; }
         set
         {
            _chartDataPath = value;
            OnPropertyChanged();
         }
      }

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
