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
   public class MainViewModel : ViewModel
   {
      #region - Fields & Properties
      public static event EventHandler SaveAllEvent;

      public Command SaveAllCmd { get; init; }
      #endregion

      #region - Constructors
      public MainViewModel()
      {
         SaveAllCmd = new Command((o) =>
         {
            SaveAllEvent?.Invoke(this, null);
         });
      }
      #endregion

      #region - Methods
      public static void SaveAll()
      {
         SaveAllEvent?.Invoke(null, null);
      }
      #endregion

      #region - Full Properties

      #endregion
   }
}
