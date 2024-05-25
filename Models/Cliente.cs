﻿using System.ComponentModel.DataAnnotations;

namespace AppLoginCore_M.Models
{
    public class Cliente
    {
        [Display(Name = "Código", Description = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nome Completo", Description = "Nome e Sobrenome")]
        [Required(ErrorMessage = "O Nome Completo é obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Nascimento")]
        [Required(ErrorMessage = "A Data é obrigatória")]
        public DateTime Nascimento { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "O Sexo é obrigatório")]
        public string Sexo { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O CPF é obrigatório")]
        public string CPF { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = "O Celular é obrigatório")]
        public string Telefone { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "O Email não é válido")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um Email válido")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "O Senha é obrigatória")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "A Senha deve ter entre 6 e 10 caracteres")]
        public string Senha { get; set; }

        [Display(Name = "Situação")]
        [Required(ErrorMessage = "O campo Situação é obrigatório")]
        public string Situacao { get; set; }
    }
}
