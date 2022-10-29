using Newtonsoft.Json;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Movies;

public class ServiceSearch
{
    static int pagenumber = 1;
    static int qidiruv = 0;
    static string qidiruvsoz = "";
    static int qator = 1;
    static List<Search> moveaTitles;
    static string url = "";
    static string id = "";
    public string Name()
    {
        Console.Write("Searching : ");
        string name = Console.ReadLine()!;
        return "&s=" + name;
    }
    public void ListOfSearchingResults()
    {
        string url = "http://www.omdbapi.com/?apikey=";

        string myKey = "ed7b702d";

        HttpClient client = new HttpClient();

        string searchbytitle = "";
        if (qidiruv == 0)
            qidiruvsoz = Name();
        searchbytitle = url + myKey + qidiruvsoz + "&page=" + pagenumber;

        var response = client.GetAsync(searchbytitle).Result;

        var clientString = response.Content.ReadAsStringAsync().Result;

        //var searchinglist = JsonSerializer.Deserialize<ResponseSeachClass>(clientString);
        var searchinglist = JsonConvert.DeserializeObject<ResponseSeachClass>(clientString);

        if (searchinglist.Search == null)
        {
            Console.WriteLine("Error 404 Not Founded");

            Console.WriteLine("Pleace press the 'Backspace'");
        }
        else
        {
            moveaTitles = searchinglist.Search;
            Console.WriteLine("Movies:\n");

            for(int i = 0;i < moveaTitles.Count;i++)
            {   if(qator-1 == i)
                    Console.WriteLine($" |>  {moveaTitles[i].Title}");
                else
                    Console.WriteLine($"   {moveaTitles[i].Title}");
                id = moveaTitles[i].imdbID;
            }
            if (pagenumber == 1)
                Console.WriteLine($"\npages: <<( {pagenumber} )>> {pagenumber + 1}> ..{int.Parse(searchinglist.totalResults) / 10+1}              total searching results:{searchinglist.totalResults}");
            else if (pagenumber == 2)
                Console.WriteLine($"\npages: <{pagenumber - 1} << ( {pagenumber} )>> {pagenumber + 1}> ..{int.Parse(searchinglist.totalResults) / 10+1}              total searching results:{searchinglist.totalResults}");
            else
                Console.WriteLine($"\npages: 1.. <{pagenumber - 1} <<( {pagenumber} )>> {pagenumber + 1}> ..{int.Parse(searchinglist.totalResults) / 10+1}             total searching results:{searchinglist.totalResults}");

            Console.WriteLine("\nPleace press the 'Backspace' for Searching page");
        }
    }
    public void InformationAboutIt()
    {
        Console.Clear();
        string myKey = "ed7b702d";
        HttpClient client2 = new HttpClient();
        //http://www.omdbapi.com/?i=tt0096895
        string url2 = $"http://www.omdbapi.com/?i={id}&plot=full&apikey=ed7b702d";
        var response2 = client2.GetAsync(url2).Result;

        var client2String = response2.Content.ReadAsStringAsync().Result;

        //var aboutMovie = JsonSerializer.Deserialize<Root>(client2String);
        var aboutMovie = JsonConvert.DeserializeObject<Root>(client2String);
 
        Console.WriteLine($"About '{aboutMovie.Title}' film");

        Console.WriteLine($"\n  Created in: {aboutMovie.Year} year ");
        Console.WriteLine($"\n  Released: {aboutMovie.Released} ");
        Console.WriteLine($"\n  Film duration: {aboutMovie.Released} ");
        Console.WriteLine($"\n  Genre: {aboutMovie.Genre} ");
        Console.WriteLine($"\n  Film director: {aboutMovie.Director} ");
        Console.WriteLine($"\n  Director: {aboutMovie.Director} ");
        Console.WriteLine($"\n  Writer: {aboutMovie.Writer} ");
        Console.WriteLine($"\n  Syujet: {aboutMovie.Plot} ");
        Console.WriteLine($"\n  Language: {aboutMovie.Language} ");
        Console.WriteLine($"\n  Country: {aboutMovie.Country} ");
        Console.WriteLine($"\n  Awards: {aboutMovie.Awards} ");

        Console.WriteLine("\nYou can press the 'Backspace'");

        ConsoleKey key = Console.ReadKey().Key;
        if (ConsoleKey.Backspace == key)
        {
            ListOfSearchingResults();
        }
    }
    public void ManageConsoleKey()
    {
        while (true)
        {
            Console.Clear();
            ListOfSearchingResults();
            qidiruv++;
            ConsoleKey key = Console.ReadKey().Key;
            if(ConsoleKey.Backspace == key)
            {
                qidiruv=0;
                pagenumber = 1;
            }else if(ConsoleKey.RightArrow == key)
            {
                pagenumber++;
            }else if (ConsoleKey.LeftArrow == key && pagenumber!=1)
            {
                pagenumber--;
            }else if (ConsoleKey.Escape == key)
            {
                Program.Main();
            }else if (ConsoleKey.UpArrow == key && qator!=1)
            {
                qator--;
            }
            else if (ConsoleKey.DownArrow == key && qator != 10)
            {
                qator++;
            }
            else if (ConsoleKey.Enter == key)
            {
                InformationAboutIt();
            }
        }
    }
}