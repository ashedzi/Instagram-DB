using System;
using System.Collections.Generic;

namespace Instagram_DB.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Bio { get; set; }

    public string? ProfilePic { get; set; }

    public string Status { get; set; } = null!;

    public string? PostId { get; set; }

    public int? CommentId { get; set; }

    public int Followers { get; set; }

    public int Following { get; set; }

    public virtual ICollection<Comment> CommentCommenterUsers { get; set; } = new List<Comment>();

    public virtual ICollection<Comment> CommentPosterUsers { get; set; } = new List<Comment>();

    public virtual ICollection<DirectMessage> DirectMessageReceiverUsers { get; set; } = new List<DirectMessage>();

    public virtual ICollection<DirectMessage> DirectMessageSenderUsers { get; set; } = new List<DirectMessage>();

    public virtual ICollection<Like> LikeLikerUsers { get; set; } = new List<Like>();

    public virtual ICollection<Like> LikePosterUsers { get; set; } = new List<Like>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<StoryLike> StoryLikes { get; set; } = new List<StoryLike>();

    public virtual ICollection<Story> StoryPosterNavigations { get; set; } = new List<Story>();

    public virtual ICollection<Story> StoryViewerNavigations { get; set; } = new List<Story>();

    public virtual ICollection<User> FollowerUsers { get; set; } = new List<User>();

    public virtual ICollection<User> FollowingUsers { get; set; } = new List<User>();
}
