﻿// <copyright file="CalendarFileGenerator.cs" company="Eli Parker">
// Copyright (c) 2024 Eli Parker. All rights reserved.
// </copyright>
// Implementation written by Eli Parker
// Date: 10/5/24
namespace CalendarFileGenerator;

using System.Text;
using Ical.Net;
using Ical.Net.Serialization;

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
        var serializer = new CalendarSerializer();
        var serializedCalendar = serializer.SerializeToString(cal);
        return Encoding.UTF8.GetBytes(serializedCalendar);
    }

    /// <summary>
    /// Adds a new event to the calendar file.
    /// </summary>
    /// <param name="eventName"> The name of the event, as it should appear on the calendar. </param>
    /// <param name="startTime"> The start time of the event, as it should appear on the calendar. </param>
    /// <param name="endTime"> The end time of the event, as it should appear on the calendar. </param>
    /// <returns> True if the calendar was changed as a result of adding the event, false otherwise. </returns>
    public bool AddCalendarEvent(string eventName, DateOnly startTime, DateOnly endTime)
    {
        return false;
        // TODO IMPLEMENT ME
    }
}