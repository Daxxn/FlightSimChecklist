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
      #endregion

      #region - Constructors
      public TagManagerViewModel()
      {
         AircraftViewModel.LoadAircraftEvent += NewAircraftEvent;
      }
      #endregion

      #region - Methods
      public void NewAircraftEvent(object sender, LoadAircraftEventArgs e)
      {
         CurrentAircraft = e.NewAircraft;
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

      public List<string> AllTags
      {
         get
         {
            if (CurrentAircraft is null) return null;
            if (!CurrentAircraft.Checklists.Any()) return null;

            List<string> output = new();
            foreach (var cl in CurrentAircraft.Checklists)
            {
               output.AddRange(cl.Tags);
            }
            return output;
         }
      }
      #endregion
   }
}
