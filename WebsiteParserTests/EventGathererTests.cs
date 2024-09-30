namespace WebsiteParserTests;

using EventGatherer;
/// <summary>
/// <para>
/// Contains a (hopefully) robust test suite for the <see cref="EventGatherer"/> class.
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
public class EventGathererTests
{

    /// <summary>
    /// Test a basic valid case with the constructor.
    /// </summary>
    [TestMethod]
    public void TestConstructor_Basic()
    {
        EventGatherer gatherer = new EventGatherer("https://registrar.utah.edu/academic-calendars/fall2024.php");
    }
}