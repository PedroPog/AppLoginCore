using System.ComponentModel.DataAnnotations;

namespace AppLoginCore_M.Models
{
    public class Colaborador
    {
        [Display(Name = "Código", Description = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nome Completo", Description = "Nome e Sobrenome")]
        [Required(ErrorMessage = "O Nome Completo é obrigatório")]
        public string Nome { get; set; }


        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "O Email não é válido")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um Email válido")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O Senha é obrigatória")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "A Senha deve ter entre 6 e 10 caracteres")]
        public string Senha { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "O Tipo é obrigatório")]
        public string Tipo { get; set; }
    }
}
