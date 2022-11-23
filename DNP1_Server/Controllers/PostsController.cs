using DNP1_Server.Controllers.ApiClasses;
using DNP1_Server.Exceptions;
using DNP1_Server.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DNP1_Server.Controllers; 

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase {
    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost([FromBody] ApiPost post) {
        try {
            var username = Program.LoginLogic.UsernameFromCookie(post.AuthorCookie);
            var dbResponse = await Program.Database.CreatePostAsync(
                username, post.Title, post.Body
            );

            return dbResponse;
        } catch (NotFoundException e) {
            return StatusCode(401, e.Message);
        } catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<Post>>> GetAllPosts() {
        try {
            var posts = await Program.Database.GetAllPostsAsync();
            return posts;
        } catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("{id:long}")]
    public async Task<ActionResult<Post>> GetPost([FromRoute] long id ) {
        try {
            var post = await Program.Database.GetPostAsync(id+"");
            return post;
        } catch (NotFoundException e) {
            return StatusCode(404, e.Message);
        } catch (Exception e) {
            return StatusCode(500, e.Message);
        }
    }
}