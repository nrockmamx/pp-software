namespace Domain.Model;

public class PassportInfo
{
    public bool Used = true;
    public string Type { get; set; }
    public string Angle { get; set; }
    public string Mrtds { get; set; }
    public string DocumentNo { get; set; }
    public string PersonalNo { get; set; }
    public string Familyname { get; set; }
    public string Givenname { get; set; }
    public string Nationality { get; set; }
    public string Birthday { get; set; }
    public string Sex { get; set; }
    public string Dateofexpiry { get; set; }
    public string IssueState { get; set; }
    public string NativeName { get; set; }
}