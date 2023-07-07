using Domain.Model;
using MongoDB.Bson;

namespace Domain.MongoDB.Collections;

[BsonCollection("promotion_check_in")]
public class PromotionCheckIn : Document
{
    public PassportInfo PassportInfo { get; set; }
    public string PersonalNo { get; set; }
    public DateTime CheckInTime { get; set; }
    public string PassportImage { get; set; }
    public string CameraImage { get; set; }
    public string PhoneNumber { get; set; }
    public string Dt { get; set; }
    public Decimal Amount { get; set; }
}