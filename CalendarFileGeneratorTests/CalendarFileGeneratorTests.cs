// <copyright file="CalendarFileGeneratorTests.cs" company="Eli Parker">
// Copyright (c) 2024 Eli Parker. All rights reserved.
// </copyright>
// Tests written by Eli Parker
// Date: 10/3/2024
namespace CalendarFileGeneratorTests;

using CalendarFileGenerator;

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
}