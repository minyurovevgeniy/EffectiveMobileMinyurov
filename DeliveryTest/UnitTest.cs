using System.Text.RegularExpressions;

namespace DeliveryTest;

[TestClass]
public class ConsoleTest
{
    private string dataSource = "_id,_weight,_district,_deliveryTime\r\n1,1,1,2024-10-21 12:00:00\r\n2,1,1,2024-10-21 13:00:00\r\n3,2,2,2024-10-22 12:00:00\r\n4,1,2,2024-10-22 12:00:00";

    [TestMethod]
    public void checkHeaderSyntax()
    {
        string header = dataSource.Split("\r\n")[0];
        Assert.IsTrue(header.Split(",_").Length>0);
    }

    [TestMethod]
    public void checkNotEmptyCSV()
    {
        int linesCount = dataSource.Split("\r\n").Length;
        Assert.IsTrue(linesCount >= 2);
    }

    [TestMethod]
    public void checkForWhitespacesInHeader()
    {
        string header = dataSource.Split("\r\n")[0];
        Assert.IsFalse(header.Split(' ').Length > 1);
    }
}