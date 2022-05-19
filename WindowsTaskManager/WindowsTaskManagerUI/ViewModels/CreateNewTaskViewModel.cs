
using System;
using System.Windows.Input;
using TaskManagerLibrary.Interfaces;
using TaskManagerLibrary.Models;
using WindowsTaskManagerUI.ViewModels.Command;
using System.Diagnostics;
using Microsoft.Win32;

namespace WindowsTaskManagerUI.ViewModels
{
    /// <summary>
    /// The <c>CreateNewTaskViewModel</c> class.
    /// Contains the commands, properties for the Create Task view,
    /// inherited BaseViewModel and implemented IWindowRequestClose
    /// </summary>
    public class CreateNewTaskViewModel : BaseViewModel, IWindowRequestClose
    {
        #region Constructor

        public CreateNewTaskViewModel()
        {
            OKCommand = new RelayCommand(a => OKCloseWindow(), p => EnableOKCommand());
            CancelCommand = new RelayCommand(a => CancelCloseWindow());
            BrowseCommand = new RelayCommand(a => BrowseFile());
        }

        #endregion

        #region Properties

        public string FileName { get; set; } = string.Empty;
        public ICommand OKCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand BrowseCommand { get; private set; }

        #endregion

        #region Events

        public event EventHandler<WindowCloseRequestedEventArgs> CloseRequested;

        #endregion

        #region Private Methods

        private bool EnableOKCommand()
        {
            return FileName != string.Empty;
        }

        private void OKCloseWindow()
        {
            CloseRequested?.Invoke(this, new WindowCloseRequestedEventArgs(true));
        }

        private void CancelCloseWindow()
        {
            CloseRequested?.Invoke(this, new WindowCloseRequestedEventArgs(false));
        }

        private void BrowseFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                DefaultExt = "*.exe",
                Filter = "Programs |*.bin;*.exe;.*.o|All files |*.*",
                FilterIndex = 0,
                InitialDirectory = Environment.SystemDirectory
            };
            bool? result = fileDialog.ShowDialog();

            if (result.HasValue)
            {
                if (result.Value)
                {
                    FileName = fileDialog.FileName;
                    OnPropertyChanged(nameof(FileName));
                }
            }
        }

        #endregion
    }
}
