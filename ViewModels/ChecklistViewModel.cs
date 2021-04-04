using ChecklistApp.Events;
using ChecklistApp.Models;
using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChecklistApp.ViewModels
{
   public class ChecklistViewModel : ViewModel
   {
      #region - Fields & Properties
      public static event EventHandler<UpdateTagsEventArgs> UpdateTagsEvent;
      private Aircraft _currentAircraft;
      private Checklist _selectedChecklist;
      private CheckItem _selectedItem;
      private int _fontSize = 18;

      public Command NewChecklistCmd { get; init; }
      public Command NewCheckItemCmd { get; init; }
      public Command SaveAllCmd { get; init; }
      public Command DeleteChecklistCmd { get; init; }
      public Command DeleteCheckItemCmd { get; init; }
      #endregion

      #region - Constructors
      public ChecklistViewModel()
      {
         AircraftViewModel.LoadAircraftEvent += LoadNewAircraft;
         NewChecklistCmd = new Command((o) => NewChecklist());
         NewCheckItemCmd = new Command((o) => NewCheckItem());
         SaveAllCmd = new Command((o) => MainViewModel.SaveAll());
         DeleteChecklistCmd = new Command((o) => DeleteChecklist());
         DeleteCheckItemCmd = new Command((o) => DeleteCheckItem());
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

      public void UpdateTags()
      {
         UpdateTagsEvent?.Invoke(this, new(CurrentAircraft));
      }

      public void UpdateTag(Tag tag)
      {
         if (tag is not null)
         {
            var checklist = CurrentAircraft.Checklists.First(ch => ch.Tags.Contains(tag));
            if (checklist is not null)
            {
               var foundTag = checklist.Tags.First(t => t == tag);
               foundTag = tag;
               UpdateTags();
            }
         }
      }

      public void DeleteTag(Tag tag)
      {
         var checklist = CurrentAircraft.Checklists.First(ch => ch.Tags.Contains(tag));
         if (checklist is not null)
         {
            checklist.Tags.Remove(tag);
         }
      }

      public void DeleteChecklist()
      {
         if (CurrentAircraft is null || CurrentAircraft.Checklists is null) return;

         if (MessageBox.Show("U Sure??", "Wait..", MessageBoxButton.YesNo) == MessageBoxResult.No) return;

         CurrentAircraft.Checklists.Remove(SelectedChecklist);
         SelectedChecklist = null;
      }

      public void DeleteCheckItem()
      {
         if (SelectedChecklist is null || SelectedChecklist.Items is null) return;
         SelectedChecklist.Items.Remove(SelectedItem);
         SelectedItem = null;
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

      public int FontSize
      {
         get { return _fontSize; }
         set
         {
            _fontSize = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}
