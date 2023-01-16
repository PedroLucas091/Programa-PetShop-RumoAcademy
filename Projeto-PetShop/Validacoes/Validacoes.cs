using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_PetShop.Validacoes
{
    internal class Validacoes
    {
        public static bool ValidacaoNumerosCpf(string? texto)
        {
            if (string.IsNullOrWhiteSpace(texto)
                || texto.Contains("."))
                 return false;

            var isInt = int.TryParse(texto, out _);

            if (!isInt) return false;
            return true;
        }
        public static bool ValidacaoTexto(string? texto, short min, short max)
        {
            if(string.IsNullOrWhiteSpace(texto)
                || texto.Trim().Length < min
                || texto.Trim().Length > max) 
                return false;

            return true;
        }
        public static bool ValidacaoNumerosCpf(string? cpfCliente, int v1, int v2)
        {
            throw new NotImplementedException();
        }
        public static bool ValidarIdadeDataBrasileira(string dataNascimimento)
        {
            if (string.IsNullOrWhiteSpace(dataNascimimento))
                return false;

            if (!DateTime.TryParseExact(dataNascimimento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var data))
                return false;

            if (DateTime.Now.Subtract(data).TotalDays / 365.25 < 16)
                return false;

            if (DateTime.Now.Subtract(data).TotalDays / 365.25 > 120)

                return false;

            return true;
        }
    }
}
