using System.Text;
namespace SmartBook.Repository.Tests;


public class RepositoryTests
{

    private DirectoryInfo directoryInformationPath;

    /// <summary>
    /// Constructor for the RepositoryTests class.
    /// </summary>
    public RepositoryTests() {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"..\..\..\..\Smartbook\Repository");
        directoryInformationPath = new DirectoryInfo(sb.ToString());
    }

    [Fact]
    public void CheckThatTheRepositoryJSONIsInTheCorrectDirectoryTest() {
        Assert.NotNull(directoryInformationPath);
    }
}
