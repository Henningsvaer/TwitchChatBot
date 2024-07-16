using EnumStringValues;

namespace ChatBot.Http.Bot.Enums
{
    /// <summary>
    /// Список основан на JokeAPI-CS-Wrapper/JokeAPIWrapper/Models/Enums.cs
    /// Добавлен русский язык
    /// </summary>
    public enum Languages
    {
        [StringValue("en")]
        EN,
        [StringValue("es")]
        ES,
        [StringValue("fr")]
        FR,
        [StringValue("de")]
        DE,
        [StringValue("pt")]
        PT,
        [StringValue("cs")]
        CS,
        [StringValue("ru")]
        RU
    }
}
