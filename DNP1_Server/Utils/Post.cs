namespace DNP1_Server.Utils; 

public class Post {
    public readonly long Id;
    public readonly string Author;
    public readonly string Title;
    public readonly string Body;
    
    public Post(long id, string author, string title, string body) {
        Id = id;
        Author = author;
        Title = title;
        Body = body;
    }
}