using System.Collections.ObjectModel;
using TaskManagerLibrary.Models;

namespace TaskManagerLibrary.Interfaces
{
    /// <summary>
    /// Represent task management for loading, adding and ending process 
    /// </summary>
    public interface ITaskManager
    {
        /// <summary>
        /// Get all the running processes in the form of ProcessInfo object
        /// </summary>
        /// <returns>
        /// ObservableCollection of ProcessInfo objects 
        /// </returns>
        ObservableCollection<ProcessInfo> LoadAllProcesses();

        /// <summary>
        /// Add new process or application
        /// </summary>
        /// <returns>
        /// void
        /// </returns>
        /// <param name="fileName">Application file path or name</param>
        void AddNewProcess(string fileName);

        /// <summary>
        /// End the task 
        /// </summary>
        /// <returns>
        /// void
        /// </returns>
        /// <param name="id">Task Id</param>
        void EndTask(int id);

        /// <summary>
        /// Dispose the collections and events
        /// </summary>
        /// <returns>
        /// void
        /// </returns>
        void Dispose();
    }
}