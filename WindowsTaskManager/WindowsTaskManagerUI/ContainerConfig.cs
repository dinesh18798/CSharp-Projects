using Autofac;
using TaskManagerLibrary.Interfaces;
using TaskManagerLibrary.Logic;
using TaskManagerLibrary.Utilities;

namespace WindowsTaskManagerUI
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ProcessHelper>().As<IProcessHelper>();
            builder.RegisterType<ActivatorWrapper>().As<IActivatorWrapper>();
            builder.RegisterType<WindowService>().As<IWindowService>();
            builder.RegisterType<TaskManager>().As<ITaskManager>();

            return builder.Build();
        }
    }
}
