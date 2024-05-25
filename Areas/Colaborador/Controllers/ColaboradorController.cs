using AppLoginCore_M.Libraries.Filtro;
using AppLoginCore_M.Models.Constant;
using AppLoginCore_M.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace AppLoginCore_M.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao(ColaboradorTipoConstant.Gerente)]
    public class ColaboradorController : Controller
    {
        private IColaboradorRepository _colaboradorRepository;
        public ColaboradorController(IColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository = colaboradorRepository;
        }

        public IActionResult Index()
        {
            return View(_colaboradorRepository.ObterTodosColaboradores());
        }
    }
}
