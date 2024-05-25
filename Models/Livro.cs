using System.ComponentModel;

namespace AppLoginCore_M.Models
{
    public class Livro
    {
        public int idLivro { get; set; }
        [DisplayName("XYZ")]
        public string nomeLivro { get; set; }
        public string imagemLivro { get; set; }
        public int quantidade { get; set; }
    }
}
