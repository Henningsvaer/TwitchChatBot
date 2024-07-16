using ChatBot.Http.Bot.Client;
using ChatBot.Http.Bot.Config;
using ChatBot.Interfaces.Client;
using Microsoft.Extensions.Configuration;

var path = $"C:\\Users\\henni\\OneDrive\\Документы\\GitHub\\TwitchChatBot\\ChatBot\\Config";
var settingsDev = "appsettings.Development.json";
var channels = new List<string>();


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

    IBot bot = new JokerBot(configuration, channels[1]);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
Console.ReadLine();