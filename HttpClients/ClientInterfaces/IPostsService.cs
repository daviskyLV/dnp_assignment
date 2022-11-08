using DNP1_Server.Controllers.ApiClasses;
using DNP1_Server.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HttpCllients.ClientInterfaces;

public interface IPostsService
{
    public Task<ActionResult<Post>> CreatePost([FromBody] ApiPost post);
}