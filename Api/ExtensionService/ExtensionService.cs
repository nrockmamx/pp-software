using Domain.Command;
using Domain.Handler;
using Domain.Model;
using Domain.MongoDB.Collections;
using Domain.Repository;
using Infrastruceture.Environments;
using Infrastruceture.Repository;
using MediatR;
using Mongo.Migration.Startup;
using Mongo.Migration.Startup.DotNetCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Serilog;

namespace Api.ExtensionService;

public static class ExtensionService
{
    public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        // convert property to camel case
        var conventionPack = new  ConventionPack {new CamelCaseElementNameConvention()};
        ConventionRegistry.Register("camelCase", conventionPack, t => true);

        // convert enum value to string
        var enumConventionPick = new ConventionPack{new EnumRepresentationConvention(BsonType.String)};
        ConventionRegistry.Register("EnumStringConvention", enumConventionPick, t => true);

        var environmentsConfig = new EnvironmentsConfig(configuration);
        var connectionString = environmentsConfig.GetConnectionString("MongoDb");

        if (string.IsNullOrEmpty(connectionString))
        {
            Log.Error($"MongoDb Conn:{connectionString}");
            Environment.Exit(0);
        }
        services.AddMigration(new MongoMigrationSettings()
        {
            ConnectionString =connectionString,
            Database = "pp-app"
        });
            
        services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(connectionString));
        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
    }

    public static void AddCommandHandler(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<AccessTokenGetCommand, ModelResponse>, AccessTokenGetCommandHandler>();
        services.AddTransient<IRequestHandler<PromotionCheckInCommand, ModelResponse>, PromotionCheckInCommandHandler>();
        services.AddTransient<IRequestHandler<PromotionCheckInCheckCommand, ModelResponse>, PromotionCheckInCheckCommandHandler>();
        services.AddTransient<IRequestHandler<PromotionCheckInHistoryBySsidCommand, ModelResponse>, PromotionCheckInHistoryBySsidCommandHandler>();
        services.AddTransient<IRequestHandler<PromotionCheckInHistoryCommand, ModelResponse>, PromotionCheckinHistoryCommandHandler>();
        services.AddTransient<IRequestHandler<SurveyRegisterCommand, ModelResponse>,SurveyRegisterCommandHandler>();
        services.AddTransient<IRequestHandler<MemberCardGenCommand, ModelResponse>,MemberCardGenCommandHandler>();
        services.AddTransient<IRequestHandler<MemberCardRegisterCommand, ModelResponse>,MemberCardRegisterCommandHandler>();
        services.AddTransient<IRequestHandler<MemberCardPrintedCommand, ModelResponse>,MemberCardPrintedCommandHandler>();
        services.AddTransient<IRequestHandler<MemberCardFindCommand, ModelResponse>,MemberCardFindCommandHandler>();
        services.AddTransient<IRequestHandler<MonthlyJackpotGetCommand, ModelResponse>,MonthlyJackpotGetCommandHandler>();
        services.AddTransient<IRequestHandler<MonthlyJackpotUpdateCommand, ModelResponse>,MonthlyJackpotUpdateCommandHandler>();
    }

    internal static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        EnvironmentsConfig env = new EnvironmentsConfig(configuration);
        string conn = env.GetValue<string>("RedisConfig");
        string insName = env.GetValue<string>("RedisService");

        services.AddDistributedRedisCache(option =>
        {
            option.Configuration = conn;
            option.InstanceName = $"{insName}:";
        });
    }
}