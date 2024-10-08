﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace gitTeste
{
    public class Users
    {
        public string? Nome { get; set; }
        public int Idade { get; set; }
        public string? Cpf { get; set; }
        public Guid Id { get; private set; }

        private static HashSet<int> idExistente = new HashSet<int>();
        private static Random random = new Random();

        #region Constructors
        public Users(string _nome, string _cpf, int _idade)
        {
            Nome = _nome;
            Cpf = _cpf;
            Idade = _idade;
            Id = GenerateUniqueId();
        }
        #endregion

        public void AdicionarMascaraCpf(string cpfMascara)
        {
            cpfMascara.Replace(".", "").Replace("-", "");

            if (!string.IsNullOrWhiteSpace(Cpf) && Cpf.Length == 11)
            {
                Cpf = Cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            }

        }

        public int ValidarIdade( )
        {
            if (Idade < 18)
            {
                Console.WriteLine("Não foi possível cadastrar idade. Para prosseguir com o cadastro, informe uma idade maior que 17 anos!\n");

                Console.Write("Idade usuário: ");
                Idade = int.Parse(Console.ReadLine());
            }
            return Idade;
        }

        private Guid GenerateUniqueId()
        {
            return Guid.NewGuid();
        }

    }

}
