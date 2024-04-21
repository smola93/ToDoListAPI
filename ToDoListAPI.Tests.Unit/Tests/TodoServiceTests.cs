using Moq;
using ToDoListAPI.Tests.Unit.Mocks;
using WebApplication_ToDoListAPI.Application.DTOs;
using WebApplication_ToDoListAPI.Application.Interfaces.Base;
using WebApplication_ToDoListAPI.Application.Models;
using WebApplication_ToDoListAPI.Application.Services;

namespace ToDoListAPI.Tests.Unit.Tests
{
    public class TodoServiceTests
    {
        [Fact]
        public async Task GetAllTodos_ReturnsListOfTodosAsync()
        {
            //Arrange
            var baseRepoMock = new Mock<IBaseRepository<TodoDto>>();
            baseRepoMock.Setup(repo => repo.GetListAsync(default)).Returns(TodoMocks.GetTestTodos());
            var eventRepoMock = new Mock<IEventRepository<TodoDto>>();
            var service = new TodoService(baseRepoMock.Object, eventRepoMock.Object);

            //Act
            var result = await service.GetListAsync();

            //Assert
            Assert.IsType<List<TodoDto>>(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetTodoById_ReturnsTodoAsync()
        {
            //Arrange
            var baseRepoMock = new Mock<IBaseRepository<TodoDto>>();
            baseRepoMock.Setup(repo => repo.GetItemAsync(1, default))
                .Returns(Task.FromResult(TodoMocks.GetTestTodos().Result.FirstOrDefault(t => t.Id == 1)!));
            var eventRepoMock = new Mock<IEventRepository<TodoDto>>();
            var service = new TodoService(baseRepoMock.Object, eventRepoMock.Object);

            //Act
            var result = await service.GetItemAsync(1);

            //Assert
            Assert.IsType<TodoDto>(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task AddTodo_ReturnsTodoAsync()
        {
            //Arrange
            var baseRepoMock = new Mock<IBaseRepository<TodoDto>>();
            baseRepoMock.Setup(repo => repo.AddAsync(It.IsAny<TodoDto>(), default))
                .Returns(Task.FromResult(new TodoDto { 
                    Id = 4, 
                    Name = "Test 4", 
                    Description = "Test 4 Description", 
                    DueDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 20) }));
            var eventRepoMock = new Mock<IEventRepository<TodoDto>>();
            var service = new TodoService(baseRepoMock.Object, eventRepoMock.Object);

            //Act
            var result = await service.AddAsync(new TodoDto { 
                Name = "Test 4", 
                Description = "Test 4 Description", 
                DueDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 20) });

            //Assert
            Assert.IsType<TodoDto>(result);
            Assert.Equal(4, result.Id);
        }

        [Fact]
        public async Task UpdateTodo_ReturnsTodoAsync()
        {
            //Arrange
            var baseRepoMock = new Mock<IBaseRepository<TodoDto>>();
            baseRepoMock.Setup(repo => repo.UpdateAsync(It.IsAny<TodoDto>(), default))
                .Returns(Task.FromResult(new TodoDto { 
                    Id = 1, 
                    Name = "Test 1 Updated", 
                    Description = "Test 1 Description Updated", 
                    DueDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 5) }));
            var eventRepoMock = new Mock<IEventRepository<TodoDto>>();
            var service = new TodoService(baseRepoMock.Object, eventRepoMock.Object);

            //Act
            var result = await service.UpdateAsync(new TodoDto { 
                Id = 1, 
                Name = "Test 1 Updated", 
                Description = "Test 1 Description Updated", 
                DueDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 5) });

            //Assert
            Assert.IsType<TodoDto>(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test 1 Updated", result.Name);
            Assert.Equal("Test 1 Description Updated", result.Description);
        }

        [Fact]
        public async Task DeleteTodo_ReturnsTrueAsync()
        {
            //Arrange
            var baseRepoMock = new Mock<IBaseRepository<TodoDto>>();
            baseRepoMock.Setup(repo => repo.DeleteAsync(1, default)).Returns(Task.FromResult(true));
            var eventRepoMock = new Mock<IEventRepository<TodoDto>>();
            var service = new TodoService(baseRepoMock.Object, eventRepoMock.Object);

            //Act
            var result = await service.DeleteAsync(1);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetEventList_ReturnsListOfEventsAsync()
        {
            //Arrange
            var baseRepoMock = new Mock<IBaseRepository<TodoDto>>();
            var eventRepoMock = new Mock<IEventRepository<TodoDto>>();
            eventRepoMock.Setup(repo => repo.GetEventListAsync(It.IsAny<Guid>(), default)).Returns(TodoMocks.GetTestCloudEventTodos());
            var service = new TodoService(baseRepoMock.Object, eventRepoMock.Object);

            //Act
            var result = await service.GetEventListAsync(Guid.Empty);

            //Assert
            Assert.IsType<List<CloudEvent<TodoDto>>>(result);
            Assert.Equal(3, result.Count);
        }
    }
}