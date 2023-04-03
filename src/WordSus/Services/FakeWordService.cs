using HtmlAgilityPack;

namespace WordSus.Services;

public class FakeWordService
{
    private readonly HttpClient webClient;

    private readonly Stack<string> fakeWordsCache;

    public FakeWordService()
    {
        webClient = new();
        fakeWordsCache = new();
    }

    public async Task<string> GetFakeWordAsync()
    {
        if (fakeWordsCache.Count == 0)
        {
            await RefreshCacheAsync();
        }
        
        return fakeWordsCache.Pop();
    }

    private async Task RefreshCacheAsync()
    {
        Stream stream = await webClient.GetStreamAsync("https://www.soybomb.com/tricks/words/");
        using (StreamReader reader = new(stream))
        {
            var html = reader.ReadToEnd();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            List<List<string>> table = doc.DocumentNode
                .SelectSingleNode("//table[@width='50%']")
                .Descendants("tr")
                .Where(tr => tr.Elements("td").Count() > 1)
                .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                .ToList();
            
            foreach (var row in table)
            {
                foreach (var fakeWord in row)
                {
                    fakeWordsCache.Push(fakeWord);
                }
            }
        }
    }
}
