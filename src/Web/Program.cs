using Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations
builder.AddServiceDefaults();

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add MongoDB client (will be configured by Aspire)
builder.AddMongoDBClient("mongodb");

// Add Redis (will be configured by Aspire)
builder.AddRedisClient("redis");

// Add data access services (MongoDB client will be resolved from DI)
builder.Services.AddScoped<IArticleRepository>(sp => 
    new ArticleRepository(sp.GetRequiredService<IMongoClient>(), "ArticleManagementDb"));
builder.Services.AddScoped<ICategoryRepository>(sp => 
    new CategoryRepository(sp.GetRequiredService<IMongoClient>(), "ArticleManagementDb"));

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapDefaultEndpoints();

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
