using Infra.DB;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SubTaskController : ControllerBase
    {
        private readonly MyDBContext _myDBContext;

        public SubTaskController(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_myDBContext.SubTasks.ToList());
        }

        [HttpGet("{subTaskId}")]
        public IActionResult Get(int subTaskId)
        {
            var subTask = _myDBContext.SubTasks.Find(subTaskId);
            return subTask is null ? NotFound() : Ok(subTask);
        }

        [HttpPost]
        public IActionResult Post([FromBody] SubTask newSubTask)
        {
            _myDBContext.SubTasks.Add(newSubTask);
            _myDBContext.SaveChanges();
            return Ok(newSubTask);
        }

        [HttpPut("{subTaskId}")]
        public IActionResult Put(int subTaskId, [FromBody] string updateDescription)
        {
            var subTask = _myDBContext.SubTasks.Find(subTaskId);
            if (subTask is null)
            {
                return NotFound("Subtarefa não encontrada.");
            }

            subTask.Description = updateDescription;

            _myDBContext.Update(subTask);
            _myDBContext.SaveChanges();

            return Ok(subTask);
        }

        [HttpDelete("{subTaskId}")]
        public IActionResult Delete([FromRoute] int subTaskId)
        {
            var subTask = _myDBContext.SubTasks.Find(subTaskId);

            if (subTask is null)
            {
                return NotFound("SubTarefa não encontrada.");
            }

            _myDBContext.Remove(subTask);
            _myDBContext.SaveChanges();

            return NoContent();
        }
    }
}
