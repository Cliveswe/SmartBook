using System.Text.Json;
namespace SmartBook.Repository;
public class JSONRepository
{

    private readonly string filePath;
    private readonly string fileName;
    private readonly string fileExtension;
    private readonly DirectoryInfo directoryInformationPath;

    public JSONRepository(string filePath, string fileName, string fileExtension) {
        this.filePath = filePath;
        this.fileName = fileName;
        this.fileExtension = fileExtension;

        directoryInformationPath = new DirectoryInfo(filePath);
        if(!directoryInformationPath.Exists) {
            directoryInformationPath.Create();
        }

        if(!File.Exists(Path.Combine(filePath, fileName + fileExtension))) {
            File.Create(Path.Combine(filePath, fileName + fileExtension)).Close();
        }
    }
    public void SaveToFile(List<LibraryBook> data) {
        ArgumentNullException.ThrowIfNull(data);
        string jsonData = JsonSerializer.Serialize(data);
        File.WriteAllText(Path.Combine(filePath, fileName + fileExtension), jsonData);
    }

    public List<LibraryBook> LoadFromFile() {
        string data = File.ReadAllText(Path.Combine(filePath, fileName + fileExtension));
        if(!string.IsNullOrEmpty(data)) {
            List<LibraryBook>? books = JsonSerializer.Deserialize<List<LibraryBook>>(data);
            if(books != null) {
                return books;
            }
        }
        throw new FileNotFoundException("File not found or empty.", Path.Combine(filePath, fileName + fileExtension));
    }
}
