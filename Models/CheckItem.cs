using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.Models
{
   public class CheckItem : Model
   {
      #region - Fields & Properties
      private string _title;
      private string _desc;
      private string _info;
      private int _index;
      #endregion

      #region - Constructors
      public CheckItem() { }
      #endregion

      #region - Methods

      #endregion

      #region - Full Properties
      public string Title
      {
         get { return _title; }
         set
         {
            _title = value;
            OnPropertyChanged();
         }
      }

      public string Description
      {
         get { return _desc; }
         set
         {
            _desc = value;
            OnPropertyChanged();
         }
      }

      public string ExtraInfo
      {
         get { return _info; }
         set
         {
            _info = value;
            OnPropertyChanged();
         }
      }

      public int Index
      {
         get { return _index; }
         set
         {
            _index = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}
