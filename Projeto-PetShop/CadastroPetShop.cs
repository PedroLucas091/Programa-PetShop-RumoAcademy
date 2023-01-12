using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projeto_PetShop
{
    internal class CadastroCliente
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }

        public override string ToString()
        {
            return $"Nome:{Nome}, CPF {CPF} , DataNascimento : {DataNascimento.ToShortDateString()}";
        }

        public class GerenciadorDeClientes
        {
            private readonly string _dadosClientes;
            public GerenciadorDeClientes(string dadosClientes)
            {
                _dadosClientes = dadosClientes;
            }
            public void AdicionarCliente(CadastroCliente cliente)
            {
                using (var sw = new StreamWriter(_dadosClientes, true))
                {
                    sw.WriteLine(cliente.ToString());
                }
            }
            public List<CadastroCliente> ListaClientes()
            {
                var clientes = new List<CadastroCliente>();
                using (var sr = new StreamReader(_dadosClientes))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        var campos = linha.Split(',');
                        var cliente = new CadastroCliente
                        {
                            Nome = campos[0],
                            CPF = campos[1],
                            DataNascimento = Convert.ToDateTime(campos[2])
                        };
                        clientes.Add(cliente);
                    }
                }
                return clientes;
            }

            public CadastroCliente BuscarClienteViaCpf(string cpf)
            {
                var clientes = ListaClientes();
                return clientes.FirstOrDefault(c => c.CPF == cpf);
            }
        }
    }
}