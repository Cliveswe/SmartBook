namespace SmartBook.Repository.Tests;


public class RepositoryTests
{
    private Library library = Library.Instance;
    private DummyData dummyData = new();
    private DirectoryInfo directoryInformationPath;
    JSONRepository jsonRepository;
    private readonly string filePath = @"..\..\..\..\Smartbook\Data\";
    private readonly string fileName = "Library";
    private readonly string fileExtension = ".json";



    /// <summary>
    /// Constructor for the RepositoryTests class.
    /// </summary>
    public RepositoryTests() {
        directoryInformationPath = new(filePath);
        jsonRepository = new JSONRepository(filePath, fileName, fileExtension);
        dummyData.PopulateLibrary(ref library);
    }

    [Fact]
    public void CheckThatTheLibraryIsSaveAndLoadedTest() {
        // Arrange
        JSONRepository jsonRepository = new(filePath, fileName, fileExtension);
        List<LibraryBook> expectedLibrary = library.GetAllAvailableBooksSortedByTitle();

        // Act
        jsonRepository.SaveToFile(library.GetAllAvailableBooksSortedByTitle());
        // Assert
        Assert.Equal(jsonRepository.LoadFromFile(), expectedLibrary);
    }
}
