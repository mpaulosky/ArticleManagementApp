using Web.Components;
using Web.Components.Articles.Handlers;
using Web.Components.Categories.Handlers;

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

// Register validators
builder.Services.AddScoped<IValidator<Article>, ArticleValidator>();
builder.Services.AddScoped<IValidator<Category>, CategoryValidator>();

// Register Article handlers
builder.Services.AddScoped<CreateArticleHandler>();
builder.Services.AddScoped<UpdateArticleHandler>();
builder.Services.AddScoped<DeleteArticleHandler>();
builder.Services.AddScoped<GetAllArticlesHandler>();
builder.Services.AddScoped<GetArticleByIdHandler>();
builder.Services.AddScoped<SearchArticlesHandler>();

// Register Category handlers
builder.Services.AddScoped<CreateCategoryHandler>();
builder.Services.AddScoped<UpdateCategoryHandler>();
builder.Services.AddScoped<DeleteCategoryHandler>();
builder.Services.AddScoped<GetAllCategoriesHandler>();
builder.Services.AddScoped<GetCategoryByIdHandler>();
builder.Services.AddScoped<GetRootCategoriesHandler>();
builder.Services.AddScoped<GetSubcategoriesHandler>();

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
