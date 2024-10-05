// <copyright file="CalendarFileGeneratorTests.cs" company="Eli Parker">
// Copyright (c) 2024 Eli Parker. All rights reserved.
// </copyright>
// Tests written by Eli Parker
// Date: 10/3/2024
namespace CalendarFileGeneratorTests;

using CalendarFileGenerator;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using System.Text;

/// <summary>
/// Contains a robust test suite for <see cref="CalendarFileGenerator"/>.
/// </summary>
[TestClass]
public class CalendarFileGeneratorTests
{
    /// <summary>
    /// Test that the constructor finishes without any errors.
    /// </summary>
    [TestMethod]
    public void TestCalendarFile_ConstructorThrowsNoErrors()
    {
        CalendarFileGenerator c = new();
    }

    /// <summary>
    /// Test that adding a calendar event returns true, and that the event is contained when the calendar file is serialized and exported.
    /// </summary>
    [TestMethod]
    public void TestCalendarFile_AddCalendarEvent()
    {
        // Setup
        CalendarFileGenerator c = new();
        c.AddCalendarEvent("Test Event", new DateOnly(2024, 10, 3), new DateOnly(2024, 10, 4));

        // Expected
        Calendar expectedCal = new();
        CalendarEvent expectedEvent = new()
        {
            Start = new CalDateTime(2024, 10, 3),
            End = new CalDateTime(2024, 10, 4),
            Summary = "Test Event",
        };
        expectedCal.Events.Add(expectedEvent);
        byte[] expectedFile = SerializeCalendar(expectedCal);

        // Actual
        byte[] actualFile = c.GetCalendarFile();

        CollectionAssert.AreEqual(expectedFile, actualFile);
    }

    /// <summary>
    /// Test that an empty calendar file is generated when no events are added, and that there is no error.
    /// </summary>
    [TestMethod]
    public void TestCalendarFile_EmptyCalendar()
    {
        // Setup
        CalendarFileGenerator c = new();

        // Expected
        Calendar expectedCal = new();
        byte[] expectedFile = SerializeCalendar(expectedCal);

        // Actual
        byte[] actualFile = c.GetCalendarFile();

        CollectionAssert.AreEqual(expectedFile, actualFile);
    }

    /// <summary>
    /// Test that an empty string is NOT added as an event to the calendar file.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestAddEvent_EmptyStringEvent_IsInvalid()
    {
        CalendarFileGenerator c = new();
        c.AddCalendarEvent(string.Empty, new DateOnly(2024, 10, 3), new DateOnly(2024, 10, 4));
    }

    /// <summary>
    /// Test that adding an event with an invalid date range returns false.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestCalendarFile_FalseForInvalidEvent()
    {
        // Setup
        CalendarFileGenerator c = new();

        // Add invalid event
        c.AddCalendarEvent("Invalid Event", new DateOnly(2024, 10, 5), new DateOnly(2024, 10, 4));
    }

    /// <summary>
    /// Test that overlapping events are handled correctly.
    /// </summary>
    [TestMethod]
    public void TestCalendarFile_OverlappingEvents()
    {
        // Setup
        CalendarFileGenerator c = new();
        c.AddCalendarEvent("Event 1", new DateOnly(2024, 10, 3), new DateOnly(2024, 10, 5));
        c.AddCalendarEvent("Event 2", new DateOnly(2024, 10, 4), new DateOnly(2024, 10, 6));

        // Expected
        Calendar expectedCal = new();
        expectedCal.Events.Add(new CalendarEvent
        {
            Summary = "Event 1",
            Start   = new CalDateTime(2024, 10, 3),
            End     = new CalDateTime(2024, 10, 5),
        });
        expectedCal.Events.Add(new CalendarEvent
        {
            Summary = "Event 2",
            Start   = new CalDateTime(2024, 10, 4),
            End     = new CalDateTime(2024, 10, 6),
        });

        byte[] expectedFile = SerializeCalendar(expectedCal);

        // Actual
        byte[] actualFile = c.GetCalendarFile();

        CollectionAssert.AreEqual(expectedFile, actualFile);
    }

    /// <summary>
    /// Test that adding an event with an invalid date range does not change the calendar file,
    /// nor does it prevent other events from being added afterwards.
    /// </summary>
    [TestMethod]
    public void TestCalendarFile_InvalidEventDates_CanStillAdd()
    {
        /*
         * Setup
         */

        CalendarFileGenerator c = new();
        c.AddCalendarEvent("Test Event1", new DateOnly(2024, 10, 3), new DateOnly(2024, 10, 4));

        // Add invalid event and ensure it throws an exception
        Assert.ThrowsException<ArgumentException>( () => c.AddCalendarEvent("Invalid Event", new DateOnly(2024, 10, 5), new DateOnly(2024, 10, 4)));

        // Add another event to make sure you can add events after an invalid event
        c.AddCalendarEvent("Test Event2", new DateOnly(2024, 10, 5), new DateOnly(2024, 10, 6));

        /*
         * Expected
         */

        Calendar expectedCal = new();
        CalendarEvent expectedEvent1 = new()
        {
            Start = new CalDateTime(2024, 10, 3),
            End = new CalDateTime(2024, 10, 4),
            Summary = "Test Event1",
        };
        CalendarEvent expectedEvent2 = new()
        {
            Start = new CalDateTime(2024, 10, 5),
            End = new CalDateTime(2024, 10, 6),
            Summary = "Test Event2",
        };
        expectedCal.Events.Add(expectedEvent1);
        expectedCal.Events.Add(expectedEvent2);
        byte[] expectedFile = SerializeCalendar(expectedCal);

        /*
         * Actual
         */

        byte[] actualFile = c.GetCalendarFile();

        CollectionAssert.AreEqual(expectedFile, actualFile);
    }

    /// <summary>
    /// <para>
    /// Test that a functional file can actually be
    /// generated from the output of the calendar file generator.
    /// </para>
    /// <remarks>
    /// This test is mostly to test by hand (and for my own curiosity),
    /// the program cannot feasibly test that the file opens in a calendar correctly.
    /// </remarks>
    /// </summary>
    [TestMethod]
    public void TestCalendarFile_GenerateFile()
    {
        // Setup
        CalendarFileGenerator c = new();
        c.AddCalendarEvent("Test Event", new DateOnly(2024, 10, 3), new DateOnly(2024, 10, 4));

        // Generate the file
        byte[] calendarFile = c.GetCalendarFile();
        string outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResults");
        Directory.CreateDirectory(outputDirectory);
        string filePath = Path.Combine(outputDirectory, "TestCalendar.ics");

        File.WriteAllBytes(filePath, calendarFile);

        // Verify the file was created
        Assert.IsTrue(File.Exists(filePath));

        // Optionally, clean up the file after the test
        bool deleteFile = true;

        if (deleteFile)
        {
            File.Delete(filePath);
        }
    }

    /// <summary>
    /// <para>
    /// Private helper method which serializes and returns a calendar object converted to a byte array.
    /// </para>
    /// <remarks>
    /// This method helps make cleaner and more readable test code.
    /// </remarks>
    /// </summary>
    /// <param name="cal"> The calendar object. </param>
    /// <returns> A byte array which contained the content of the iCal file.</returns>
    private static byte[] SerializeCalendar(Calendar cal)
    {
        return Encoding.UTF8.GetBytes(new CalendarSerializer().SerializeToString(cal));
    }
}