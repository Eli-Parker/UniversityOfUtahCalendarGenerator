// <copyright file="EventList.cs" company="Eli Parker">
// Copyright (c) 2024 Eli Parker. All rights reserved.
// </copyright>
// Implementation written by Eli Parker
// Date: 10/1/24

using System.Diagnostics;

namespace EventList;

/// <summary>
/// <para>
/// Class to list out all the events separated by the table they came from.
/// This serves two main purposes: Store the data in a way that is easy to access and manipulate,
/// and to make it easy to display the data in a user-friendly way.
/// </para>
/// <remarks>
/// You may notice that the code doesn't put many restrictions on what the formatting of events can look like.
/// This was by design, as I wanted to preserve the formatting of the original website as much as possible.
/// </remarks>
/// </summary>
[DebuggerDisplay("EventList: {events.Count} tables")]
public class EventList
{
    /// <summary>
    /// Careful consideration was given to the data structure used here.
    /// I needed a data structure which:
    /// <list type="number">
    /// <item>Can store items grouped by which table they came from on the site, for user readability in the UI</item>
    /// <item>Can be iterated through relatively quickly and easily</item>
    /// <item>Is quick and efficient to pull and push data to.</item>
    /// </list>
    /// <para>
    /// I landed on a Dictionary with the table name as the key and a list of event objects as the value.
    /// I recognize using a hash table is rather basic,
    /// but I felt is made for a simplistic and fast implementation.
    /// </para>
    /// </summary>
    private readonly Dictionary<string, List<Event>> events;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventList"/> class.
    /// </summary>
    public EventList()
    {
        this.events = new();
    }

    /// <summary>
    /// <para>
    /// Gets all the names of the event tables as a list of strings.
    /// </para>
    /// <example>
    /// IE: If the website has 2 separate tables with "breaks" as the title of the first table
    /// and "coursework deadlines" as the title of the second, GetAllEventTables returns ["breaks", "coursework deadlines"]
    /// </example>
    /// </summary>
    /// <returns> A list of strings which contains all the lists of events.</returns>
    public List<string> GetAllEventTables()
    {
        return events.Keys.ToList();
    }

    /// <summary>
    /// <para>
    /// Get all events from a specific table on the site, and returns them as
    /// three lists containing the event name, and date ranges for each event.
    /// </para>
    /// <para>
    /// Note that the table event name, start date, and end date lists are all synchronized.
    /// tableDatesStart[1] contains the start date for the event at tableEvents[1].
    /// </para>
    /// </summary>
    /// <param name="table"> The name of the table to grab events from. </param>
    /// <param name="tableEvents"> A list which contains an ordered list of the events. </param>
    /// <param name="tableDatesStart"> A list which contains the start dates for events. Note that this lists' ordering is synced with <paramref name="tableEvents"/>. </param>
    /// <param name="tableDatesEnd"> A list which contains the start dates for events. Note that this lists' ordering is synced with <paramref name="tableEvents"/>.
    ///                              If the given event only happens on a single day, this parameter contains identical info to <paramref name="tableDatesStart"/>. </param>
    public void GetEvents(string table, out List<string> tableEvents, out List<DateOnly> tableDatesStart, out List<DateOnly> tableDatesEnd)
    {
        // initialize out values
        tableEvents = new();
        tableDatesStart = new();
        tableDatesEnd = new();

        // Get the values associated with the given table
        List<Event>? rawEvents;
        this.events.TryGetValue(table, out rawEvents);

        // Run through all values and add them to lists
        if (rawEvents != null)
        {
            foreach (Event ev in rawEvents)
            {
                // Get event values from Event object
                string curEventName = ev.GetEventName();
                DateOnly start;
                DateOnly end;
                ev.GetDateRange(out start, out end);

                // Set event values to lists
                tableEvents.Add(ev.GetEventName());
                tableDatesStart.Add(start);
                tableDatesEnd.Add(end);
            }
        }
    }

    /// <summary>
    /// Add a new event to the list of events.
    /// </summary>
    /// <param name="eventTable"> The table which the particular event came from. </param>
    /// <param name="name"> The name of the event. </param>
    /// <param name="startDate"> The start date (first day) which the given event takes place. </param>
    /// <param name="endDate"> The final date (last day) which the given event takes place. </param>
    public void AddEvent(string eventTable, string name, DateOnly startDate, DateOnly endDate)
    {
        // Check to see if there already exists an event list for the given table
        bool tableAlreadyExists = events.ContainsKey(eventTable);
        if (! tableAlreadyExists)
        {
            // Table does not already exist, make a new one
            events.Add(eventTable, new());
        }

        // Add new Event object
        events[eventTable].Add(new Event(name, startDate, endDate));
    }

    /// <summary>
    /// Stores details on a specific event, IE start and end time, name of event, etc.
    /// </summary>
    private class Event
    {
        /// <summary>
        /// The name of the event.
        /// </summary>
        private readonly string eventName;

        /// <summary>
        /// The starting time of the given event.
        /// </summary>
        private readonly DateOnly startDate;

        /// <summary>
        /// The finishing time of the given event.
        /// </summary>
        private readonly DateOnly endDate;

        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// Created with the given name and date range.
        /// </summary>
        /// <param name="eventName"> The name of the event. </param>
        /// <param name="startDate"> The First day of the event. </param>
        /// <param name="endDate"> The last day of the event. </param>
        public Event(string eventName, DateOnly startDate, DateOnly endDate)
        {
            // Assign values
            this.eventName = eventName;
            this.startDate = startDate;
            this.endDate   =   endDate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// Used when the event only occurs on a single day.
        /// </summary>
        /// <param name="eventName"> The name of the event. </param>
        /// <param name="eventDate"> The day of the event. </param>
        public Event(string eventName, DateOnly eventDate)
        {
            // Assign values
            this.eventName = eventName;
            this.startDate = eventDate;
            this.endDate   = eventDate;
        }

        /// <summary>
        /// Gets the name for the Event object.
        /// </summary>
        /// <returns> The event name, as a string. </returns>
        public string GetEventName()
        {
            return this.eventName;
        }

        /// <summary>
        /// Returns the calendar date ranges for the given event.
        /// If the event only occurs on one day, the start and end date are identical.
        /// </summary>
        /// <param name="startDate"> The starting date. </param>
        /// <param name="endDate"> The ending date (last day) of the event. This will be identical to
        ///                        <paramref name="startDate"/> if the event lasts for only one day. </param>
        public void GetDateRange(out DateOnly startDate, out DateOnly endDate)
        {
            startDate = this.startDate;
            endDate = this.endDate;
        }
    }
}
