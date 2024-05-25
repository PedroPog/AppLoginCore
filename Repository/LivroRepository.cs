using AppLoginCore_M.Models;
using AppLoginCore_M.Repository.Contract;
using MySql.Data.MySqlClient;
using System.Data;

namespace AppLoginCore_M.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private readonly string _conexao;

        public LivroRepository(IConfiguration conf)
        {
            _conexao = conf.GetConnectionString("ConexaoMySQL");
        }

        public void Atualizar(Livro livro)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Livro livro)
        {
            using(var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into tbLivro " +
                    "values(default,@NomeLivro,@ImagemLivro,@Quantidade);", conexao);

                cmd.Parameters.Add("@NomeLivro", MySqlDbType.VarChar).Value = livro.nomeLivro;
                cmd.Parameters.Add("@ImagemLivro", MySqlDbType.VarChar).Value = livro.imagemLivro;
                cmd.Parameters.Add("@Quantidade", MySqlDbType.VarChar).Value = livro.quantidade;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public Livro ObterLivro(int id)
        {
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbLivro where idLivro=@id", conexao);
                cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Livro livro = new Livro();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    livro.idLivro = (int)dr["idLivro"];
                    livro.nomeLivro = (string)dr["nomeLivro"];
                    livro.imagemLivro = (string)dr["imagemLivro"];
                    livro.quantidade = (int)dr["quantidade"];
                }
                return livro;
            }
        }

        public IEnumerable<Livro> ObterTodosLivros()
        {
            List<Livro> LivroList = new List<Livro>();
            using (var conexao = new MySqlConnection(_conexao))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbLivro;", conexao);

                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sd.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    LivroList.Add(
                        new Livro
                        {
                            idLivro = (int)dr["idLivro"],
                            nomeLivro = (string)dr["nomeLivro"],
                            imagemLivro = (string)dr["imagemLivro"],
                            quantidade = (int)dr["quantidade"]
                        });
                }
                
                return LivroList;
            }
        }
    }
}
