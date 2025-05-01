using SmartBook.Repository;

namespace SmartBook.LibraryTests.Repository;

public class RepositoryTests
{
    private Library library = Library.Instance;
    private readonly DummyData dummyData = new();
    private DirectoryInfo? directoryInformationPath;
    private JSONRepository? jsonRepository;

    [Fact]
    public void CheckThatTheLibraryIsSaveAndLoadedTest() {
        // Arrange
        directoryInformationPath = new(JSONRepository.FilePath);
        jsonRepository = new JSONRepository();
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
        jsonRepository = new JSONRepository();

        // Act
        ArgumentNullException caughtExecption = Assert.Throws<ArgumentNullException>(
            () => jsonRepository.SaveToFile(books!));

        // Assert
        Assert.Equal("Data cannot be null. (Parameter 'data')",
            caughtExecption.Message);

        // Clean up the test data
        jsonRepository.DeleteFile();
    }
}
