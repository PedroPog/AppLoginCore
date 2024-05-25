namespace AppLoginCore_M.Cookie
{
    public class Cookie
    {
        private IHttpContextAccessor _context;
        private IConfiguration _configuration;

        public Cookie(IHttpContextAccessor context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public void Cadastrar(string key, string valor)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(7);
            options.IsEssential = true;

            _context.HttpContext.Response.Cookies.Append(key, valor, options);
        }

        public void Atualizar(string key, string valor)
        {
            if (Existe(key))
            {
                Remover(key);
            }
            Cadastrar(key, valor);
        }

        public void Remover(string key)
        {
            _context.HttpContext.Response.Cookies.Delete(key);
        }
        public string Consultar(string key, bool Cript = true)
        {
            var valor = _context.HttpContext.Request.Cookies[key];
            return valor;
        }
        public bool Existe(string key)
        {
            if (_context.HttpContext.Request.Cookies[key] == null)
            {
                return false;
            }
            return true;
        }
    }
}
