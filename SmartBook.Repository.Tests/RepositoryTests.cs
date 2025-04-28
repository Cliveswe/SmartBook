namespace SmartBook.Repository.Tests;

public class RepositoryTests
{
    private readonly DirectoryInfo directoryInformationPath = new DirectoryInfo(@"..\..\..\..\Smartbook\Repository");
    [Fact]
    public void CheckThatTheRepositoryJSONIsInTheCorrectDirectoryTest() {

        var y = directoryInformationPath.GetFiles();
        Assert.NotNull(y);
    }
}
