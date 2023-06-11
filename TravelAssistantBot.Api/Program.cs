using TravelAssistantBot.Core;
using TravelAssistantBot.Core.EventManager;

var builder = WebApplication.CreateBuilder(args);
var allowAllOriginsPolicy = "_allowAllOriginsPolicy";

// Add services to the container
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddAutoMapper(typeof(Program));
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(allowAllOriginsPolicy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
