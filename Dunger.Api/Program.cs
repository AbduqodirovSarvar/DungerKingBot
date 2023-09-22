using Dunger.Api.Controllers;
using Dunger.Application;
using Dunger.Application.Models;
using Dunger.Infrastructure;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.BotServices();
builder.Services.InfrastructureServices(builder.Configuration);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.Configure<BotConfiguration>(builder.Configuration.GetSection(BotConfiguration.Configuration));

builder.Services.AddHttpClient("webhook")
                .AddTypedClient<ITelegramBotClient>((client, sp) =>
{
    BotConfiguration? botConfig = sp.GetRequiredService<IConfiguration>().GetSection(BotConfiguration.Configuration).Get<BotConfiguration>();
    TelegramBotClientOptions options = new(botConfig.Token);
    return new TelegramBotClient(options, client);
});

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
    var controllerName = typeof(BotController).Name.Replace("Controller", "", StringComparison.Ordinal);
    var actionName = typeof(BotController).GetMethods()[0].Name;

    string? pattern = builder.Configuration.GetSection(BotConfiguration.RouteSection).Value;

    endpoints.MapControllerRoute(
            name: "bot_webhook",
            pattern: pattern ?? "/api/bot",
            defaults: new { controller = controllerName, action = actionName });

    endpoints.MapControllers();
});

app.UseHttpsRedirection();

app.Run();