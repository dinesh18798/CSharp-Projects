using Autofac.Extras.Moq;
using NUnit.Framework;
using System;
using TaskManagerLibrary.Utilities;
using WindowsTaskManagerUI.ViewModels;
using WindowsTaskManagerUI.Views;

namespace TaskManagerLibrary.Tests
{
    public class WindowServiceTests
    {
        [Test]
        public void Register_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //-- Arrange
                var windowService = mock.Create<WindowService>();

                //-- Act
                int expectedMappingCount = windowService.WindowMapping.Count;
                windowService.Register<CreateNewTaskViewModel, CreateNewTaskWindow>();

                //-- Assert
                Assert.That(windowService.WindowMapping.Count, Is.GreaterThan(expectedMappingCount));
                Assert.That(windowService.WindowMapping.ContainsKey(typeof(CreateNewTaskViewModel)), Is.True);
            }
        }

        [Test]
        public void Register_AddExistingViewModelTypeThrowsException()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //-- Arrange
                var windowService = mock.Create<WindowService>();

                //-- Act
                int expectedMappingCount = windowService.WindowMapping.Count;
                windowService.Register<CreateNewTaskViewModel, CreateNewTaskWindow>();

                //-- Assert
                Assert.That(windowService.WindowMapping.Count, Is.GreaterThan(expectedMappingCount));
                Assert.Throws<ArgumentException>(() => windowService.Register<CreateNewTaskViewModel, CreateNewTaskWindow>(),
                    $"Type CreateNewTaskViewModel is already existed");
            }
        }
    }
}
