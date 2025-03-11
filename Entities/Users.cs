using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using gitTeste.Entities;
using gitTeste.Entities.Enums;


namespace gitTeste.Entities
{
    public class Users
    {
        public string? Nome { get; set; }
        public int Idade { get; set; }
        public string? Cpf { get; set; }
        public string? Documento { get; set; }
        public int Id { get; private set; }
        public Enumerados.UsuarioTipo UsuarioTipo { get; set; }
        public Enumerados.PermissoesUsuario Permissoes { get; set; }

        List<Users> ListaUsuarios = new List<Users>();

        // public Guid UniqueId { get; private set; }

        #region Constructors

        public Users()
        {
        }

        public Users(string _nome, string _cpf, int _idade, Enumerados.UsuarioTipo _usuarioTipo)
        {
            Nome = _nome;
            Cpf = _cpf;
            Idade = _idade;
            UsuarioTipo = _usuarioTipo;
            Id = IdCreateUser(Id);
        }

        public Users(string _nome, string _cpf, int _idade)
        {
            Nome = _nome;
            Cpf = _cpf;
            Idade = _idade;
            Id = IdCreateUser(Id);
        }

        #endregion

        public void MascaraCpf(string cpfMascara)
        {
            if (cpfMascara.Length == 11)
            {
                // Mascara CPF (000.000.000-00)
                Cpf = cpfMascara.Insert(9, "-").Insert(6, ".").Insert(3, ".");
            }
        }

        public int ValidarIdade()
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

        public int IdCreateUser(int _id)
        {
            Random idGerado = new Random();

            if (Id == 0)
            {
                _id = idGerado.Next(0, 15);
            }
            return _id;
        }

        public void GetTipoUsuario()
        {
            Console.WriteLine("Tipos de Usuários: (1 = Administrador, 2 = Master, 3 = Operador e 4 = Externo)");
            Console.Write("Informe o tipo do Usuario: ");
            var tipoUsuario = Console.ReadLine();

            switch (tipoUsuario?.ToUpper())
            {
                case "0":
                case "NENHUM":
                    UsuarioTipo = Enumerados.UsuarioTipo.Nenhum;
                    break;

                case "1":
                case "ADMINISTRADOR":
                    UsuarioTipo = Enumerados.UsuarioTipo.Administrador;
                    break;
                case "2":
                case "MASTER":
                    UsuarioTipo = Enumerados.UsuarioTipo.Master;
                    break;

                case "3":
                case "OPERADOR":
                    UsuarioTipo = Enumerados.UsuarioTipo.Operador;
                    break;

                case "4":
                case "EXTERNO":
                    UsuarioTipo = Enumerados.UsuarioTipo.Externo;
                    break;

                default:
                    throw new Exception("Nenhum tipo de usuário selecionado!!");
            }
        }

        public void PermissoesTipoUsuario()
        {
            if (UsuarioTipo == Enumerados.UsuarioTipo.Administrador)
            {
                Permissoes = Enumerados.PermissoesUsuario.Editar | Enumerados.PermissoesUsuario.Excluir | Enumerados.PermissoesUsuario.Procurar;
            }
            if (UsuarioTipo == Enumerados.UsuarioTipo.Master)
            {
                Permissoes = Enumerados.PermissoesUsuario.Procurar | Enumerados.PermissoesUsuario.Editar;
            }
            if (UsuarioTipo == Enumerados.UsuarioTipo.Operador)
            {
                Permissoes = Enumerados.PermissoesUsuario.Procurar;
            }
            if (UsuarioTipo == Enumerados.UsuarioTipo.Externo)
            {
                Permissoes = Enumerados.PermissoesUsuario.EditarProprioUsuario;
            }
        }

        /* Adicionar Validação na classe tela para verificar o tipo de usuário se é permitido Editar */

        /* Preciso pegar qual o campo, verificar e adicionar o novo valor a propriedade do usuario*/

        /* Adicionar try/catch para validação dos campos */

        public void PermissaoEditar()
        {
            if (UsuarioTipo == Enumerados.UsuarioTipo.Administrador || UsuarioTipo == Enumerados.UsuarioTipo.Master)
            {
                var campo = SelecionarPropriedade();

                // passa o valor do dado pra prop ai verifica qual o tipo da prop e o retorno dela, assim ajuda a validar o dado
                switch (campo)
                {
                    case "NomeCampo":
                        Nome = Editar(campo);
                        break;
                    case "IdadeCampo":
                        Idade = EditarIdade();
                        break;
                    case "TipoUsuarioCampo":
                        GetTipoUsuario();
                        break;
                    default:
                        throw new Exception("Nenhum campo informado!!");
                }
            }

            string Editar(string campo)
            {
                try
                {
                    Console.Write($"Digite o novo {campo.Replace("Campo", "")} do Usuário: ");
                    var novoDado = Console.ReadLine();

                    if (string.IsNullOrEmpty(novoDado) || string.IsNullOrWhiteSpace(novoDado))
                    {
                        Console.Write("Campo Não pode ser nulo ou vazio. Digite novamente: ");
                        novoDado = Console.ReadLine();
                    }
                    if (string.IsNullOrEmpty(novoDado) || string.IsNullOrWhiteSpace(novoDado))
                        throw new ArgumentException("O Dado informado não pode ser nulo ou vazio");

                    return novoDado;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                    return Nome;
                }
            }

            int EditarIdade()
            {
                Console.Write("Informe a Idade do Usuário: ");
                int novaIdade = int.Parse(Console.ReadLine());

                if (novaIdade < 18)
                {
                    novaIdade = ValidarIdade();
                }
                return novaIdade;
            }
        }

        /* PermissaoExcluir deve conter: 

            Excluir Usuarios - Somente UsuarioTipo.Administrador

            Excluir Dados de outro UsuarioTipo - UsuarioTipo.Administrador && UsuarioTipo.Master

            Excluir Dados proprios - Administrador && Master && Operador
        
        */
        public void PermissaoExcluir()
        {
        }

        public void GetPermissoes()
        {
        }

        private string SelecionarPropriedade()
        {
            Console.Write("Selecione o Campo que seja alterar: ");
            var editarProp = Console.ReadLine();

            switch (editarProp)
            {
                // Criar metodo 
                case "1":
                    editarProp = Enumerados.CampoPropriedade.NomeCampo.ToString();
                    break;
                case "2":
                    editarProp = Enumerados.CampoPropriedade.IdadeCampo.ToString();
                    break;
                case "3":
                    editarProp = Enumerados.CampoPropriedade.TipoUsuarioCampo.ToString(); // Aqui adicionar o método GetTipoUsuario
                    break;
                default:
                    throw new Exception("Nenhum campo selecionado!!");
            }
            return editarProp;
        }

        // Adicionar validação, ao qual será possível escolher antes de informar o tipo ex: SELECIONE O TIPO DE DOCUMENTO CPF = 1 E RG = 2. Ao escolher chamar validação de cada
        // Criar método do CPF e Método RG para cada validação.  
        public void SelecionarDocumento()
        {
            Console.WriteLine("Informe o tipo de documento do Usuário: ");
        }

        // Implementar método que verifica as Permissões do UsuarioTipo. Ex: Admin = Editar, Excluir e Procurar.
        public void VerificarPermissoes()
        {
            string[] listPermissoes = Permissoes.ToString().Split(", ");
            foreach (Enumerados.PermissoesUsuario permissao in Enum.GetValues(typeof(Enumerados.PermissoesUsuario)))
            {
                if (permissao != Enumerados.PermissoesUsuario.Nenhuma && Permissoes.HasFlag(permissao))
                {
                    Console.WriteLine($"{Nome} Permissões: {permissao}");
                }
            }
        }

        public void AdicionarUsuarioLista(string nome, string cpf, int idade, Enumerados.UsuarioTipo tipoUsuario)
        {
            ListaUsuarios.Add(new Users {Nome = nome, Cpf = cpf, Idade = idade, UsuarioTipo = tipoUsuario});
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
            if (!string.IsNullOrEmpty(requestUser))
            {
                var usuarioEncontrado = ListaUsuarios.FirstOrDefault(u => u.Nome == requestUser);

                if (usuarioEncontrado?.Nome == requestUser)
                {
                    return usuarioEncontrado?.Nome;
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
