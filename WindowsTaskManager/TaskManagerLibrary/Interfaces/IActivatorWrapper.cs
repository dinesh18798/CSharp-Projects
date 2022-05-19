using System;

namespace TaskManagerLibrary.Interfaces
{
    /// <summary>
    /// Represent the interaction with System.Activator
    /// </summary>
    public interface IActivatorWrapper
    {
        /// <summary>
        /// Create instance based on the type
        /// </summary>
        /// <returns>
        /// object
        /// </returns>
        /// <param name="type">Type of the instance</param>
        object CreateInstance(Type type);
    }
}
