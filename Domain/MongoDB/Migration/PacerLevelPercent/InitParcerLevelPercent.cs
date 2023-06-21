using Mongo.Migration.Migrations.Database;
using MongoDB.Driver;

namespace Domain.MongoDB.Migration.PacerLevelPercent;

public class InitParcerLevelPercent :  DatabaseMigration
{
    public InitParcerLevelPercent(string version) : base("0.0.1")
    {
    }

    public override void Up(IMongoDatabase db)
    {
        /*var collection = db.GetCollection<Collections.PacerLevelPercent>("pacer_level_percent");
        collection.InsertOne(new Collections.PacerLevelPercent
        {
            Level = 1,
            GamePercent = 8,
            LottoPercent = 8,
            DownLine2500 = 0,
            MaxIncomePerDay = 5000,
            Salary = 0,
        })*/
   
    }

    public override void Down(IMongoDatabase db)
    {
     
    }

}