namespace DNP1_Server.Database; 

public class DataAccess : IDataAccess {
    public void SaveData(string path, string data) {
        File.WriteAllText(path, data);
    }

    public string LoadJsonData(string path) {
        if (!File.Exists(path))
            File.WriteAllText(path, "[]");
        
        using (StreamReader r = new StreamReader(path)) {
            string json = r.ReadToEnd();
            return json;
        }
    }
}