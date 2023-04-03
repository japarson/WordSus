using System.Text.Json.Serialization;

namespace WordSus.Models;

public class UrbanDictionaryWord
{
    [JsonPropertyName("word")]
    public string Word { get; set; }

    [JsonPropertyName("definition")]
    public string Definition { get; set; }

    [JsonPropertyName("defid")]
    public int DefinitionId { get; set; }

    [JsonPropertyName("author")]
    public string Author { get; set; }

    [JsonPropertyName("example")]
    public string Example { get; set; }

    [JsonPropertyName("written_on")]
    public DateTime WrittenOn { get; set; }

    [JsonPropertyName("permalink")]
    public string PermaLink { get; set; }

    [JsonPropertyName("current_vote")]
    public string CurrentVote { get; set; }

    [JsonPropertyName("thumbs_up")]
    public int ThumbsUp { get; set; }

    [JsonPropertyName("thumbs_down")]
    public int ThumbsDown { get; set; }
}
