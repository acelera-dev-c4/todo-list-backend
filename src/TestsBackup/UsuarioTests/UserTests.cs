using Api.Controllers;
using Domain.Request;
using Domain.Responses;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service;

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

        _mockContext.Setup(m => m.List()).ReturnsAsync(usuarios);

        _controller = new UserController(_mockContext.Object);
    }

    [Fact]
    public async Task Get_QuandoChamado_RetornaTodosUsuarios()
    {
        var result = await _controller.List();

        result.Should().BeOfType<OkObjectResult>();
        var list = (result as OkObjectResult)?.Value as List<UserResponse>;

        list.Should().NotBeNull();
        list?.Count.Should().Be(2);
    }

    [Fact]
    public async Task Add_UsuarioValido_DeveRetornarUsuarioCriado()
    {
        var userRequest = new UserRequest { Name = "Teste 123", Email = "teste@aceleraDev.com", Password = "senha123" };
        var userResponse = new UserResponse { Id = 1, Name = "Teste 123", Email = "teste@aceleraDev.com" };
        _mockContext.Setup(m => m.Create(userRequest)).ReturnsAsync(userResponse);

        var result = await _controller.Post(userRequest);

        var item = (result as OkObjectResult)?.Value as UserResponse;

        item.Should().BeEquivalentTo(new UserResponse { Id = 1, Name = "Teste 123", Email = "teste@aceleraDev.com" });
    }
}
