using RepositorioCadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_PetShop.Servicos
{
    internal class GerenciadorClientesCadastro

    {
        private readonly RepositorioCadastro.CadastroCliente _repositorio;
        public GerenciadorClientesCadastro()
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

            Console.WriteLine($"Adicionado!\n");
            Console.WriteLine($"Aperte alguma tecla para prosseguir!");
        }
        public void Lista()
        {
            var idClientes = _repositorio.lista();
            Console.Write("Deseja uma lista com todos os clientes inativos? S/N");
            if (Console.ReadLine() == "N")
                idClientes = idClientes.Where(x => x.Atividade == true).ToList();
            Console.Clear();

            foreach (var C in idClientes)
            {
                Console.WriteLine($"Cliente =>{ C.ID} Nome => { C.Nome }; CPF => { C.CPF }; {(C.Atividade ? "Ativo"  : " Inativo")}");
            }
        }
    }
}