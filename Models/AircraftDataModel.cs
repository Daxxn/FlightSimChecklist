using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.Models
{
   public class AircraftDataModel
   {
      #region - Fields & Properties
      public IEnumerable<AircraftSaveModel> AircraftData { get; set; }
      public string ChecklistDir { get; set; }
      #endregion

      #region - Constructors
      public AircraftDataModel() { }
      #endregion

      #region - Methods
      public IEnumerable<Aircraft> LoadAircraft()
      {
         List<Aircraft> output = new();
         foreach (var ac in AircraftData)
         {
            output.Add(new Aircraft
            {
               Make = ac.Make,
               Model = ac.Model,
               SubModel = ac.SubModel,
               SavePath = ac.SavePath,
               Type = ac.Type,
               Version = ac.Version
            });
         }
         return output;
      }

      public void SaveAircraft(IEnumerable<Aircraft> aircraft)
      {
         var data = new List<AircraftSaveModel>();
         foreach (var ac in aircraft)
         {
            data.Add(ac.Save());
         }
         AircraftData = data;
      }
      #endregion

      #region - Full Properties

      #endregion
   }
}
