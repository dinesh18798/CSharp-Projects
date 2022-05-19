using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerLibrary.Interfaces;
using TaskManagerLibrary.Logic;
using TaskManagerLibrary.Models;

namespace TaskManagerLibrary.Tests
{
    public class TaskManagerTests
    {
        [Test]
        public void LoadAllProcesses_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //-- Arrange
                mock.Mock<IProcessHelper>()
                   .Setup(x => x.GetAllProcesses()).Returns(GetSampleProcess());

                var taskManager = mock.Create<TaskManager>();
                var expected = GetSampleProcess();

                //-- Act
                var actual = taskManager.LoadAllProcesses();

                //-- Assert
                Assert.True(actual != null);
                Assert.That(actual.Count, Is.EqualTo(expected.Count));

                for (int i = 0; i < actual.Count; i++)
                {
                    if (expected.ContainsKey(actual[i].Id))
                    {
                        ProcessInfo processInfo = expected[actual[i].Id];
                        Assert.That(actual[i].Id, Is.EqualTo(processInfo.Id));
                        Assert.That(actual[i].Name, Is.EqualTo(processInfo.Name));
                        Assert.That(actual[i].ThreadCount, Is.EqualTo(processInfo.ThreadCount));
                    }
                }
            }
        }

        [Test]
        public void EndTask_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //-- Arrange
                ProcessInfo processInfo = GetSampleProcess().Values.First();

                mock.Mock<IProcessHelper>()
                  .Setup(x => x.GetAllProcesses()).Returns(GetSampleProcess());

                mock.Mock<IProcessHelper>()
                   .Setup(x => x.TerminateProcess(processInfo.Id));

                var taskManager = mock.Create<TaskManager>();

                //-- Act
                var actual = taskManager.LoadAllProcesses();
                taskManager.EndTask(processInfo.Id);

                //-- Assert
                Assert.True(actual != null);
                Assert.That(actual.Any(p => p.Id != processInfo.Id));

                mock.Mock<IProcessHelper>()
                   .Verify(x => x.TerminateProcess(processInfo.Id), Times.Exactly(1));
            }
        }

        [Test]
        public void EndTask_ThrowsExceptionWhenProcessNotExist()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //-- Arrange
                ProcessInfo processInfo = new ProcessInfo { Name = "Skype", Id = 212 };

                mock.Mock<IProcessHelper>()
                  .Setup(x => x.GetAllProcesses()).Returns(GetSampleProcess());

                mock.Mock<IProcessHelper>()
                   .Setup(x => x.TerminateProcess(processInfo.Id)).Throws<ArgumentException>();

                var taskManager = mock.Create<TaskManager>();

                //-- Act
                var actual = taskManager.LoadAllProcesses();

                //-- Assert
                Assert.True(actual != null);
                Assert.That(actual.Any(p => p.Id == processInfo.Id), Is.False);
                Assert.Throws<ArgumentException>(() => taskManager.EndTask(processInfo.Id));
            }
        }

        [Test]
        public void AddNewProcess_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //-- Arrange
                ProcessInfo processInfo = new ProcessInfo { Name = "Zoom", Id = 8452 };

                mock.Mock<IProcessHelper>()
                   .Setup(x => x.RunNewProcess(processInfo.Name));

                var taskManager = mock.Create<TaskManager>();

                //-- Act
                taskManager.AddNewProcess(processInfo.Name);

                //-- Assert

                mock.Mock<IProcessHelper>()
                   .Verify(x => x.RunNewProcess(processInfo.Name), Times.Exactly(1));
            }
        }

        [Test]
        public void AddNewProcess_ThrowsExceptionWhenFilenameNotExist()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //-- Arrange
                ProcessInfo processInfo = new ProcessInfo { Name = "UnkownProcess", Id = 8452 };

                mock.Mock<IProcessHelper>()
                   .Setup(x => x.RunNewProcess(processInfo.Name)).Throws<Exception>();

                var taskManager = mock.Create<TaskManager>();

                //-- Act and Assert
                Assert.Throws<ArgumentException>(() => taskManager.AddNewProcess(processInfo.Name));
            }
        }

        [Test]
        public void Dipose_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //-- Arrange
                mock.Mock<IProcessHelper>()
                   .Setup(x => x.GetAllProcesses()).Returns(GetSampleProcess());

                var actualTaskManager = mock.Create<TaskManager>();

                //-- Act
                actualTaskManager.LoadAllProcesses();
                actualTaskManager.Dispose();
                var expectedTaskManager = mock.Create<TaskManager>();

                //-- Assert
                Assert.That(actualTaskManager, Is.EqualTo(expectedTaskManager));
            }
        }

        private Dictionary<int, ProcessInfo> GetSampleProcess()
        {
            Dictionary<int, ProcessInfo> output = new Dictionary<int, ProcessInfo>()
            {
                {12021, new ProcessInfo{Name = "Notepad", Id = 12021, ThreadCount = 5} },
                {13452, new ProcessInfo{Name = "Calculator", Id = 13452, ThreadCount = 7} },
                {4785,  new ProcessInfo{Name = "Chrome", Id = 4785, ThreadCount = 3} },
                {3652, new ProcessInfo{Name = "serverhost", Id = 3652, ThreadCount = 10} }
            };
            return output;
        }
    }
}
