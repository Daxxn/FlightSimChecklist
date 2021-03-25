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
      private ObservableCollection<string> _tags;
      private ObservableCollection<CheckItem> _items = new ObservableCollection<CheckItem>();
      #endregion

      #region - Constructors
      public Checklist()
      {
         Name = "New Checklist";
      }
      public Checklist(string name)
      {
         Name = name;
      }
      #endregion

      #region - Methods
      public void OrganizeItems()
      {
         BubbleSort.SortProperty(Items, (I) => I.Index);
      }
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
