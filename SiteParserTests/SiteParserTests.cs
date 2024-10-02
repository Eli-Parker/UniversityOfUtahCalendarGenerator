// <copyright file="SiteParserTests.cs" company="Eli Parker">
// Copyright (c) 2024 Eli Parker. All rights reserved.
// </copyright>
// Implementation written by Eli Parker
// Date: 10/1/24

namespace SiteParserTests;

using SiteParser;
using EventList;

/// <summary>
/// <para>
/// Contains a basic test suite for the <see cref="SiteParser"/> class.
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
public class SiteParserTests
{
    /// <summary>
    /// Test ParseSite gets through an entire valid site
    /// without throwing an error when given a valid link.
    /// </summary>
    [TestMethod]
    public void TestParseSite_NoErrors()
    {
        EventList list = SiteParser.ParseSite("https://registrar.utah.edu/academic-calendars/spring2024.php");
    }

    /// <summary>
    /// Test that the list is full of the correct values once the parser is finished.
    /// </summary>
    [TestMethod]
    public void TestParseSite_FillsListWithProperValues()
    {
        EventList list = SiteParser.ParseSite("https://registrar.utah.edu/academic-calendars/fall2024.php");

        /*
         * Compare event table titles
        */

        // Expected
        List<string> expectedTableList = [
            "General Calendar Dates",
            "Semester Length Classes",
            "First Half Classes",
            "Second Half Classes",
            "Holidays"];

        string expectedTableListStr = string.Join(", ", expectedTableList);

        // Actual
        var actualTableList = list.GetAllEventTables();

        string actualTableListStr = string.Join(", ", actualTableList);

        Assert.AreEqual(expectedTableListStr, actualTableListStr);

        /*
         * Compare event table values
         */

        // Expected
        List<string> expectedEventTitles = [
            "Labor Day",
            "Fall Break",
            "Thanksgiving Break",
            "Holiday Recess"];

        List<DateOnly> expectedEventStartDates = [
            new DateOnly(2024,  9,  2),
            new DateOnly(2024, 10,  6),
            new DateOnly(2024, 11, 28),
            new DateOnly(2024, 12, 14)];

        List<DateOnly> expectedEventEndDates = [
            new DateOnly(2024,  9,  2),
            new DateOnly(2024, 10, 13),
            new DateOnly(2024, 12,  1),
            new DateOnly(2025,  1,  5)];

        // Actual
        list.GetEvents(
            "Holidays",
            out List<string> actualEventTitles,
            out List<DateOnly> actualEventStartDates,
            out List<DateOnly> actualEventEndDates);

        // Assertions
        Assert.AreEqual(
            string.Join(", ", expectedEventTitles),
            string.Join(", ", actualEventTitles) );

        Assert.AreEqual(
            string.Join(", ", expectedEventStartDates),
            string.Join(", ", actualEventStartDates) );

        Assert.AreEqual(
            string.Join(", ", expectedEventEndDates),
            string.Join(", ", actualEventEndDates) );
    }

    /// <summary>
    /// Test that parse site throws an InvalidLinkException when bad links are given.
    /// </summary>
    [TestMethod]
    public void TestParseSite_BadLinks()
    {
        Assert.ThrowsException<InvalidLinkException>(() => { _ = SiteParser.ParseSite(string.Empty); });
        Assert.ThrowsException<InvalidLinkException>(() => { _ = SiteParser.ParseSite("google.com"); });
        Assert.ThrowsException<InvalidLinkException>(() => { _ = SiteParser.ParseSite("https://www.google.com/"); });
        Assert.ThrowsException<InvalidLinkException>(() => { _ = SiteParser.ParseSite("https://registrar.utah.edu/handbook/transfer-student-resources.php"); });
        Assert.ThrowsException<InvalidLinkException>(() => { _ = SiteParser.ParseSite("https://registrar.utah.edu/academic-calendars/dentistry-2024-2025.php"); });
    }

    /// <summary>
    /// Test that parse site throws an error when the page is invalid but all other checks pass.
    /// </summary>
    [TestMethod]
    public void TestParseSite_LinkWithoutPHPAtEnd()
    {
        Assert.ThrowsException<InvalidLinkException>(() => { _ = SiteParser.ParseSite("https://registrar.utah.edu/academic-calendars/fall2024"); });
        Assert.ThrowsException<InvalidLinkException>(() => { _ = SiteParser.ParseSite("https://registrar.utah.edu/academic-calendars/wefwefwef.php"); });
    }

    /// <summary>
    /// <para>
    /// Test that a website which is invalid because the HTML reader cant get it
    /// throws an invalid link exception instead of another exception from libraries.
    /// Intended to test what happens when the website goes down.
    /// </para>
    /// NOTE: To properly use this test case, CheckURL needs to be disabled in <see cref="SiteParser"/>.
    /// Otherwise this throws an invalid link exception for a different reason.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidLinkException))]
    public void TestParseSite_DownWebsite()
    {
        _ = SiteParser.ParseSite(string.Empty);
    }
}