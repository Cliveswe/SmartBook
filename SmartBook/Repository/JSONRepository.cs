using System.Text.Json;
namespace SmartBook.Repository;
public class JSONRepository
{

    private readonly DirectoryInfo directoryInformationPath;

    public static string FilePath => @"..\..\..\..\Smartbook\Data\";
    public static string FileName => "Library";
    public static string FileExtension => ".json";
    public static string PathToFile => Path.Combine(FilePath, FileName + FileExtension);
    public JSONRepository() {

        directoryInformationPath = new DirectoryInfo(FilePath);
        if(!directoryInformationPath.Exists) {
            directoryInformationPath.Create();
        }

        if(!File.Exists(PathToFile)) {
            File.Create(PathToFile).Close();
        }
    }
    public void SaveToFile(List<LibraryBook> data) {
        ArgumentNullException.ThrowIfNull(data);

        string jsonData = JsonSerializer.Serialize(data);
        File.WriteAllText(PathToFile, jsonData);
    }

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

    public void DeleteFile() {
        // Remove the file if it exists
        if(File.Exists(PathToFile)) {
            File.Delete(PathToFile);
        }
    }
}
