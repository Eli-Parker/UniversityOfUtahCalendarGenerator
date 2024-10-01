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
    /// Test the constructor doesn't throw an error when given a valid link.
    /// </summary>
    [TestMethod]
    public void TestConstructor()
    {
        EventList list = new();
        SiteParser parser = new SiteParser("https://registrar.utah.edu/academic-calendars/fall2024.php", list);
    }
}