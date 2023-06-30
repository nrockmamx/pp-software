namespace Domain.MongoDB.Collections;

[BsonCollection("iden_card")]
public class IdenCard  : Document
{
    public string Ssid { get; set; }
    public string NameTh { get; set; }
    public string NameEng { get; set; }
    public string Address { get; set; }
    public string Tel { get; set; }
    public string Sex { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PersonalImage { get; set; }
    public string PassportImage { get; set; }
}