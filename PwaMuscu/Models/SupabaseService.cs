using PwaMuscu.Models;
using System.Net.Http.Json;
using System.Text.Json;

public class SupabaseService
{
    private readonly HttpClient _http;
    private readonly string _tableUrl;
    private readonly string _apiKey;

    public SupabaseService(HttpClient http)
    {
        _http = http;
        _apiKey = "<TA_CLEF_API_SUPABASE>"; // À récupérer dans Supabase > Project Settings > API > anon key
        _tableUrl = "https://<ton_projet>.supabase.co/rest/v1/entries"; // Voir dans Supabase > API > REST
        _http.DefaultRequestHeaders.Add("apikey", _apiKey);
        _http.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
    }

    public async Task<List<PoidsEntry>> GetEntriesAsync()
    {
        var response = await _http.GetFromJsonAsync<List<PoidsEntry>>(_tableUrl);
        return response ?? new();
    }

    public async Task AddEntryAsync(PoidsEntry entry)
    {
        var response = await _http.PostAsJsonAsync(_tableUrl, new[] { entry });
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteEntryAsync(Guid id)
    {
        var url = $"{_tableUrl}?id=eq.{id}";
        var request = new HttpRequestMessage(HttpMethod.Delete, url);
        var response = await _http.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}

