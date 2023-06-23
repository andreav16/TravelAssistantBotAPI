using TravelAssistantBot.Core;
using TravelAssistantBot.Core.ConversationalLanguageInterpreter;
using TravelAssistantBot.Core.EventManager;
using TravelAssistantBot.Core.Options;
using TravelAssistantBot.Core.GeopifyManager;
using Microsoft.EntityFrameworkCore;
using TravelAssistantBot.Infrastructure.EntityFramework;
using TravelAssistantBot.Core.FlightManager;
using TravelAssistantBot.Infrastructure.EntityFramework.Repositories;

var builder = WebApplication.CreateBuilder(args);
var allowAllOriginsPolicy = "_allowAllOriginsPolicy";

// Add services to the container
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<TravelAssistantBotContext>(options => options.UseSqlite("DataSource=Tflights_info.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IFlightService, FlightsService>();
builder.Services.AddScoped<IGeopifyServices, GeopifyService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.Configure<GeopifyOptions_Geocode>(builder.Configuration.GetSection("Geopify_GeocodeAPI"));
builder.Services.Configure<GeopifyOptions_Places>(builder.Configuration.GetSection("Geopify_PlacesAPI"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<LanguageInterpreterOptions>(builder.Configuration.GetSection(LanguageInterpreterOptions.ConfigurationKey));
builder.Services.AddHttpClient<ILanguageInterpreter, LanguageInterpreter>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAllOriginsPolicy,
                      policy =>
                      {
                          policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                      });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(allowAllOriginsPolicy);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();