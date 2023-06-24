namespace Domain.MongoDB.Collections;

[BsonCollection("survey")]
public class Survey : Document
{
    public string NickName { get; set; }
    public string Tel { get; set; }
    public string Province { get; set; }
    public string LineId { get; set; }
    public List<string> Like { get; set; }
    public decimal Buget { get; set; }
}