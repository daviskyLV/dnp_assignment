

namespace DNP1_Server.Models;

public class Post
{
    public int Id { get; set; }
    public User Owner { get; }
    public string Title { get; }
    public bool IsCompleted { get; }
    

    public Post(User owner, string title)
    {
        Owner = owner;
        Title = title;
    }
}