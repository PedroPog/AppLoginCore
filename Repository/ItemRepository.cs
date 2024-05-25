using AppLoginCore_M.Models;
using AppLoginCore_M.Repository.Contract;
using MySql.Data.MySqlClient;

namespace AppLoginCore_M.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly string _Conexao;
        public ItemRepository(IConfiguration conexao)
        {
            _Conexao = conexao.GetConnectionString("ConexaoMySQL");
        }
        public void Atualizar(Item item)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Item item)
        {
            using (var conexao = new MySqlConnection(_Conexao))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO itemsEmp VALUES(default,@idEmp,@idLivro);", conexao);
                cmd.Parameters.Add("@idEmp", MySqlDbType.VarChar).Value = item.idEmp;
                cmd.Parameters.Add("@idLivro", MySqlDbType.VarChar).Value = item.idLivro;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Deletar(int idItem)
        {
            throw new NotImplementedException();
        }

        public Item ObteItemPorId(int idItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> ObterTodosOsItens()
        {
            throw new NotImplementedException();
        }
    }
}
