using DNP1_Server.Controllers.ApiClasses;
using Microsoft.AspNetCore.Mvc;
    
namespace Assignment2.Data.ClientInterfaces;

public interface IPostService
{
    public Task<Post> CreatePost(Post submittedPost);
    public Task<List<Post>> GetAllPosts();
    public Task<Post> GetPost(long id);
    
}