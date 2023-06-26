namespace Domain.MongoDB.Collections;

[BsonCollection("game_withdraw")]
public class GameWithdraw : Document
{
    public string CardId { get; set; }
    public string Name { get; set; }
    public bool NoCard { get; set; } = false;
    public decimal Amount { get; set; }
    public decimal GameType { get; set; }
    public string Dt { get; set; }
}