using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GuessMate.Models;

namespace GuessMate.Services
{
    public class DashboardService : ObservableObject
    {
        private static readonly HttpClient _httpClient = new();
        private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        private int _wordleNumber;
        public int WordleNumber
        {
            get => _wordleNumber;
            private set => SetProperty(ref _wordleNumber, value);
        }
        private string? _wordleSolution;
        public string WordleSolution
        {
            get => _wordleSolution!;
            private set => SetProperty(ref _wordleSolution, value);
        }
        private string? _wordleEditor;
        public string WordleEditor
        {
            get => _wordleEditor!;
            private set => SetProperty(ref _wordleEditor, value);
        }

        // API call
        public async Task LoadWordleAsync(DateTime date)
        {
            string url = $"https://www.nytimes.com/svc/wordle/v2/{date:yyyy-MM-dd}.json";

            using var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<WordleAPI>(json, _jsonOptions) ?? throw new InvalidOperationException("Failed to parse Wordle API response.");
            WordleNumber = data.Days_Since_Launch;
            WordleSolution = data.Solution;
            WordleEditor = data.Editor;
        }
    }
}