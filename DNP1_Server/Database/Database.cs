using System.Text.Json;
using DNP1_Server.Database.Enums;
using DNP1_Server.Utils;

namespace DNP1_Server.Database; 

public class Database : IDatabase {
    private readonly string _dbUsers;
    private readonly string _dbPosts;

    private Dictionary<string, User> _cachedUsers; // key is username
    private Dictionary<long, Post> _cachedPosts; // key is id

    public Database(string dbUsers, string dbPosts) {
        _dbUsers = dbUsers;
        _dbPosts = dbPosts;

        _cachedUsers = new Dictionary<string, User>();
        _cachedPosts = new Dictionary<long, Post>();
        
        // creating database files if they dont exist
        if (!File.Exists(_dbUsers))
            File.WriteAllText(_dbUsers, "[]");
        if (!File.Exists(_dbPosts))
            File.WriteAllText(_dbPosts, "[]");
        
        // loading data into cache
        using (StreamReader r = new StreamReader(dbUsers)) {
            string json = r.ReadToEnd();
            var users = JsonSerializer.Deserialize<List<User>>(json);
            foreach (var u in users) {
                _cachedUsers.Add(u.GetUsername(), u);
            }
        }
        
        using (StreamReader r = new StreamReader(dbPosts)) {
            string json = r.ReadToEnd();
            var posts = JsonSerializer.Deserialize<List<Post>>(json);
            foreach (var p in posts) {
                _cachedPosts.Add(p.Id, p);
            }
        }
    }

    private void saveUsers() {
        var json = JsonSerializer.Serialize(_cachedUsers.Values);
        File.WriteAllText(_dbUsers, json);
    }
    
    private void savePosts() {
        var json = JsonSerializer.Serialize(_cachedPosts.Values);
        File.WriteAllText(_dbPosts, json);
    }

    public (CreateUserEnum, User?) CreateUser(string username, string password) {
        if (_cachedUsers.ContainsKey(username))
            return (CreateUserEnum.AlreadyExists, null);

        var user = new User(username) {Password = password};
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
        while (_cachedPosts.ContainsKey(id)) {
            id = rand.NextInt64();
        }
        
        // adding and saving post
        var post = new Post(id, author, title, body);
        _cachedPosts.Add(id, post);
        savePosts();
        return (CreatePostEnum.Success, post);
    }

    public (GetPostEnum, Post?) GetPost(long id) {
        if (!_cachedPosts.ContainsKey(id))
            return (GetPostEnum.NotFound, null);

        return (GetPostEnum.Success, _cachedPosts[id]);
    }

    public (GetPostEnum, List<Post>?) GetAllPosts() {
        return (GetPostEnum.Success, new List<Post>(_cachedPosts.Values));
    }
}