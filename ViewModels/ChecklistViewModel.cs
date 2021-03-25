using ChecklistApp.Events;
using ChecklistApp.Models;
using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChecklistApp.ViewModels
{
   public class ChecklistViewModel : ViewModel
   {
      #region - Fields & Properties
      private Aircraft _currentAircraft;
      private Checklist _selectedChecklist;
      private CheckItem _selectedItem;
      private string _selectedTag;

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
         if (e.NewAircraft is null) return;
         if (e?.NewAircraft.Checklists != null)
         {
            SelectedChecklist = CurrentAircraft.Checklists.Count > 0 ? CurrentAircraft.Checklists[0] : null;
         }
      }

      public void NewChecklist()
      {
         if (CurrentAircraft is null) return;

         Checklist newChecklist = new();
         CurrentAircraft.Checklists.Add(newChecklist);
         SelectedChecklist = newChecklist;
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

      public static void NewTag(Checklist cl)
      {
         if (cl is null) return;

         if (cl.Tags is null) cl.Tags = new();

         cl.Tags.Add("New-Tag");
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

      public string SelectedTag
      {
         get { return _selectedTag; }
         set
         {
            _selectedTag = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}
