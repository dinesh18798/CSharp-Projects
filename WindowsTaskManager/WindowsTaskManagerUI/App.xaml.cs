using Autofac;
using System.Windows;
using TaskManagerLibrary.Interfaces;
using WindowsTaskManagerUI.ViewModels;
using WindowsTaskManagerUI.Views;

namespace WindowsTaskManagerUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = ContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var taskManager = scope.Resolve<ITaskManager>();
                var windowService = scope.Resolve<IWindowService>();

                windowService.Register<CreateNewTaskViewModel, CreateNewTaskWindow>();

                TaskManagerViewModel taskManagerViewModel = new TaskManagerViewModel(taskManager, windowService);

                TaskManagerWindow taskManagerWindow = new TaskManagerWindow()
                {
                    DataContext = taskManagerViewModel
                };

                windowService.Owner = taskManagerWindow;
                taskManagerWindow.ShowDialog();

                base.OnStartup(e);
            }
        }
    }
}
