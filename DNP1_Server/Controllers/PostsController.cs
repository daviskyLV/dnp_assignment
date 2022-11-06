using DNP1_Server.Controllers.ApiClasses;
using DNP1_Server.Database.Enums;
using DNP1_Server.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DNP1_Server.Controllers; 

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase {
    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost([FromBody] ApiPost post) {
        /// TODO: Check if user is logged in
        
        var dbResponse = Program.Database.CreatePost(post.Author, post.Title, post.Body);
        return dbResponse.Item1 switch {
            CreatePostEnum.Success => dbResponse.Item2,
            CreatePostEnum.AuthorNotFound => StatusCode(400, "Author not found"),
            CreatePostEnum.InternalError => StatusCode(500, "Database error"),
            _ => StatusCode(500, "Unknown server error")
        };
    }

    [HttpGet]
    public async Task<ActionResult<List<Post>>> GetAllPosts() {
        var dbResponse = Program.Database.GetAllPosts();
        return dbResponse.Item1 switch {
            GetPostEnum.Success => dbResponse.Item2,
            GetPostEnum.InternalError => StatusCode(500, "Database error"),
            _ => StatusCode(500, "Unknown server error")
        };
    }
    
    [HttpGet]
    [Route("{id:long}")]
    public async Task<ActionResult<Post>> GetPost([FromRoute] long id ) {
        var dbResponse = Program.Database.GetPost(id);
        return dbResponse.Item1 switch {
            GetPostEnum.Success => dbResponse.Item2,
            GetPostEnum.NotFound => StatusCode(404, "Post id not found"),
            GetPostEnum.InternalError => StatusCode(500, "Database error"),
            _ => StatusCode(500, "Unknown server error")
        };
    }
}