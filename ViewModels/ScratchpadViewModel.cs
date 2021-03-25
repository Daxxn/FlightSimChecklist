using Microsoft.Win32;
using MVVMLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChecklistApp.ViewModels
{
   public class ScratchpadViewModel : ViewModel
   {
      #region - Fields & Properties
      private readonly List<FileDialogCustomPlace> _customPlaces = new()
      {
         new FileDialogCustomPlace(@"C:\Users\Cody\Documents\MSFSFreqNotes"),
         new FileDialogCustomPlace(@"D:\FlightSimData\CustomChecklists\ScratchPadNotes"),
      };
      private string _savePath;
      private string _scratchNotes;
      private int _fontSize = 16;

      public Command IncreaseFontSizeCmd { get; init; }
      public Command DecreaseFontSizeCmd { get; init; }

      public Command SaveCmd { get; init; }
      public Command OpenCmd { get; init; }

      public Command ClearAllCmd { get; init; }
      #endregion

      #region - Constructors
      public ScratchpadViewModel()
      {
         IncreaseFontSizeCmd = new Command((o) => FontSize++);
         DecreaseFontSizeCmd = new Command((o) => FontSize--);
         SaveCmd = new Command((o) => Save());
         OpenCmd = new Command((o) => Open());
         ClearAllCmd = new Command((o) => ScratchPadNotes = "");
      }
      #endregion

      #region - Methods
      public void Save()
      {
         try
         {
            SaveFileDialog dialog = new()
            {
               AddExtension = true,
               DefaultExt = ".scr",
               Title = "Save Scratchpad",
               OverwritePrompt = true,
               CustomPlaces = _customPlaces,
               Filter = "Scratch|*.scr|All|*.*",
               FilterIndex = 0
            };

            if (dialog.ShowDialog() is not true) return;

            SavePath = dialog.FileName;

            using StreamWriter writer = new(SavePath);
            writer.Write(ScratchPadNotes);
            writer.Flush();
         }
         catch (Exception e)
         {
            MessageBox.Show($"Unable to save scratchpad. {e.Message}");
         }
      }

      public void Open()
      {
         try
         {
            OpenFileDialog dialog = new()
            {
               AddExtension = true,
               DefaultExt = ".scr",
               CustomPlaces = _customPlaces,
               Multiselect = false,
               Filter = "Scratch|*.scr|Text|*.txt|All|*.*",
               FilterIndex = 0,
               Title = "Open ScratchPad Notes"
            };

            if (dialog.ShowDialog() is not true) return;

            SavePath = dialog.FileName;

            using StreamReader reader = new(SavePath);
            ScratchPadNotes = reader.ReadToEnd();
         }
         catch (Exception e)
         {
            MessageBox.Show($"Unable to open scratchpad. {e.Message}");
         }
      }
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

      public string ScratchPadNotes
      {
         get { return _scratchNotes; }
         set
         {
            _scratchNotes = value;
            OnPropertyChanged();
         }
      }
      #endregion
   }
}
