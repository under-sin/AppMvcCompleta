﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;

namespace DevIO.App.ViewModels
{
    public class EnderecoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid FornecedorId { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório !!!")]
        [StringLength(200, ErrorMessage = "O Campo {0} deve ter entre {2} e {1} caracteres...", MinimumLength = 2)]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório !!!")]
        [StringLength(50, ErrorMessage = "O Campo {0} deve ter entre {2} e {1} caracteres...", MinimumLength = 1)]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório !!!")]
        [StringLength(8, ErrorMessage = "O Campo {0} deve ter entre {1} caracteres...", MinimumLength = 8)]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório !!!")]
        [StringLength(100, ErrorMessage = "O Campo {0} deve ter entre {2} e {1} caracteres...", MinimumLength = 2)]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório !!!")]
        [StringLength(200, ErrorMessage = "O Campo {0} deve ter entre {2} e {1} caracteres...", MinimumLength = 2)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório !!!")]
        [StringLength(50, ErrorMessage = "O Campo {0} deve ter entre {2} e {1} caracteres...", MinimumLength = 2)]
        public string Estado { get; set; }

        [HiddenInput] // propriedade -> vai tratar sempre o formulário como input hidden
        public FornecedorViewModel fornecedor { get; set; }
    }
}
