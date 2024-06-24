using Api.Controllers;
using Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service;

namespace Tests
{
    public class SubTaskTest
    {
        private readonly SubTaskController _controller;
        private readonly Mock<ISubTaskService> _mockContext;

        public SubTaskTest()
        {
            _mockContext = new Mock<ISubTaskService>();

            var subtask = new List<SubTask>
        {
            new SubTask { Id = 1, MainTaskId = 1, Description = "Subtask1", Finished = false },
            new SubTask { Id = 1, MainTaskId = 2, Description = "Subtask2", Finished = true },
            new SubTask { Id = 1, MainTaskId = 3, Description = "Subtask3", Finished = false },
            new SubTask { Id = 1, MainTaskId = 4, Description = "Subtask4", Finished = true }

        };
            _mockContext.Setup(m => m.List(1)).ReturnsAsync(subtask);

            _controller = new SubTaskController(_mockContext.Object);
        }

        [Fact]
        public async Task Get_ReturnsAll_Success()
        {
            var result = await _controller.Get(1);

            var list = (result as OkObjectResult)?.Value as List<SubTask>;


            list.Should().NotBeNull();
            list.Should().HaveCount(4);

        }

        [Fact]
        public async Task Post_CreatesNewSubTask_Success()
        {
            var subTaskTest = new SubTaskRequest { MainTaskId = 1, Description = "Very Cool Subtask" };
            var subTasktest2 = new SubTask { Id = 1, MainTaskId = 1, Description = "Very Cool Subtask", Finished = false };
            _mockContext.Setup(x => x.Create(subTaskTest)).ReturnsAsync(subTasktest2);


            var result = await _controller.Post(subTaskTest);
            var item = (result as OkObjectResult)?.Value as SubTask;

            item.Should().BeEquivalentTo(new SubTask { Id = 1, MainTaskId = 1, Description = "Very Cool Subtask", Finished = false });
        }
        [Fact]
        public async Task Put_UpdatesSubtask_Success()
        {
            //arrange
            var newSubTask = new SubTask() { MainTaskId = 1, Id = 1, Description = "New subtask", Finished = true };
            var newSubTaskUpdate = new SubTaskUpdate() { Description = "New subtask", Finished = true };


            _mockContext.Setup(m => m.Update(newSubTaskUpdate, 1)).ReturnsAsync(newSubTask);



            //act
            var result = await _controller.Put(1, newSubTaskUpdate);
            var item = (result as OkObjectResult)?.Value as SubTask;

            //assert
            item.Should().BeEquivalentTo(new SubTask { MainTaskId = 1, Id = 1, Description = "New subtask", Finished = true });

        }

        [Fact]
        public async Task Delete_DeletesSubtask_Success()
        {
            //act
            var result = await _controller.Delete(1);

            //assert
            result.Should().BeEquivalentTo(new NoContentResult());

        }
    }
}
