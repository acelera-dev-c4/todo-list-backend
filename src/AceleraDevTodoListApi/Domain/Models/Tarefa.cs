using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitys;
public class Tarefa
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public string? Descricao { get; set; }
}
