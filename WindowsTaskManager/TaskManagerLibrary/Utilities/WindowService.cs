using System;
using System.Collections.Generic;
using System.Windows;
using TaskManagerLibrary.Interfaces;
using TaskManagerLibrary.Models;

namespace TaskManagerLibrary.Utilities
{
    /// <summary>
    /// The main <c>WindowService</c> class.
    /// Contains all methods and properties for performing window operation
    /// which implement IWindowService
    /// </summary>
    public class WindowService : IWindowService
    {
        #region Private Variables

        private readonly IActivatorWrapper _activatorWrapper;

        #endregion

        #region Constructor

        public WindowService(IActivatorWrapper activatorWrapper)
        {
            _activatorWrapper = activatorWrapper;
            WindowMapping = new Dictionary<Type, Type>();
        }

        #endregion

        #region Properties 

        public Window Owner { get; set; }

        public IDictionary<Type, Type> WindowMapping { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Register new view model with respective view, adding into the local window mapping
        /// </summary>
        /// <return>
        /// void
        /// </return>
        /// <exception cref="Exception">Thrown when the view model existed in the mapping 
        /// </exception>
        public void Register<TViewModel, TView>() where TViewModel : IWindowRequestClose
                                                  where TView : IWindow
        {
            if (WindowMapping.ContainsKey(typeof(TViewModel)))
            {
                throw new ArgumentException($"Type {typeof(TViewModel)} is already existed");
            }

            WindowMapping.Add(typeof(TViewModel), typeof(TView));
        }

        /// <summary>
        /// Open window or view based on the view model
        /// </summary>
        /// <return>
        /// void
        /// </return>
        public bool? OpenWindow<TViewModel>(TViewModel viewModel) where TViewModel : IWindowRequestClose
        {
            if (!WindowMapping.ContainsKey(typeof(TViewModel)))
                return null;

            Type viewType = WindowMapping[typeof(TViewModel)];

            IWindow window = (IWindow)_activatorWrapper.CreateInstance(viewType);

            void handler(object sender, WindowCloseRequestedEventArgs e)
            {
                viewModel.CloseRequested -= handler;

                if (e.WindowResult.HasValue)
                {
                    window.DialogResult = e.WindowResult;
                }
                else
                {
                    window.Close();
                }
            }

            viewModel.CloseRequested += handler;

            window.DataContext = viewModel;
            window.Owner = Owner;

            return window.ShowDialog();
        }

        #endregion
    }
}
