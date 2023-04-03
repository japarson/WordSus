using System.Text.Json.Serialization;

namespace WordSus.Models;

public class RandomUrbanDictionaryWords
{
    [JsonPropertyName("list")]
    public List<UrbanDictionaryWord> RandomWords { get; set; }
}
