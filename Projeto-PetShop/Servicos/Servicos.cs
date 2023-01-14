using RepositorioCadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_PetShop.Servicos
{
    internal class Servicos
    {
        private readonly RepositorioCadastro.CadastroCliente _repositorio;
        public Servicos()
        {
            _repositorio = new CadastroCliente();

        }
        public void Cadastro()
        {
            Console.WriteLine("Informe seu nome completo: ");
            var nomeCliente = Console.ReadLine();

            Console.WriteLine("Informe o seu CPF: ");
            string cpf = Console.ReadLine();
            int cpfClienteConvertido;
            bool converta = int.TryParse(cpf, out cpfClienteConvertido);

            _repositorio.Adicionar(new Models.idClientes()
            {
                Nome = nomeCliente,
                CPF = cpfClienteConvertido,
                Atividade = true
            });
        }
    }
}



