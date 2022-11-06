namespace DNP1_Server.Database.Enums; 

public enum GetPostEnum {
    /// <summary>
    /// No problems found
    /// </summary>
    Success,
    /// <summary>
    /// No post with such id found
    /// </summary>
    NotFound,
    /// <summary>
    /// Error while loading data
    /// </summary>
    InternalError
}