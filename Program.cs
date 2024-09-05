using gitTeste;

class Program
{
    static void Main(string[] args)
    {
        Tela tela = new Tela();

        string nome = ""; string cpf = ""; int idade = 0;
        
        tela.TelaLogin(nome, cpf, idade);
    }
}