namespace SmartBook.Repository.Tests;


public class RepositoryTests
{
    private Library library;
    private DirectoryInfo directoryInformationPath;
    JSONRepository jsonRepository;
    private readonly string filePath = @"..\..\..\..\Smartbook\Repository";
    private readonly string fileName = "Library";
    private readonly string fileExtension = ".json";

    /// <summary>
    /// Constructor for the RepositoryTests class.
    /// </summary>
    public RepositoryTests() {
        directoryInformationPath = new(filePath);
        jsonRepository = new JSONRepository(filePath, fileName, fileExtension);
        library = Library.Instance;
    }

    [Fact]
    public void CheckThatTheRepositoryJSONIsInTheCorrectDirectoryTest() {
        Assert.NotNull(directoryInformationPath);
    }
}
