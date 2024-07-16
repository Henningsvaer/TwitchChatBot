using ChatBot.Http.Bot.Enums;

namespace ChatBot.Http.Bot.Config
{
    public class BotConfig
    {
        public string? ChannelName { get; set; }
        public string? BotName { get; set; }
        public string? PasswordOAuth { get; set; }
        public Languages Language { get; set; } = Languages.RU;
        public string? TranslatorApiKey { get; set; }
        public string? TranslatorApiHost { get; set; }
    }
}
