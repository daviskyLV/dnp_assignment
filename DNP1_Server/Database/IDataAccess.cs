namespace DNP1_Server.Database; 

public interface IDataAccess {
    /// <summary>
    /// Save data to a file
    /// </summary>
    /// <param name="path">Filepath</param>
    /// <param name="data">The data in text format to be saved</param>
    void SaveData(string path, string data);
    
    /// <summary>
    /// Load JSON data
    /// </summary>
    /// <param name="path">The filepath from which to load data</param>
    /// <returns>Data represented as a JSON string</returns>
    string LoadJsonData(string path);
}