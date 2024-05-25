using AppLoginCore_M.Models;

namespace AppLoginCore_M.Repository.Contract
{
    public interface IClienteRepository
    {
        // Login do Cliente
        Cliente Login(string Email, string Senha);

        //CRUD
        void Cadastrar(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Excluir(int Id);
        Cliente ObterCliente(int Id);
        IEnumerable<Cliente> ObterTodosClientes();
        void Ativar(int id);
        void Desativar(int id);
    }
}
