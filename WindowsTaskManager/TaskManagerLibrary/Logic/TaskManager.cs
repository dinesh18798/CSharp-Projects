using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Windows.Data;
using TaskManagerLibrary.Interfaces;
using TaskManagerLibrary.Models;

namespace TaskManagerLibrary.Logic
{
    /// <summary>
    /// The main <c>TaskManager</c> class.
    /// Contains all methods for performing task management implement ITaskManager
    /// </summary>
    public class TaskManager : ITaskManager
    {
        #region Private Variables

        private const string QueryForProcessStartTrace = "SELECT * FROM Win32_ProcessStartTrace";
        private const string QueryForProcessStopTrace = "SELECT * FROM Win32_ProcessStopTrace";

        private readonly object _lock = new object();
        private readonly IProcessHelper _processHelper;

        private ManagementEventWatcher startWatch;
        private ManagementEventWatcher stopWatch;

        private Dictionary<int, ProcessInfo> processMapping;
        private ObservableCollection<ProcessInfo> processInfosObservable;

        #endregion

        #region Constructor

        public TaskManager(IProcessHelper process)
        {
            _processHelper = process;
        }

        #endregion

        #region Private Methods

        private void SubscribeToEvents()
        {
            startWatch = new ManagementEventWatcher(new WqlEventQuery(QueryForProcessStartTrace));
            startWatch.EventArrived += new EventArrivedEventHandler(StartEventArrived);

            stopWatch = new ManagementEventWatcher(new WqlEventQuery(QueryForProcessStopTrace));
            stopWatch.EventArrived += new EventArrivedEventHandler(StopEventArrived);

            startWatch.Start();
            stopWatch.Start();
        }

        private void UnsubscribeToEvents()
        {
            startWatch.EventArrived -= new EventArrivedEventHandler(StartEventArrived);
            stopWatch.EventArrived -= new EventArrivedEventHandler(StopEventArrived);

            startWatch.Stop();
            stopWatch.Stop();
        }


        private void StartEventArrived(object sender, EventArrivedEventArgs e)
        {
            int newProcessId = Convert.ToInt32(e.NewEvent.Properties["ProcessId"].Value);
            try
            {
                Process process = Process.GetProcessById(newProcessId);
                ProcessInfo newProcess = new ProcessInfo
                {
                    Name = process.ProcessName,
                    Id = process.Id,
                    ThreadCount = process.Threads.Count
                };

                AddProcessToCollections(newProcess);
            }
            catch (Exception) { }
        }

        private void StopEventArrived(object sender, EventArrivedEventArgs e)
        {
            int currentId = Convert.ToInt32(e.NewEvent.Properties["ProcessId"].Value);
            RemoveProcessFromCollections(currentId);
        }

        private void AddProcessToCollections(ProcessInfo processInfo)
        {
            if (!processMapping.ContainsKey(processInfo.Id))
            {
                processMapping.Add(processInfo.Id, processInfo);
                processInfosObservable.Add(processInfo);
            }
        }

        private void RemoveProcessFromCollections(int processId)
        {
            if (processMapping.ContainsKey(processId))
            {
                processMapping.Remove(processId);
                processInfosObservable.Remove(processInfosObservable.Where(p => p.Id == processId).First());
            }
        }

        #endregion

        #region Public Methods

        public ObservableCollection<ProcessInfo> LoadAllProcesses()
        {
            processMapping = _processHelper.GetAllProcesses();
            processInfosObservable = new ObservableCollection<ProcessInfo>(processMapping.Values);
            BindingOperations.EnableCollectionSynchronization(processInfosObservable, _lock);

            SubscribeToEvents();
            return processInfosObservable;
        }

        public void EndTask(int id)
        {
            try
            {
                _processHelper.TerminateProcess(id);
                RemoveProcessFromCollections(id);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"The process with {id} is not exist!" + e.Message);
            }

        }

        public void AddNewProcess(string fileName)
        {
            try
            {
                _processHelper.RunNewProcess(fileName);
            }
            catch (Exception e)
            {
                throw new ArgumentException($"The file name {fileName} is not valid!" + e.Message);
            }
        }

        public void Dispose()
        {
            processMapping.Clear();
            processInfosObservable.Clear();
            BindingOperations.DisableCollectionSynchronization(processInfosObservable);

            UnsubscribeToEvents();
        }

        #endregion
    }
}
