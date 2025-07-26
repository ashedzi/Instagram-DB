using System;
using System.Collections.Generic;

namespace Instagram_DB.Models;

public partial class Post
{
    public string PostId { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public string Image { get; set; } = null!;

    public string? Caption { get; set; }

    public int Likes { get; set; }

    public int Saves { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Like> LikesNavigation { get; set; } = new List<Like>();

    public virtual User User { get; set; } = null!;
}
