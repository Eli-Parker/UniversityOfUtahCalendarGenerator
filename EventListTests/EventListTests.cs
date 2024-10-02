// <copyright file="EventListTests.cs" company="Eli Parker">
// Copyright (c) 2024 Eli Parker. All rights reserved.
// </copyright>
// Implementation written by Eli Parker
// Date: 10/1/24
namespace EventListTests;

using EventList;
using System.Collections.Generic;

/// <summary>
/// <para>
/// Contains a basic test suite for the <see cref="EventList"/> class.
/// </para>
/// <remarks>
/// This program is currently only tested with a few basic cases, just to
/// verify its in working order and will work for my use case.
/// </remarks>
/// </summary>
[TestClass]
public class EventListTests
{
    /// <summary>
    /// Test a basic valid case of adding two values with dates to two tables.
    /// </summary>
    [TestMethod]
    public void TestConstructor_Tables()
    {
        EventList list = new EventList();
        list.AddEvent("table 1", "event 1", new DateOnly(2024, 12, 24), new DateOnly(2024, 12, 25));
        list.AddEvent("table 1", "event 2", new DateOnly(2024, 12, 24), new DateOnly(2024, 12, 25));
        list.AddEvent("table 2", "event 1", new DateOnly(2024, 12, 24), new DateOnly(2024, 12, 25));
        list.AddEvent("table 2", "event 2", new DateOnly(2024, 12, 24), new DateOnly(2024, 12, 25));

        // Check tables are correct
        List<string> expectedTableNames = ["table 1", "table 2"];
        string expectedTableSetString = string.Join(", ", expectedTableNames);

        List<string> actualResult = list.GetAllEventTables();
        string actualSetString = string.Join(", ", actualResult);

        Assert.AreEqual(expectedTableSetString, actualSetString);
    }

    /// <summary>
    /// Test that the values are correct with a basic given input.
    /// </summary>
    [TestMethod]
    public void TestConstructor_Values()
    {
        EventList list = new EventList();
        list.AddEvent("table 1", "event 1", new DateOnly(2024, 12, 24), new DateOnly(2024, 12, 25));
        list.AddEvent("table 1", "event 2", new DateOnly(2024, 12, 24), new DateOnly(2024, 12, 25));
        list.AddEvent("table 2", "event 1", new DateOnly(2024, 12, 24), new DateOnly(2024, 12, 25));
        list.AddEvent("table 2", "event 2", new DateOnly(2024, 12, 24), new DateOnly(2024, 12, 25));

        List<string> expectedTableNames = ["table 1", "table 2"];

        // Check individual events
        // Expected values
        List<string> expectedEventsNames = ["event 1", "event 2"];
        List<DateOnly> expectedStartDates = [new DateOnly(2024, 12, 24), new DateOnly(2024, 12, 24)];
        List<DateOnly> expectedEndDates = [new DateOnly(2024, 12, 25), new DateOnly(2024, 12, 25)];

        // Make strings for AreEqual
        string expectedEventString = string.Join(", ", expectedEventsNames);
        string expectedEventStartString = string.Join(", ", expectedStartDates);
        string expectedEventEndString = string.Join(", ", expectedEndDates);

        // Actual values
        foreach (string value in expectedTableNames)
        {
            // Initialize values
            List<string> actualEventNames;
            List<DateOnly> actualEventDateStart;
            List<DateOnly> actualEventDateEnd;
            list.GetEvents(value, out actualEventNames, out actualEventDateStart, out actualEventDateEnd);

            // Expected name
            string actualEventString = string.Join(", ", actualEventNames);
            Assert.AreEqual(expectedEventString, actualEventString);

            // Expected start date
            string actualStartDateString = string.Join(", ", actualEventDateStart);
            Assert.AreEqual(expectedEventStartString, actualStartDateString);

            // Expected end date
            string actualEndDateString = string.Join(", ", actualEventDateEnd);
            Assert.AreEqual(expectedEventEndString, actualEndDateString);
        }
    }

    /// <summary>
    /// Test that an empty constructor returns an
    /// empty list of tables and doesn't error out.
    /// </summary>
    [TestMethod]
    public void TestConstructor_TablesEmpty()
    {
        EventList list = new EventList();

        List<string> actualResult = list.GetAllEventTables();
        string actualSetString = string.Join(", ", actualResult);

        Assert.AreEqual(string.Empty, actualSetString);
    }

    /// <summary>
    /// Check that calling for the events of a table that
    /// doesn't exist doesn't error out and returns empty lists.
    /// </summary>
    [TestMethod]
    public void TestConstructor_ValuesEmpty()
    {
        EventList list = new EventList();

        list.GetEvents("table 1", out List<string> tableEvents, out List<DateOnly> ds, out List<DateOnly> de);

        Assert.AreEqual(tableEvents.Count, 0);
        Assert.AreEqual(ds.Count, 0);
        Assert.AreEqual(de.Count, 0);
    }

    /// <summary>
    /// Test the behavior of the list when events are added.
    /// </summary>
    [TestMethod]
    public void TestAddEvent_DuplicateValues()
    {
        EventList list = new EventList();
        list.AddEvent("table 1", "event 1", new DateOnly(2024, 12, 24), new DateOnly(2024, 12, 25));
        list.AddEvent("table 1", "event 1", new DateOnly(2024, 12, 24), new DateOnly(2024, 12, 25));

        // Check for only multiple values
        list.GetEvents("table 1", out List<string> tableEvents, out List<DateOnly> ds, out List<DateOnly> de);

        Assert.AreEqual(tableEvents.Count, 2);
        Assert.AreEqual(ds.Count, 2);
        Assert.AreEqual(de.Count, 2);
    }
}