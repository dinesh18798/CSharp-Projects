using System.Collections.Generic;
using TaskManagerLibrary.Models;

namespace TaskManagerLibrary.Interfaces
{
    /// <summary>
    /// Represent the interaction with System.Diagnostics to get, add and end the task 
    /// </summary>
    public interface IProcessHelper
    {
        /// <summary>
        /// Get all the running processes of the system
        /// </summary>
        /// <returns>
        /// List of ProcessInfo objects 
        /// </returns>
        Dictionary<int, ProcessInfo> GetAllProcesses();

        /// <summary>
        /// Start new process or application
        /// </summary>
        /// <returns>
        /// void
        /// </returns>
        /// <param name="fileName">Application file path or name</param>
        void RunNewProcess(string fileName);

        /// <summary>
        /// Terminate the process by using process id
        /// </summary>
        /// <returns>
        /// void
        /// </returns>
        /// <param name="id">Id of the process or application</param>
        void TerminateProcess(int id);
    }
}
