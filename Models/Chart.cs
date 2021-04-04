using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistApp.Models
{
   public class Chart : Model
   {
      #region - Fields & Properties
      private string _filePath;
      private string _title;
      private ObservableCollection<string> _tags;
      private double _width;
      #endregion

      #region - Constructors
      public Chart() { }
      #endregion

      #region - Methods

      #endregion

      #region - Full Properties

      public string FilePath
      {
         get { return _filePath; }
         set
         {
            _filePath = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(FileNameDisplay));
         }
      }


      public string FileNameDisplay
      {
         get => FilePath is null ? Path.GetFileNameWithoutExtension(FilePath) : null;
      }

      public string Title
      {
         get { return _title; }
         set
         {
            _title = value;
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

      public double Width
      {
         get { return _width; }
         set
         {
            _width = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}
