namespace SmartBook.Repository.Tests;


public class RepositoryTests
{
    private Library library = Library.Instance;
    private DummyData dummyData = new();
    private DirectoryInfo directoryInformationPath;
    private JSONRepository jsonRepository;
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
    public void CheckThatTheLibraryIsNotNullTest() {
        // Arrange
        library.ClearLibrary();

        // Act
        dummyData.PopulateLibrary(ref library);

        // Assert
        Assert.NotNull(library);
    }

    [Fact]
    public void CheckThatTheLibraryIsNotEmptyTest() {
        // Arrange
        library.ClearLibrary();

        // Act
        dummyData.PopulateLibrary(ref library);

        // Assert
        Assert.NotEmpty(library.Books);
    }

    [Fact]
    public void CheckThatSaveToFileNullTest() {
        //Arrange
        library.ClearLibrary();
        List<LibraryBook> books = null;
        // jsonRepository.SaveToFile(books);

        // Act
        var caughtExecption = Assert.Throws<ArgumentNullException>(() => jsonRepository.SaveToFile(books));

        // Assert
        Assert.Equal("Library cannot be null. (Parameter 'data')", caughtExecption.Message);

    }

}
