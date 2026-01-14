using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest;

public class SeleniumNavegator
{
    public static void NavegateToPage()
    {
        var options = new ChromeOptions();
        options.AddArguments("--incognito", "--start-maximized");
    
        IWebDriver driver = new ChromeDriver(options);
    
        driver.Navigate().GoToUrl("https://www.imdb.com/pt/chart/top/");

        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));

        var principalTitule = wait.Until(d => d.FindElement(By.ClassName("chart-layout-specific-title-text"))).Text;
        //Console.WriteLine(principalTitule);
    
        var allMovies = wait.Until(d => d.FindElement(By.ClassName("sc-d24d5d37-0")));
        var listMovies = allMovies.FindElements(By.ClassName("ipc-metadata-list-summary-item__tc"));
        List<Movies> moviesList = [];
        foreach (var movie in listMovies)
        {
            string movieTitle = movie.FindElement(By.ClassName("ipc-title__text")).Text;
            string movieRating = movie.FindElement(By.ClassName("ipc-signpost__text")).Text;
            double movieScore = double.Parse(movie.FindElement(By.ClassName("ipc-rating-star--rating")).Text.Replace(",", "."));
            

            var addInfos = movie.FindElements(By.ClassName("sc-b4f120f6-7"));

            string movieClassific;
            try
            {
                movieClassific = addInfos[2].Text;
            }
            catch (Exception)
            {
                movieClassific = "Não possui";
            }

            string movieDuration = addInfos[0].Text;
            string movieYear = addInfos[1].Text;

            Movies movies = new(movieTitle, movieYear, movieDuration, movieClassific, movieScore, movieRating);
            
            moviesList.Add(movies);        
        }

        driver.Quit();
        SearchMovies.Menu(moviesList);
    }
}
