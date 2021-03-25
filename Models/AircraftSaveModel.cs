using ChecklistApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.Models
{
   public class AircraftSaveModel
   {
      #region - Fields & Properties
      public string Make { get; set; }
      public string Model { get; set; }
      public string SubModel { get; set; }
      public AircraftType Type { get; set; }
      public int Version { get; set; }
      public string SavePath { get; set; }
      #endregion

      #region - Constructors
      public AircraftSaveModel() { }
      #endregion

      #region - Methods

      #endregion

      #region - Full Properties

      #endregion
   }
}
