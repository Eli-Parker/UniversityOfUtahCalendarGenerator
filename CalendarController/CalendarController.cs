// <copyright file="CalendarController.cs" company="Eli Parker">
// Copyright (c) 2024 Eli Parker. All rights reserved.
// </copyright>
// Implementation written by Eli Parker
// Date: 10/8/24
namespace CalendarController;

using EventList;
using SiteParser;
using CalendarFileGenerator;

/// <summary>
/// Interfaces with the back-end and front-end, providing a simple
/// class which can handle all talking between the libraries.
/// </summary>
public class CalendarController
{
    private readonly EventList list;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarController"/> class.
    /// <para>
    /// Grabs and stores the data from the site.
    /// </para>
    /// </summary>
    /// <param name="calendarSite"> The site to grab the data from. </param>
    /// <exception cref="InvalidLinkException"> Thrown when the given link is invalid. </exception>
    public CalendarController(string calendarSite)
    {
        // Initialize list
        list = SiteParser.ParseSite(calendarSite);
    }

    /// <summary>
    /// Returns all the event names from the site grouped by the table they came from.
    /// </summary>
    /// <returns> A dictionary with the table name as the key and a list of event names as the value.</returns>
    public Dictionary<string, List<string>> GetEventsFromSite()
    {
        // Initialize return value
        Dictionary<string, List<string>> resultDictionary = new();

        // For every table in the EventList
        foreach (string table in list.GetAllEventTables())
        {
            // Call GetEvents and store it to list
            List<string> tableEventNames;
            list.GetEvents(table, out tableEventNames, out _, out _);

            // Add that list to dictionary
            bool addedSuccessfully = resultDictionary.TryAdd(table, tableEventNames);
            if(!addedSuccessfully)
            {
                // If there was already a value in the dictionary, add the new list to the existing list
                resultDictionary[table] = resultDictionary[table].Concat(tableEventNames).ToList();
            }
        }

        // Return result
        return resultDictionary;
    }

    /// <summary>
    /// Get the calendar file from the selected list of events.
    /// </summary>
    /// <param name="eventNames"> The list of event names which we want added to the calendar file. </param>
    /// <param name="startDates"> The list of event start dates which we want added to the calendar file. </param>
    /// <param name="endDates"> The list of event end dates which we want added to the calendar file. </param>
    /// <returns> A byte array which contains the data for a .ics file. </returns>
    public byte[] GetCalendarFileFromSelected(List<string> eventNames, List<DateOnly> startDates, List<DateOnly> endDates)
    {
        // Make a new instance of the calendar file generator
        CalendarFileGenerator calFileGen = new();
        // Add those lists to the calendar file generator minus the events which aren't contained in the selection
        for (int i = 0; i < eventNames.Count; i++)
        {
            calFileGen.AddCalendarEvent(eventNames[i], startDates[i], endDates[i]);
        }

        // Return final value
        return calFileGen.GetCalendarFile();
    }
}
