using ChecklistApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.Events
{
   public class SaveAllEventArgs : EventArgs
   {
      #region - Fields & Properties
      public Aircraft CurrentAircraft { get; set; }
      #endregion

      #region - Constructors
      public SaveAllEventArgs() { }
      #endregion
   }
}
