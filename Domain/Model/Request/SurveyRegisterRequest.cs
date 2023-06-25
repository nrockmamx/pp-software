namespace Domain.Model.Request;

public class SurveyRegisterRequest
{
    public string NickName { get; set; }
    public string Tel { get; set; }
    public string Province { get; set; }
    public string LineId { get; set; }
    public string Travel { get; set; }
    public List<string> Like { get; set; }
    public decimal Budget { get; set; }
}