using EnumStringValues;

namespace ChatBot.Http.Bot.Enums
{
   
    public enum Commands
    {
        [StringValue("info"), StringValue("инфо"), StringValue("инфа"), StringValue("about"), StringValue("о"), StringValue("o")]
        InfoAboutBot = 0,

        [StringValue("joke"), StringValue("шутка")]
        Joke = 1,

        /// <summary>
        /// После команды через пробел пишется язык
        /// Пример: 
        ///     !lang ru
        ///     !язык en
        /// </summary>
        [StringValue("lang"), StringValue("язык")]
        ChangeLanguage = 2,

        [StringValue("commands"), StringValue("команды")]
        Commands = 3,
    }
}
