using System;
using System.Collections.Generic;

namespace Instagram_DB.Models;

public partial class DirectMessage
{
    public int MessageId { get; set; }

    public int SenderUserId { get; set; }

    public int ReceiverUserId { get; set; }

    public DateTime Timestamp { get; set; }

    public string TextContent { get; set; } = null!;

    public virtual User ReceiverUser { get; set; } = null!;

    public virtual User SenderUser { get; set; } = null!;
}
