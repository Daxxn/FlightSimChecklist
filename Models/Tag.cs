using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.Models
{
   public class Tag : Model
   {
      #region - Fields & Properties
      private string _value;
      //private Guid _id;
      #endregion

      #region - Constructors
      //public Tag() => Id = Guid.NewGuid();
      public Tag() { }
      #endregion

      #region - Methods

      #endregion

      #region - Full Properties
      public string Value
      {
         get { return _value; }
         set
         {
            _value = value;
            OnPropertyChanged();
         }
      }

      //public Guid Id
      //{
      //   get { return _id; }
      //   set
      //   {
      //      _id = value;
      //      OnPropertyChanged();
      //   }
      //}
      #endregion
   }
}
