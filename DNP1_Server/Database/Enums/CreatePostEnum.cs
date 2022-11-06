namespace DNP1_Server.Database.Enums; 

public enum CreatePostEnum {
    /// <summary>
    /// No problems found
    /// </summary>
    Success,
    /// <summary>
    /// The author's username was not found in database
    /// </summary>
    AuthorNotFound,
    /// <summary>
    /// Error while loading/saving data
    /// </summary>
    InternalError
}