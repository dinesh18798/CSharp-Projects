using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;
using TaskManagerLibrary.Interfaces;
using TaskManagerLibrary.Models;
using WindowsTaskManagerUI.ViewModels.Command;

namespace WindowsTaskManagerUI.ViewModels
{
    /// <summary>
    /// The <c>TaskManagerViewModel</c> class.
    /// Contains the properties for the Task Manager view and inherited BaseViewModel
    /// </summary>
    public class TaskManagerViewModel : BaseViewModel
    {
        #region Private Variables

        private readonly ITaskManager _taskManager;
        private readonly IWindowService _windowService;

        private ProcessInfo selectedProcessInfo;
        private ObservableCollection<ProcessInfo> processInfos;

        #endregion

        #region Constructor

        public TaskManagerViewModel(ITaskManager taskManager, IWindowService windowService)
        {
            _taskManager = taskManager;
            _windowService = windowService;

            RunNewTaskCommand = new RelayCommand(a => RunNewTask());
            EndTaskCommand = new RelayCommand(a => EndTask(), p => EndTaskOptionEnable());
            CloseWindowCommand = new RelayCommand(a => CloseWindow(a));
            ClosingCommand = new RelayCommand(a => ClosingWindow());

            InitializeAllProcesses();
            CurrentProcesses.CollectionChanged += CurrentProcesses_CollectionChanged;
        }

        #endregion

        #region Properties

        public ICommand RunNewTaskCommand { get; private set; }

        public ICommand EndTaskCommand { get; private set; }

        public ICommand CloseWindowCommand { get; private set; }

        public ICommand ClosingCommand { get; private set; }

        public ObservableCollection<ProcessInfo> CurrentProcesses
        {
            get { return processInfos; }
        }

        public ProcessInfo SelectedProcess
        {
            get { return selectedProcessInfo; }
            set
            {
                selectedProcessInfo = value;
                //-- OnPropertyChanged(nameof(SelectedProcess)); Have to implement
            }
        }

        public string NumberOfProcesses
        {
            get { return processInfos.Count.ToString(); }
        }

        #endregion

        #region Private Methods

        private void InitializeAllProcesses()
        {
            processInfos = _taskManager.LoadAllProcesses();

            OnPropertyChanged(nameof(CurrentProcesses));
            OnPropertyChanged(nameof(NumberOfProcesses));
        }

        private void CurrentProcesses_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(NumberOfProcesses));
        }

        private void RunNewTask()
        {
            CreateNewTaskViewModel newTaskViewModel = new CreateNewTaskViewModel();
            bool? result = _windowService.OpenWindow(newTaskViewModel);

            if (result.HasValue)
            {
                if (result.Value)
                {
                    try
                    {
                        _taskManager.AddNewProcess(newTaskViewModel.FileName);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"Windows cannot find '{newTaskViewModel.FileName}'. Make sure you've typed the name correctly, then try again.",
                            newTaskViewModel.FileName, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private bool EndTaskOptionEnable()
        {
            return selectedProcessInfo != null;
        }

        private void EndTask()
        {
            _taskManager.EndTask(selectedProcessInfo.Id);
            selectedProcessInfo = null;
        }

        private void CloseWindow(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }

        private void ClosingWindow()
        {
            CurrentProcesses.CollectionChanged -= CurrentProcesses_CollectionChanged;
            _taskManager.Dispose();
        }

        #endregion
    }
}
