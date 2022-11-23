using DNP1_Server.Utils;

namespace DNP1_Server.Database; 

/// <summary>
/// Communication to save and fetch data from database
/// </summary>
public interface IDatabase {
    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="user">The user object to create</param>
    /// <returns>The newly created user</returns>
    Task<User> CreateUserAsync(User user);
    
    /// <summary>
    /// Get a user's info
    /// </summary>
    /// <param name="username">Their username</param>
    /// <returns>User info, if found</returns>
    Task<User> GetUserInfoAsync(string username);
    
    /// <summary>
    /// Create a new post
    /// </summary>
    /// <param name="author">The username of the author of the post (author must exist in database!)</param>
    /// <param name="title">Title of the post</param>
    /// <param name="body">Main content of the post</param>
    /// <returns>The newly created post</returns>
    Task<Post> CreatePostAsync(string author, string title, string body);
    
    /// <summary>
    /// Get a list of all posts in the database
    /// </summary>
    /// <returns>All posts, including their id, title, body and author username</returns>
    Task<List<Post>> GetAllPostsAsync();
    
    /// <summary>
    /// Get a singular post by it's id
    /// </summary>
    /// <param name="id">The id of the post</param>
    /// <returns>A singular post information</returns>
    Task<Post> GetPostAsync(string id);
}