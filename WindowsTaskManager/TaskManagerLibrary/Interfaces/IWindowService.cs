using System.Windows;

namespace TaskManagerLibrary.Interfaces
{
    /// <summary>
    /// Represent window service for registering viewmodel and opening window 
    /// </summary>
    public interface IWindowService
    {
        Window Owner { get; set; }

        /// <summary>
        /// Register new view model with respective view in the mapping
        /// </summary>
        /// <returns>
        /// void
        /// </returns>
        /// <typeparam name="TViewModel">A type that inherits from the IWindowRequestClose interface</typeparam>
        /// <typeparam name="TView">A type that inherits from the IWindow interface</typeparam>
        void Register<TViewModel, TView>() where TViewModel : IWindowRequestClose
                                           where TView : IWindow;

        /// <summary>
        /// Open the view/window based on the view model
        /// </summary>
        /// <returns>
        /// bool
        /// </returns>
        /// <param name="viewModel">View Model which need to open window</param>
        /// <typeparam name="TViewModel">A type that inherits from the IWindowRequestClose interface</typeparam>
        bool? OpenWindow<TViewModel>(TViewModel viewModel) where TViewModel : IWindowRequestClose;
    }
}
