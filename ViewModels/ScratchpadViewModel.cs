using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.ViewModels
{
   public class ScratchpadViewModel : ViewModel
   {
      #region - Fields & Properties
      private string _savePath;
      private int _fontSize = 16;

      public Command IncreaseFontSizeCmd { get; init; }
      public Command DecreaseFontSizeCmd { get; init; }
      #endregion

      #region - Constructors
      public ScratchpadViewModel()
      {
         IncreaseFontSizeCmd = new Command((o) => FontSize++);
         DecreaseFontSizeCmd = new Command((o) => FontSize--);
      }
      #endregion

      #region - Methods

      #endregion

      #region - Full Properties
      public string SavePath
      {
         get { return _savePath; }
         set
         {
            _savePath = value;
            OnPropertyChanged();
         }
      }

      public int FontSize
      {
         get { return _fontSize; }
         set
         {
            if (value <= 0) return;
            _fontSize = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}
