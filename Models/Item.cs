namespace AppLoginCore_M.Models
{
    public class Item
    {
        public Guid ItemPedidoID { get; set; }
        public int idEmp { get; set; }
        public int idLivro { get; set; }
        public string nomeLivro { get; set; }
        public string imagem { get; set; }
        public string quantidade { get; set; }
    }
}
