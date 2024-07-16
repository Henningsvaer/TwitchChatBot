using JokeAPIWrapper;
using JokeAPIWrapper.Models;

namespace ChatBot.Http.Bot.Jokes
{
    public static class JokesAPI
    {
        private static ApiClientV2 _apiClientV2;

        static JokesAPI()
        {
            var httpClient = HttpClientFactory.Create();
            _apiClientV2 = new ApiClientV2(httpClient);
        }

        public static async Task<JokeModel?> GetJoke()
        {
            var category = JokeCategory.Dark;
            var model = await _apiClientV2.GetJokeAsync();

            return model;
        }
    }
}
