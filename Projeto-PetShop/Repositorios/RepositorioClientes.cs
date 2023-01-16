using Projeto_PetShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioCadastro

{
    internal class CadastroCliente
    {
        #region Metodos Publicos
        private readonly string _baseDados = "C:\\RumoAcademy\\DataBase.Local\\dados.csv";
        private List<idClientes> TabelaClientes = new List<idClientes>();
        public CadastroCliente()
        {
            if (!File.Exists(_baseDados))
            {
                var file = File.Create(_baseDados);
                file.Close();
            }
        }
        public List<idClientes> Lista()
        {
            CarregarDados();

            return TabelaClientes;
        }
        public bool SeExiste(int identifacadorCliente)
        {
            CarregarDados();
            return TabelaClientes.Any(x => x.ID == identifacadorCliente);
        }
        public void Adicionar(idClientes id)
        {
            var Identificador = IdentifacadorCliente();

            var sw = new StreamWriter(_baseDados);
            sw.WriteLine($"{Identificador}{id.Nome};{id.CPF};{id.Atividade}");
            sw.Close();
        }
        public void Modificacao(idClientes id) 
        {
            CarregarDados();
            var localizacao = TabelaClientes.FindIndex(x => x.ID == id.ID);
            TabelaClientes[localizacao] = id;
            RegravarClientes(TabelaClientes);
        }
        public void Excluir(int ID)
        {
            CarregarDados();
            var localizacao = TabelaClientes.FindIndex(x => x.ID == ID);
            TabelaClientes.RemoveAt(localizacao);
            RegravarClientes(TabelaClientes);

        }
        public void Desativar(int ID) 
        {
            CarregarDados();

            var localizcao = TabelaClientes.FindIndex(x => x.ID == ID);
            TabelaClientes[localizcao].Atividade = false;
            RegravarClientes(TabelaClientes);
        }
        #endregion

        #region Metodos privados
        private void CarregarDados()
        {
            TabelaClientes.Clear();
            var sr = new StreamReader(_baseDados);

            while (true)
            {
                var linha = sr.ReadLine();
                if (linha == null)
                    break;

                TabelaClientes.Add(LinhasParatabelaClientes(linha));

            }
            sr.Close();
        }
        private int IdentifacadorCliente()
        {
            CarregarDados();
            if (TabelaClientes.Count == 0)
                return 1;

            return TabelaClientes.Max(x => x.ID) + 1;
        }
        private idClientes LinhasParatabelaClientes(string linha)
        {
            var colunas = linha.Split(','); 
            
            var cliente = new idClientes();
            cliente.ID = int.Parse(colunas[0]); 
            cliente.Nome = colunas[1];
            cliente.CPF = int.Parse(colunas[2]);
            cliente.Atividade = Convert.ToBoolean(colunas[3]);

            return cliente;
        }
        private void RegravarClientes(List<idClientes> clientes)
        {
            var sw = new StreamWriter(_baseDados);

            foreach(var cliente in clientes.OrderBy(x => x.ID))
            {
                sw.WriteLine(CriarLinhaCliente(cliente.ID,cliente));
            }
            
            sw.Close();
        }
        private string CriarLinhaCliente(int identificacao, idClientes cliente)
        {
            return $"{identificacao};{cliente.ID};{cliente.Nome};{cliente.CPF};{cliente.Atividade}";
        }

        public object RemoverCliente(int localizacao)
        {
            throw new NotImplementedException();
        }
        public void DesativarCliente(int localizacao)
        {
            throw new NotImplementedException();
        }
        public void AtualizarCliente(idClientes cliente)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}