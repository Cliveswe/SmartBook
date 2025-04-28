namespace SmartBook.Repository.Tests;

public class UnitTest1
{
    private readonly DirectoryInfo directoryInformationPath = new DirectoryInfo(@"..\..\..\..\Smartbook\Repository");
    [Fact]
    public void Test1() {

        var y = directoryInformationPath.GetFiles();
        Assert.NotNull(y);
    }
}
