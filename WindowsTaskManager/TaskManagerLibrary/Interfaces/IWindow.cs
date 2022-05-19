using System.Windows;

namespace TaskManagerLibrary.Interfaces
{
    /// <summary>
    /// Represent window extension functionalities 
    /// </summary>
    public interface IWindow
    {
        object DataContext { get; set; }

        bool? DialogResult { get; set; }

        Window Owner { get; set; }

        /// <summary>
        /// Close the window
        /// </summary>
        /// <returns>
        /// void
        /// </returns>
        void Close();

        /// <summary>
        /// Show the window
        /// </summary>
        /// <returns>
        /// void
        /// </returns>
        bool? ShowDialog();
    }
}
