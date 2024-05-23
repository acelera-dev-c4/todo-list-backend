using Microsoft.AspNetCore.Mvc;
using Moq;
using Api.Controllers;
using FluentAssertions;
using Domain.Models;
using Service;
using Domain.Responses;
using Domain.Request;

namespace Tests;
public class UserTests
{
    private readonly UserController _controller;
    private readonly Mock<IUserService> _mockContext;

    public UserTests()
    {
        _mockContext = new Mock<IUserService>();

        var usuarios = new List<UserResponse>
        {
            new UserResponse { Id = 1, Name = "João", Email = "joao@aceleraDev.com"},
            new UserResponse { Id = 2, Name = "Maria", Email = "maria@aceleraDev.com" }
        };

        _mockContext.Setup(m => m.List()).Returns(usuarios);

        _controller = new UserController(_mockContext.Object);
    }

    [Fact]
    public void Get_QuandoChamado_RetornaTodosUsuarios()
    {
        var result = _controller.List();

        result.Should().BeOfType<OkObjectResult>();
        var list = (result as OkObjectResult)?.Value as List<UserResponse>;

        list.Should().NotBeNull();
        list?.Count.Should().Be(2);
    }

    [Fact]
    public void Add_UsuarioValido_DeveRetornarUsuarioCriado()
    {
        var userRequest = new UserRequest { Name = "Teste 123", Email = "teste@aceleraDev.com", Password = "senha123" };
        var userResponse = new UserResponse { Id = 1, Name = "Teste 123", Email = "teste@aceleraDev.com" };
        _mockContext.Setup(m => m.Create(userRequest)).Returns(userResponse);

        var result = _controller.Post(userRequest);

        var item = (result as OkObjectResult)?.Value as UserResponse;

        item.Should().BeEquivalentTo(new UserResponse { Id = 1, Name = "Teste 123", Email = "teste@aceleraDev.com" });
    }
}
