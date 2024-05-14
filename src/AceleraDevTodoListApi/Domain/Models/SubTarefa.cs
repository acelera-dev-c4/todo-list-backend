using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitys;
public class SubTarefa
{
    public int Id { get; set; }
    public int IdTarefa { get; set; }
    public string? Descricao { get; set; }
    public bool Concluida { get; set; } = false;
}
