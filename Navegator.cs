using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace SeleniumTest;

public partial class SeleniumNavegator
{
    public static void NavegateToPage()
    {
        List<Movies> moviesList = [];
        string movieRating;

        var options = new ChromeOptions();
        options.AddArguments("--incognito", "--start-maximized");

        ChromeDriver driver = new(options);
        driver.Navigate().GoToUrl("https://www.imdb.com/pt/chart/top/");

        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));

        var principalTitule = wait.Until(d => d.FindElement(By.ClassName("chart-layout-specific-title-text"))).Text;
        var allMovies = wait.Until(d => d.FindElement(By.ClassName("sc-d24d5d37-0")));
        var listMovies = allMovies.FindElements(By.ClassName("ipc-metadata-list-summary-item__tc"));

        foreach (var movie in listMovies)
        {
            string movieTitle = movie.FindElement(By.ClassName("ipc-title__text")).Text;
            int movieClassific = int.Parse(movie.FindElement(By.ClassName("ipc-signpost__text")).Text.Replace("#", ""));
            double movieScore = double.Parse(movie.FindElement(By.ClassName("ipc-rating-star--rating")).Text.Replace(",", "."));
            
            var addInfos = movie.FindElements(By.ClassName("sc-b4f120f6-7"));

            try
            {
                movieRating = addInfos[2].Text;
            }
            catch (Exception)
            {
                movieRating = "Sem informação";
            }

            string movieDuration = addInfos[1].Text;
            int movieYear = int.Parse(MyRegex().Match(addInfos[0].Text).Value);

            Movies movies = new(movieTitle, movieDuration, movieYear, movieRating, movieScore, movieClassific);
            moviesList.Add(movies);        
        }

        new ChromeDriver(options).Quit();
        SearchMovies.Menu(moviesList);
    }

    [GeneratedRegex(@"\d{4}")]
    private static partial Regex MyRegex();
}
