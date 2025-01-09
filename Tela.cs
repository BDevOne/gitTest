using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace gitTeste
{
    public class Tela
    {
        List<Users> listaCadastros = new List<Users>();
        List<Users> usuariosValidos = new List<Users>();
        List<Users> usuariosInvalidos = new List<Users>();

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

        // Mover método para classe Users - Criar método de chamada para esse método - getSearchUser()
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
                listaCadastros.Find(u => u.Nome == requestUser);
                return requestUser;
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

                Console.WriteLine("Qual tipo de Usuário: (1 = Administrador, 2 = Master, 3 = Operador e 4 = Externo)");
                var tipo = Console.ReadLine();

                if (Enum.TryParse(tipo, true, out Enumerados.UsuarioTipo usuarioTipo))
                {
                    Users dadosUsuarios = new Users(nome, cpf, idade, usuarioTipo);
                    dadosUsuarios.PermissoesTipoUsuario();
                    dadosUsuarios.AddListUsers(listaCadastros);

                    // dadosUsuarios.AddListUsers(dadosUsuarios);
                }
                else
                {
                    Users usuarioSemTipoDefinido = new Users(nome, cpf, idade);
                    listaCadastros.Add(usuarioSemTipoDefinido);
                }

                Console.WriteLine(usuarioTipo);

                seguirRegistro = SeguirCadastro(seguirRegistro);
            }
        }
        
        public string VerificarValor(string valueNull)
        {
            return valueNull ?? "Operação falhou, informe um valor válido!!";
        }
    }
}
