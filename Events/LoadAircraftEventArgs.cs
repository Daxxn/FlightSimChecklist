using ChecklistApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.Events
{
   public class LoadAircraftEventArgs : EventArgs
   {
      #region - Fields & Properties
      public Aircraft NewAircraft { get; init; }
      #endregion

      #region - Constructors
      public LoadAircraftEventArgs(Aircraft newAircraft)
      {
         NewAircraft = newAircraft;
      }
      #endregion
   }
}
