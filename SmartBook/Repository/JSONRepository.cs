namespace SmartBook.Repository;
public class JSONRepository
{

    private string filePath;
    private string fileName;
    private string fileExtension;
    public JSONRepository(string filePath, string fileName, string fileExtension) {
        this.filePath = filePath;
        this.fileName = fileName;
        this.fileExtension = fileExtension;
    }
    public void SaveToFile(string data) {
        // TODO Implementation for saving data to a JSON file
    }
    public string LoadFromFile() {
        // TODO Implementation for loading data from a JSON file
        return string.Empty;
    }
}
