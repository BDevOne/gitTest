using Course.Entities.Exceptions;
using gitTeste.Entities;

namespace gitTeste.Entities
{
    public class TratarDados
    {
        public string SepararTextoEnum(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                throw new ArgumentException("Texto inválido!");

            return System.Text.RegularExpressions.Regex.Replace(
                texto,
                "([a-z])([A-Z])",
                "$1 $2"
            );
        }

        public string MascaraCpf(string cpfMascara)
        {
            if (cpfMascara.Length < 11)
            {
                throw new DomainException("CPF inválido!!!");
            }
            return cpfMascara.Insert(9, "-").Insert(6, ".").Insert(3, "."); ;
        }

        public DateTime TratarDataNascimento(string dataNascimentoStr)
        {
            DateTime dataNascimento;

            if (!DateTime.TryParseExact(dataNascimentoStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dataNascimento))
            {
                throw new DomainException("Formato de data inválido! Use o formato dd/MM/yyyy.");
            }

            if (dataNascimento > DateTime.Now)
            {
                throw new DomainException("Data de nascimento inválida! A data não pode ser futura.");
            }

            if (dataNascimento < DateTime.Now.AddYears(-120))
            {
                throw new DomainException("Data de nascimento inválida! A data não pode ser mais antiga que 120 anos.");
            }

            return dataNascimento;
        }
    }
}