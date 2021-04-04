using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.Models
{
    public record ChartsDataModel
    {
      #region - Fields & Properties
      public string Name { get; set; }
      public Aircraft Aircraft { get; set; }
      public List<Chart> Charts { get; set; }
      #endregion

      #region - Constructors
      public ChartsDataModel() { }
      #endregion

      #region - Methods

      #endregion

      #region - Full Properties

      #endregion
    }
}
