using ChecklistApp.Models.Enums;
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
      private string _action;
      private string _desc;
      private string _info;
      private int _index;

      private bool _checked;
      #endregion

      #region - Constructors
      public CheckItem()
      {
         Action = "NEW ITEM";
      }
      #endregion

      #region - Methods

      #endregion

      #region - Full Properties
      public string Action
      {
         get { return _action; }
         set
         {
            _action = value;
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
            //OnPropertyChanged(nameof(IndexDisplay));
         }
      }

      public bool Checked
      {
         get { return _checked; }
         set
         {
            _checked = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}
