using AceleraDevTodoListApi.DB;
using Domain.Entitys;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubTarefaController : Controller
    {
        private readonly MyDBContext _myDBContext;

        public SubTarefaController(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;
        }

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
                var subTarefa = _myDBContext.SubTarefas.Find(idSubtarefa);
                return subTarefa is null ? NotFound() : Ok(subTarefa);
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
