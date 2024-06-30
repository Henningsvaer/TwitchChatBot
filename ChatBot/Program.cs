using ChatBot.Http.Bot.Client;
using ChatBot.Interfaces.Client;
using Microsoft.Extensions.Configuration;

var path = $"C:\\Users\\henni\\OneDrive\\Документы\\GitHub\\TwitchChatBot\\ChatBot\\Config";
var settingsDev = "appsettings.Development.json";

try
{
    IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(path)
        .AddJsonFile(settingsDev, optional: false)
        .Build();

    IBot bot = new Bot(configuration);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
Console.ReadLine();