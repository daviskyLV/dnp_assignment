using DNP1_Server.Exceptions;
using DNP1_Server.Utils;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DNP1_Server.Database; 

public class Database : IDatabase { 
    public Database() { }

    public async Task<User> CreateUserAsync(User user) {
        await using WebApiContext context = new WebApiContext();
        
        if (context.Users.Find(user.Username) != null)
            throw new DuplicateDataException("User already exists!");
        
        EntityEntry<User> entityAdded = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return entityAdded.Entity;
    }

    public async Task<User> GetUserInfoAsync(string username) {
        await using WebApiContext context = new WebApiContext();

        User? userObj = context.Users.Find(username);
        if (userObj == null)
            throw new NotFoundException("User not found");
        
        return userObj;
    }

    public async Task<Post> CreatePostAsync(string author, string title, string body) {
        await using WebApiContext context = new WebApiContext();

        User? authorObj = context.Users.Find(author);
        if (authorObj == null)
            throw new NotFoundException("Author not found!");

        // generating random post id
        Random rand = new Random();
        string postId = ""+rand.NextInt64();
        while (context.Posts.Find(postId) != null) {
            postId = ""+rand.NextInt64();
        }

        DbPost newPost = new DbPost(postId, authorObj, title, body);
        EntityEntry<DbPost> entityAdded = await context.Posts.AddAsync(newPost);
        await context.SaveChangesAsync();
        
        var entity = entityAdded.Entity;
        return new Post{Title = entity.Title, Body = entity.Body, Id = entity.Id, Author = entity.Author.Username};
    }

    public async Task<Post> GetPostAsync(string id) {
        await using WebApiContext context = new WebApiContext();
        
        DbPost? postObj = context.Posts.Find(id);
        if (postObj == null)
            throw new NotFoundException("Post id not found!");
        
        return new Post{Title = postObj.Title, Body = postObj.Body, Id = postObj.Id, Author = postObj.Author.Username};
    }

    public async Task<List<Post>> GetAllPostsAsync() {
        await using WebApiContext context = new WebApiContext();
        var postsEnumerable = context.Posts.GetAsyncEnumerator();
        var posts = new List<Post>();

        var cur = postsEnumerable.Current;
        // i assume first post is always there?
        posts.Add(new Post{Title = cur.Title, Body = cur.Body, Id = cur.Id, Author = cur.Author.Username});
        while (await postsEnumerable.MoveNextAsync()) {
            cur = postsEnumerable.Current;
            posts.Add(new Post{Title = cur.Title, Body = cur.Body, Id = cur.Id, Author = cur.Author.Username});
        }

        return posts;
    }
}