using AppLoginCore_M.Libraries.Filtro;
using AppLoginCore_M.Models.Constant;
using AppLoginCore_M.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace AppLoginCore_M.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao]
    public class ClienteController : Controller
    {
        private IClienteRepository _clienteRepository;
        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IActionResult Index()
        {
            return View(_clienteRepository.ObterTodosClientes());
        }

    }
}
