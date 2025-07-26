using System;
using System.Collections.Generic;

namespace Instagram_DB.Models;

public partial class Story
{
    public string StoryId { get; set; } = null!;

    public int Poster { get; set; }

    public int? Viewer { get; set; }

    public DateTime Timestamp { get; set; }

    public int? Views { get; set; }

    public string? Image { get; set; }

    public string? Caption { get; set; }

    public virtual User PosterNavigation { get; set; } = null!;

    public virtual ICollection<StoryLike> StoryLikes { get; set; } = new List<StoryLike>();

    public virtual User? ViewerNavigation { get; set; }
}
