using System;

namespace TaskManagerLibrary.Models
{
    /// <summary>
    /// The <c>WindowCloseRequestedEventArgs</c> class.
    /// Contains the event argument and method of window closing that inherited from EventArgs class
    /// </summary>
    public class WindowCloseRequestedEventArgs : EventArgs
    {
        #region Properties

        public bool? WindowResult { get; }

        #endregion

        #region Constructor

        public WindowCloseRequestedEventArgs(bool? windowResult)
        {
            WindowResult = windowResult;
        }

        #endregion
    }
}
