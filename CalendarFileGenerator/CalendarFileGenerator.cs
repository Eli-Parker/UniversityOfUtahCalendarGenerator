// <copyright file="CalendarFileGenerator.cs" company="Eli Parker">
// Copyright (c) 2024 Eli Parker. All rights reserved.
// </copyright>
// Implementation written by Eli Parker
// Date: 10/5/24
namespace CalendarFileGenerator;

using System.Text;
using Ical.Net;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Ical.Net.CalendarComponents;

/// <summary>
/// A class which can generate iCal files from the given
/// calendar events.
/// </summary>
public class CalendarFileGenerator
{
    /// <summary>
    /// Calendar object to store events in.
    /// </summary>
    private readonly Calendar cal;

    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarFileGenerator"/> class.
    /// </summary>
    public CalendarFileGenerator()
    {
        cal = new Calendar();
    }

    /// <summary>
    /// <para>
    /// Export the calendar to an array of bytes.
    /// This array of bytes can be easily outputted on the front end as an iCal file.
    /// </para>
    /// </summary>
    /// <returns> An array of bytes which contains the contents of the .ics file encoded in UTF8. </returns>
    public byte[] GetCalendarFile()
    {
        CalendarSerializer serializer = new();
        string serializedCalendar = serializer.SerializeToString(cal);
        return Encoding.UTF8.GetBytes(serializedCalendar);
    }

    /// <summary>
    /// <para>
    /// Adds a new event to the calendar file.
    /// </para>
    /// <para>
    /// Note that empty strings are not allowed for the event name,
    /// and the start date must be before the end date. Both of these
    /// errors will throw an <see cref="ArgumentException"/>.
    /// </para>
    /// </summary>
    /// <param name="eventName"> The name of the event, as it should appear on the calendar. This parameter cannot be empty or whitespace. </param>
    /// <param name="startDate"> The start date of the event, as it should appear on the calendar. This date must come before or be equal to the <paramref name="endDate"/>. </param>
    /// <param name="endDate"> The end date of the event, as it should appear on the calendar. This date must come after or be equal to the <paramref name="startDate"/>. </param>
    /// <exception cref="ArgumentException"> Thrown when the <paramref name="eventName"/> is empty or whitespace, or when the <paramref name="startDate"/> comes after the <paramref name="endDate"/>. </exception>
    public void AddCalendarEvent(string eventName, DateOnly startDate, DateOnly endDate)
    {
        /*
         * Invalid Event Checks
         */

        // Check for invalid event name
        if (string.IsNullOrWhiteSpace(eventName))
        {
            throw new ArgumentException($"Event name {eventName} cannot be empty or whitespace.");
        }

        // Check for bad date range
        if (startDate > endDate)
        {
            throw new ArgumentException($"Event start date ({startDate}) cannot come after end date ({endDate}).");
        }

        /*
         * Add event
         */

        cal.Events.Add(new CalendarEvent
        {
            Summary = eventName,
            Start   = new CalDateTime(startDate.Year, startDate.Month, startDate.Day),
            End     = new CalDateTime(endDate.Year, endDate.Month, endDate.Day),
        });
    }
}
