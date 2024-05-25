using AppLoginCore_M.Models;
using Newtonsoft.Json;

namespace AppLoginCore_M.CarrinhoCompra
{
    public class Carrinho
    {
        private string Key = "COOKIE.COMPRAS";
        private Cookie.Cookie _cookie;

        public Carrinho(Cookie.Cookie cookie)
        {
            _cookie = cookie;

        }
        public void Salvar(List<Livro> list)
        {
            string Valor = JsonConvert.SerializeObject(list);
            _cookie.Cadastrar(Key, Valor);
        }
        public List<Livro> Consultar()
        {
            if (_cookie.Existe(Key))
            {
                string valor = _cookie.Consultar(Key);
                return JsonConvert.DeserializeObject<List<Livro>>(valor);
            }
            else
            {
                return new List<Livro>();
            }
        }
        public void Cadastrar(Livro item)
        {
            List<Livro> Lista;
            if (_cookie.Existe(Key))
            {
                Lista = Consultar();
                var ItemLocalizado = Lista.SingleOrDefault(a => a.idLivro == item.idLivro);

                if (ItemLocalizado != null)
                {
                    ItemLocalizado.quantidade = item.quantidade + 1;
                }
                else
                {
                    Lista.Add(item);
                }
            }
            else
            {
                Lista = new List<Livro> { };
                Lista.Add(item);
            }
            Salvar(Lista);
        }
        public void Atualizar(Livro item)
        {
            var Lista = Consultar();
            var ItemLocalizado = Lista.SingleOrDefault(a => a.idLivro == item.idLivro);
            if (ItemLocalizado != null)
            {
                ItemLocalizado.quantidade = item.quantidade + 1;
                Salvar(Lista);
            }
        }
        public void Remover(Livro item)
        {
            var Lista = Consultar();
            var ItemLocalizado = Lista.SingleOrDefault(a => a.idLivro == item.idLivro);


            if (ItemLocalizado != null)
            {
                Lista.Remove(ItemLocalizado);
                Salvar(Lista);
            }
        }
        public bool Existe(String key)
        {
            if (_cookie.Existe(key))
            {
                return false;
            }
            return true;
        }
        public void RemoverTodos()
        {
            _cookie.Remover(Key);
        }
    }
}
