using ChecklistApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.Events
{
   public class UpdateTagsEventArgs : EventArgs
   {
      #region - Fields & Properties
      public Aircraft UpdatedAircraft { get; init; }
      #endregion

      #region - Constructors
      public UpdateTagsEventArgs(Aircraft updatedAircraft) => UpdatedAircraft = updatedAircraft;
      #endregion

      #region - Methods

      #endregion

      #region - Full Properties

      #endregion
   }
}
