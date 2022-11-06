namespace DNP1_Server.Database.Enums; 

public enum CreateUserEnum {
    /// <summary>
    /// No problems found
    /// </summary>
    Success,
    /// <summary>
    /// The username already exists in the database
    /// </summary>
    AlreadyExists,
    /// <summary>
    /// Error while loading/saving data
    /// </summary>
    InternalError
}