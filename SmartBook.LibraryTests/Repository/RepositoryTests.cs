using SmartBook.Repository;

namespace SmartBook.LibraryTests.Repository;


public class RepositoryTests
{
    private readonly Library library = Library.Instance;
    private readonly DummyData dummyData = new();
    private readonly DirectoryInfo directoryInformationPath;
    private readonly JSONRepository jsonRepository;
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

    [Fact]
    public void CheckThatSaveToFileNullTest() {
        //Arrange
        List<LibraryBook>? books = null;
        // Act
        var caughtExecption = Assert.Throws<ArgumentNullException>(
            () => jsonRepository.SaveToFile(books!));
        // Assert
        Assert.Equal("Value cannot be null. (Parameter 'data')",
            caughtExecption.Message);

    }

}
