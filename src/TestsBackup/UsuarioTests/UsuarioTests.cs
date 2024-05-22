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
    private readonly UserController _controller;
    private readonly Mock<IUserService> _mockContext;
    
    public UsuarioTests()
    {
        _mockContext = new Mock<IUserService>();

        var users = new List<User>
        {
            new User { Id = 1, Name = "João", Email = "joao@aceleraDev.com", Password = "senha123" },
            new User { Id = 2, Name = "Maria", Email = "maria@aceleraDev.com", Password = "senha321" }
        };

        var mockSet = new Mock<List<UserResponse>>();
        mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
        mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
        mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
        mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

        _mockContext.Setup(m => m.List()).Returns(mockSet.Object);

        _controller = new UserController(_mockContext.Object);
    }

    [Fact]
    public void Get_QuandoChamado_RetornaTodosUsuarios()
    {
        var result = _controller.List();

        result.Should().BeOfType<OkObjectResult>();
        var list = (result as OkObjectResult)?.Value as List<User>;
        if (list != null)
        {
            list.Should().NotBeNull();
            list.Count.Should().Be(2);
        }
    }

    [Fact]
    public void Add_UsuarioValido_DeveRetornarUsuarioCriado()
    {
        var user = new UserRequest { Name = "Teste 123", Email = "teste@aceleraDev.com", Password = "senha123" };
        var result = _controller.Post(user);

        result.Should().BeOfType<OkObjectResult>();
        var item = (result as OkObjectResult)?.Value as User;
        item?.Should().BeEquivalentTo(user, options => options.ComparingByMembers<User>());
    }
}
