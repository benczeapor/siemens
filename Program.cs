using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//builder.Services.AddScoped<IItemReader, ItemRepository>();
builder.Services.AddScoped<IItemStatisticsService, ItemStatisticsService>();

builder.Services.AddHttpClient<IItemReader, ItemRepository>(client =>
{
    client.BaseAddress = new Uri("https://gist.githubusercontent.com/");
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();