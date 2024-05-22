using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.EntityFrameworkCore;
using Api.Controllers;
using FluentAssertions;
using Infra.DB;
using Domain.Models;

namespace Tests
{
    public class SubTasksTests
    {
        //private readonly SubTaskController _controller;
        //private readonly Mock<MyDBContext> _mockContext;

        //public SubTasksTests()
        //{
        //    _mockContext = new Mock<MyDBContext>();

        //    var subTasks = new List<SubTask>
        //{
        //    new SubTask { Id = 1, MainTaskId = 10, Description = "SubTarefa1", Finished = false },
        //    new SubTask { Id = 5, MainTaskId = 1, Description = "SubTarefa2", Finished = true },
        //    new SubTask { Id = 61, MainTaskId = 87, Description = "SubTarefa3", Finished = false },
        //    new SubTask { Id = 6, MainTaskId = 500, Description = "SubTarefa4", Finished = true }

        //};

        //    var mockSet = new Mock<DbSet<SubTask>>();
        //    mockSet.As<IQueryable<SubTask>>().Setup(m => m.Provider).Returns(subTasks.AsQueryable().Provider);
        //    mockSet.As<IQueryable<SubTask>>().Setup(m => m.Expression).Returns(subTasks.AsQueryable().Expression);
        //    mockSet.As<IQueryable<SubTask>>().Setup(m => m.ElementType).Returns(subTasks.AsQueryable().ElementType);
        //    mockSet.As<IQueryable<SubTask>>().Setup(m => m.GetEnumerator()).Returns(subTasks.GetEnumerator());

        //    _mockContext.Setup(m => m.SubTasks).Returns(mockSet.Object);

        //    _controller = new SubTaskController(_mockContext.Object);
        //}

        //[Fact]
        //public void Get_RetornaTodaLista_Sucesso()
        //{
        //    var result = _controller.Get();

        //    var list = (result as OkObjectResult)?.Value as List<SubTask>;


        //    list.Should().NotBeNull();
        //    list.Should().HaveCount(4);

        //}
        //[Fact]
        //public void Get_RetornaUmaSubTarefa_Sucesso()
        //{

        //    var subTasks = new List<SubTask>
        //{
        //    new SubTask { Id = 1, MainTaskId = 10, Description = "SubTarefa1", Finished = false },
        //    new SubTask { Id = 5, MainTaskId = 1, Description = "SubTarefa2", Finished = true },
        //    new SubTask { Id = 61, MainTaskId = 87, Description = "SubTarefa3", Finished = false },
        //    new SubTask { Id = 6, MainTaskId = 500, Description = "SubTarefa4", Finished = true }

        //};
        //    _mockContext.Setup(m => m.SubTasks.Find(61)).Returns(subTasks[2]);


        //    var result = _controller.Get(61);
        //    var item = (result as OkObjectResult)?.Value as SubTask;


        //    item.Should().BeEquivalentTo(new SubTask { Id = 61, MainTaskId = 87, Description = "SubTarefa3", Finished = false });
        //}

        //[Fact]
        //public void Post_CriaNovaSubTarefa_Sucesso()
        //{
        //    var subTaskTest = new SubTask { Id = 9, MainTaskId = 2, Description = "subtarefa teste", Finished = true };


        //    var result = _controller.Post(subTaskTest);
        //    var item = (result as OkObjectResult)?.Value as SubTask;

        //    item.Should().Be(subTaskTest);
        //}
        //[Fact]
        //public void Put_TarefaAtualiza_Sucesso()
        //{
        //    //arrange
        //    var subTasks = new List<SubTask>
        //{
        //    new SubTask { Id = 1, MainTaskId = 10, Description = "SubTarefa1", Finished = false }
        //};

        //    var mockSet = new Mock<DbSet<SubTask>>();
        //    mockSet.As<IQueryable<SubTask>>().Setup(m => m.Provider).Returns(subTasks.AsQueryable().Provider);
        //    mockSet.As<IQueryable<SubTask>>().Setup(m => m.Expression).Returns(subTasks.AsQueryable().Expression);
        //    mockSet.As<IQueryable<SubTask>>().Setup(m => m.ElementType).Returns(subTasks.AsQueryable().ElementType);
        //    mockSet.As<IQueryable<SubTask>>().Setup(m => m.GetEnumerator()).Returns(subTasks.GetEnumerator());

        //    _mockContext.Setup(m => m.SubTasks.Find(1)).Returns(subTasks[0]);

        //    //act
        //    var resultado = _controller.Put(1, "Nova Subtarefa");
        //    var item = (resultado as OkObjectResult)?.Value as SubTask;

        //    //assert
        //    item.Description.Should().Be("Nova Subtarefa");

        //}
        //[Fact]
        //public void Delete_TarefaDeletada_Sucesso()
        //{
        //    //arrange
        //    var subTasks = new List<SubTask>
        //{
        //    new SubTask { Id = 1, MainTaskId = 10, Description = "SubTarefa1", Finished = false },
        //    new SubTask { Id = 5, MainTaskId = 1, Description = "SubTarefa2", Finished = true },
        //    new SubTask { Id = 61, MainTaskId = 87, Description = "SubTarefa3", Finished = false },
        //    new SubTask { Id = 6, MainTaskId = 500, Description = "SubTarefa4", Finished = true }
        //};

        //    var mockSet = new Mock<DbSet<SubTask>>();
        //    mockSet.As<IQueryable<SubTask>>().Setup(m => m.Provider).Returns(subTasks.AsQueryable().Provider);
        //    mockSet.As<IQueryable<SubTask>>().Setup(m => m.Expression).Returns(subTasks.AsQueryable().Expression);
        //    mockSet.As<IQueryable<SubTask>>().Setup(m => m.ElementType).Returns(subTasks.AsQueryable().ElementType);
        //    mockSet.As<IQueryable<SubTask>>().Setup(m => m.GetEnumerator()).Returns(subTasks.GetEnumerator());

        //    _mockContext.Setup(m => m.SubTasks.Find(1)).Returns(subTasks[0]);

        //    //act
        //    var result = _controller.Delete(1);
        //    var item = (result as OkObjectResult)?.Value as SubTask;

        //    //assert
        //    item.Should().BeNull();

        }
    }
}