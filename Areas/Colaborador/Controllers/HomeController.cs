using AppLoginCore_M.Libraries.Filtro;
using AppLoginCore_M.Libraries.Login;
using AppLoginCore_M.Models.Constant;
using AppLoginCore_M.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace AppLoginCore_M.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class HomeController : Controller
    {
        private IColaboradorRepository _colaboradorRepository;
        private LoginColaborador _loginColaborador;
        private ILivroRepository _livroRepository; 

        public HomeController(IColaboradorRepository colaboradorRepository, LoginColaborador loginColaborador, ILivroRepository livroRepository)
        {
            _colaboradorRepository = colaboradorRepository;
            _loginColaborador = loginColaborador;
            _livroRepository = livroRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] Models.Colaborador colaborador)
        {
            Models.Colaborador colaboradorDB = _colaboradorRepository.Login(colaborador.Email, colaborador.Senha);

            if(colaboradorDB.Email != null && colaboradorDB.Senha != null)
            {
                _loginColaborador.Login(colaboradorDB);

                return new RedirectResult(Url.Action(nameof(Painel)));

            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique o e-mail e senha!";
                return View();
            }
        }

        //[HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar(Models.Colaborador colaborador)
        {
            colaborador.Tipo = ColaboradorTipoConstant.Comum;
            _colaboradorRepository.Cadastrar(colaborador);
            TempData["MSG_S"] = "Registro salvo com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult Atualizar(int id)
        {
            Models.Colaborador colaborador = _colaboradorRepository.ObterColaborador(id);
            return View(colaborador);
        }
        [HttpPost]
        public IActionResult Atualizar([FromForm] Models.Colaborador colaborador)
        {
            if(ModelState.IsValid)
            {
                _colaboradorRepository.Atualizar(colaborador);
                TempData["MSG_S"] = "Registro salvo com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            _colaboradorRepository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Livros()
        {
            return View(_livroRepository.ObterTodosLivros());
        }

        [ColaboradorAutorizacaoAttribute]
        public IActionResult Painel()
        {
            return View();
        }
        [ColaboradorAutorizacaoAttribute]
        public IActionResult Logout()
        {
            _loginColaborador.Logout();
            return RedirectToAction("Login", "Home");
        }
    }
}
