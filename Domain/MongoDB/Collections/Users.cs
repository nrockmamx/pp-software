namespace Domain.MongoDB.Collections;

[BsonCollection("users")]

public class Users  : Document
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }
}
