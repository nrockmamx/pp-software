namespace Domain.Model.Request;

public class PromotionCheckInRequest
{
    public string Ssid { get; set; }
    public string NameTh { get; set; }
    public string NameEng { get; set; }
    public string AddressTh { get; set; }
    public string AddressEng { get; set; }
    public string Tel { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PersonalImage { get; set; }
    public string PassportImage { get; set; }
    public decimal Amount { get; set; }
}