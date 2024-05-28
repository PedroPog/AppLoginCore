namespace AppLoginCore_M.Cookie
{
    public class Cookie
    {
        private readonly IHttpContextAccessor _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<Cookie> _logger;

        public Cookie(IHttpContextAccessor context, IConfiguration configuration, ILogger<Cookie> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public void Cadastrar(string key, string valor)
        {
            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                IsEssential = true,
                Secure = true, // Defina como seguro
                SameSite = SameSiteMode.None // Exigir secure quando SameSite=None
            };

            _context.HttpContext.Response.Cookies.Append(key, valor, options);
            _logger.LogInformation($"Cookie {key} criado com valor {valor}");
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
            _logger.LogInformation($"Cookie {key} removido");
        }

        public string Consultar(string key, bool Cript = true)
        {
            var valor = _context.HttpContext.Request.Cookies[key];
            _logger.LogInformation($"Cookie {key} consultado com valor {valor}");
            return valor;
        }

        public bool Existe(string key)
        {
            bool existe = _context.HttpContext.Request.Cookies[key] != null;
            _logger.LogInformation($"Cookie {key} existe: {existe}");
            return existe;
        }
    }
}
