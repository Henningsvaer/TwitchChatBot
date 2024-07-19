using ChatBot.Http.Bot.Client;
using ChatBot.Interfaces.Client;
using Microsoft.Extensions.Configuration;

var path = $"C:\\Users\\henni\\OneDrive\\Документы\\GitHub\\TwitchChatBot\\ChatBot\\Config";
var settingsDev = "appsettings.Development.json";
List<string> channels = new();
List<IBot> botsPull = new();

try
{
    IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(path)
        .AddJsonFile(settingsDev, optional: false)
        .Build();

    var channelsSection = configuration.GetSection("channels");

    foreach (var item in channelsSection.AsEnumerable())
    {
        if (item.Value is null)
            continue;
        channels.Add(item.Value);
    }

    foreach (var channel in channels)
    {
        botsPull.Add(new JokerBot(configuration, channel));
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
Console.ReadLine();