using ChatBot.Http.Bot.Config;
using EnumStringValues;
using System.Net.Http.Headers;

namespace ChatBot.Http.Bot.Translater
{
    public static class TranslaterAPI
    {
        public static async Task GetLanguages(BotConfig botConfig)
        {
            var httpClient = HttpClientFactory.Create();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://microsoft-translator-text.p.rapidapi.com/languages?api-version=3.0"),
                Headers =
                {
                    { "x-rapidapi-key", botConfig.TranslatorApiKey },
                    { "x-rapidapi-host", botConfig.TranslatorApiHost },
                },
            };
            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
        }

        public static async Task<string> GetTranslate(BotConfig botConfig, string joke)
        {
            var httpClient = HttpClientFactory.Create();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = 
                new Uri($"https://microsoft-translator-text.p.rapidapi.com/translate?from=en&to={botConfig.Language.GetStringValue()}&api-version=3.0&profanityAction=NoAction&textType=plain"),
                Headers =
                {
                    { "x-rapidapi-key", botConfig.TranslatorApiKey },
                    { "x-rapidapi-host", botConfig.TranslatorApiHost },
                },
                Content = new StringContent("[{\"Text\":\"" + joke + "\"}]")
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json"),
                    }
                }
            };

            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                body = body
                    .Replace("[{\"translations\":[{\"text\":\"", "")
                    .Replace("\",\"to\":\"ru\"}]}]", "");

                return body;
            }
        }
    }
}
