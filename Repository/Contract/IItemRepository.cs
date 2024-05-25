using AppLoginCore_M.Models;

namespace AppLoginCore_M.Repository.Contract
{
    public interface IItemRepository
    {
        IEnumerable<Item> ObterTodosOsItens();
        Item ObteItemPorId(int idItem);
        void Cadastrar(Item item);
        void Atualizar(Item item);
        void Deletar(int idItem);
    }
}
