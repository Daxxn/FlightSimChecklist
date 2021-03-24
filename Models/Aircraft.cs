using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.Models
{
   public enum AircraftType
   {
      Other,
      Prop,
      TurboProp,
      Jet,
      Heli
   };

   public class Aircraft : Model
   {
      #region - Fields & Properties
      private string _make;
      private string _model;
      private string _subModel;
      private AircraftType _type;
      public int Version { get; set; }

      private ObservableCollection<Checklist> _checklists;
      #endregion

      #region - Constructors
      public Aircraft() { }
      #endregion

      #region - Methods
      public override string ToString()
      {
         return $"{Make} {Model} {SubModel} ({Type})";
      }
      public string ChecklistString()
      {
         return $"{Make}{Model}{SubModel}{(int)Type}{Version}";
      }
      #endregion

      #region - Full Properties
      public string Make
      {
         get { return _make; }
         set
         {
            _make = value;
            OnPropertyChanged();
         }
      }

      public string Model
      {
         get { return _model; }
         set
         {
            _model = value;
            OnPropertyChanged();
         }
      }

      public string SubModel
      {
         get { return _subModel; }
         set
         {
            _subModel = value;
            OnPropertyChanged();
         }
      }

      public AircraftType Type
      {
         get { return _type; }
         set
         {
            _type = value;
            OnPropertyChanged();
         }
      }

      public ObservableCollection<Checklist> Checklists
      {
         get { return _checklists; }
         set
         {
            _checklists = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}
