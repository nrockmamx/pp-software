namespace Domain.MongoDB.Collections;

[BsonCollection("promotion_cutcard")]
public class PromotionCutCard : Document
{
    public string CardId { get; set; }
    public decimal Amount { get; set; }
    public string Dt { get; set; }
}