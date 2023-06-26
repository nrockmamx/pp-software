namespace Domain.MongoDB.Collections;

[BsonCollection("membercard_generate")]
public class MemberCardGenerate : Document
{
    public string CardId { get; set; }
    public bool Used { get; set; } = false;
}