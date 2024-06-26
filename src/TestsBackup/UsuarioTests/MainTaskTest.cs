﻿using Api.Controllers;
using Domain.Models;
using Domain.Request;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service;

namespace Tests
{
    public class MainTaskTest
    {
        private readonly MainTaskController _controller;
        private readonly Mock<IMainTaskService> _mockContext;

        public MainTaskTest()
        {
            _mockContext = new Mock<IMainTaskService>();

            var mainTasks = new List<MainTask>
        {
            new MainTask {UserId = 1, Id = 1, Description = "MainTask1"},
            new MainTask {UserId = 1, Id = 2, Description = "MainTask2"},
            new MainTask {UserId = 1, Id = 3, Description = "MainTask3"}
        };
            _mockContext.Setup(m => m.Get(1)).ReturnsAsync(mainTasks);

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var _httpClient = new HttpClient();
            mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            _controller = new MainTaskController(_mockContext.Object, mockHttpClientFactory.Object);

        }

        [Fact]
        public async Task Get_ReturnsAll_Success()
        {
            var result = await _controller.Get(1);

            var list = (result as OkObjectResult)?.Value as List<MainTask>;

            list.Should().NotBeNull();
            list.Should().HaveCount(3);
        }

        [Fact]
        public async Task Post_CreatesNewMainTask_Success()
        {
            //arrange
            var mainTaskRequest = new MainTaskRequest() { UserId = 1, Description = "New Task" };
            var newMainTask = new MainTask() { UserId = 1, Id = 1, Description = "New Task" };
            _mockContext.Setup(m => m.Create(mainTaskRequest)).ReturnsAsync(newMainTask);

            //act
            var result = await _controller.Post(mainTaskRequest);
            var item = (result as OkObjectResult)?.Value as MainTask;

            //assert
            item.Should().BeEquivalentTo(new MainTask() { UserId = 1, Id = 1, Description = "New Task" });
        }

        [Fact]
        public async Task Put_UpdatesMainTask_Success()
        {
            //arrange
            var mainTaskUpdate = new MainTaskUpdate() { Description = "New Task" };
            var newMainTask = new MainTask() { UserId = 1, Id = 1, Description = "New Task" };

            _mockContext.Setup(m => m.Update(mainTaskUpdate, 1)).ReturnsAsync(newMainTask);

            //act
            var result = await _controller.Put(1, mainTaskUpdate);
            var item = (result as OkObjectResult)?.Value as MainTask;

            //assert
            item.Should().BeEquivalentTo(new MainTask { UserId = 1, Id = 1, Description = "New Task" });
        }

        [Fact]
        public async Task Delete_DeletesMainTask_Success()
        {
            //act
            var result = await _controller.Delete(1);

            //assert
            result.Should().BeEquivalentTo(new NoContentResult());
        }
    }
}
