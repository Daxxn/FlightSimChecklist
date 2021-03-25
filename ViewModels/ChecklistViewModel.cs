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
      private CheckItem _selectedItem;

      public Command NewChecklistCmd { get; init; }
      public Command NewCheckItemCmd { get; init; }
      #endregion

      #region - Constructors
      public ChecklistViewModel()
      {
         AircraftViewModel.LoadAircraftEvent += LoadNewAircraft;
         NewChecklistCmd = new Command((o) => NewChecklist());
         NewCheckItemCmd = new Command((o) => NewCheckItem());
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

      public void NewChecklist()
      {
         if (CurrentAircraft is null) return;

         CurrentAircraft.Checklists.Add(new Checklist());
      }

      public void NewCheckItem()
      {
         if (SelectedChecklist is null) return;

         CheckItem newItem = new();
         SelectedItem = newItem;
         if (SelectedChecklist.Items is null)
         {
            SelectedChecklist.Items = new ObservableCollection<CheckItem>();
         }
         else
         {
            SelectedItem.Index = SelectedChecklist.Items.Count;
         }
         SelectedChecklist.Items.Add(newItem);
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

      public CheckItem SelectedItem
      {
         get { return _selectedItem; }
         set
         {
            _selectedItem = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}
