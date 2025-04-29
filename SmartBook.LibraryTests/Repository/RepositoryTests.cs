using SmartBook.Repository;

namespace SmartBook.LibraryTests.Repository;


public class RepositoryTests
{
    private Library library = Library.Instance;
    private readonly DummyData dummyData = new();
    private DirectoryInfo directoryInformationPath;
    private JSONRepository jsonRepository;
    private readonly string filePath = @"..\..\..\..\Smartbook\Data\";
    private readonly string fileName = "Library";
    private readonly string fileExtension = ".json";



    [Fact]
    public void CheckThatTheLibraryIsSaveAndLoadedTest() {
        // Arrange
        directoryInformationPath = new(filePath);
        jsonRepository = new JSONRepository(filePath, fileName, fileExtension);
        dummyData.PopulateLibrary(ref library);
        List<LibraryBook> expectedLibrary = library.GetAllAvailableBooksSortedByTitle();

        // Act
        jsonRepository.SaveToFile(library.GetAllAvailableBooksSortedByTitle());

        // Assert
        Assert.Equal(jsonRepository.LoadFromFile(), expectedLibrary);

        // Clean up the test data
        jsonRepository.DeleteFile();
    }

    [Fact]
    public void CheckThatSaveToFileNullTest() {
        //Arrange
        List<LibraryBook>? books = null;
        jsonRepository = new JSONRepository(filePath, fileName, fileExtension);

        // Act
        ArgumentNullException caughtExecption = Assert.Throws<ArgumentNullException>(
            () => jsonRepository.SaveToFile(books!));

        // Assert
        Assert.Equal("Value cannot be null. (Parameter 'data')",
            caughtExecption.Message);

        // Clean up the test data
        jsonRepository.DeleteFile();
    }
}
