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

        public Users(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;
        }
    }

}
