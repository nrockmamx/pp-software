namespace Domain.MongoDB.Collections;

[BsonCollection("promotion_birthday")]
public class PromotionBirthDay : Document
{
    public string CardId { get; set; }
    public decimal Amount { get; set; }
    public string Dt { get; set; }
}