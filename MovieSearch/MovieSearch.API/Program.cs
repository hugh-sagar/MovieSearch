using Microsoft.EntityFrameworkCore;
using MovieSearch.API.Services.ErrorReporting;
using MovieSearch.API.Services.Movie;
using MovieSearch.DAL;
using MovieSearch.DAL.Repositories.Movies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieSearchDatabaseContext>(optionsAction => optionsAction.UseSqlite("Data Source=.\\mymoviedb.db"));
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IErrorReportingService, FakeErrorReportingService>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
