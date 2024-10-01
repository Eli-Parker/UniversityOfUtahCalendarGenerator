namespace WebsiteParserTests;

using EventList;
using System.Collections.Generic;

/// <summary>
/// <para>
/// Contains a (hopefully) robust test suite for the <see cref="EventList"/> class.
/// </para>
/// <para>
/// Notable testing considerations included:
/// <list type="bullet">
/// <item> What happens when you give an invalid link</item>
/// <item> What happens when you test Websites of different years? </item>
/// <item> Does the program error out when you give it websites from the same domain with different info? </item>
/// <item> </item>
/// </list>
/// </para>
/// </summary>
[TestClass]
public class EventListTests
{

    /// <summary>
    /// Test a basic valid case of adding two values with dates to two tables
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

    public void TestConstructor_Empty()
    {
        EventList list = new EventList();
        List<string> expectedTableNames = [];
        string expectedTableSetString = string.Join(", ", expectedTableNames);

        List<string> actualResult = list.GetAllEventTables();
        string actualSetString = string.Join(", ", actualResult);

        Assert.AreEqual(expectedTableSetString, actualSetString);
    }

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
    /// Test the behavior of the list when events are added.
    /// </summary>
    [TestMethod]
    public void TestAddEvent_DuplicateValues() 
    {
        EventList list = new EventList();
        list.AddEvent("table 1", "event 1", new DateOnly(2024, 12, 24), new DateOnly(2024, 12, 25));
        list.AddEvent("table 1", "event 1", new DateOnly(2024, 12, 24), new DateOnly(2024, 12, 25));

        //Check for only multiple values

        list.GetEvents("table 1", out List<string> tableEvents, out List<DateOnly> ds, out List<DateOnly> de);

        Assert.AreEqual(tableEvents.Count, 2);
        Assert.AreEqual(ds.Count, 2);
        Assert.AreEqual(de.Count, 2);

    }
}