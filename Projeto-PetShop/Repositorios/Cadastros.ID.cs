using Projeto_PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioCadastro

{
    internal class CadastroCliente
    {
        private readonly string _baseDados = "C:\\RumoAcademy\\DataBase.Local\\dados.csv";
        private List<idClientes> IdClients = new List<idClientes>();
        public CadastroCliente()
        {
            if (!File.Exists(_baseDados))
            {
                var file = File.Create(_baseDados);
                file.Close();
            }
        }
        public List<idClientes> lista()
        {
            CarregarDados();

            return IdClients;
        }
        private idClientes LinhaTextoParaCadastro(string linha)
        {
            var colunas = linha.Split(',');

            var Cliente = new idClientes();
            Cliente.ID = int.Parse(colunas[0]);
            Cliente.Nome = colunas[1];
            Cliente.CPF = int.Parse(colunas[2]);
            Cliente.Atividade = true;

            return Cliente;
        }
        private void CarregarDados()
        {
            IdClients.Clear();
            var sr = new StreamReader(_baseDados);

            while (true)
            {
                var linha = sr.ReadLine();
                if (linha == null)
                    break;

                IdClients.Add(LinhaTextoParaCadastro(linha));

            }
            sr.Close();
        }
        private int IdentifacadorCliente()
        {
            CarregarDados();
            if (IdClients.Count == 0)
                return 1;

            return IdClients.Max(x => x.ID) + 1;
        }
        public void Adicionar(idClientes id)
        {
            var Identificador = IdentifacadorCliente();

            var sw = new StreamWriter(_baseDados);
            sw.WriteLine($"{Identificador}{id.Nome};{id.CPF};{id.Atividade}");
            sw.Close();
        }
       
    }
}