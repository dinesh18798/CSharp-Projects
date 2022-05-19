using System;
using System.Windows.Input;

namespace WindowsTaskManagerUI.ViewModels.Command
{
    /// <summary>
    /// The <c>RelayCommand</c> class.
    /// Contains the all the methods for invoke the commands that implemented ICommand
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private Variables

        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        #endregion

        #region Constructor

        public RelayCommand(Action<object> action) : this(action, null) { }

        public RelayCommand(Action<object> action, Predicate<object> canExe)
        {
            execute = action ?? throw new ArgumentNullException("The execute action is null");
            canExecute = canExe;
        }

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion

        #region Public Methods

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute?.Invoke(parameter ?? "<N/A>");
        }

        #endregion
    }
}
