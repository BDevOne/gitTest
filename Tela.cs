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

            /*while ()
            {
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
            }*/
            

            gerarListaUsuarios(listaCadastros);
        }

        public string seguirCadastro()
        {
            Console.Write("Deseja cadastrar mais usuários: S/N");
            var seguirCadastro = Console.ReadLine();

            if (!string.IsNullOrEmpty(seguirCadastro) && seguirCadastro.ToUpper() == "S")
            {
                return seguirCadastro;
            }
            return seguirCadastro;
        }

        public void gerarListaUsuarios(List<Users> listaCadastros)
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

        public void Response()
        {

        }
    }
}
