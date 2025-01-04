using Mapster;
using Scalar.AspNetCore;
using TimeTracker.API.Data;
using TimeTracker.Shared.Models.Project;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Get the connection string from the environment variable
var connectionString = Environment.GetEnvironmentVariable("TimeTrackerDefaultConnection");

// Fallback to appsettings.json if the environment variable is not set
if (string.IsNullOrEmpty(connectionString))
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();
builder.Services.AddScoped<ITimeEntryService, TimeEntryService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

var app = builder.Build();

// Automatically apply migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ConfigureMapster();

app.Run();

void ConfigureMapster()
{
    TypeAdapterConfig<Project, ProjectResponse>.NewConfig()
                                               .Map(dest => dest.Description, src => src.ProjectDetails != null ? src.ProjectDetails.Description : string.Empty)
                                               .Map(dest => dest.StartDate, src => src.ProjectDetails != null ? src.ProjectDetails.StartDate : null)
                                               .Map(dest => dest.EndDate, src => src.ProjectDetails != null ? src.ProjectDetails.EndDate : null);

    TypeAdapterConfig<ProjectCreateRequest, Project>.NewConfig()
                                                    .Map(dest => dest.ProjectDetails, src => src.Adapt<ProjectDetails>());

    TypeAdapterConfig<ProjectUpdateRequest, Project>.NewConfig()
                                                    .Map(dest => dest.ProjectDetails, src => src.Adapt<ProjectDetails>());
}