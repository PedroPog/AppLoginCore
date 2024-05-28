using AppLoginCore_M.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AppLoginCore_M.Cookie
{
    public class Carrinho
    {
        private string Key = "COOKIE.COMPRAS";
        private Cookie _cookie;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public Carrinho(Cookie cookie, IHttpContextAccessor httpContext)
        {
            _cookie = cookie;
            _httpContextAccessor = httpContext;
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
            
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                IsEssential = true
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("key", "valor", options);
            


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
                Lista = new List<Livro>();
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
        public bool Existe(string key)
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
