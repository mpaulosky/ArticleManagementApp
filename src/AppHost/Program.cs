var builder = DistributedApplication.CreateBuilder(args);

// Add MongoDB resource
var mongodb = builder.AddMongoDB("mongodb")
    .WithDataVolume()
    .AddDatabase("articledb", "ArticleManagementDb");

// Add Redis resource
var redis = builder.AddRedis("redis")
    .WithDataVolume();

// Add the Web project with dependencies
var web = builder.AddProject<Projects.Web>("web")
    .WithReference(mongodb)
    .WithReference(redis);

await builder.Build().RunAsync();
