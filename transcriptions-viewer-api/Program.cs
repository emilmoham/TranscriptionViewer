
using Microsoft.EntityFrameworkCore;
using TranscriptionsViewerApi.Repositories;
using TranscriptionsViewerApi.Services;

namespace TranscriptionsViewerApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        
        // SQL
        string? connectionString = builder.Configuration.GetConnectionString("transcriptionsdb");
        builder.Services.AddDbContext<ApplicationContext>(x => x.UseNpgsql(connectionString));

        // Add Repositories
        builder.Services.AddScoped<ITranscriptionsRepository, TranscriptionsRepository>();

        // Add Services
        builder.Services.AddScoped<TranscriptionsService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }
}
