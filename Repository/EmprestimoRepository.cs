using AppLoginCore_M.Models;
using AppLoginCore_M.Repository.Contract;
using MySql.Data.MySqlClient;

namespace AppLoginCore_M.Repository
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly string _Conexao;
        public EmprestimoRepository(IConfiguration conexao)
        {
            _Conexao = conexao.GetConnectionString("ConexaoMySQL");
        }
        public void Atualizar(Emprestimo emprestimo)
        {
            throw new NotImplementedException();
        }

        public void buscarUltimoEmp(Emprestimo emprestimo)
        {
            using (var conexao = new MySqlConnection(_Conexao))
            {
                conexao.Open();
                MySqlDataReader dr;

                MySqlCommand cmd = new MySqlCommand("SELECT idEmp FROM tbEmprestimo ORDER BY idEmp DESC LIMIT 1;", conexao);
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    emprestimo.idEmp = (int)dr[0];//dr[0].ToString();
                }
                conexao.Close();
            }
        }

        public void Cadastrar(Emprestimo emprestimo)
        {
            using (var conexao = new MySqlConnection(_Conexao))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO tbEmprestimo VALUES(default,@dataemp,@datadev,@idusuario);", conexao);
                cmd.Parameters.Add("@dataemp", MySqlDbType.VarChar).Value = emprestimo.dtEmpre;
                cmd.Parameters.Add("@datadev", MySqlDbType.VarChar).Value = emprestimo.dtDev;
                cmd.Parameters.Add("@idusuario", MySqlDbType.VarChar).Value = emprestimo.idCliente;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Deletar(int idEmpre)
        {
            throw new NotImplementedException();
        }

        public Emprestimo ObteEmprestimoPorId(int idEmpre)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Emprestimo> ObterTodosOsEmprestimo()
        {
            throw new NotImplementedException();
        }
    }
}
