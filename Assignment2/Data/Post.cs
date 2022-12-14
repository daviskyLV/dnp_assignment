using System.ComponentModel.DataAnnotations;

namespace Assignment2.Data; 

public class Post {
    public string AuthorCookie { get; init; } = null!;
    public string Author { get; set; } = null!;
    public string Id { get; init; } = null!;
    [Required]
    public string Title { get; set; }
    [Required]
    public string Body { get; set; }
    
    /// <summary>
    /// Constructor for when the post already exists
    /// </summary>
    /// <param name="id">The id of the post</param>
    /// <param name="author">The author's username of the post</param>
    /// <param name="title">The title of the post</param>
    /// <param name="body">The main body of the post</param>
    /*public Post(string id, string author, string title, string body) {
        Id = id;
        Author = author;
        Title = title;
        Body = body;
    }*/

    /// <summary>
    /// Constructor for when the post is prepared as a POST request
    /// </summary>
    /// <param name="authorCookie">Cookie of the author's login</param>
    /// <param name="title">The title of the post</param>
    /// <param name="body">The main body of the post</param>
    /*public Post(string authorCookie, string title, string body) {
        Title = title;
        Body = body;
        AuthorCookie = authorCookie;
    }*/
}