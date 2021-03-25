using ChecklistApp.Events;
using ChecklistApp.Models;
using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.ViewModels
{
   public class TagManagerViewModel : ViewModel
   {
      #region - Fields & Properties
      private Aircraft _currentAircraft;
      private string _selectedTag;
      #endregion

      #region - Constructors
      public TagManagerViewModel()
      {
         AircraftViewModel.LoadAircraftEvent += NewAircraftEvent;
         ChecklistViewModel.UpdateTagsEvent += UpdateTags;
      }
      #endregion

      #region - Methods
      public void NewAircraftEvent(object sender, LoadAircraftEventArgs e)
      {
         CurrentAircraft = e.NewAircraft;
      }

      public void UpdateTags(object sender, UpdateTagsEventArgs e)
      {
         if (e is not null) CurrentAircraft = e.UpdatedAircraft;
         OnPropertyChanged(nameof(AllTags));
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
            OnPropertyChanged(nameof(AllTags));
         }
      }

      public List<string> AllTags
      {
         get
         {
            if (CurrentAircraft is null) return null;
            if (!CurrentAircraft.Checklists.Any()) return null;

            List<string> output = new();
            foreach (var cl in CurrentAircraft.Checklists)
            {
               if (cl.Tags != null)
               {
                  output.AddRange(cl.Tags);
               }
            }
            return output;
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
