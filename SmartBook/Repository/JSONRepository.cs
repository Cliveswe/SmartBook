namespace SmartBook.Repository;
public class JSONRepository
{

    private string filePath;
    private string fileName;
    private string fileExtension;
    private DirectoryInfo directoryInformationPath;
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
    public void SaveToFile(Library data) {
        if(data == null)
            throw new ArgumentNullException(nameof(data), "Library cannot be null.");
        string jsonData = System.Text.Json.JsonSerializer.Serialize(data);
        File.WriteAllText(Path.Combine(filePath, fileName + fileExtension), jsonData);
    }

    public Library LoadFromFile() {
        string data = File.ReadAllText(Path.Combine(filePath, fileName + fileExtension));
        if(!string.IsNullOrEmpty(data)) {
            Library? library = System.Text.Json.JsonSerializer.Deserialize<Library>(data);
            if(library != null) {
                return library;
            }
        }
        throw new FileNotFoundException("File not found or empty.", Path.Combine(filePath, fileName + fileExtension));
    }
}
