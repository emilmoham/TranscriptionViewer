
using Microsoft.EntityFrameworkCore;
using TranscriptionsViewerApi.Repositories;
using TranscriptionsViewerApi.Seeders;
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

        // Configure CORS
        builder.Services.AddCors(p => p.AddDefaultPolicy(builder => {
            builder
                .AllowAnyOrigin() // TODO: lock this down once we're closer to beta
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
        
        // SQL
        string? connectionString = builder.Configuration.GetConnectionString("transcriptionsdb");
        builder.Services.AddDbContext<ApplicationContext>(x => x.UseNpgsql(connectionString));

        // Add Repositories
        builder.Services.AddScoped<ITranscriptionsRepository, TranscriptionsRepository>();

        // Add Services
        builder.Services.AddScoped<TranscriptionsService>();

        

        var app = builder.Build();


        if (args.Contains("--seed") || args.Contains("-s")) {
          Console.WriteLine("Running seeders...");

          // Ensure migrations are run
          using (var scope = app.Services.CreateScope())
          {
            ApplicationContext context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            context.Database.Migrate();
          
          
            // Add test data
            ITranscriptionsRepository transcriptionsRepository = scope.ServiceProvider.GetRequiredService<ITranscriptionsRepository>();
            MeetingSeeder meetingSeeder = new MeetingSeeder(transcriptionsRepository);
            meetingSeeder.SeedAsync().Wait();
          }

          return;
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors();
        app.MapControllers();
        app.Run();
    }
}
