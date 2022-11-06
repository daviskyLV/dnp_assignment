namespace DNP1_Server.Database.Enums; 

public enum GetUserEnum {
    /// <summary>
    /// No problems found
    /// </summary>
    Success,
    /// <summary>
    /// No user found in database with such username
    /// </summary>
    NotFound,
    /// <summary>
    /// Error while loading data
    /// </summary>
    InternalError
}