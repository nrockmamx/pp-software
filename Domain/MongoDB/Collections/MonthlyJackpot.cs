namespace Domain.MongoDB.Collections;

[BsonCollection("mpnthly_jackpot")]
public class MonthlyJackpot : Document
{
    public long Total { get; set; }
    public long Number1 { get; set; }
    public long Number2 { get; set; }
    public long Number3 { get; set; }
    public long Number4 { get; set; }
    public long Number5 { get; set; }
    public long Number6 { get; set; }
    public long Number7to20 { get; set; }
    public long Number21to29 { get; set; }
}