// <copyright file="SiteParser.cs" company="Eli Parker">
// Copyright (c) 2024 Eli Parker. All rights reserved.
// </copyright>
// Implementation written by Eli Parker
// Date: 10/1/24

namespace SiteParser;

using HtmlAgilityPack;
using EventList;
using System.Text.RegularExpressions;

/// <summary>
/// New exception class to throw when the link isn't valid,
/// so we can catch it in the GUI code.
/// </summary>
public class InvalidLinkException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidLinkException"/> class.
    /// This constructor is used with a message.
    /// </summary>
    /// <param name="message"> The message to throw the exception with. </param>
    public InvalidLinkException(string message)
        : base(message)
    {
    }
}

/// <summary>
/// <para>
/// Contains code to read from and parse the website.
/// </para>
/// <para>
/// In terms of the formatting of the website: All rows are contained on tables
/// marked with the "table" HTML tag, and each row is marked with the "tr" tag within "tbody".
/// </para>
/// <para>
/// The program works by drilling into the given HTML document and finding all the tables, then
/// taking each entry and formatting it into the proper numbers from text,
/// then adding it to an <see cref="EventList"/> object.
/// </para>
/// </summary>
public class SiteParser
{
    /// <summary>
    /// Parses the given site passed on the given URL. Note that the
    /// given URL must be a link to the University of Utah campus site.
    /// </summary>
    /// <param name="url"> The link to the University of Utah campus site. </param>
    /// <returns> An EventList object which contains all event on the given U of U Campus Site. </returns>
    public static EventList ParseSite(string url)
    {
        // Check if the URL is valid
        CheckURL(url);

        // Initialize EventList
        EventList list = new();

        // Setup web scraper
        HtmlDocument doc;

        // Wrap in try-catch in case of downed website
        try
        {
            HtmlWeb web = new HtmlWeb();
            doc = web.Load(url);
        }
        catch (UriFormatException e)
        {
            throw new InvalidLinkException(e.Message);
        }

        // Grab the year from the title of the document
        string year = doc.DocumentNode.SelectSingleNode("//title").InnerText.Split(" ")[1];

        // Search for all HTML elements of type <Table>
        HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table");

        if(tables == null)
        {
            // No tables, link must be invalid or not exist
            throw new InvalidLinkException("Invalid URL, check that provided URL goes to the right place. (Dev: URL has no table HTML attributes in HTMLdoc)");
        }

        // Loop through all tables
        foreach (var table in tables)
        {
            // Add table values to list
            AddTableToEventList(table, list, year);
        }

        // Return final EventList
        return list;
    }

    /// <summary>
    /// Adds all the events on a given table HTML node to the given event list.
    /// <remarks>
    /// Year is a separate parameter because the tables on their own do not contain the year.
    /// </remarks>
    /// </summary>
    /// <param name="table"> The node which contains the table to evaluate. </param>
    /// <param name="list"> The list to add all the events to. </param>
    /// <param name="year"> The year to add to the event. </param>
    private static void AddTableToEventList(HtmlNode table, EventList list, string year)
    {
        // Get the title of the table
        string tableTitle = table.SelectSingleNode("caption").InnerText;

        // Replace the non-breaking space HTML tag with a space
        tableTitle = Regex.Replace(tableTitle, @"&nbsp;", " ");

        // Remove HTML entities from title
        tableTitle = Regex.Replace(tableTitle, @"&[a-zA-Z0-9#]+;", string.Empty);

        // Get all the rows in the table minus the title
        HtmlNodeCollection rows = table.SelectSingleNode("tbody").SelectNodes("tr");

        // Loop through all rows
        foreach (HtmlNode row in rows)
        {
            // Get all columns in row
            List<string> values = new();
            foreach (var tableCell in row.SelectNodes("td"))
            {
                values.Add(tableCell.InnerText);
            }

            // Process token values and remove HTML entities
            string eventTitle = Regex.Replace(values[0], @"&amp;", "&");
            eventTitle = Regex.Replace(eventTitle, @"&[a-zA-Z0-9#]+;", string.Empty);

            // Format the date values
            DateOnly startDate;
            DateOnly endDate;
            ConvertTextToDate(values[1], year, out startDate, out endDate);

            // Add to EventList with formatted info
            list.AddEvent(tableTitle, eventTitle, startDate, endDate);
        }
    }

    /// <summary>
    /// <para>
    /// Takes the given raw date string and converts it to a DateOnly object.
    /// </para>
    /// <para>
    /// Raw dates can be formatted in one of a few ways:
    /// <list type="number">
    /// <item> As "Month Day" (e.g. October 12)</item>
    /// <item> As "Month Day - Month day" (e.g. October 12 - December 15)</item>
    /// <item> As "Month Day - day" (e.g. October 12-15)</item>
    /// <item> As "Month Day - DoW., Month day" (e.g. December 16 - Sun., January 7)</item>
    /// </list>
    /// </para>
    /// </summary>
    /// <param name="rawDate"> The unparsed string. For formatting see the class description.</param>
    /// <param name="year">A string which contains the year, formatted as YYYY (e.g. 2024).</param>
    /// <param name="startDate"> The starting date from the rawDate.</param>
    /// <param name="endDate"> The ending date from the rawDate.</param>
    private static void ConvertTextToDate(string rawDate, string year, out DateOnly startDate, out DateOnly endDate)
    {
        // Remove HTML entities
        rawDate = Regex.Replace(rawDate, @"&[a-zA-Z0-9#]+;", string.Empty);

        // Split the text based on the date range splitter sign
        List<string> dateSplitByRange = rawDate.Split("-").ToList();

        // Check for case where a different kind of dash is used
        if(dateSplitByRange.Count == 1)
        {
            dateSplitByRange = rawDate.Split("‐").ToList();
        }

        // Check the case where days of the week is formatted with a dash (e.g. mon-fri)
        if(dateSplitByRange.Count == 3)
        {
            dateSplitByRange.RemoveAt(0);
        }

        // Format the day of the week out of the value
        string formattedFirstDate = Regex.Replace(dateSplitByRange[0], @"^.*,", string.Empty).Trim();

        // Date is formatted as "Month Day", split it and store result
        string month = formattedFirstDate.Split(" ")[0];
        string day = formattedFirstDate.Split(" ")[1];

        // Remove value we already checked for simplicity
        dateSplitByRange.RemoveAt(0);

        startDate = ParseDateValues(day, month, year);

        // If there are still values in the list, then the date is a range
        if (dateSplitByRange.Count > 0)
        {
            // Check for the case where the date is formatted as "Month Day - day"
            if (int.TryParse(dateSplitByRange[0].Trim(), out _))
            {
                dateSplitByRange[0] =  startDate.ToString("MMMM") + " " + dateSplitByRange[0];
            }

            // Format the day of the week out of the value
            formattedFirstDate = Regex.Replace(dateSplitByRange[0], @"^.*,", string.Empty).Trim();

            // Set the end date value
            month = formattedFirstDate.Split(" ")[0];
            day = formattedFirstDate.Split(" ")[1];
            endDate = ParseDateValues(day, month, year);

            // If month of end date is before month of start date, year must be different
            if(endDate.Month < startDate.Month)
            {
                endDate = endDate.AddYears(1);
            }
        }
        else
        {
            // Case of single day event, set end date identical to start date
            endDate = startDate;
        }
    }

    /// <summary>
    /// Takes a list of strings which are formatted as "Month Day" and converts them to a DateOnly object.
    /// Helper method for the <see cref="ConvertTextToDate"/> method.
    /// </summary>
    /// <param name="dayStr"> a list of strings with length 2 where arr[0] contains the month formatted. </param>
    /// <param name="monthStr"> A string which has the month, formatted as the entire month (e.g. January). </param>
    /// <param name="yearStr"> The year to be added to the DateOnly object. </param>
    /// <returns> A DateOnly object which has the date as described by the day, month, and year params. </returns>
    private static DateOnly ParseDateValues(string dayStr, string monthStr, string yearStr)
    {
        // Cleanup month and day strings
        monthStr = monthStr.Trim().ToLower();
        dayStr = dayStr.Trim().ToLower();

        // Convert the month to a number
        int month = 0;
        try
        {
            // First try built-in parse method
            month = DateTime.ParseExact(monthStr, "MMMM", System.Globalization.CultureInfo.CurrentCulture).Month;
        }
        catch(FormatException)
        {
            // If parse fails, use a switch
            // Format month string to get rid of periods
            monthStr = Regex.Replace(monthStr, @"\.", string.Empty);
            month = monthStr.ToLower() switch
            {
                "jan" =>  1,
                "feb" =>  2,
                "mar" =>  3,
                "apr" =>  4,
                "may" =>  5,
                "jun" =>  6,
                "jul" =>  7,
                "aug" =>  8,
                "sep" =>  9,
                "oct" => 10,
                "nov" => 11,
                "dec" => 12,
                 _    =>  0
            };
        }

        // Convert the day and year to a number
        int day = int.Parse(dayStr);
        int year = int.Parse(yearStr);

        // Return the finished date
        return new DateOnly(year, month, day);
    }

    /// <summary>
    /// Checks the given URL for validity, and
    /// that it points to the correct domain.
    /// </summary>
    /// <param name="url"> The URL to check against. </param>
    /// <exception cref="InvalidLinkException"> Thrown if the formatting or domain is incorrect.</exception>
    private static void CheckURL(string url)
    {
        // Check if the URL is from the correct domain and has the right formatting
        if (!url.Contains("https://registrar.utah.edu/academic-calendars/"))
        {
            throw new InvalidLinkException("The URL must be a link to the University of Utah campus site (i.e. https://registrar.utah.edu/academic-calendars/fall2024.php)");
        }

        // Check for a semester calendar specifically
        bool hasValidSemester = url.Contains("fall") || url.Contains("spring") || url.Contains("summer");
        bool hasYear = Regex.IsMatch(url, @"[0-9]{4}");

        if (!hasYear || !hasValidSemester)
        {
            throw new InvalidLinkException("The ending of the URL must be a valid semester and year (i.e. [beginning of link]/fall2024.php) ");
        }
    }
}
