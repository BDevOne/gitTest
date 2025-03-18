using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using gitTeste.Entities;
using gitTeste.Entities.Enums;

namespace gitTeste.Entities
{
    public class Tela
    {
        List<Users> listaCadastros = new List<Users>();
        List<Users> usuariosValidos = new List<Users>();
        List<Users> usuariosInvalidos = new List<Users>();
        List<Users> usuariosSemTipoDefinido = new List<Users>();

        private Users users = new Users();

        public void TelaLogin()
        {
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

        // Remover método SearchUser
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
            if (!string.IsNullOrEmpty(requestUser))
            {
                var usuarioEncontrado = listaCadastros.FirstOrDefault(u => u.Nome == requestUser);

                if (usuarioEncontrado?.Nome == requestUser)
                {
                    return usuarioEncontrado?.Nome;
                }
            }
            return requestUser;
        }

        public void ExibirDadosUsuarios()
        {
            foreach (var listUsers in listaCadastros)
            {
                Console.WriteLine($"Nome: {listUsers.Nome}");
                listUsers.MascaraCpf(listUsers.Cpf);
                Console.WriteLine($"CPF: {listUsers.Cpf}");
                Console.WriteLine($"Idade: {listUsers.Idade}");
                Console.WriteLine($"Id: {listUsers.Id}");
                Console.WriteLine($"Usuário Tipo: {listUsers.UsuarioTipo}");

                // listUsers.PermissaoEditar();
                listUsers.VerificarPermissoes();
                Console.WriteLine();
            }
        }

        public void TelaCriarUsuario()
        {
            string seguirRegistro = "S";

            while (seguirRegistro.ToUpper() == "S")
            {
                Console.WriteLine($"\nDados do Usuário\n");
                Console.Write("Nome Usuário: ");
                var nome = Console.ReadLine();

                Console.Write("CPF Usuário: ");
                var cpf = Console.ReadLine();

                Console.Write("Idade Usuário: ");
                int idade = int.Parse(Console.ReadLine());

                Console.WriteLine("Selecione o tipo de Usuário: (1 = Administrador, 2 = Master, 3 = Operador e 4 = Externo)");
                int tipo = int.Parse(Console.ReadLine());

                users.AdicionarUsuarioLista(nome, cpf, idade, (Enumerados.UsuarioTipo)tipo);

                seguirRegistro = SeguirCadastro(seguirRegistro);
            }
            SepararUsuarioTipo();
        }

        /* 
        Criar verificação foreach, no qual tem por objetivo verificar em uma lista todos os usuarios e separar os que não possue tipo. (AFIM DE VIABILIZAR O FLUXO DE CADASTRO)
        Mover método SepararUsuarioTipo(), ao qual deve pertencer a classe Users.
        */
        public void SepararUsuarioTipo()
        {
            foreach (var separar in listaCadastros)
            {
                // Adicionar um if para verificar se o tipo do usuario é igual a nenhum
                if (Enum.TryParse(separar.UsuarioTipo.ToString(), true, out Enumerados.UsuarioTipo usuarioTipo) && usuarioTipo != Enumerados.UsuarioTipo.Nenhum)
                {
                    separar.PermissoesTipoUsuario();
                    usuariosValidos.Add(separar);
                    continue;
                }
                if (separar.UsuarioTipo == 0 || separar.UsuarioTipo == Enumerados.UsuarioTipo.Nenhum)
                {
                    usuariosSemTipoDefinido.Add(separar);
                }
                if (separar.Cpf == null)
                {
                    usuariosInvalidos.Add(separar);
                }
            }
        }

        // Alterar nome do método para getEditarUsuario, pois chama o editar da classe Users e não realiza alteração do usuário.
        public void GetEditarUsuario(string usuarioEncontrado)
        {
            GetSearchUser(usuarioEncontrado);
            foreach (var user in listaCadastros)
            {
                if (usuarioEncontrado == user.Nome)
                {
                    user.PermissaoEditar();
                }
                else
                {
                    Console.WriteLine("Usuário não encontrado!!!");
                }

            }
        }

        public void GetSearchUser(string requestName)
        {
            requestName = "";
            Console.Write("Informe Nome do usuário que deseja procurar: ");
            users.SearchUser(requestName);

            Console.WriteLine($"Usuário encontrado: {requestName}");
        }

        public string VerificarValor(string valueNull)
        {
            return valueNull ?? "Operação falhou, informe um valor válido!!";
        }
    }
}
