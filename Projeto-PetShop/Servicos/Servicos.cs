using Projeto_PetShop.Models;
using RepositorioCadastro;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        #region Metodos Publicos
        public GerenciadorClientesCadastro()
        {
            _repositorio = new CadastroCliente();

        }
        public void Pergunta()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Bem vindo!");
                Console.WriteLine("Em que posso ajuda-lo?");
                Console.WriteLine("1 - Listar Clientes Cadastrados");
                Console.WriteLine("2 - Cadastrar Cliente");
                Console.WriteLine("3 - Atualizar Cliente");
                Console.WriteLine("4 - Remover Cliente");
                Console.WriteLine("5 - Desativar Cliente");
                    var resposta = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (resposta)
                    {
                        case "1":
                            ListaClientes();
                            break;
                        case "2":
                            Cadastro();
                            break;
                        case "3":
                            AtualizarCliente();
                            break;
                        case "4":
                            RemoverCliente();
                            break;
                        case "5":
                            DesativarCliente();
                            break;
                        default:
                            Console.WriteLine("Selecione uma opção correspondente as alternativas!");
                            continue;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Console.Clear();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Aperte alguma tecla para continuar!");
                    Console.ReadKey();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro grave no programa, Contate um administrador: " + ex.Message);
                    Console.WriteLine("Aperte alguma tecla para continuar!");
                    gConsole.ReadKey();
                }
            }
        }
        #endregion

        #region Metodo Privados
        private void Cadastro()
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
        private void ListaClientes()
        {
            var idClientes = _repositorio.Lista();
            Console.Write("Deseja uma lista com todos os clientes inativos? S/N");
            if (Console.ReadLine() == "N")
                idClientes = idClientes.Where(x => x.Atividade == true).ToList();
            Console.Clear();

            foreach (var C in idClientes)
            {
                Console.WriteLine($"Cliente =>{ C.ID} Nome => { C.Nome }; CPF => { C.CPF }; {(C.Atividade ? "Ativo"  : " Inativo")}");
            }

            Console.WriteLine("Pressione alguma tecla para sair da Lista!");
            Console.ReadKey();
        }
        private void RemoverCliente()
        {
            var localizacao = PerguntarIdDosClientes("Remover");
            _repositorio.RemoverCliente(localizacao);
        }
        private void DesativarCliente()
        {
            var localizacao = PerguntarIdDosClientes("Desativar");
            _repositorio.DesativarCliente(localizacao);
        }
        private void AtualizarCliente()
        {
            var localizao = PerguntarIdDosClientes("Atualizar");
            var cliente = ColetarDadosClientes();
            cliente.ID = localizao;
            _repositorio.AtualizarCliente(cliente);

            throw new InvalidOperationException($"{cliente.Nome}Atualizado com sucesso para{localizao}!");
        }
        private idClientes ColetarDadosClientes()
        {
            Console.WriteLine("Informe Nome para Cadastro:");
            var nomeCliente = Console.ReadLine();
            if (!Validacoes.Validacoes.ValidacaoTexto(nomeCliente, 3, 80))
                throw new InvalidOperationException("O nome deve contar entre 3 e 80 caracteres");

            Console.WriteLine($"Informe o CPF,{nomeCliente}?");
            var cpfCliente = Console.ReadLine();
            if (!Validacoes.Validacoes.ValidacaoNumerosCpf(cpfCliente, 11, 11))
                throw new InvalidOperationException("CPF INVÁLIDO!!");

            var cpf = Convert.ToInt16(cpfCliente);

            Console.WriteLine("Informe a data de nascimento: ");
            var data = Console.ReadLine();

            if (!Validacoes.Validacoes.ValidarIdadeDataBrasileira(data))
                
                {
                    Console.WriteLine("A idade minima permitida é de 16 anos!");
                    Console.ReadKey();
                    return null;
                }

            DateTime dataNascimimento = DateTime.ParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            return new idClientes()

            {
                Nome = nomeCliente,
                CPF = cpf,
                DataNascimento = dataNascimimento,
                Atividade = true,
            };
        }
     
        private int PerguntarIdDosClientes(string idDosClientes)
        {
            Console.WriteLine($"Informe o ID do Cliente {idDosClientes}?");
            string idInformadoString = Console.ReadLine();
            if (int.TryParse(idInformadoString, out _))
                throw new InvalidCastException("O ID informado não é válido.");

            var idInformado = Convert.ToInt16(idInformadoString);
            if (!_repositorio.SeExiste(idInformado))
                throw new InvalidCastException("Este ID não existe. Tente novamente!...");

            return idInformado;
        }
        #endregion
    }
}