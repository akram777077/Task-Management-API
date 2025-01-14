using System;

namespace TaskMangment.Test.Tests.Buisness;




using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using TaskMangment.Buisness.Mapping;
using TaskMangment.Buisness.Models;
using TaskMangment.Buisness.Services.STask;
using TaskMangment.Data.Entities;
using TaskMangment.Data.Repositories.RTask;
using Xunit;

    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _repositoryMock;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _repositoryMock = new Mock<ITaskRepository>();
            _taskService = new TaskService(_repositoryMock.Object);
        }

        [Fact]
        public async Task CompleteAsync_ValidId_ReturnsTrue()
        {
            // Arrange
            int taskId = 1;
            _repositoryMock.Setup(repo => repo.CompleteTaskAsync(taskId)).ReturnsAsync(true);

            // Act
            var result = await _taskService.CompleteAsync(taskId);

            // Assert
            Assert.True(result);
            _repositoryMock.Verify(repo => repo.CompleteTaskAsync(taskId), Times.Once);
        }

        [Fact]
        public async Task CompleteAsync_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            int invalidId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _taskService.CompleteAsync(invalidId));
        }

        [Fact]
        public async Task CreateAsync_ValidTask_ReturnsCreatedTask()
        {
            // Arrange
            var newTaskModel = new TaskModel(0, "Test Task", "Description", DateTime.UtcNow);
            var taskEntity = newTaskModel.ToEntity();
            var createdEntity = new TaskEntity { Id = 1, Title = "Test Task", Description = "Description", DueDate = DateTime.UtcNow, IsCompleted = false };

            _repositoryMock.Setup(repo => repo.CreateTaskAsync(It.Is<TaskEntity>(
                task => task.Title == newTaskModel.Title &&
                task.Description == newTaskModel.Description &&
                task.DueDate == newTaskModel.DueDate &&
                task.IsCompleted == newTaskModel.IsCompleted
            ))).ReturnsAsync(createdEntity);

            // Act
            var result = await _taskService.CreateAsync(newTaskModel);

            // Assert
            Assert.Equal(createdEntity.Id, result.Id);
            Assert.Equal(createdEntity.Title, result.Title);
            _repositoryMock.Verify(repo => repo.CreateTaskAsync(It.IsAny<TaskEntity>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_InvalidTask_ThrowsArgumentException()
        {
            // Arrange
            var invalidTaskModel = new TaskModel(1, "Invalid Task", null, DateTime.UtcNow);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _taskService.CreateAsync(invalidTaskModel));
        }

        [Fact]
        public async Task DeleteAsync_ValidId_CallsRepositoryDelete()
        {
            // Arrange
            int taskId = 1;

            // Act
            await _taskService.DeleteAsync(taskId);

            // Assert
            _repositoryMock.Verify(repo => repo.DeleteTaskAsync(taskId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            int invalidId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _taskService.DeleteAsync(invalidId));
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllTasks()
        {
            // Arrange
            var tasks = new List<TaskEntity>
            {
                new TaskEntity { Id = 1, Title = "Task 1", Description = "Description 1", DueDate = DateTime.UtcNow, IsCompleted = false },
                new TaskEntity { Id = 2, Title = "Task 2", Description = "Description 2", DueDate = DateTime.UtcNow, IsCompleted = true }
            };

            _repositoryMock.Setup(repo => repo.GetAllTasksAsync()).ReturnsAsync(tasks);

            // Act
            var result = await _taskService.GetAllAsync();

            // Assert
            Assert.Equal(tasks.Count, result.Count);
            _repositoryMock.Verify(repo => repo.GetAllTasksAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnsTask()
        {
            // Arrange
            int taskId = 1;
            var taskEntity = new TaskEntity { Id = taskId, Title = "Task 1", Description = "Description 1", DueDate = DateTime.UtcNow, IsCompleted = false };

            _repositoryMock.Setup(repo => repo.GetTaskByIdAsync(taskId)).ReturnsAsync(taskEntity);

            // Act
            var result = await _taskService.GetByIdAsync(taskId);
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal(taskEntity.Id, result.Value.Id);
            _repositoryMock.Verify(repo => repo.GetTaskByIdAsync(taskId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            int invalidId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _taskService.GetByIdAsync(invalidId));
        }

        [Fact]
        public async Task ReopenAsync_ValidId_ReturnsTrue()
        {
            // Arrange
            int taskId = 1;
            _repositoryMock.Setup(repo => repo.ReopenTaskAsync(taskId)).ReturnsAsync(true);

            // Act
            var result = await _taskService.ReopenAsync(taskId);

            // Assert
            Assert.True(result);
            _repositoryMock.Verify(repo => repo.ReopenTaskAsync(taskId), Times.Once);
        }

        [Fact]
        public async Task ReopenAsync_InvalidId_ThrowsArgumentException()
        {
            // Arrange
            int invalidId = 0;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _taskService.ReopenAsync(invalidId));
        }

        [Fact]
        public async Task UpdateAsync_ValidTask_ReturnsTrue()
        {
            // Arrange
            var updatedTaskModel = new TaskModel(1, "Updated Task", "Updated Description", DateTime.UtcNow);
            var updatedTaskEntity = updatedTaskModel.ToEntity();

                _repositoryMock.Setup(repo => repo.UpdateTaskAsync(It.Is<TaskEntity>(
                task => task.Id == updatedTaskEntity.Id &&
                task.Title == updatedTaskEntity.Title &&
                task.Description == updatedTaskEntity.Description &&
                task.DueDate == updatedTaskEntity.DueDate &&
                task.IsCompleted == updatedTaskEntity.IsCompleted
                ))).ReturnsAsync(true);


            // Act
            var result = await _taskService.UpdateAsync(updatedTaskModel);

            // Assert
            Assert.True(result);
            _repositoryMock.Verify(repo => repo.UpdateTaskAsync(It.IsAny<TaskEntity>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_InvalidTask_ThrowsArgumentException()
        {
            // Arrange
            var invalidTaskModel = new TaskModel(0, "Invalid Task", null, DateTime.UtcNow);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _taskService.UpdateAsync(invalidTaskModel));
        }
        [Fact]
        public void ToEntity_ValidTaskModel_ReturnsTaskEntity()
        {
            var date = DateTime.UtcNow;
            var taskModel = new TaskModel(1, "Test Task", "Description", date);
            var taskEntity = taskModel.ToEntity();

            Assert.Equal(1, taskEntity.Id);
            Assert.Equal("Test Task", taskEntity.Title);
            Assert.Equal("Description", taskEntity.Description);
            Assert.Equal(date, taskEntity.DueDate);
            Assert.False(taskEntity.IsCompleted);
        }
        [Fact]
        public void ToEntity_NullIdTaskModel_ReturnsTaskEntity()
        {
            var date = DateTime.UtcNow;
            var taskModel = new TaskModel(null, "Test Task", "Description", date);
            var taskEntity = taskModel.ToEntity();

            Assert.Equal(0, taskEntity.Id);
            Assert.Equal("Test Task", taskEntity.Title);
            Assert.Equal("Description", taskEntity.Description);
            Assert.Equal(date, taskEntity.DueDate);
            Assert.False(taskEntity.IsCompleted);
        }
        [Fact]
        public async Task GetByUserAsync_ShouldReturnTaskModels()
        {
            // Arrange
            string username = "testuser";
            var tasksEntities = new List<TaskEntity>
            {
                new TaskEntity { Id = 1, Title = "Task1", Username = username,Description = "Description 1", DueDate = DateTime.UtcNow, IsCompleted = false },
                new TaskEntity { Id = 2, Title = "Task2", Username = username, Description = "Description 2", DueDate = DateTime.UtcNow.AddDays(5), IsCompleted = true },
                new TaskEntity { Id = 3, Title = "Task2", Username = username, Description = "Description 3", DueDate = DateTime.UtcNow.AddDays(7), IsCompleted = false },
                new TaskEntity { Id = 4, Title = "Task2", Username = username, Description = "Description 4", DueDate = DateTime.UtcNow.AddDays(10), IsCompleted = true }
            };

            var expectedModels = tasksEntities.Select(te => te.ToModel()).ToList();

            _repositoryMock
                .Setup(repo => repo.GetTasksByUserAsync(username))
                .ReturnsAsync(tasksEntities);

            // Act
            var result = await _taskService.GetByUserAsync(username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedModels.Count, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.Equal(expectedModels[i].Id, result[i].Id);
                Assert.Equal(expectedModels[i].Description, result[i].Description);
                Assert.Equal(expectedModels[i].IsCompleted, result[i].IsCompleted);
                Assert.Equal(expectedModels[i].Title, result[i].Title);
                Assert.Equal(expectedModels[i].DueDate, result[i].DueDate);
            }

            _repositoryMock.Verify(repo => repo.GetTasksByUserAsync(username), Times.Once);
        }
        [Fact]
        public async Task CompleteOfUserAsync_ThrowsArgumentException_WhenTaskIdIsLessThan1()
        {
            // Arrange
            int invalidTaskId = 0;
            string username = "testUser";

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                _taskService.CompleteOfUserAsync(invalidTaskId, username));

            Assert.Equal("Id must be greater than 0", exception.Message);
        }

        [Fact]
        public async Task CompleteOfUserAsync_CallsRepositoryMethod_WhenTaskIdIsValid()
        {
            // Arrange
            int validTaskId = 1;
            string username = "testUser";

            _repositoryMock
                .Setup(repo => repo.CompleteTaskOfUserAsync(validTaskId, username))
                .ReturnsAsync(true);

            // Act
            var result = await _taskService.CompleteOfUserAsync(validTaskId, username);

            // Assert
            Assert.True(result);
            _repositoryMock.Verify(repo => repo.CompleteTaskOfUserAsync(validTaskId, username), Times.Once);
        }

        [Fact]
        public async Task CompleteOfUserAsync_ReturnsFalse_WhenRepositoryReturnsFalse()
        {
            // Arrange
            int validTaskId = 1;
            string username = "testUser";

            _repositoryMock
                .Setup(repo => repo.CompleteTaskOfUserAsync(validTaskId, username))
                .ReturnsAsync(false);

            // Act
            var result = await _taskService.CompleteOfUserAsync(validTaskId, username);

            // Assert
            Assert.False(result);
            _repositoryMock.Verify(repo => repo.CompleteTaskOfUserAsync(validTaskId, username), Times.Once);
        }
        [Fact]
        public async Task ReopenOfUserAsync_ThrowsArgumentException_WhenTaskIdIsLessThan1()
        {
            // Arrange
            int invalidTaskId = 0;
            string username = "testUser";

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                _taskService.ReopenOfUserAsync(invalidTaskId, username));

            Assert.Equal("Id must be greater than 0", exception.Message);
        }

        [Fact]
        public async Task ReopenOfUserAsync_CallsRepositoryMethod_WhenTaskIdIsValid()
        {
            // Arrange
            int validTaskId = 1;
            string username = "testUser";

            _repositoryMock
                .Setup(repo => repo.ReopenTaskOfUserAsync(validTaskId, username))
                .ReturnsAsync(true);

            // Act
            var result = await _taskService.ReopenOfUserAsync(validTaskId, username);

            // Assert
            Assert.True(result);
            _repositoryMock.Verify(repo => repo.ReopenTaskOfUserAsync(validTaskId, username), Times.Once);
        }

        [Fact]
        public async Task ReopenOfUserAsync_ReturnsFalse_WhenRepositoryReturnsFalse()
        {
            // Arrange
            int validTaskId = 1;
            string username = "testUser";

            _repositoryMock
                .Setup(repo => repo.ReopenTaskOfUserAsync(validTaskId, username))
                .ReturnsAsync(false);

            // Act
            var result = await _taskService.ReopenOfUserAsync(validTaskId, username);

            // Assert
            Assert.False(result);
            _repositoryMock.Verify(repo => repo.ReopenTaskOfUserAsync(validTaskId, username), Times.Once);
        }
        [Fact]
        public async Task RemoveFromUserAsync_ValidTaskIdAndUsername_ReturnsTrue()
        {
            // Arrange
            var mockRepository = new Mock<ITaskRepository>();
            mockRepository
                .Setup(repo => repo.RemoveTaskFromUserAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var taskService = new TaskService(mockRepository.Object);
            int taskId = 1;
            string username = "testuser";

            // Act
            var result = await taskService.RemoveFromUserAsync(taskId, username);

            // Assert
            Assert.True(result);
            mockRepository.Verify(repo => repo.RemoveTaskFromUserAsync(taskId, username), Times.Once);
        }

        [Fact]
        public async Task RemoveFromUserAsync_InvalidTaskId_ThrowsArgumentException()
        {
            // Arrange
            var mockRepository = new Mock<ITaskRepository>();
            var taskService = new TaskService(mockRepository.Object);
            int invalidTaskId = 0;
            string username = "testuser";

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => taskService.RemoveFromUserAsync(invalidTaskId, username));
            Assert.Equal("Id must be greater than 0", exception.Message);
        }
        [Fact]
        public async Task RemoveFromUserAsync_ValidTaskIdAndWrongUsername_ReturnsFalse()
        {
            // Arrange
            var mockRepository = new Mock<ITaskRepository>();
            mockRepository
                .Setup(repo => repo.RemoveTaskFromUserAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var taskService = new TaskService(mockRepository.Object);
            int taskId = 1;
            string username = "notTheUserOfThisTask";

            // Act
            var result = await taskService.RemoveFromUserAsync(taskId, username);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public async Task UpdateFromUserAsync_ValidTaskModel_ReturnsTrue()
        {
            // Arrange
            var taskModel = new TaskModel(1, "Test Task", "Description", DateTime.UtcNow);
            var repositoryMock = new Mock<ITaskRepository>();
            repositoryMock.Setup(repo => repo.UpdateTaskFromUserAsync(It.IsAny<TaskEntity>())).ReturnsAsync(true);
            var taskService = new TaskService(repositoryMock.Object);

            // Act
            var result = await taskService.UpdateFromUserAsync(taskModel);

            // Assert
            Assert.True(result);
            repositoryMock.Verify(repo => repo.UpdateTaskFromUserAsync(It.IsAny<TaskEntity>()), Times.Once);
        }

        [Fact]
        public async Task UpdateFromUserAsync_InvalidTaskModel_ThrowsArgumentException()
        {
            // Arrange
            var taskModel = new TaskModel(0, "Test Task", "Description", DateTime.UtcNow);
            var taskService = new TaskService(new Mock<ITaskRepository>().Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => taskService.UpdateFromUserAsync(taskModel));
        }

    }

