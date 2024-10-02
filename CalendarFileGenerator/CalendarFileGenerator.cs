// <copyright file="CalendarFileGenerator.cs" company="Eli Parker">
// Copyright (c) 2024 Eli Parker. All rights reserved.
// </copyright>
// Implementation written by Eli Parker
// Date: 10/2/24
namespace CalendarFileGenerator;
using Ical.Net;

/// <summary>
/// TODO Eli Parker: Add class summary here.
/// </summary>
public class CalendarFileGenerator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarFileGenerator"/> class.
    /// </summary>
    public CalendarFileGenerator()
    {
    }

    /// <summary>
    /// Export the calendar to a file at the given file-path as a .ics file.
    /// </summary>
    /// <returns> True if writing to the given path was successful, false otherwise. </returns>
    public void ExportCalendarToFilePath()
    {
        // TODO IMPLEMENT ME
    }

    /// <summary>
    /// Adds a new event to the calendar file.
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public bool AddCalendarEvent(string eventName, DateOnly startTime, DateOnly endTime)
    {
        return false;
        // TODO IMPLEMENT ME
    }
}
