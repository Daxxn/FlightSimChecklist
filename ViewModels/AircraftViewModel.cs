using ChecklistApp.Events;
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
using System.Windows.Media;

namespace ChecklistApp.ViewModels
{
   public class AircraftViewModel : ViewModel
   {
      #region - Fields & Properties
      public static event EventHandler<LoadAircraftEventArgs> LoadAircraftEvent;
      private string _checklistDir;
      private string _aircraftDataSavePath;
      private ObservableCollection<Aircraft> _aircraftData;
      private Aircraft _selectedAircraft;

      private bool _keepChecklistsCompleted = true;

      public Command NewAircraftDataCmd { get; init; }
      public Command OpenAircraftDataCmd { get; init; }
      public Command OpenAircraftChecklistCmd { get; init; }

      public Command CreateAircraftCmd { get; init; }
      public Command CreateChecklistCmd { get; init; }

      public Command SaveAllAircraftCmd { get; init; }
      public Command SaveAllAircraftAsCmd { get; init; }
      public Command SaveSelectedAircraftCmd { get; init; }
      public Command SaveAllChecklistsCmd { get; init; }

      public Command SelectChecklistDirCmd { get; init; }
      #endregion

      #region - Constructors
      public AircraftViewModel()
      {
         NewAircraftDataCmd = new Command((o) => NewAircraftData());
         OpenAircraftDataCmd = new Command((o) => LoadAircraftData());
         OpenAircraftChecklistCmd = new Command((o) => LoadAircraftChecklistFile());
         CreateAircraftCmd = new Command((o) => CreateAircraft());
         CreateChecklistCmd = new Command((o) => CreateChecklist());
         SaveAllAircraftCmd = new Command((o) => SaveAllAircraft());
         SaveAllAircraftAsCmd = new Command((o) => SaveAllAircraftAs());
         SaveSelectedAircraftCmd = new Command((o) => SaveSelectedAircraft());
         SaveAllChecklistsCmd = new Command((o) => SaveAllChecklists());
         SelectChecklistDirCmd = new Command((o) => SelectChecklistDir());

         MainViewModel.SaveAllEvent += MainViewModel_SaveAllEvent;
      }
      #endregion

      #region - Methods
      private void MainViewModel_SaveAllEvent(object sender, EventArgs e)
      {
         SaveAllAircraft();
      }

      public void NewAircraftData()
      {
         if (AircraftData is not null)
         {
            var result = MessageBox.Show("Theres one open already. Save it first??", "Hold On", MessageBoxButton.YesNoCancel);

            if (result == MessageBoxResult.Yes)
            {
               SaveAllAircraft();
            }
            else if (result == MessageBoxResult.Cancel)
            {
               return;
            }
         }

         AircraftData = null;
         ChecklistDir = null;
         AircraftDataSavePath = null;
         SelectedAircraft = null;

         AircraftData = new();
      }

      public void SelectChecklistDir()
      {
         OpenFileDialog dialog = new()
         {
            DefaultExt = null,
            CheckPathExists = false,
            CheckFileExists = false,
            AddExtension = false,
            ValidateNames = false,
            Title = "Select Checklist Dir"
         };

         if (dialog.ShowDialog() is not true) return;

         ChecklistDir = dialog.FileName;
      }

      public void LoadAircraftData()
      {
         List<FileDialogCustomPlace> customPlaces = new()
         {
            new FileDialogCustomPlace(@"D:\FlightSimData\CustomChecklists")
         };

         OpenFileDialog dialog = new()
         {
            CustomPlaces = customPlaces,
            Title = "Open Aircraft Data File"
         };

         if (dialog.ShowDialog() != true) return;

         try
         {
            AircraftDataSavePath = dialog.FileName;

            var aircraftData = JsonReader.OpenJsonFile<AircraftDataModel>(AircraftDataSavePath, false);

            if (aircraftData.AircraftData.Any())
            {
               AircraftData = new ObservableCollection<Aircraft>(aircraftData.LoadAircraft());
               ChecklistDir = aircraftData.ChecklistDir;
               if (AircraftData.Any())
               {
                  SelectedAircraft = AircraftData[0];
               }
               LoadChecklists();
            }
            else
            {
               MessageBox.Show("File doesnt contain any aircraft.", "Hmm...");
            }
         }
         catch (Exception e)
         {
            MessageBox.Show($"Error during aircraft load. :: {e.Message}", "ERROR");
         }
      }

      public void LoadChecklists()
      {
         if (AircraftData is null) return;

         var errors = new List<Exception>();
         foreach (var aircraft in AircraftData)
         {
            try
            {
               if (String.IsNullOrEmpty(aircraft.SavePath))
               {
                  string tempPath = Path.Combine(ChecklistDir, aircraft.ToChecklistString());
                  if (File.Exists(tempPath))
                  {
                     aircraft.Checklists = new(JsonReader.OpenJsonFile<List<Checklist>>(tempPath));
                  }
               }
               else
               {
                  aircraft.Checklists = new(JsonReader.OpenJsonFile<List<Checklist>>(aircraft.SavePath));
               }
            }
            catch (Exception e)
            {
               errors.Add(e);
            }
         }

         if (errors.Any())
         {
            throw new Exception("Problems loading some checklists.");
         }
      }

      public void LoadAircraftChecklistFile(bool selectFirst = true)
      {
         try
         {
            if (ChecklistDir != null && SelectedAircraft != null)
            {
               SelectedAircraft.SavePath = Path.Combine(ChecklistDir, SelectedAircraft.ToChecklistString());
            }

            if (selectFirst)
            {
               if (AircraftData.Count > 0)
               {
                  SelectedAircraft.Checklists = new ObservableCollection<Checklist>(JsonReader.OpenJsonFile<List<Checklist>>(SelectedAircraft.SavePath));
               }
            }
            else
            {
               SelectedAircraft.Checklists = new ObservableCollection<Checklist>(JsonReader.OpenJsonFile<List<Checklist>>(Path.Combine(ChecklistDir, SelectedAircraft.ToChecklistString())));
            }

            if (KeepChecklistsCompleted) ClearChecklistStatus();
         }
         catch (Exception e)
         {
            MessageBox.Show($"Error during checklist load. :: {e.Message}", "ERROR");
         }
      }

      public void ClearChecklistStatus()
      {
         foreach (var aircraft in AircraftData)
         {
            foreach (var checklist in aircraft.Checklists)
            {
               foreach (var item in checklist.Items)
               {
                  item.Checked = false;
               }
            }
         }
      }

      public void CreateAircraft()
      {
         if (AircraftData != null)
         {
            var newAircraft = new Aircraft();
            AircraftData.Add(newAircraft);
            SelectedAircraft = newAircraft;
         }
         else
         {
            MessageBox.Show("No aircraft data file selected. Open an aircraft file.", "Hold ON!!");
         }
      }

      public void CreateChecklist()
      {
         if (SelectedAircraft is null) return;

         SelectedAircraft.Checklists.Add(new Checklist());
      }

      public void SaveAllAircraft()
      {
         // Saves aircraft checklists first.
         SaveAllChecklists();

         try
         {
            AircraftDataModel data = new();
            data.SaveAircraft(AircraftData);
            data.ChecklistDir = ChecklistDir;
            JsonReader.SaveJsonFile(AircraftDataSavePath, data, true);

            MessageBox.Show("Data saved.", "Saved");
         }
         catch (Exception e)
         {
            MessageBox.Show($"Problems saving all aircraft. {e.Message}", "Hmm..");
         }
      }

      public void SaveAllAircraftAs()
      {
         SaveFileDialog dialog = new()
         {
            CustomPlaces = MainViewModel.CustomChecklistPlaces,
            Title = "Save Aircraft Data File",
            DefaultExt = ".ard",
            AddExtension = true,
            Filter = "Aircraft Data|*.ard|JSON|*.json|ALL|*.*",
            OverwritePrompt = true,
            CheckFileExists = false,
         };

         if (dialog.ShowDialog() is not true) return;

         AircraftDataSavePath = dialog.FileName;

         SaveAllAircraft();
      }

      public void SaveAllChecklists()
      {
         try
         {
            var errors = new List<Exception>();
            foreach (var aircraft in AircraftData)
            {
               try
               {
                  SaveAircraft(aircraft);
               }
               catch (Exception e)
               {
                  errors.Add(e);
               }
            }

            if (KeepChecklistsCompleted) ClearChecklistStatus();

            if (errors.Count > 0)
            {
               StringBuilder bd = new("Errors during save.\n");
               foreach (var err in errors)
               {
                  bd.AppendLine($"\t{err.Message}");
               }
               MessageBox.Show(bd.ToString(), "Hmm..");
            }
         }
         catch (Exception e)
         {
            MessageBox.Show($"Problems saving all checklists. {e.Message}", "Hmm..");
         }
      }

      public void SaveSelectedAircraft()
      {
         try
         {
            SaveAircraft(SelectedAircraft);
         }
         catch (Exception e)
         {
            MessageBox.Show($"Couldnt save aircraft. {e.Message}", "Hmm..");
         }
      }

      private void SaveAircraft(Aircraft aircraft)
      {
         if (String.IsNullOrEmpty(aircraft.SavePath))
         {
            aircraft.SavePath = Path.Combine(ChecklistDir, aircraft.ToChecklistString());
         }
         JsonReader.SaveJsonFile(aircraft.SavePath, aircraft.Checklists, true);
      }
      #endregion

      #region - Full Properties
      public string ChecklistDir
      {
         get { return _checklistDir; }
         set
         {
            _checklistDir = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(GoodChecklistDir));
         }
      }

      public string AircraftDataSavePath
      {
         get { return _aircraftDataSavePath; }
         set
         {
            _aircraftDataSavePath = value;
            OnPropertyChanged();
         }
      }

      public ObservableCollection<Aircraft> AircraftData
      {
         get { return _aircraftData; }
         set
         {
            _aircraftData = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(SelectedAircraft));
         }
      }

      public Aircraft SelectedAircraft
      {
         get { return _selectedAircraft; }
         set
         {
            _selectedAircraft = value;
            LoadAircraftEvent?.Invoke(this, new LoadAircraftEventArgs(value));
            OnPropertyChanged();
         }
      }


      public bool KeepChecklistsCompleted
      {
         get { return _keepChecklistsCompleted; }
         set
         {
            _keepChecklistsCompleted = value;
            OnPropertyChanged();
         }
      }


      public SolidColorBrush GoodChecklistDir
      {
         get => Directory.Exists(ChecklistDir) ? new(Color.FromRgb(0,255,0)) : new(Color.FromRgb(255,0,0));
      }
      #endregion
   }
}
