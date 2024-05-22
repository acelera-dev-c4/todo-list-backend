using Domain.Models;

namespace Infra.DB;

public class DBSeed
{
    private readonly MyDBContext _context;
    private const string password = "senhasegura";
    public DBSeed(MyDBContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        _context.Database.EnsureCreated();

        if (!_context.Usuarios.Any())
        {
            var users = new Usuario[]
            {
                new Usuario { Nome = "Jesus Wildes", Email = "jesus@mail.com", Senha = password },
                new Usuario { Nome = "Maurício Mafra", Email = "mauricio@mail.com", Senha = password },
                new Usuario { Nome = "Paulo Ewerson", Email = "paulo@mail.com", Senha = password },
                new Usuario { Nome = "Pedro Augusto", Email = "pedro@mail.com", Senha = password },
                new Usuario { Nome = "Raffaello Damgaard", Email = "raffaello@mail.com", Senha = password },
                new Usuario { Nome = "Vinícius Silva", Email = "vinicius@mail.com", Senha = password },
                new Usuario { Nome = "Weslley Batista", Email = "weslley@mail.com", Senha = password }
            };

            _context.Usuarios.AddRange(users);
            _context.SaveChanges();
        }

        if (!(_context.Tarefas.Any() && _context.SubTarefas.Any()))
        {
            var tarefas = new MainTask[]
            {
                new MainTask { IdUsuario = 1, Descricao = "Compras no supermercado" },
                new MainTask { IdUsuario = 2, Descricao = "Corrida matinal" },
                new MainTask { IdUsuario = 3, Descricao = "Preparação para apresentação" },
                new MainTask { IdUsuario = 4, Descricao = "Reunião de equipe" },
                new MainTask { IdUsuario = 5, Descricao = "Atualização de relatórios" },
                new MainTask { IdUsuario = 6, Descricao = "Desenvolvimento de novos recursos" },
                new MainTask { IdUsuario = 7, Descricao = "Revisão de código" }
            };

            _context.Tarefas.AddRange(tarefas);
            _context.SaveChanges();

            var subtarefas = new SubTarefa[]
            {
                new SubTarefa { IdTarefa = (int)tarefas[0].Id!, Descricao = "Comprar leite", Concluida = false },
                new SubTarefa { IdTarefa = (int)tarefas[0].Id!, Descricao = "Adquirir carne", Concluida = false },
                new SubTarefa { IdTarefa = (int)tarefas[0].Id!, Descricao = "Buscar pão", Concluida = false },

                new SubTarefa { IdTarefa = (int)tarefas[1].Id!, Descricao = "Correr 5 km", Concluida = false },
                new SubTarefa { IdTarefa = (int)tarefas[1].Id!, Descricao = "Fazer alongamentos", Concluida = false },
                new SubTarefa { IdTarefa = (int)tarefas[1].Id!, Descricao = "Hidratar-se", Concluida = false },

                new SubTarefa { IdTarefa = (int)tarefas[2].Id!, Descricao = "Preparar slides", Concluida = false },
                new SubTarefa { IdTarefa = (int)tarefas[2].Id!, Descricao = "Praticar apresentação", Concluida = false },
                new SubTarefa { IdTarefa = (int)tarefas[2].Id!, Descricao = "Revisar conteúdo", Concluida = false },

                new SubTarefa { IdTarefa = (int)tarefas[3].Id!, Descricao = "Agendar horário", Concluida = false },
                new SubTarefa { IdTarefa = (int)tarefas[3].Id!, Descricao = "Preparar pauta", Concluida = false },
                new SubTarefa { IdTarefa = (int)tarefas[3].Id!, Descricao = "Enviar convites", Concluida = false },

                new SubTarefa { IdTarefa = (int)tarefas[4].Id!, Descricao = "Coletar dados", Concluida = false },
                new SubTarefa { IdTarefa = (int)tarefas[4].Id!, Descricao = "Analisar resultados", Concluida = false },
                new SubTarefa { IdTarefa = (int)tarefas[4].Id!, Descricao = "Gerar relatório", Concluida = false },

                new SubTarefa { IdTarefa = (int)tarefas[5].Id!, Descricao = "Codificar funcionalidade X", Concluida = false },
                new SubTarefa { IdTarefa = (int)tarefas[5].Id!, Descricao = "Testar integração", Concluida = false },
                new SubTarefa { IdTarefa = (int)tarefas[5].Id!, Descricao = "Documentar alterações", Concluida = false },

                new SubTarefa { IdTarefa = (int)tarefas[6].Id!, Descricao = "Revisar pull request", Concluida = false },
                new SubTarefa { IdTarefa = (int)tarefas[6].Id!, Descricao = "Identificar bugs", Concluida = false },
                new SubTarefa { IdTarefa = (int)tarefas[6].Id!, Descricao = "Sugerir melhorias", Concluida = false }
            };

            _context.SubTarefas.AddRange(subtarefas);
            _context.SaveChanges();
        }

    }
}
