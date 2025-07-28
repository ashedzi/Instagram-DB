using System;
using System.Collections.Generic;

namespace Instagram_DB.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public DateTime Timestamp { get; set; }

    public int Likes { get; set; }

    public string PostId { get; set; } = null!;

    public int CommenterUserId { get; set; }

    public int PosterUserId { get; set; }

    public string Content { get; set; } = null!;

    public virtual User CommenterUser { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;

    public virtual User PosterUser { get; set; } = null!;
}
