using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.Models
{
   public class Checklist : Model
   {
      #region - Fields & Properties
      private string _name;
      private Aircraft _aircraft;
      private ObservableCollection<string> _tags;
      private ObservableCollection<CheckItem> _items;
      #endregion

      #region - Constructors
      public Checklist() { }
      #endregion

      #region - Methods

      #endregion

      #region - Full Properties
      public string Name
      {
         get { return _name; }
         set
         {
            _name = value;
            OnPropertyChanged();
         }
      }

      public Aircraft Aircraft
      {
         get { return _aircraft; }
         set
         {
            _aircraft = value;
            OnPropertyChanged();
         }
      }

      public ObservableCollection<string> Tags
      {
         get { return _tags; }
         set
         {
            _tags = value;
            OnPropertyChanged();
         }
      }

      public ObservableCollection<CheckItem> Items
      {
         get { return _items; }
         set
         {
            _items = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}
