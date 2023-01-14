using Projeto_PetShop.Models;

namespace Projeto_PetShop.Servicos
{
    internal class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine(new RepositorioCadastro.CadastroCliente().IdentifacadorCliente());
        }
    }
}