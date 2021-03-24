using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.Models
{
   public enum State
   {
      ON,
      OFF,
      SET,
      CHECKED,
      READY,
      UP,
      DOWN,
      TAKEOFF,
      LANDING
   };

   public class CheckItem : Model
   {
      #region - Fields & Properties
      private string _action;
      private State _state;
      private string _desc;
      private string _info;
      private int _index;

      private bool _checked;
      #endregion

      #region - Constructors
      public CheckItem() { }
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
      public State State
      {
         get { return _state; }
         set
         {
            _state = value;
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
