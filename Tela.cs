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

            gerarListaUsuarios(listaCadastros);
        }

        public void gerarListaUsuarios(List<Users> listaCadastros)
        {
            foreach (var user in listaCadastros)
            {
                Console.WriteLine($"Nome: {user.Nome}");
                Console.WriteLine($"CPF: {user.Cpf}");
                Console.WriteLine($"Idade: {user.Idade}");
                break;
            }
            
        }

        public void Response()
        {
            
        }
    }
}
