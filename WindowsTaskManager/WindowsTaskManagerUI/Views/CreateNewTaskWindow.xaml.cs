using System.Windows;
using TaskManagerLibrary.Interfaces;

namespace WindowsTaskManagerUI.Views
{
    /// <summary>
    /// Interaction logic for CreateNewTaskWindow.xaml
    /// </summary>
    public partial class CreateNewTaskWindow : Window, IWindow
    {
        public CreateNewTaskWindow()
        {
            InitializeComponent();
        }     
    }
}
