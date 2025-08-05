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

        private Users users = new Users();
        private TratarDados tratarDados = new TratarDados();

        public void TelaLogin()
        {
        }

        // Mover para classe Users
        public void ExibirDadosUsuarios()
        {
            foreach (var listUsers in listaCadastros)
            {
                Console.WriteLine($"Nome: {listUsers.Nome}");
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
                DateTime dataAtual = DateTime.Now;

                Console.WriteLine($"\nDados do Usuário\n");
                Console.Write("Nome Usuário: ");
                var nome = Console.ReadLine();

                Console.Write("CPF Usuário: ");
                var cpf = Console.ReadLine();

                Console.WriteLine("Informe a Data de Nascimento do Usuário (dd/MM/yyyy): ");
                var dataNascimentoStr = Console.ReadLine();

                // Somente solicitar idade caso a data de nascimento não seja informada.
                if (!string.IsNullOrEmpty(dataNascimentoStr) || !string.IsNullOrWhiteSpace(dataNascimentoStr))
                {
                    Console.Write("Idade Usuário: ");
                    int idade = int.Parse(Console.ReadLine());
                }

                Console.WriteLine("Selecione o tipo de Usuário: (1 = Administrador, 2 = Master, 3 = Operador e 4 = Externo)");
                int tipo = int.Parse(Console.ReadLine());

                users.AdicionarUsuarioLista(
                    nome,
                    tratarDados.MascaraCpf(cpf),
                    int.Parse(Console.ReadLine()),
                    (Enumerados.UsuarioTipo)tipo,
                    tratarDados.TratarDataNascimento(dataNascimentoStr)
                );

                seguirRegistro = SeguirCadastro(seguirRegistro);
            }
        }

        private string SeguirCadastro(string seguirCadastro)
        {
            try
            {
                Console.Write("Deseja cadastrar mais usuários (S/N): ");
                seguirCadastro = Console.ReadLine();

                if (string.IsNullOrEmpty(seguirCadastro) || string.IsNullOrWhiteSpace(seguirCadastro))
                    throw new NullReferenceException("Nenhum valor informado!!");
                if (seguirCadastro.ToUpper() != "S")
                    throw new ArgumentException("Valor informado incorreto!!");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            return seguirCadastro;
        }

        /* 
        Criar verificação foreach, no qual tem por objetivo verificar em uma lista todos os usuarios e separar os que não possue tipo. (AFIM DE VIABILIZAR O FLUXO DE CADASTRO)
        Mover método SepararUsuarioTipo(), ao qual deve pertencer a classe Users.
        */

        // Mover método para a classe Users.
        public void SepararUsuarioTipo()
        {
            foreach (var separar in listaCadastros)
            {
                // Adicionar um if para verificar se o tipo do usuario é igual a nenhum
                if (Enum.TryParse(separar.UsuarioTipo.ToString(), true, out Enumerados.UsuarioTipo usuarioTipo) && usuarioTipo != Enumerados.UsuarioTipo.Nenhum)
                {
                    separar.AtribuirUsuarioPermissao();
                    continue;
                }
                if (separar.UsuarioTipo == 0 || separar.UsuarioTipo == Enumerados.UsuarioTipo.Nenhum)
                {
                    // usuariosSemTipoDefinido.Add(separar);
                }
                if (separar.Cpf == null)
                {
                    // usuariosInvalidos.Add(separar);
                }
            }
        }

        public void GetEditarUsuario(string usuarioEncontrado)
        {
            GetSearchUser(usuarioEncontrado);
            foreach (var user in listaCadastros)
            {
                if (usuarioEncontrado == user.Nome)
                {
                    user.EdicaoUsuario(usuarioEncontrado);
                }
                else
                {
                    Console.WriteLine("Usuário não encontrado!!!");
                }
            }
        }

        public void GetSearchUser(string responseUser)
        {
            users.SearchUser(responseUser);

            Console.WriteLine($"Usuário encontrado: {responseUser}");
        }
    }
}
