using System.Collections;
using AppLoginCore_M.Models;

namespace AppLoginCore_M.Repository.Contract
{
    public interface ILivroRepository
    {
        IEnumerable<Livro> ObterTodosLivros();
        void Cadastrar(Livro livro);
        void Atualizar(Livro livro);
        Livro ObterLivro(int id);
        void Excluir(int id);
    }
}
