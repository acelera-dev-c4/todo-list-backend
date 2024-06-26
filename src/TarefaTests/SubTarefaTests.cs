using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.EntityFrameworkCore;
using Api.Controllers;
using Domain.Entitys;
using AceleraDevTodoListApi.DB;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TarefaTests
{
    public class SubTarefaTests
    {
        private readonly SubTarefaController _controller;
        private readonly Mock<MyDBContext> _mockContext;

        public SubTarefaTests()
        {
            _mockContext = new Mock<MyDBContext>();

            var subtarefas = new List<SubTarefa>
        {
            new SubTarefa { Id = 1, IdTarefa = 10, Descricao = "SubTarefa1", Concluida = false },
            new SubTarefa { Id = 5, IdTarefa = 1, Descricao = "SubTarefa2", Concluida = true },
            new SubTarefa { Id = 61, IdTarefa = 87, Descricao = "SubTarefa3", Concluida = false },
            new SubTarefa { Id = 6, IdTarefa = 500, Descricao = "SubTarefa4", Concluida = true }

        };

            var mockSet = new Mock<DbSet<SubTarefa>>();
            mockSet.As<IQueryable<SubTarefa>>().Setup(m => m.Provider).Returns(subtarefas.AsQueryable().Provider);
            mockSet.As<IQueryable<SubTarefa>>().Setup(m => m.Expression).Returns(subtarefas.AsQueryable().Expression);
            mockSet.As<IQueryable<SubTarefa>>().Setup(m => m.ElementType).Returns(subtarefas.AsQueryable().ElementType);
            mockSet.As<IQueryable<SubTarefa>>().Setup(m => m.GetEnumerator()).Returns(subtarefas.GetEnumerator());

            _mockContext.Setup(m => m.SubTarefas).Returns(mockSet.Object);

            _controller = new SubTarefaController(_mockContext.Object);
        }

        [Fact]
        public void Get_RetornaTodaLista_Sucesso()
        {
            var resultado = _controller.Get();

            var lista = (resultado as OkObjectResult)?.Value as List<SubTarefa>;
            
            lista.Should().NotBeNull();
            lista.Should().HaveCount(4);
     
        }
        [Fact]
        public void Post_CriaNovaSubTarefa_Sucesso()
        {
            var subTarefaTest = new SubTarefa { Id = 9, IdTarefa = 2, Descricao = "subtarefa teste", Concluida = true };
            

            var resultado = _controller.Post(subTarefaTest);
            var item = (resultado as OkObjectResult)?.Value as SubTarefa;
            
            item.Should().Be(subTarefaTest);
        }
        [Fact]
        public void Put_TarefaAtualiza_Sucesso()
        {
            
        }
        [Fact]
        public void Delete_TarefaDeletada_Sucesso()
        {
          
        }
    }
}