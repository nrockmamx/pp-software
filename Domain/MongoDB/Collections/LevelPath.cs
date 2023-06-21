namespace Domain.MongoDB.Collections;

[BsonCollection("level_path")]

public class LevelPath  : Document
{
    public long Uid { get; set; }
    public List<PathClass> PathUpLine { get; set; } = new List<PathClass>();
    public int Level { get; set; }
    public string? Username { get; set; }
    public long? AgentId { get; set; }
    public long? RefUid { get; set; }
    public long? ChildTotalMemberCount { get; set; } = 0;
    public decimal? Score { get; set; } = 0;
    public string? Tel { get; set; }
    public string? LineId { get; set; }
    public string? ChatVipUrl { get; set; }
    public bool? ByPassPay { get; set; } = false;
    public string? PhoneNumber { get; set; }
}

public class PathClass
{
    public int Class { get; set; }
    public int Level { get; set; }
    public string? Username { get; set; }
    public long? Uid { get; set; }
    public long? AgentId { get; set; }
    public long? RefUid { get; set; }
    public long? ChildTotalMemberCount { get; set; } = 0;
    public decimal? Score { get; set; } = 0;
    public string? Tel { get; set; }
    public string? LineId { get; set; }
    public string? ChatVipUrl { get; set; }
    public bool? ByPassPay { get; set; } = false;
    public string? PhoneNumber { get; set; }
}