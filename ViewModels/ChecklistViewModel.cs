using ChecklistApp.Events;
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
   public class ChecklistViewModel : ViewModel
   {
      #region - Fields & Properties
      private Aircraft _currentAircraft;
      private Checklist _selectedChecklist;
      #endregion

      #region - Constructors
      public ChecklistViewModel()
      {
         AircraftViewModel.LoadAircraftEvent += LoadNewAircraft;
         // Just for testing.
         CurrentAircraft = new Aircraft
         {
            Make = "TBM",
            Model = "930",
            Type = AircraftType.TurboProp,
            Version = 0,
            Checklists = new ObservableCollection<Checklist>
            {
               new Checklist
               {
                  Name = "Test",
                  Tags = new ObservableCollection<string>
                  {
                     "Pre-Flight",
                     "Basic",
                     "Testing-ONLY"
                  },
                  Items = new ObservableCollection<CheckItem>
                  {
                     new CheckItem
                     {
                        Action = "Master Switch",
                        State = State.ON,
                        Description = "Set master switch for main electrical system",
                        ExtraInfo = "",
                        Index = 1,
                     },
                     new CheckItem
                     {
                        Action = "Fuel Boost Pump",
                        State = State.ON,
                        Description = "Turn on boost pumps",
                        ExtraInfo = "Needs to be on during startup.",
                        Index = 2,
                     },
                     new CheckItem
                     {
                        Action = "Flaps",
                        State = State.TAKEOFF,
                        Description = "Set flaps for takeoff.",
                        ExtraInfo = "",
                        Index = 3,
                     },
                  }
               },
               new Checklist
               {
                  Name = "Test 2",
                  Tags = new ObservableCollection<string>{"Testing-ONLY" },
                  Items = new ObservableCollection<CheckItem>
                  {
                     new CheckItem
                     {
                        Action = "Master Switch",
                        State = State.ON,
                        Description = "Set master switch for main electrical system",
                        ExtraInfo = "",
                        Index = 1,
                     },
                     new CheckItem
                     {
                        Action = "Flaps",
                        State = State.TAKEOFF,
                        Description = "Set flaps for takeoff.",
                        ExtraInfo = "",
                        Index = 2,
                     },
                  }
               }
            }
         };
      }
      #endregion

      #region - Methods
      public void LoadNewAircraft(object sender, LoadAircraftEventArgs e)
      {
         CurrentAircraft = e.NewAircraft;
         if (e.NewAircraft.Checklists != null)
         {
            SelectedChecklist = CurrentAircraft.Checklists.Count > 0 ? CurrentAircraft.Checklists[0] : null;
         }
      }
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

      public Checklist SelectedChecklist
      {
         get { return _selectedChecklist; }
         set
         {
            _selectedChecklist = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}
