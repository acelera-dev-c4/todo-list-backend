namespace Domain.Models;
public class SubTarefa
{
    public int? Id { get; set; }
    public int IdTarefa { get; set; }
    public string? Descricao { get; set; }
    public bool Concluida { get; set; } = false;
}
