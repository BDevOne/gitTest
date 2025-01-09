using gitTeste;

class Program
{
    static void Main(string[] args)
    {
        Tela tela = new Tela();

        Console.WriteLine("Opção 1: Tela de Cadastro");
        Console.WriteLine("Opção 2: Tela Login");
        Console.WriteLine("Opção 3: Tela de Cadastro");

        Console.Write("Qual opção: ");
        var opcoes = Console.ReadLine();

        switch (opcoes)
        {
           case "1":
               tela.TelaCriarUsuario();
           break;
           case "2":
               tela.SearchUser();
           break;
            
           default:
               throw new Exception("Nenhuma opção selecionada!!");
        }

        tela.ExibirDadosUsuarios();

        // var dbTestConnection = db_firebase.CreateCredential();

        // Console.WriteLine(dbTestConnection);

        Console.ReadKey();
    }
}