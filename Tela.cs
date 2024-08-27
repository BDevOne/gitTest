﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitTeste
{
    public class Tela
    {
        List<Users> listaCadastros = new List<Users>();

        public void TelaLogin()
        {
            string seguirRegistro = "S";

            while (seguirRegistro.ToUpper() == "S")
            {
                Users dadosUsuarios = new Users();

                Console.WriteLine($"\nDados do Usuário\n");
                Console.Write("Nome Usuário: ");
                dadosUsuarios.Nome = Console.ReadLine();

                Console.Write("CPF Usuário: ");
                dadosUsuarios.Cpf = Console.ReadLine();
                dadosUsuarios.validacaoCpf();

                Console.Write("Idade Usuário: ");
                dadosUsuarios.Idade = int.Parse(Console.ReadLine());
                dadosUsuarios.validarIdade();

                listaCadastros.Add(dadosUsuarios);

                seguirRegistro = SeguirCadastro(seguirRegistro);
                
            }

            GenerateUserList(listaCadastros);
            SearchUser();
        }

        public string SeguirCadastro(string seguirCadastro)
        {
            Console.Write("Deseja cadastrar mais usuários (S/N): ");
            seguirCadastro = Console.ReadLine();

            if (!string.IsNullOrEmpty(seguirCadastro) && seguirCadastro.ToUpper() == "S")
            {
                return seguirCadastro;
            }
            return seguirCadastro ?? "Operação Encerrada";
        }

        public void GenerateUserList(List<Users> listaCadastros)
        {
            foreach (var user in listaCadastros)
            {
                Console.WriteLine($"Nome: {user.Nome}");

                if (user.Cpf != null)
                {
                    Console.WriteLine($"CPF: {user.Cpf}");
                }
                Console.WriteLine($"Idade: {user.Idade}");
            }

        }

        public string SearchUser()
        {
            Console.Write("Informe Nome do usuário que deseja procurar: ");
            var requestUser = Console.ReadLine();

            if (string.IsNullOrEmpty(requestUser))
            {
                requestUser = VerificarValor(null);
                Console.WriteLine(requestUser);
                return requestUser;
            }
            if (!string.IsNullOrEmpty(requestUser)) // melhorar esse busca, está com erro.
            {
                Users requestNameUser = listaCadastros.First(u => u.Nome == requestUser); // Erro ao passar valor nulo

                if (requestNameUser != null)
                {
                    Console.WriteLine($"{requestNameUser.Nome}");
                }
            }
            return requestUser;
        }

        public string VerificarValor(string valueNull)
        {
            return valueNull ?? "Operação falhou, informe um valor válido!!";
        }
    }
}
