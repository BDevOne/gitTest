using Course.Entities.Exceptions;
using Firebase.Auth;
using gitTeste.Entities;
using gitTeste.Entities.Enums;

class Program
{
    static void Main(string[] args)
    {
        Tela tela = new Tela();
        Console.WriteLine("Opção 1: Cadastrar Usuário");

        Console.Write("Informe a opção: ");
        var opcoes = Console.ReadLine();

        try
        {
            if (Enum.TryParse(opcoes, out Enumerados.OpcoesEscolhaTela escolha))
            {
                switch (escolha)
                {
                    case Enumerados.OpcoesEscolhaTela.CadastrarUsuario:
                        tela.TelaCriarUsuario();
                        break;
                    case Enumerados.OpcoesEscolhaTela.ProcurarUsuario:
                        string? procurarUser = "";
                        tela.GetSearchUser(procurarUser);
                        break;
                    default:
                        throw new DomainException("Nenhuma opção válida encontrada!!");
                }
            }
            else
            {
                throw new ArgumentException($"O valor '{opcoes}' não é válido!!!!");
            }
        }
        catch (DomainException e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return;
        }

        // Aplicar validação para verificar se possui usuários cadastrados para ser chamado o método: tela.ExibirDadosUsuarios();
        tela.ExibirDadosUsuarios();

        Console.WriteLine("Passou Aqui!!");

        // Aplicar validação para verificar se possui usuários cadastrados para ser chamado o método: tela.GetEditarUsuario();

        // var dbTestConnection = db_firebase.CreateCredential();

        // Console.WriteLine(dbTestConnection);

        Console.ReadKey();
    }
}