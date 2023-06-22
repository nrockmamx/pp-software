using Mongo.Migration.Migrations.Database;
using MongoDB.Driver;

namespace Domain.MongoDB.Migration.PacerLevelPercent;

public class Mg001 :  DatabaseMigration
{
    public Mg001(string version) : base("0.0.1")
    {
    }

    public override void Up(IMongoDatabase db)
    {
        var collection = db.GetCollection<Collections.Users>("users");
        collection.InsertOne(new Collections.Users()
        {
            Username = "admin",
            Password = "admin",
            Token = ""
        });

    }

    public override void Down(IMongoDatabase db)
    {
     
    }

}