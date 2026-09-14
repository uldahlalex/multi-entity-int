using Infra;
using LinqToDB;

var builder = WebApplication.CreateBuilder(args);

var dataSource = "Data Source=db.db";
var options = new DataOptions().UseSQLite(dataSource);
var dataOptions = new DataOptions<MyDatabaseConnection>(options);

builder.Services.AddScoped<MyDatabaseConnection>(_ => new MyDatabaseConnection(dataOptions));
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddScoped<LibraryService>();
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowed(_ => true));

using (var scope = app.Services.CreateScope())
{
    var connectionToDb = scope.ServiceProvider.GetRequiredService<MyDatabaseConnection>();
    connectionToDb.CreateTable<Book>(tableOptions:TableOptions.CreateIfNotExists);
    connectionToDb.CreateTable<Author>(tableOptions:TableOptions.CreateIfNotExists);
    if (connectionToDb.Authors.Count() == 0)
    {
        connectionToDb.Insert(new Author()
        {
            AuthorId = "1",
            AuthorName = "Bob"
        });
    }
    
    if (connectionToDb.Books.Count() == 0)
    {
        connectionToDb.Insert(new Book()
        {
            BookId = "1",
            BookTitle = "Bobs book",
            AuthorId = "1"
        });
    }
}

app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi();

app.Run();
