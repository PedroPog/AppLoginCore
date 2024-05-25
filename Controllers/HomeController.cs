using AppLoginCore_M.CarrinhoCompra;
using AppLoginCore_M.Libraries.Filtro;
using AppLoginCore_M.Libraries.Login;
using AppLoginCore_M.Models;
using AppLoginCore_M.Models.Constant;
using AppLoginCore_M.Repository;
using AppLoginCore_M.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppLoginCore_M.Controllers
{
    public class HomeController : Controller
    {
        private IClienteRepository _clienteRepository;
        private LoginCliente _loginCliente;

        private ILivroRepository _livroRepository;
        private Carrinho _carrinho;
        private IEmprestimoRepository _emprestimoRepository;
        private IItemRepository _itemRepository;


        public HomeController(IClienteRepository clienteRepository, LoginCliente loginCliente,
            ILivroRepository livroRepository,
            IEmprestimoRepository emprestimoRepository,
            IItemRepository itemRepository, Carrinho carrinho)
        {
            _clienteRepository = clienteRepository;
            _loginCliente = loginCliente;
            _carrinho = carrinho;
            _livroRepository = livroRepository;
            _emprestimoRepository = emprestimoRepository;
            _itemRepository = itemRepository;
        }

        public IActionResult Index()
        {
            return View(_livroRepository.ObterTodosLivros());
        }
        public IActionResult AdicionarItem(int id)
        {
            Livro produto = _livroRepository.ObterLivro(id);
            if (produto == null)
            {
                return View("NãoExisteItem");
            }
            else
            {
                var item = new Livro()
                {
                    idLivro = id,
                    quantidade = 1,
                    imagemLivro = produto.imagemLivro,
                    nomeLivro = produto.nomeLivro
                };
                _carrinho.Cadastrar(item);
                return RedirectToAction(nameof(Carrinho));
            }
        }
        public IActionResult Carrinho()
        {
            return View(_carrinho.Consultar());
        }
        public IActionResult RemoverItem(int id)
        {
            _carrinho.Remover(new Livro() { idLivro = id });
            return RedirectToAction(nameof(Carrinho));
        }

        DateTime data;
        public IActionResult SalvarCarrinho(Emprestimo emprestimo)
        {
            List<Livro> carrinho = _carrinho.Consultar();

            Emprestimo mdE = new Emprestimo();
            Item mdI = new Item();

            data = DateTime.Now.ToLocalTime();

            mdE.dtEmpre = data.ToString("dd/MM/yyyy");
            mdE.dtDev = data.AddDays(7).ToString("dd/MM/yyyy");
            mdE.idCliente = "1";

            _emprestimoRepository.Cadastrar(mdE);
            _emprestimoRepository.buscarUltimoEmp(emprestimo);//verifiar essa busca

            for (int i = 0; i < carrinho.Count; i++)
            {
                mdI.idEmp = emprestimo.idEmp;
                mdI.idLivro = carrinho[i].idLivro;

                //_itemRepository.Cadastrar(mdI);
            }
            _carrinho.RemoverTodos();
            return RedirectToAction("confEmp");
        }
        public IActionResult confEmp()
        {
            return View();
        }
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Cliente cliente)
        {
            cliente.Situacao = SituacaoConstant.Ativo;
            _clienteRepository.Cadastrar(cliente);
            return RedirectToAction(nameof(Login));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([FromForm] Cliente cliente)
        {
            Cliente cleinteDB = _clienteRepository.Login(cliente.Email, cliente.Senha);

            if (cleinteDB.Email != null && cliente.Senha != null)
            {
                _loginCliente.Login(cleinteDB);
                return new RedirectResult(Url.Action(nameof(PainelCliente)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não localizado, por favor verifique e-mail e senha digitado";
                return View();
            }
        }

        [ValidateHttpReferer]
        public IActionResult Ativar(int id)
        {
            _clienteRepository.Ativar(id);
            return RedirectToAction(nameof(Index));
        }
        [ValidateHttpReferer]
        public IActionResult Desativar(int id)
        {
            _clienteRepository.Desativar(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult PainelCliente()
        {
            ViewBag.Nome = _loginCliente.GetCliente().Nome;
            ViewBag.CPF = _loginCliente.GetCliente().CPF;
            ViewBag.Email = _loginCliente.GetCliente().Email;
            return View();
        }

        [ClienteAutorizacao]
        public IActionResult LogoutCliente()
        {
            _loginCliente.Logout();
            return RedirectToAction(nameof(Index));
        }
    }
}
