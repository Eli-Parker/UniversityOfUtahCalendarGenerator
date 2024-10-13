namespace JsonFileMaker;

using SiteParser;
using EventList;

/// <summary>
/// A simple program to make a JSON file from a list of URLs.
/// 
/// This program is designed to "bake in" the URLs to the JSON file,
/// so that the Blazor App can be run without needing to fetch the URLs from a server.
/// </summary>
internal class JsonFileMaker
{
    private class Site
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
    /// <summary>
    /// Runs the program.
    /// </summary>
    /// <param name="args"> CLA </param>
    static void Main(string[] args)
    {
        // Make a new instance of the EventList and add the URLs to it
        List<Site> sites = new();

        sites.Add(new Site { Name = "Fall 2024", Url = "https://registrar.utah.edu/academic-calendars/fall2024.php" });
        sites.Add(new Site { Name = "Spring 2024", Url = "https://registrar.utah.edu/academic-calendars/spring2024.php" });
        sites.Add(new Site { Name = "Summer 2024", Url = "https://registrar.utah.edu/academic-calendars/summer2024.php" });
        sites.Add(new Site { Name = "Spring 2025", Url = "https://registrar.utah.edu/academic-calendars/spring2025.php" });
        sites.Add(new Site { Name = "Summer 2025", Url = "https://registrar.utah.edu/academic-calendars/summer2025.php" });

        // Make a JSON file from the list of URLs

        foreach (Site site in sites)
        {
            EventList list = SiteParser.ParseSite(site.Url);
            list.ExportJSON(site.Name,"C:\\Users\\EliParker\\Source\\Repos\\Eli-Parker\\UniversityOfUtahCalendarGenerator\\UofUCalGen\\wwwroot\\preset-data");
        }

    }
}