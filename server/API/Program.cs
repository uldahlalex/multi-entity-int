using Infra;
using LinqToDB;

var builder = WebApplication.CreateBuilder(args);

var dataSource = "Data Source=db.db";
var options = new DataOptions().UseSQLite(dataSource);
var dataOptions = new DataOptions<MyDatabaseConnection>(options);

builder.Services.AddScoped<Seeder>();
builder.Services.AddScoped<MyDatabaseConnection>(_ => new MyDatabaseConnection(dataOptions));
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddScoped<LibraryService>();
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowed(_ => true));

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
    seeder.Seed();
}
app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi();
app.Run();