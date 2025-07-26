using System;
using System.Collections.Generic;

namespace Instagram_DB.Models;

public partial class Like
{
    public string PostId { get; set; } = null!;

    public int PosterUserId { get; set; }

    public int LikerUserId { get; set; }

    public virtual User LikerUser { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;

    public virtual User PosterUser { get; set; } = null!;
}
