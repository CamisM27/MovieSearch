namespace SeleniumTest
{
    public class Movies(string title, string duration, int year, string rating, double score, int classification)
    {
        public string Title { get; set; } = title;
        public int Year { get; set; } = year;
        public string Rating { get; set; } = rating;
        public double Score { get; set; } = score;
        public string Duration { get; set; } = duration;
        public int Classification { get; set; } = classification;

        public void DescriptionMovie()
        {
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($"Nome do filme: {Title}\nDuração: {Duration}   Ano de lançamento: {Year}     Classificação indicativa: {Rating}\nAvaliação: {Score}    Posição: {Classification}");
        }

    }
}