namespace Domain.MongoDB.Collections;

[BsonCollection("membercard")]
public class MemberCard : Document
{
    public string CardId { get; set; }
    public string NickName { get; set; }
    public string Tel { get; set; }
    public IdenCard IdenCard { get; set; }
    public DateTime RegisterDate { get; set; }
    public int Level { get; set; } = 1;
    public decimal ChipRollingTotal { get; set; } = 0;
    public decimal ChipDepositTotal { get; set; }  = 0;
    public decimal ChipWithdrawTotal { get; set; }  = 0;
    public decimal GameWithdrawTotal { get; set; }  = 0;
}