namespace Domain.MongoDB.Collections;

[BsonCollection("promotion_luckydraw")]
public class PromotionLuckyDraw
{
    public string CardId { get; set; }
    public decimal Amount { get; set; }
    public string Dt { get; set; }
}