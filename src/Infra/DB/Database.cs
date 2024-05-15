using Domain.Entitys;
using System.Data.SqlClient;

namespace AceleraDevTodoListApi.DB;

public class Database
{
    //private readonly MyDBContext _myDBContext;

    //public Database(MyDBContext myDBContext)
    //{
    //    _myDBContext = myDBContext;
    //}

    //public static void GetData(MyDBContext _myDBContext)
    //{
    //    using (var connection = new SqlConnection(DatabaseHelper.ConnectionString))
    //    {
    //        connection.Open();
    //        List<Usuario> usuarios = _myDBContext.Usuarios.ToList();
    //        List<Tarefa> tarefas = _myDBContext.Tarefas.ToList();
    //        List<SubTarefa> subTarefas = _myDBContext.SubTarefas.ToList();
    //        connection.Close();
    //    }
    //}

    //public static void SaveData()
    //{
    //    using (var connection = new SqlConnection(DatabaseHelper.ConnectionString))
    //    {
    //        connection.Open();
    //        _myDBContext.SaveChanges();
    //        connection.Close();
    //    }
    //}
}
