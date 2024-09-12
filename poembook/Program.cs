using poembook.Data;
using poembook.Models;
using poembook.Repository;
using poembook.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var mongoSettings = builder.Configuration.GetSection("MongoDB");

var connectionString = mongoSettings["ConnectionString"]
    ?? throw new ArgumentNullException("MongoDB connection string is not provided.");

var databaseName = mongoSettings["DatabaseName"]
    ?? throw new ArgumentNullException("MongoDB database name is not provided.");

builder.Services.AddSingleton<IMongoDbContext>(sp => new MongoDbContext(connectionString, databaseName));
builder.Services.AddScoped<IGenericRepo<PoemModel>>(sp =>
{
    var dbContext = sp.GetRequiredService<IMongoDbContext>();
    return new GenericRepo<PoemModel>(dbContext, "Poems"); // "Poems" is the collection name
});

builder.Services.AddScoped<IPoemService, PoemService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
