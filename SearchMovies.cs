using System;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumTest;

public class SearchMovies
{
    public static void Menu(List<Movies> movies)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Escolha uma opção para a pesquisa de filmes:");
            Console.WriteLine("1 - Pesquisar por classificação indicativa");
            Console.WriteLine("2 - Pesquisar por avaliação");
            Console.WriteLine("3 - Pesquisar por posição");
            Console.WriteLine("4 - Pesquisar por ano de lançamento");
            Console.WriteLine("5 - Listar todos os filmes");
            Console.WriteLine("6 - Sair");

            if (!int.TryParse(Console.ReadLine(), out int opcao))
                continue;

            Console.Clear();
            switch (opcao)
            {
                case 1: SearchRating(movies); break;
                case 2: SearchAvaliate(movies); break;
                case 3: SearchPosition(movies); break;
                case 4: SearchYear(movies); break;
                case 5: ShowMovies(movies); break;    
                case 6: Console.Clear(); Environment.Exit(0); break;
                default: return;
            }
        }
    }

    public static void SearchRating(List<Movies> movies)
    {
        Console.WriteLine("Pesquisar por classificação indicativa:");
        string idade = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(idade))
            return;

        if (idade.Equals("livre", StringComparison.OrdinalIgnoreCase))
            idade = "Livre";

        var resultado = movies.Where(m => m.Rating == idade);
        ShowMovies(resultado);
    }

    public static void SearchAvaliate(List<Movies> movies)
    {
        Console.WriteLine("Pesquisar por avaliação [0-10]:");

        if (!double.TryParse(Console.ReadLine()?.Replace(",", "."), out double avaliacao) ||
            avaliacao < 0 || avaliacao > 10)
        {
            Console.WriteLine("Avaliação inválida.");
            Espera();
            return;
        }

        var resultado = movies.Where(m => m.Score == avaliacao);
        ShowMovies(resultado);
    }

    public static void SearchPosition(List<Movies> movies)
    {
        Console.WriteLine("Pesquisar por posição [1-250]");

        Console.Write("De: ");
        if (!int.TryParse(Console.ReadLine(), out int inicio))
            return;

        Console.Write("Para: ");
        if (!int.TryParse(Console.ReadLine(), out int fim))
            return;

        var resultado = movies.Where(m =>
            m.Classification >= inicio && m.Classification <= fim);

        ShowMovies(resultado);
    }

    public static void SearchYear(List<Movies> movies)
    {
        Console.WriteLine("Pesquisar por ano de lançamento");

        Console.Write("De: ");
        if (!int.TryParse(Console.ReadLine(), out int inicio))
            return;

        Console.Write("Para: ");
        if (!int.TryParse(Console.ReadLine(), out int fim))
            return;

        var resultado = movies.Where(m => m.Year >= inicio && m.Year <= fim);

        ShowMovies(resultado);
    }

    public static void ShowMovies(IEnumerable<Movies> movies)
    {
        int count = 0;

        Console.WriteLine("----- RESULTADOS -----\n");

        foreach (var movie in movies)
        {
            movie.DescriptionMovie();
            count++;
        }

        if (count == 0)
            Console.WriteLine("Nenhum filme encontrado.");

        Espera();
    }

    public static void Espera()
    {
        Console.WriteLine("\nPressione ESC para voltar ao menu...");
        while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
    }
}
