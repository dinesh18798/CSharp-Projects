using System;
using System.Collections.Generic;
using System.Diagnostics;
using TaskManagerLibrary.Interfaces;
using TaskManagerLibrary.Models;

namespace TaskManagerLibrary.Utilities
{
    /// <summary>
    /// The main <c>ProcessHelper</c> class.
    /// Contains all methods for performing process management methods 
    /// which implement IProcessHelper
    /// </summary>
    public class ProcessHelper : IProcessHelper
    {
        #region Public Methods

        /// <summary>
        /// Get the process and create ProcessInfo object for each of the processes
        /// </summary>
        public Dictionary<int, ProcessInfo> GetAllProcesses()
        {
            Dictionary<int, ProcessInfo> processInfos = new Dictionary<int, ProcessInfo>();
            foreach (Process process in Process.GetProcesses())
            {
                processInfos.Add(process.Id, new ProcessInfo { Name = process.ProcessName, Id = process.Id, ThreadCount = process.Threads.Count });
            }
            return processInfos;
        }

        /// <summary>
        /// Run new process based on the file name
        /// </summary>
        /// <return>
        /// void
        /// </return>
        /// <exception cref="Exception">Thrown when the file name is invalid
        /// </exception>
        public void RunNewProcess(string fileName)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = fileName;
                process.Start();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Terminate the process based on the process id
        /// </summary>
        /// <return>
        /// void
        /// </return>
        /// <exception cref="Exception">Thrown when the process id is invalid
        /// </exception>
        public void TerminateProcess(int id)
        {
            try
            {
                Process process = Process.GetProcessById(id);
                process.Kill();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
