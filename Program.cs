using gitTeste;

class Program
{
    static void Main(string[] args)
    {
        Users usuario = new Users("Pedro", 18);

        var Idade = usuario.Idade;

        if (Idade != 0)
        {
            Console.WriteLine("Teste");
        }

    }
}