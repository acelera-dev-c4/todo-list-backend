namespace Domain.Models;
public class SubTarefas
{
    public int Id { get; set; }
    public int IdTarefa { get; set; }
    public string? Descricao { get; set; }
    public bool Concluido { get; set; } = false;
}
