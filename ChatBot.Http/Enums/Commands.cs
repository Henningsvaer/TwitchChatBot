using EnumStringValues;

namespace ChatBot.Http.Bot.Enums
{
   
    public enum Commands
    {
        [StringValue("тест")]
        Test = 0,

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
    }
}
