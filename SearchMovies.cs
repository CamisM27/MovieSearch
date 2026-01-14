namespace SeleniumTest;

public class SearchMovies
{
    public static void Menu(List<Movies> movies)
    {
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Escolha uma opção para a pesquisa de filmes:");
        Console.WriteLine("1 - Pesquisar por classificação indicativa");
        Console.WriteLine("2 - Pesquisar por avaliação");
        Console.WriteLine("3 - Pesquisar por posição");
        Console.WriteLine("4 - Pesquisar por ano de lançamento");
        Console.WriteLine("5 - Listar todos os filmes");
        Console.WriteLine("6 - Sair");
        
        int opcao = int.Parse(Console.ReadLine()!);
        switch (opcao)
        {
            case 1: SearchClassification(movies); break;
            case 2: SearchAvaliate(movies); break;
            case 3: SearchPosition(movies); break;
            case 4: SearchYear(movies); break;
            case 5: ListMovies(movies); break;
            case 6: Console.Clear(); Environment.Exit(0); break;
            default: Menu(movies); break;
        }
        Console.Clear();
        Menu(movies);
    }

    public static void SearchClassification(List<Movies> movies)
    {
        Console.WriteLine("Deseja pesquisar por qual classificação indicativa:");
        string idade = Console.ReadLine() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(idade))
            Menu(movies);
        if (idade.Equals("livre", StringComparison.CurrentCultureIgnoreCase))
            idade = "Livre";
        foreach (var movie in movies)
        {
            if (movie.Rating == idade)
                movie.DescriptionMovie();
        }
        Espera();
    }

    public static void SearchAvaliate(List<Movies> movies)
    {
        try
        {
            Console.WriteLine("Deseja pesquisar por qual avaliação[0-10]:");
            double avaliacao = double.Parse(Console.ReadLine()!.Replace(",", "."));
            if (avaliacao < 0 || avaliacao > 10)
            {
                Console.WriteLine("Opção inválida");
                Thread.Sleep(2000);
                Menu(movies);
            }

            Console.Clear();
            Console.WriteLine("Clique 'ESC' para sair do modo visualização\n-----MODO VISUALIZAÇÃO-----");
            int countMovies = 0;
            foreach (var movie in movies)
            {
                if (movie.Score == avaliacao)
                {
                    movie.DescriptionMovie();
                    countMovies += 1;
                }       
            }

            if (countMovies == 0)
                Console.WriteLine("Não possui filmes com essa avaliação");
            Espera();
        }
        catch
        {
            Console.Clear();
            Menu(movies);
        } 
    }

    public static void SearchPosition(List<Movies> movies)
    {
        Console.WriteLine("Deseja pesquisar por qual posição[1-250]:");
        Console.WriteLine("De: ");
        int posicaoInicial = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Para: ");
        int posicaoFinal = int.Parse(Console.ReadLine()!);

        foreach (var movie in movies)
        {
            string testeTeste = movie.Classification.Replace("#", "");
            int teste = int.Parse(testeTeste);

            if (teste >= posicaoInicial & teste <= posicaoFinal)
                movie.DescriptionMovie();
        }
        Espera();
    }
    public static void SearchYear(List<Movies> movies)
    {
        Console.WriteLine("Deseja pesquisar por qual ano:");
        Console.WriteLine("De: ");
        int anoInicial = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Para: ");
        int anoFinal = int.Parse(Console.ReadLine()!);

        foreach (var movie in movies)
        {
            int teste = int.Parse(movie.Year);

            if (teste >= anoInicial & teste <= anoFinal)
                movie.DescriptionMovie();
        }
        Espera();
    }

    public static void ListMovies(List<Movies> movies)
    {
        foreach (var movie in movies)
        {
            movie.DescriptionMovie();
        }
        Espera();
        
    }
    public static void Espera()
    {
        do
        {
            Thread.Sleep(1000);
        }
        while (Console.ReadKey().Key != ConsoleKey.Escape);
    }
}