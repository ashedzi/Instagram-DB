using System;
using System.Collections.Generic;

namespace Instagram_DB.Models;

public partial class Follower
{
    public int FollowId { get; set; }

    public int FollowerUserId { get; set; }

    public int FollowingUserId { get; set; }
}
