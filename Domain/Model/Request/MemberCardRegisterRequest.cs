namespace Domain.Model.Request;

public class MemberCardRegisterRequest
{
    public CardIdenDetail CardIden { get; set; }
    public string NickName { get; set; }
    public string CardId { get; set; }
    public string Tel { get; set; }
    public string From { get; set; }
    public class CardIdenDetail
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
}

