namespace Domain.Model.Request;

public class PromotionCheckInRequest
{
    public PassportInfo PassportInfo { get; set; }
    public decimal Amount { get; set; }
    public string PassportImage { get; set; }
    public string CameraImage { get; set; }
}