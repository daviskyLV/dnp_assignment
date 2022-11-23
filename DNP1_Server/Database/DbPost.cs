using System.ComponentModel.DataAnnotations;
using DNP1_Server.Utils;

namespace DNP1_Server.Database; 

/// <summary>
/// Used exclusively for database
/// </summary>
public class DbPost {
	[Key]
	[Required]
	public string Id { get; set; }
	[Required]
	public User Author { get; set; }
	[Required]
	public string Title { get; set; }
	[Required]
	public string Body { get; set; }

	public DbPost(string id, User user, string title, string body) {
		Id = id;
		Author = user;
		Title = title;
		Body = body;
	}
	
	private DbPost() {}
}