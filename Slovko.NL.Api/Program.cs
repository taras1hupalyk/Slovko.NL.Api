using Microsoft.EntityFrameworkCore;
using Slovko.NL.Api.Models;
using Newtonsoft.Json;
using Slovko.NL.Api.Services;
using Slovko.NL.Api.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//add database context for postgre sql 

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<WordsService>();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
       .AllowAnyMethod()
          .AllowAnyHeader());

// add new  router for api
app.MapGet("/", () => "Hi");

app.MapGet("/api/words", async (WordsService wordsService) => await wordsService.GetWords());


app.MapGet("/some", () => "some");


//resolve post request
app.MapPost("/api/words/filtered", async  (WordsService wordsService, HttpRequest request) =>
{
    var body = await new StreamReader(request.Body).ReadToEndAsync();
    var filter = JsonConvert.DeserializeObject<LetterGroup[]>(body);  

    return await wordsService.ApplyFilter(filter);
});


app.Run();


