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
    }

