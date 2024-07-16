using EnumStringValues;

namespace ChatBot.Http.Bot.Enums
{
   
    public enum Commands
    {
        [StringValue("тест")]
        Test = 0,

        [StringValue("Joke")]
        Joke = 1,
    }
}
