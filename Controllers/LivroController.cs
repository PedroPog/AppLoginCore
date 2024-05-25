using AppLoginCore_M.GerenciaArquivos;
using AppLoginCore_M.Models;
using AppLoginCore_M.Repository;
using AppLoginCore_M.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace AppLoginCore_M.Controllers
{
    public class LivroController : Controller
    {
        private ILivroRepository _liivroRepository;
        public LivroController(ILivroRepository liivroRepository)
        {
            _liivroRepository = liivroRepository;
        }

        public IActionResult CadastrarLivro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarLivro(Livro livro, IFormFile file)
        {
            var caminho = GerenciadorArquivo.CadastrarImagemProduto(file);

            livro.imagemLivro = caminho;

            _liivroRepository.Cadastrar(livro);

            ViewBag.msg = "Cadastro realizado";
            return RedirectToAction("Index","Home");
        }
    }
}
