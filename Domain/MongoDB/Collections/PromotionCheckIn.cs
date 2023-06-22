using MongoDB.Bson;

namespace Domain.MongoDB.Collections;

[BsonCollection("promotion_check_in")]
public class PromotionCheckIn : Document
{
    public IdenCard IdenCard { get; set; }
    public DateTime CheckInTime { get; set; }
    public string PassportImage { get; set; }
    public string Dt { get; set; }
    public Decimal Amount { get; set; }
}