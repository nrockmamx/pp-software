using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
    public class PromotionCheckInResponse
    {
        public string Id { get; set; }
        public PassportInfo PassportInfo { get; set; }
        public string PersonalNo { get; set; }
        public DateTime CheckInTime { get; set; }
        public string PassportImage { get; set; }
        public string CameraImage { get; set; }
        public string PhoneNumber { get; set; }
        public string Dt { get; set; }
        public Decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
