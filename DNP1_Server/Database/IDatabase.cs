using DNP1_Server.Database.Enums;
using DNP1_Server.Utils;

namespace DNP1_Server.Database; 

/// <summary>
/// Communication to save and fetch data from database
/// </summary>
public interface IDatabase {
    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="username">New, unique username</param>
    /// <param name="password">User's password</param>
    /// <returns>The newly created user</returns>
    (CreateUserEnum, User?) CreateUser(string username, string password);
    
    /// <summary>
    /// Get a user's info
    /// </summary>
    /// <param name="username">Their username</param>
    /// <returns>User info, if found</returns>
    (GetUserEnum, User?) GetUserInfo(string username);
    
    /// <summary>
    /// Create a new post
    /// </summary>
    /// <param name="author">The username of the author of the post (author must exist in database!)</param>
    /// <param name="title">Title of the post</param>
    /// <param name="body">Main content of the post</param>
    /// <returns>The newly created post</returns>
    (CreatePostEnum, Post?) CreatePost(string author, string title, string body);
    
    /// <summary>
    /// Get a list of all posts in the database
    /// </summary>
    /// <returns>All posts, including their id, title, body and author username</returns>
    (GetPostEnum, List<Post>?) GetAllPosts();
    
    /// <summary>
    /// Get a singular post by it's id
    /// </summary>
    /// <param name="id">The id of the post</param>
    /// <returns>A singular post information</returns>
    (GetPostEnum, Post?) GetPost(string id);
}