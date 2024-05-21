namespace Domain.Models;

public class Tarefa
{
    public int? Id { get; set; }
    public int IdUsuario { get; set; }
    public string? Descricao { get; set; }
}
