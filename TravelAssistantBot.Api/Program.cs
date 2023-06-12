using TravelAssistantBot.Core;
using TravelAssistantBot.Core.ConversationalLanguageInterpreter;
using TravelAssistantBot.Core.EventManager;
using TravelAssistantBot.Core.Options;
using TravelAssistantBot.Core.GeopifyManager;

var builder = WebApplication.CreateBuilder(args);
var allowAllOriginsPolicy = "_allowAllOriginsPolicy";

// Add services to the container
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAllOriginsPolicy,
                      policy =>
                      {
                          policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                      });
});
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IGeopifyServices, GeopifyService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<LanguageInterpreterOptions>(builder.Configuration.GetSection(LanguageInterpreterOptions.ConfigurationKey));
builder.Services.AddHttpClient<ILanguageInterpreter, LanguageInterpreter>();


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