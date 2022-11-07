using System.Text.Json;
using DNP1_Server.Database.Enums;
using DNP1_Server.Utils;

namespace DNP1_Server.Database; 

public class Database : IDatabase {
    private readonly string _dbUsers;
    private readonly string _dbPosts;
    private readonly IDataAccess _dbAccess;

    private Dictionary<string, User> _cachedUsers; // key is username
    private Dictionary<string, Post> _cachedPosts; // key is id

    public Database(string dbUsers, string dbPosts) {
        _dbUsers = dbUsers;
        _dbPosts = dbPosts;
        _dbAccess = new DataAccess();

        _cachedUsers = new Dictionary<string, User>();
        _cachedPosts = new Dictionary<string, Post>();
        
        // loading data into cache
        var users = JsonSerializer.Deserialize<List<User>>(_dbAccess.LoadJsonData(dbUsers));
        foreach (var u in users) {
            if (u.Username != null)
                _cachedUsers.Add(u.Username, u);
        }
        
        var posts = JsonSerializer.Deserialize<List<Post>>(_dbAccess.LoadJsonData(dbPosts));
        foreach (var p in posts) {
            if (p.Id != null)
                _cachedPosts.Add(p.Id, p);
        }
    }

    private void saveUsers() {
        var json = JsonSerializer.Serialize(_cachedUsers.Values);
        _dbAccess.SaveData(_dbUsers, json);
    }
    
    private void savePosts() {
        var json = JsonSerializer.Serialize(_cachedPosts.Values);
        _dbAccess.SaveData(_dbPosts, json);
    }

    public (CreateUserEnum, User?) CreateUser(string username, string password) {
        if (_cachedUsers.ContainsKey(username))
            return (CreateUserEnum.AlreadyExists, null);

        var user = new User {Username = username, Password = password};
        _cachedUsers.Add(username, user);
        saveUsers();
        return (CreateUserEnum.Success, user);
    }

    public (GetUserEnum, User?) GetUserInfo(string username) {
        if (!_cachedUsers.ContainsKey(username))
            return (GetUserEnum.NotFound, null);

        return (GetUserEnum.Success, _cachedUsers[username]);
    }

    public (CreatePostEnum, Post?) CreatePost(string author, string title, string body) {
        if (!_cachedUsers.ContainsKey(author))
            return (CreatePostEnum.AuthorNotFound, null);

        // generating random post id
        long id = 0;
        var rand = new Random();
        while (_cachedPosts.ContainsKey(""+id)) {
            id = rand.NextInt64();
        }
        
        // adding and saving post
        var post = new Post{Id = ""+id, Author = author, Title = title, Body = body};
        _cachedPosts.Add(""+id, post);
        savePosts();
        return (CreatePostEnum.Success, post);
    }

    public (GetPostEnum, Post?) GetPost(string id) {
        if (!_cachedPosts.ContainsKey(id))
            return (GetPostEnum.NotFound, null);

        return (GetPostEnum.Success, _cachedPosts[id]);
    }

    public (GetPostEnum, List<Post>?) GetAllPosts() {
        return (GetPostEnum.Success, new List<Post>(_cachedPosts.Values));
    }
}