namespace ChatBot.Http.Bot.Config
{
    public class BotConfig
    {
        public List<string> ChannelNames { get; set; } = new();
        public string BotName { get; set; }
        public string PasswordOAuth { get; set; }
    }
}
