namespace Domain.MongoDB.Collections;

[BsonCollection("membercard")]
public class MemberCard : Document
{
    public string CardId { get; set; }
    public IdenCard IdenCard { get; set; }
    public DateTime RegisterDate { get; set; }
    public int Level { get; set; }
    public decimal ChipRollingTotal { get; set; }
    public decimal ChipDepositTotal { get; set; }
    public decimal ChipWithdrawTotal { get; set; }
    public decimal GameWithdrawTotal { get; set; }
}