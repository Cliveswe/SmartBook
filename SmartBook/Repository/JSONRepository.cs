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
    public void SaveToFile(string data) {
        // ToDo Implementation for saving data to a JSON file
    }
    public string LoadFromFile() {
        // ToDo Implementation for loading data from a JSON file
        return string.Empty;
    }
}
