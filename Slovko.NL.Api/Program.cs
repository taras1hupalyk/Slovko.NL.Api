using Slovko.NL.Api.Models;
using Newtonsoft.Json;
using Slovko.NL.Api.Services;
using Slovko.NL.Api.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<WordsService>();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x => x
    .AllowAnyOrigin()
       .AllowAnyMethod()
          .AllowAnyHeader());


app.UseHttpsRedirection();


// add new  router for api
app.MapGet("/", (WordsService wordsService) => wordsService.GetWords());

app.MapGet("/api/words",  (WordsService wordsService) =>  wordsService.GetWords());


//resolve post request
app.MapPost("/api/words/filtered", async  (WordsService wordsService, HttpRequest request) =>
{
    var body = await new StreamReader(request.Body).ReadToEndAsync();
    var filter = JsonConvert.DeserializeObject<LetterGroup[]>(body);  

    return await wordsService.ApplyFilter(filter);
});


app.Run();
