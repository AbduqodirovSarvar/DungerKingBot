using Dunger.Application;
using Dunger.Application.Models;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ApplicationServices(builder.Configuration);
builder.Services.AddControllers();//.AddNewtonsoftJson();

builder.Services.Configure<BotConfiguration>(builder.Configuration.GetSection("BotConfig"));

builder.Services.AddHttpClient("tgwebhook").AddTypedClient<ITelegramBotClient>(client =>
                new TelegramBotClient(builder.Configuration.GetSection("BotConfig:Token").Value, client));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    var token = builder.Configuration.GetSection("BotConfig:Token").Value;
    endpoints.MapControllerRoute(name: "tgwebhook",
        pattern: $"bot/{token}",
        new { controller = "WebHook", action = "Post" });

    endpoints.MapControllers();
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
