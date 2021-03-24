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
      private List<Checklist> AircraftChecklists { get; set; }

      public Command OpenAircraftDataCmd { get; init; }
      public Command OpenAircraftChecklistCmd { get; init; }
      public Command SelectAircraftCmd { get; init; }
      public Command CreateAircraftCmd { get; init; }
      public Command CreateChecklistCmd { get; init; }
      #endregion

      #region - Constructors
      public AircraftViewModel()
      {
         OpenAircraftDataCmd = new Command((o) => LoadAircraftData());
         OpenAircraftChecklistCmd = new Command((o) => LoadAircraftChecklistFile());
         SelectAircraftCmd = new Command((o) => LoadNewAircraft());
         CreateAircraftCmd = new Command((o) => CreateAircraft());
         CreateChecklistCmd = new Command((o) => CreateChecklist());
      }
      #endregion

      #region - Methods
      public void LoadAircraftData()
      {
         List<FileDialogCustomPlace> customPlaces = new List<FileDialogCustomPlace>
         {
            new FileDialogCustomPlace(@"D:\FlightSimData\CustomChecklists")
         };

         OpenFileDialog dialog = new OpenFileDialog
         {
            CustomPlaces = customPlaces,
            Title = "Open Aircraft Data File"
         };

         if (dialog.ShowDialog() == true)
         {
            try
            {
               AircraftDataSavePath = dialog.FileName;

               var aircraftData = JsonReader.OpenJsonFile<AircraftDataModel>(AircraftDataSavePath);

               if (aircraftData.AircraftData.Count() > 0)
               {
                  AircraftData = new ObservableCollection<Aircraft>(aircraftData.AircraftData);
                  LoadAircraftChecklistFile();
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
      }

      public void LoadAircraftChecklistFile(bool selectFirst = true)
      {
         try
         {
            if (selectFirst)
            {
               if (AircraftData.Count > 0)
               {
                  AircraftChecklists = JsonReader.OpenJsonFile<ChecklistDataModel>(Path.Combine(ChecklistDir, AircraftData[0].ChecklistString() + ".json")).Checklists;
               }
            }
            else
            {
               AircraftChecklists = JsonReader.OpenJsonFile<ChecklistDataModel>(Path.Combine(ChecklistDir, SelectedAircraft.ChecklistString() + ".json")).Checklists;
            }
         }
         catch (Exception e)
         {
            MessageBox.Show($"Error during checklist load. :: {e.Message}", "ERROR");
         }
      }

      public void LoadNewAircraft()
      {
         if (AircraftChecklists != null)
         {
            SelectedAircraft.Checklists = new ObservableCollection<Checklist>(AircraftChecklists);
         }
         LoadAircraftEvent?.Invoke(this, new LoadAircraftEventArgs(SelectedAircraft));
      }

      public void CreateAircraft()
      {
         var newAircraft = new Aircraft();
         AircraftData.Add(newAircraft);
         SelectedAircraft = newAircraft;
      }

      public void CreateChecklist()
      {
         if (SelectedAircraft is null) return;

         SelectedAircraft.Checklists.Add(new Checklist());
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
         }
      }

      public Aircraft SelectedAircraft
      {
         get { return _selectedAircraft; }
         set
         {
            _selectedAircraft = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}
