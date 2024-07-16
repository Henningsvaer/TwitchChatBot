using JokeAPIWrapper;
using System.Net.Http;

namespace Jokes.API.API
{
    public static class JokesAPI
    {
        public static ApiClientV2 ApiClientV2 { get; private set; }

        static JokesAPI()
        {
            IHttpClientFactory httpClientFactory = new HttpClientFactory();
            ApiClientV2 ApiClientV2 = new ApiClientV2(httpClientFactory.CreateClient());
        }
    }
}
