using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitTeste
{
    public class Tela
    {
        List<Users> listaCadastros = new List<Users>();

        public void TelaLogin(string nome, string cpf, int idade)
        {
            string seguirRegistro = "S";

            while (seguirRegistro.ToUpper() == "S")
            {
                Console.WriteLine($"\nDados do Usuário\n");
                Console.Write("Nome Usuário: ");
                nome = Console.ReadLine();

                Console.Write("CPF Usuário: ");
                cpf = Console.ReadLine();

                Console.Write("Idade Usuário: ");
                idade = int.Parse(Console.ReadLine());

                Users dadosUsuarios = new Users(nome, cpf, idade);

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

            if (string.IsNullOrEmpty(seguirCadastro) || seguirCadastro.ToUpper() != "S")
            {
                seguirCadastro = VerificarValor(seguirCadastro);
                Console.WriteLine(seguirCadastro);
                return seguirCadastro;
            }
            return seguirCadastro;
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

        public string? SearchUser()
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
                listaCadastros.Find(u => u.Nome == requestUser); // Erro ao passar valor nulo

                if (requestUser != null)
                {
                    Console.WriteLine($"{requestUser}");
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
