using System;
using System.Collections.Generic;

namespace Instagram_DB.Models;

public partial class StoryLike
{
    public string StoryId { get; set; } = null!;

    public int Liker { get; set; }

    public DateTime Timestamp { get; set; }

    public virtual User LikerNavigation { get; set; } = null!;

    public virtual Story Story { get; set; } = null!;
}
