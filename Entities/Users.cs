using System;
using gitTeste.Entities.Enums;


namespace gitTeste.Entities
{
    public class Users
    {
        #region Properties
        public string? Nome { get; set; }
        public int Idade { get; set; }
        public string? Cpf { get; set; }
        public string? Documento { get; set; }
        public int Id { get; private set; }
        public Enumerados.UsuarioTipo UsuarioTipo { get; set; }
        public Enumerados.PermissoesUsuario Permissoes { get; set; }
        public DateTime DataNascimento { get; set; }
        // public Guid UniqueId { get; private set; }
        #endregion

        List<Users> ListaUsuarios = new List<Users>();
        TratarDados tratarDados = new TratarDados();

        #region Constructors
        public Users()
        {
        }

        public Users(string _nome, string _cpf, int _idade, Enumerados.UsuarioTipo _usuarioTipo, DateTime dataNascimento)
        {
            Nome = _nome;
            Cpf = _cpf;
            Idade = _idade;
            UsuarioTipo = _usuarioTipo;
            DataNascimento = dataNascimento;
            Id = IdCreateUser(Id);
        }

        public Users(string _nome, string _cpf, DateTime _dataNascimento, Enumerados.UsuarioTipo _usuarioTipo)
        {
            Nome = _nome;
            Cpf = _cpf;
            DataNascimento = _dataNascimento;
            UsuarioTipo = _usuarioTipo;
            Id = IdCreateUser(Id);
        }
        
        #endregion

        // Remover método e adicionar validação no método de criação de usuário
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

        public void AtribuirUsuarioPermissao()
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

        /* Adicionar validação na classe tela para apenas exibir essa opção caso usuário logado tenha permissão de Edição de Usuário*/
        public void EdicaoUsuario(string campoEditar)
        {
            if (UsuarioTipo == Enumerados.UsuarioTipo.Administrador || UsuarioTipo == Enumerados.UsuarioTipo.Master)
            {
                SelecionarPropriedade(campoEditar);

                string novoDadoTexto = " ";
                switch (campoEditar)
                {
                    case "NomeCampo":
                        EditarNomeUsuario(novoDadoTexto);
                        break;
                    case "IdadeCampo":
                        EditarDataNascimento();
                        break;
                    case "TipoUsuarioCampo":
                        GetTipoUsuario();
                        break;
                    default:
                        throw new Exception("Nenhum campo informado!!");
                }
            }
        }

        private void EditarNomeUsuario(string novoNomeUsuario)
        {
            try
            {
                Console.Write($"Digite o novo Nome do Usuário: ");
                novoNomeUsuario = Console.ReadLine();

                if (string.IsNullOrEmpty(novoNomeUsuario) || string.IsNullOrWhiteSpace(novoNomeUsuario))
                {
                    Console.WriteLine("Informação inválida!! Digite novamente: ");
                    novoNomeUsuario = Console.ReadLine();

                    if (string.IsNullOrEmpty(novoNomeUsuario) || string.IsNullOrWhiteSpace(novoNomeUsuario))
                        throw new ArgumentException("O Campo informado não pode ser nulo ou vazio!!!");
                }
                Nome = novoNomeUsuario;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Erro: {e.Message}");
            }
        }

        // Criar prop DataNascimentoUsuario e adicionar validação para calcular a idade do usuário.
        private void EditarDataNascimento() // Alterar para EditarDataNascimentoUsuario, 
        {
            Console.Write("Informe a Idade do Usuário: ");
            int novaIdade = int.Parse(Console.ReadLine());

            if (novaIdade < 18)
            {

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

        private void SelecionarPropriedade(string propCampo)
        {
            Console.Write("Selecione o Campo que seja alterar: ");
            var campo = Console.ReadLine();

            switch (campo)
            {
                case "1":
                    propCampo = Enumerados.CampoPropriedade.NomeCampo.ToString();
                    break;
                case "2":
                    propCampo = Enumerados.CampoPropriedade.DataNascimentoCampo.ToString();
                    break;
                case "3":
                    propCampo = Enumerados.CampoPropriedade.TipoUsuarioCampo.ToString();
                    break;
                default:
                    throw new Exception("Nenhum campo selecionado!!");
            }
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

        // Criar método que adiciona usuário (sem idade) na lista de usuários.
        public void AdicionarUsuarioLista(string nome, string cpf, int idade, Enumerados.UsuarioTipo tipoUsuario, DateTime dataNascimento)
        {
            ListaUsuarios.Add(new Users
            {
                Nome = nome,
                Cpf = tratarDados.MascaraCpf(cpf),
                Idade = idade,
                UsuarioTipo = tipoUsuario,
                DataNascimento = dataNascimento
            });
        }

        public void SearchUser(string requestUser)
        {
            try
            {
                Console.Write("Informe Nome do usuário que deseja procurar: ");

                requestUser = Console.ReadLine();
                if (requestUser == null || string.IsNullOrWhiteSpace(requestUser))
                {
                    Console.WriteLine("Campo não pode ser nulo ou vazio. Digite novamente: ");
                    requestUser = Console.ReadLine();

                    if (requestUser == null || string.IsNullOrWhiteSpace(requestUser))
                        throw new NullReferenceException("Campo não pode ser nulo ou vazio!!!");
                }

                var usuarioEncontrado = ListaUsuarios.FirstOrDefault(u => u.Nome?.Equals(requestUser, StringComparison.OrdinalIgnoreCase) == true);

                if (usuarioEncontrado != null && usuarioEncontrado.Nome == requestUser)
                {
                    requestUser = usuarioEncontrado?.Nome;
                }
                else
                {
                    throw new NullReferenceException("Nenhum usuário encontrado!!!");
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        public void SepararUsuarioTipo()
        {
            if (ListaUsuarios.FirstOrDefault(c => c.UsuarioTipo == Enumerados.UsuarioTipo.Nenhum) != null)
            {
                foreach (var separar in ListaUsuarios)
                {
                    if (Enum.TryParse(separar.UsuarioTipo.ToString(), true, out Enumerados.UsuarioTipo usuarioTipo) && usuarioTipo != Enumerados.UsuarioTipo.Nenhum)
                    {
                        separar.AtribuirUsuarioPermissao();
                        continue;
                    }
                    if (separar.UsuarioTipo == 0 || separar.UsuarioTipo == Enumerados.UsuarioTipo.Nenhum)
                    {
                        // usuariosSemTipoDefinido.Add(separar);
                    }
                }
            }
        }

    }
}
