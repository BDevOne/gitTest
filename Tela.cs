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

                seguirRegistro = SeguirCadastro();

            }

            GenerateUserList(listaCadastros);
            SearchUser();
        }

        public string SeguirCadastro()
        {
            Console.Write("Deseja cadastrar mais usuários: S/N");
            var seguirCadastro = Console.ReadLine();

            if (!string.IsNullOrEmpty(seguirCadastro) && seguirCadastro.ToUpper() == "S")
            {
                return seguirCadastro;
            }
            return seguirCadastro ?? VerificarValor(null);
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
            if (!string.IsNullOrEmpty(requestUser)) // melhorar esse cadastros, está com erro.
            {
                Users requestNameUser = listaCadastros.First(u => u.Nome == requestUser); // Erro ao passar valor nulo

                if (requestNameUser != null)
                {
                    Console.WriteLine($"{requestNameUser.Nome}");
                    return requestNameUser.Nome;
                }
            }
            return VerificarValor(null);
        }

        public string VerificarValor(string valueNull)
        {
            return valueNull ?? "Operação falhou, informe um valor válido!!";
        }
    }
}
