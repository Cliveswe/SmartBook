using System.Text.Json;
namespace SmartBook.Repository;
public class JSONRepository
{

    private readonly DirectoryInfo directoryInformationPath;

    /// <summary>
    /// The path to the file where the data is stored.
    /// </summary>
    public static string FilePath => @"..\..\..\..\Smartbook\Data\";
    /// <summary>
    /// The name of the file where the data is stored.
    /// </summary>
    public static string FileName => "Library";
    /// <summary>
    /// The file extension of the file where the data is stored.
    /// </summary>
    public static string FileExtension => ".json";
    /// <summary>
    /// The full path to the file where the data is stored.
    /// </summary>
    public static string PathToFile => Path.Combine(FilePath, FileName + FileExtension);

    /// <summary>
    /// Constructor for the JSONRepository class.
    /// </summary>
    public JSONRepository() {

        directoryInformationPath = new DirectoryInfo(FilePath);
        if(!directoryInformationPath.Exists) {
            directoryInformationPath.Create();
        }

        if(!File.Exists(PathToFile)) {
            File.Create(PathToFile).Close();
        }
    }

    /// <summary>
    /// Saves the data to a file in JSON format.
    /// </summary>
    /// <param name="data"></param>
    public void SaveToFile(List<LibraryBook> data) {
        ArgumentNullException.ThrowIfNull(data);

        string jsonData = JsonSerializer.Serialize(data);
        File.WriteAllText(PathToFile, jsonData);
    }

    /// <summary>
    /// Loads the data from a file in JSON format.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public List<LibraryBook> LoadFromFile() {
        string data = File.ReadAllText(PathToFile);
        if(!string.IsNullOrEmpty(data)) {
            List<LibraryBook>? books = JsonSerializer.Deserialize<List<LibraryBook>>(data);
            if(books != null) {
                return books;
            }
        }
        throw new FileNotFoundException("File not found or empty.", PathToFile);
    }

    /// <summary>
    /// Deletes the file if it exists.
    /// </summary>
    public void DeleteFile() {
        // Remove the file if it exists
        if(File.Exists(PathToFile)) {
            File.Delete(PathToFile);
        }
    }
}
