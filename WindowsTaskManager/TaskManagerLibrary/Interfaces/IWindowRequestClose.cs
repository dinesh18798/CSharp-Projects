using System;
using TaskManagerLibrary.Models;

namespace TaskManagerLibrary.Interfaces
{
    /// <summary>
    /// Represent window close events 
    /// </summary>
    public interface IWindowRequestClose
    {
        event EventHandler<WindowCloseRequestedEventArgs> CloseRequested;
    }
}
