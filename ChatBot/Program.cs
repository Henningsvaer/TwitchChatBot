using ChatBot.Http.Bot.Client;
using ChatBot.Interfaces.Client;
using Microsoft.Extensions.Configuration;

//https://dev.twitch.tv/docs/irc/get-started/
//https://dev.twitch.tv/console/apps/4ez3nzdxi5e7s5hddfya8wjcrydg2h
// TODO: Add settings from json 
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.Development.json", optional: true)
    .Build();

IBot bot = new Bot(configuration);
Console.ReadLine();