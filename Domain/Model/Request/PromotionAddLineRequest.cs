namespace Domain.Model.Request;

public class PromotionAddLineRequest
{
    public PassportInfo PassportInfo { get; set; }
    public decimal Amount { get; set; }
    public string PassportImage { get; set; }
    public string PhoneNumber { get; set; } 
    public string CameraImage { get; set; }
}