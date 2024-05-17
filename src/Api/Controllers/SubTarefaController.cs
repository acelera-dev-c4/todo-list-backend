using AceleraDevTodoListApi.DB;
using Domain.Entitys;
using Domain.Mappers;
using Domain.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubTarefaController : Controller
    {
        private readonly MyDBContext _myDBContext;

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_myDBContext.SubTarefas);
            }
            catch (Exception ex)
            {
                return BadRequest($"Falha ao mostrar as subtarefas. {ex.Message}");
            }
        }

        [HttpGet("{idSubtarefa}")]
        public IActionResult Get(int idSubtarefa)
        {
            try
            {
                var tarefa = _myDBContext.SubTarefas.Where(x => x.Id == idSubtarefa).FirstOrDefault();
                return tarefa is null ? NotFound() : Ok(tarefa);
            }
            catch (Exception ex)
            {
                return BadRequest($"Falha ao mostrar a subtarefa. {ex.Message}");
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody]SubTarefa novaSubTarefa)
        {
            try
            {
                _myDBContext.SubTarefas.Add(novaSubTarefa);
                _myDBContext.SaveChanges();
                return Ok(novaSubTarefa);
            }
            catch (Exception ex)
            {
                return BadRequest($"Falha na criação da subtarefa. {ex.Message}");
            }
        }

        [HttpPut("{idSubTarefa}")]
        public IActionResult Put(int idSubTarefa, [FromBody] string updateDescription)
        {
            try
            {
                var subTarefa = _myDBContext.Tarefas.Find(idSubTarefa);
                if (subTarefa is null)
                    return NotFound($"Subtarefa não encontrada.");

                subTarefa.Descricao = updateDescription;

                _myDBContext.Update(subTarefa);
                _myDBContext.SaveChanges();

                return Ok(subTarefa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{idSubTarefa}")]
        public IActionResult Delete([FromRoute]int idSubTarefa)
        {
            try
            {
                var subtarefa = _myDBContext.Tarefas.Find(idSubTarefa);
                if (subtarefa is null)
                    return NotFound($"SubTarefa não encontrada.");

                _myDBContext.Remove(subtarefa);
                _myDBContext.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Falha ao deletar a subtarefa. {ex.Message}");
            }
        }

    }
}
