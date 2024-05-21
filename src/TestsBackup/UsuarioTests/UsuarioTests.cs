using Microsoft.AspNetCore.Mvc;
using Moq;
using Api.Controllers;
using FluentAssertions;
using Domain.Models;
using Service;
using Domain.Responses;
using Domain.Request;

namespace Tests;
public class UsuarioTests
{
    private readonly UsuarioController _controller;
    private readonly Mock<IUsuarioService> _mockContext;
    
    public UsuarioTests()
    {
        _mockContext = new Mock<IUsuarioService>();

        var usuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Nome = "João", Email = "joao@aceleraDev.com", Senha = "senha123" },
            new Usuario { Id = 2, Nome = "Maria", Email = "maria@aceleraDev.com", Senha = "senha321" }
        };

        var mockSet = new Mock<List<UserResponse>>();
        mockSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(usuarios.AsQueryable().Provider);
        mockSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(usuarios.AsQueryable().Expression);
        mockSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(usuarios.AsQueryable().ElementType);
        mockSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(usuarios.GetEnumerator());

        _mockContext.Setup(m => m.List()).Returns(mockSet.Object);

        _controller = new UsuarioController(_mockContext.Object);
    }

    [Fact]
    public void Get_QuandoChamado_RetornaTodosUsuarios()
    {
        var resultado = _controller.List();

        resultado.Should().BeOfType<OkObjectResult>();
        var lista = (resultado as OkObjectResult)?.Value as List<Usuario>;
        if (lista != null)
        {
            lista.Should().NotBeNull();
            lista.Count.Should().Be(2);
        }
    }

    [Fact]
    public void Add_UsuarioValido_DeveRetornarUsuarioCriado()
    {
        var usuario = new UserRequest { Nome = "Teste 123", Email = "teste@aceleraDev.com", Senha = "senha123" };
        var resultado = _controller.Post(usuario);

        resultado.Should().BeOfType<OkObjectResult>();
        var item = (resultado as OkObjectResult)?.Value as Usuario;
        item?.Should().BeEquivalentTo(usuario, options => options.ComparingByMembers<Usuario>());
    }
}
