using DNP1_Server.Database.Enums;
using DNP1_Server.Utils;

namespace DNP1_Server.Database; 

public interface IDatabase {
    (CreateUserEnum, User) CreateUser(string username, string password);
    (GetUserEnum, User) GetUserInfo(string username);
    (CreatePostEnum, Post) CreatePost(string author, string title, string body);
    (GetPostEnum, List<Post>) GetAllPosts();
    (GetPostEnum, Post) GetPost(long id);
}