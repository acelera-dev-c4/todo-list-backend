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

        if (!_context.Users.Any())
        {
            var users = new User[]
            {
                new User { Name = "Jesus Wildes", Email = "jesus@mail.com", Password = password },
                new User { Name = "Maurício Mafra", Email = "mauricio@mail.com", Password = password },
                new User { Name = "Paulo Ewerson", Email = "paulo@mail.com", Password = password },
                new User { Name = "Pedro Augusto", Email = "pedro@mail.com", Password = password },
                new User { Name = "Raffaello Damgaard", Email = "raffaello@mail.com", Password = password },
                new User { Name = "Vinícius Silva", Email = "vinicius@mail.com", Password = password },
                new User { Name = "Weslley Batista", Email = "weslley@mail.com", Password = password }
            };

            _context.Users.AddRange(users);
            _context.SaveChanges();
        }

        if (!(_context.MainTasks.Any() && _context.SubTasks.Any()))
        {
            var tasks = new MainTask[]
            {
                new MainTask { UserId = 1, Description = "Compras no supermercado" },
                new MainTask { UserId = 2, Description = "Corrida matinal" },
                new MainTask { UserId = 3, Description = "Preparação para apresentação" },
                new MainTask { UserId = 4, Description = "Reunião de equipe" },
                new MainTask { UserId = 5, Description = "Atualização de relatórios" },
                new MainTask { UserId = 6, Description = "Desenvolvimento de novos recursos" },
                new MainTask { UserId = 7, Description = "Revisão de código" }
            };

            _context.MainTasks.AddRange(tasks);
            _context.SaveChanges();

            var subtasks = new SubTask[]
            {
                new SubTask { MainTaskId = (int)tasks[0].Id!, Description = "Comprar leite", Finished = false },
                new SubTask { MainTaskId = (int)tasks[0].Id!, Description = "Adquirir carne", Finished = false },
                new SubTask { MainTaskId = (int)tasks[0].Id!, Description = "Buscar pão", Finished = false },

                new SubTask { MainTaskId = (int)tasks[1].Id!, Description = "Correr 5 km", Finished = false },
                new SubTask { MainTaskId = (int)tasks[1].Id!, Description = "Fazer alongamentos", Finished = false },
                new SubTask { MainTaskId = (int)tasks[1].Id!, Description = "Hidratar-se", Finished = false },

                new SubTask { MainTaskId = (int)tasks[2].Id!, Description = "Preparar slides", Finished = false },
                new SubTask { MainTaskId = (int)tasks[2].Id!, Description = "Praticar apresentação", Finished = false },
                new SubTask { MainTaskId = (int)tasks[2].Id!, Description = "Revisar conteúdo", Finished = false },

                new SubTask { MainTaskId = (int)tasks[3].Id!, Description = "Agendar horário", Finished = false },
                new SubTask { MainTaskId = (int)tasks[3].Id!, Description = "Preparar pauta", Finished = false },
                new SubTask { MainTaskId = (int)tasks[3].Id!, Description = "Enviar convites", Finished = false },

                new SubTask { MainTaskId = (int)tasks[4].Id!, Description = "Coletar dados", Finished = false },
                new SubTask { MainTaskId = (int)tasks[4].Id!, Description = "Analisar resultados", Finished = false },
                new SubTask { MainTaskId = (int)tasks[4].Id!, Description = "Gerar relatório", Finished = false },

                new SubTask { MainTaskId = (int)tasks[5].Id!, Description = "Codificar funcionalidade X", Finished = false },
                new SubTask { MainTaskId = (int)tasks[5].Id!, Description = "Testar integração", Finished = false },
                new SubTask { MainTaskId = (int)tasks[5].Id!, Description = "Documentar alterações", Finished = false },

                new SubTask { MainTaskId = (int)tasks[6].Id!, Description = "Revisar pull request", Finished = false },
                new SubTask { MainTaskId = (int)tasks[6].Id!, Description = "Identificar bugs", Finished = false },
                new SubTask { MainTaskId = (int)tasks[6].Id!, Description = "Sugerir melhorias", Finished = false }
            };

            _context.SubTasks.AddRange(subtasks);
            _context.SaveChanges();
        }

    }
}
