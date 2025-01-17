﻿using ChatBot.Http.Bot.Config;
using ChatBot.Http.Bot.Enums;
using ChatBot.Http.Bot.Jokes;
using ChatBot.Http.Bot.Translater;
using ChatBot.Interfaces.Client;
using EnumStringValues;
using JokeAPIWrapper.Models;
using Microsoft.Extensions.Configuration;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace ChatBot.Http.Bot.Client
{
    public class JokerBot : IJokerBot
    {
        private string _specialCommandSymbol = "!";
        private readonly IConfiguration _configuration;
        private readonly TwitchClient _client;
        private BotConfig _botConfig;
        private ConnectionCredentials _connectionCredentials;
        private ClientOptions _clientOptions;

        public JokerBot(IConfiguration configuration, string channelName)
        {
            _configuration = configuration;
            AddConfigAndCredentials(channelName);
            var customClient = new WebSocketClient(_clientOptions);
            _client = new TwitchClient(customClient);
            _client.Initialize(_connectionCredentials, _botConfig.ChannelName);
            AddEvents();
            _client.Connect();
        }

        public void OnLog(object sender, OnLogArgs e)
        {
            //Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        public void OnConnected(object sender, OnConnectedArgs e)
        {
            //Console.WriteLine($"Connected to {_botConfig.ChannelName}");
            //_client.SendMessage(_botConfig.ChannelName, $"Connected {_botConfig.ChannelName} - {DateTime.Now}");
        }

        public void OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            //Console.WriteLine("Hey guys! I am a bot connected via TwitchLib!");
            //_client.SendMessage(e.Channel, "Hey guys! I am a bot connected via TwitchLib!");
        }

        public async void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            try
            {
                if (!e.ChatMessage.Message.StartsWith('!'))
                {
                    return;
                }

                string message = e.ChatMessage.Message.ToLower().Replace("!", "");

                // switch не работает с динамическими строками из GetStringValue()
                if (Commands.Joke.GetAllStringValues().Contains(message))
                {
                    var joke = await JokesAPI.GetJoke();

                    // TODO: Перевод шутки
                    if (joke?.GetType() == typeof(SingleJokeModel))
                    {
                        var singleJokeModel = joke as SingleJokeModel;
                        var translatedJoke = await TranslaterAPI.GetTranslate(_botConfig, singleJokeModel?.Joke);
                        _client.SendMessage(_botConfig.ChannelName, translatedJoke + " Maaaan");
                    }
                    else
                    {
                        var twoPartJokeModel = joke as TwoPartJokeModel;
                        var translatedSetup = await TranslaterAPI.GetTranslate(_botConfig, twoPartJokeModel?.Setup);
                        var translatedDelivery = await TranslaterAPI.GetTranslate(_botConfig, twoPartJokeModel?.Delivery);

                        _client.SendMessage(_botConfig.ChannelName, "1/2: " + translatedSetup + " eeeh ");
                        await Task.Delay(2000);
                        _client.SendMessage(_botConfig.ChannelName, "2/2: " + translatedDelivery + " Maaaan");
                    }
                }

                // TODO: Можно добавить поддержку остальных языков
                if (Commands.ChangeLanguage.GetAllStringValues().Contains(message.Split(' ')[0]))
                {
                    string lang = message.Split(' ')[1];
                    if (string.IsNullOrEmpty(lang))
                    {
                        return;
                    }

                    _ = lang switch
                    {
                        "ru" => _botConfig.Language = Languages.RU,
                        "en" => _botConfig.Language = Languages.EN,
                        _ => throw new NotImplementedException($"Язык '{lang}' не поддерживается"),
                    };

                }

                if (Commands.InfoAboutBot.GetAllStringValues().Contains(message))
                {
                    _client.SendMessage(_botConfig.ChannelName, _botConfig.InfoAboutBot);
                }

                if (Commands.Commands.GetAllStringValues().Contains(message))
                {
                    var commands = Enum.GetValues(typeof(Commands)).Cast<Commands>().ToList();
                    var result = string.Empty;

                    foreach (var command in commands) 
                    {
                        var commandStrings = command.GetAllStringValues().ToList();

                        for (int i = 0; i < commandStrings?.Count - 1; i++)
                        {
                            result += "!" + commandStrings?[i] + " ";
                        }
                        result += " ||| ";
                    }
                    _client.SendMessage(_botConfig.ChannelName, result);
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.ToString());
            }
            //if (e.ChatMessage.Message.Contains("bad"))
            //  _client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(30), "Bad word! 30 minute timeout!");
        }

        public void OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
            //if (e.WhisperMessage.Username == _botConfig.ChannelNames[0])
            //    _client.SendWhisper(e.WhisperMessage.Username, "шутка");
        }

        public void OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            //if (e.Subscriber.SubscriptionPlan == SubscriptionPlan.Prime)
            //    _client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
            //else
            //    _client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points!");
        }

        private void AddConfigAndCredentials(string channelName)
        {
            _botConfig = new BotConfig()
            {
                BotName = _configuration.GetSection("identity:username").Value,
                PasswordOAuth = _configuration.GetSection("identity:password").Value,
                ChannelName = channelName,
                TranslatorApiHost = _configuration.GetSection("rapidapi:host").Value,
                TranslatorApiKey = _configuration.GetSection("rapidapi:key").Value,
                InfoAboutBot = _configuration.GetSection("info").Value
            };

            Console.WriteLine($"Bot:{_botConfig.BotName} - Channel:{_botConfig.ChannelName} - Каналы добавлены");

            _connectionCredentials = new ConnectionCredentials(_botConfig.BotName, _botConfig.PasswordOAuth);
            _clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            Console.WriteLine($"Bot:{_botConfig.BotName} - Channel:{_botConfig.ChannelName} - Доступы добавлены");
        }

        private void AddEvents()
        {
            _client.OnLog += OnLog;
            _client.OnJoinedChannel += OnJoinedChannel;
            _client.OnMessageReceived += OnMessageReceived;
            _client.OnWhisperReceived += OnWhisperReceived;
            _client.OnNewSubscriber += OnNewSubscriber;
            _client.OnConnected += OnConnected;

            Console.WriteLine($"Bot:{_botConfig.BotName} - Channel:{_botConfig.ChannelName} - События добавлены");
        }

        // TODO: Смена языка
        private void ChangeLanguage()
        {

        }
    }
}
