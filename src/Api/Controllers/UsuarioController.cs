﻿using Services;
using Domain.Request;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet("Lista")]
    public IActionResult List()
    {
        var users = _usuarioService.List();
        return Ok(users);
    }

    [HttpPost("Cadastro")]
    public IActionResult Post([FromBody] UserRequest usuario)
    {
        var newUser = _usuarioService.Create(usuario);
        return Ok(newUser);
    }

    [HttpPut("Atualizar/{Id}")]
    public IActionResult Put(int id, [FromBody] UpdatedUserRequest usuario)
    {
        usuario.Id = id;
        var updatedUser = _usuarioService.Update(usuario);
        return Ok(updatedUser);
    }
}
