using System;
using TaskManagerLibrary.Interfaces;

namespace TaskManagerLibrary.Utilities
{
    /// <summary>
    /// The main <c>ActivatorWrapper</c> class.
    /// Wrapper for System.Activator which implement IActivatorWrapper
    /// </summary>
    public class ActivatorWrapper : IActivatorWrapper
    {
        #region Public Methods

        public object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        #endregion
    }
}
