namespace AceleraDevTodoListApi.Services
{
    public class TarefaService
    {
        private readonly TarefaRepository _tarefaRepository;

        public TarefaService(TarefaRepository tarefaRepository) {
            _tarefaRepository = tarefaRepository;
        }


        public Tarefa Create(Tarefa tarefa)
        {
            return _tarefaRepository.Create(tarefa);
        }

        public void Delete(int id)
        {
            _tarefaRepository.Delete(id);
        }

        public Tarefa? GetById(int id)
        {
            return _tarefaRepository.GetById(id);
        }

        public List<Tarefa> List()
        {
            return _tarefaRepository.GetAll();
        }

        public Tarefa Update(Tarefa tarefa, int id)
        {
            return _tarefaRepository.Update(tarefa, id);
        }
    }
}
