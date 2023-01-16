using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projeto_PetShop.Models
{
    internal class idClientes
    {
        #region Metodos Publicos

        public int ID { get; set; }
        public string? Nome { get; set; }
        public int CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Atividade { get; set; }

        #endregion
    }
}