using System.Text.Json;
using WordSus.Models;

namespace WordSus.Services;

public class RandomWordService
{
    private readonly HttpClient webClient;

    private readonly Stack<UrbanDictionaryWord> randomWordsCache;

    public RandomWordService()
    {
        webClient = new();
        randomWordsCache = new();
    }

    public async Task<UrbanDictionaryWord> GetRandomWordAsync()
    {
        if (randomWordsCache.Count == 0)
        {
            await Task.Run(RefreshCacheAsync);
        }

        return randomWordsCache.Pop();
    }

    private async Task RefreshCacheAsync()
    {
        Stream stream = await webClient.GetStreamAsync("https://api.urbandictionary.com/v0/random");
        using (StreamReader reader = new(stream))
        {
            var json = reader.ReadToEnd();

            var randomUrbanDictionaryWords = JsonSerializer.Deserialize<RandomUrbanDictionaryWords>(json);
            foreach (var word in randomUrbanDictionaryWords.RandomWords)
            {
                randomWordsCache.Push(word);
            }
        }
    }
}
