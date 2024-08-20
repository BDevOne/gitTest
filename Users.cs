using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitTeste
{
    public class Users
    {
        public string? Nome { get; set; }
        public int Idade { get; set; }
        public string? Cpf { get; set; }

        public string validacaoCpf()
        {
            Cpf.Replace(".", "").Replace("-", "");

            if (!string.IsNullOrWhiteSpace(Cpf) && Cpf.Length == 11)
            {
                Cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
                return Cpf;
            }
            return Cpf;
        }

        public int validarIdade()
        {
            if (Idade >= 18)
            {
                return Idade;
            }
            return Idade;
        }

    }

}
