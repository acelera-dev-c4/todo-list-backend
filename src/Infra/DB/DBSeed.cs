using Domain.Models;

namespace Infra.DB;

public class DBSeed
{
    private readonly MyDBContext _context;
    private const string password = "4674:a6AtKJMgypWPXSPp1qZlvDc4b0EMImgs:OerG6dNhcIuLslOFqE7uTlTKttE="; //SecurePassword123
    public DBSeed(MyDBContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (!(_context.MainTasks.Any() && _context.SubTasks.Any() && _context.Users.Any()))
        {
            var users = new User[]
            {
                new User { Name = "Jesus Wildes", Email = "jesus@mail.com", Password = password },
                new User { Name = "Maurício Mafra", Email = "mauricio@mail.com", Password = password },
                new User { Name = "Paulo Ewerson", Email = "paulo@mail.com", Password = password },
                new User { Name = "Pedro Augusto", Email = "pedro@mail.com", Password = password },
                new User { Name = "Raffaello Damgaard", Email = "raffaello@mail.com", Password = password },
                new User { Name = "Vinícius Silva", Email = "vinicius@mail.com", Password = password },
                new User { Name = "Weslley Batista", Email = "weslley@mail.com", Password = password },
                new User { Name = "Igor Cordeiro", Email = "igor@mail.com", Password= password},
                new User { Name = "Anderson Gomes", Email = "anderson@mail.com", Password= password},
                new User { Name = "Diogenes Lima", Email = "diogenes@mail.com", Password= password},
                new User { Name = "Leandro Pio", Email = "leandro@mail.com", Password= password},
                new User { Name = "Patric Costa", Email = "patric@mail.com", Password= password},
                new User { Name = "Vagner Silva", Email = "vagner@mail.com", Password= password},
                new User { Name = "system", Email = "system@mail.com", Password= password},
            };

            _context.Users.AddRange(users);
            _context.SaveChanges();

            var tasks = new MainTask[]
            {
                new MainTask { UserId = (int)users[0].Id!, Description = "Compras no supermercado" },
                new MainTask { UserId = (int)users[1].Id!, Description = "Corrida matinal" },
                new MainTask { UserId = (int)users[2].Id!, Description = "Preparação para apresentação" },
                new MainTask { UserId = (int)users[3].Id!, Description = "Reunião de equipe" },
                new MainTask { UserId = (int)users[4].Id!, Description = "Atualização de relatórios" },
                new MainTask { UserId = (int)users[5].Id!, Description = "Desenvolvimento de novos recursos" },
                new MainTask { UserId = (int)users[6].Id!, Description = "Revisão de código" },
                new MainTask { UserId = (int)users[7].Id!, Description = "Leitura matinal" },
                new MainTask { UserId = (int)users[8].Id!, Description = "Trabalhar" },
                new MainTask { UserId = (int)users[9].Id!, Description = "Escrever código" },
                new MainTask { UserId = (int)users[10].Id!, Description = "Malhar" },
                new MainTask { UserId = (int)users[11].Id!, Description = "Capacitação" },
                new MainTask { UserId = (int)users[12].Id!, Description = "Estudar" },

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
                new SubTask { MainTaskId = (int)tasks[6].Id!, Description = "Sugerir melhorias", Finished = false },

                new SubTask { MainTaskId = (int)tasks[7].Id!, Description = "Preparar um cafe", Finished = false },
                new SubTask { MainTaskId = (int)tasks[7].Id!, Description = "Ler", Finished = false },
                new SubTask { MainTaskId = (int)tasks[7].Id!, Description = "Refletir sobre o que foi lido", Finished = false },

                new SubTask { MainTaskId = (int)tasks[8].Id!, Description = "Tomar banho", Finished = false },
                new SubTask { MainTaskId = (int)tasks[8].Id!, Description = "Se arrumar", Finished = false },
                new SubTask { MainTaskId = (int)tasks[8].Id!, Description = "Pegar condução", Finished = false },

                new SubTask { MainTaskId = (int)tasks[9].Id!, Description = "Revisar codigo", Finished = false },
                new SubTask { MainTaskId = (int)tasks[9].Id!, Description = "Codar", Finished = false },
                new SubTask { MainTaskId = (int)tasks[9].Id!, Description = "Testar o que foi feito", Finished = false },

                new SubTask { MainTaskId = (int)tasks[10].Id!, Description = "Malhar Peito", Finished = false },
                new SubTask { MainTaskId = (int)tasks[10].Id!, Description = "Malhar triceps", Finished = false },
                new SubTask { MainTaskId = (int)tasks[10].Id!, Description = "Fazer cardio", Finished = false },

                new SubTask { MainTaskId = (int)tasks[11].Id!, Description = "Ver video-aula", Finished = false },
                new SubTask { MainTaskId = (int)tasks[11].Id!, Description = "Fazer anotacoes", Finished = false },
                new SubTask { MainTaskId = (int)tasks[11].Id!, Description = "Fazer um resumo", Finished = false },

                new SubTask { MainTaskId = (int)tasks[12].Id!, Description = "Chegar na faculdade", Finished = false },
                new SubTask { MainTaskId = (int)tasks[12].Id!, Description = "Assistir as aulas", Finished = false },
                new SubTask { MainTaskId = (int)tasks[12].Id!, Description = "Fazer exercicios", Finished = false },
            };

            _context.SubTasks.AddRange(subtasks);
            _context.SaveChanges();

            var subscriptions = new Subscription[]
            {
                new Subscription { MainTaskIdTopic = (int)tasks[0].Id!, SubTaskIdSubscriber = (int)subtasks[3].Id! },
                new Subscription { MainTaskIdTopic = (int)tasks[2].Id!, SubTaskIdSubscriber = (int)subtasks[7].Id! },
                new Subscription { MainTaskIdTopic = (int)tasks[4].Id!, SubTaskIdSubscriber = (int)subtasks[2].Id! },
                new Subscription { MainTaskIdTopic = (int)tasks[6].Id!, SubTaskIdSubscriber = (int)subtasks[5].Id! },
                new Subscription { MainTaskIdTopic = (int)tasks[5].Id!, SubTaskIdSubscriber = (int)subtasks[1].Id! },
            };

            _context.Subscriptions.AddRange(subscriptions);
            _context.SaveChanges();

            var notifications = new Notifications[]
            {
                new Notifications { SubscriptionId = (int)subscriptions[0].Id!, Message = $"Tarefa {tasks[0].Description!} Completa!", Readed = false, UserId = 2 },
                new Notifications { SubscriptionId = (int)subscriptions[1].Id!, Message = $"Tarefa {tasks[2].Description!} Completa!", Readed = false, UserId = 3 },
                new Notifications { SubscriptionId = (int)subscriptions[2].Id!, Message = $"Tarefa {tasks[4].Description!} Completa!", Readed = false, UserId = 1 },
                new Notifications { SubscriptionId = (int)subscriptions[3].Id!, Message = $"Tarefa {tasks[6].Description!} Completa!", Readed = false, UserId = 2 },
                new Notifications { SubscriptionId = (int)subscriptions[4].Id!, Message = $"Tarefa {tasks[5].Description!} Completa!", Readed = false, UserId = 1 }
            };

            _context.Notifications.AddRange(notifications);
            _context.SaveChanges();
        }

    }
}
