using System.ComponentModel;

namespace WindowsTaskManagerUI.ViewModels
{
    /// <summary>
    /// The <c>BaseViewModel</c> class.
    /// Contains the event and methods for notify property change that implemented INotifyPropertyChanged
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #endregion

        #region Protected Methods

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
