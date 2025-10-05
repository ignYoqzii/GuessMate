using GuessMate.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using GuessMate.Classes;

namespace GuessMate.Services
{
    public class WordService(DashboardService dashboardService)
    {
        private readonly DashboardService _dashboardService = dashboardService;

        private readonly string _wordsFilePath = Path.Combine(App.AppFolderPath, "Words.json");
        private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

        public ObservableCollection<WordEntry> Words { get; } = [];
        public List<string> AllWords { get; set; } = [];

        public void AddWord(string newWord)
        {
            var solution = _dashboardService.WordleSolution;

            var entry = new WordEntry
            {
                Index = Words.Count + 1,
                Word = newWord.ToUpper(),
                // Fill the Letters list with correct colors
                Letters = LetterColoring.ColorGuess(newWord, solution)
            };

            Words.Add(entry);
        }


        public void ResetWords()
        {
            Words.Clear();
        }

        public void SaveWords()
        {
            var json = JsonSerializer.Serialize(Words, _jsonOptions);
            File.WriteAllText(_wordsFilePath, json);
        }

        public async Task LoadAllWordsAsync()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Assets", "valid-wordle-words.txt");
            if (!File.Exists(path))
                throw new FileNotFoundException("Word list not found.", path);

            var lines = await File.ReadAllLinesAsync(path);
            AllWords = [.. lines.Select(w => w.ToUpper().Trim()).Where(w => w.Length == 5)];
        }

        public void LoadWords()
        {
            if (File.Exists(_wordsFilePath))
            {
                var json = File.ReadAllText(_wordsFilePath);
                var loaded = JsonSerializer.Deserialize<ObservableCollection<WordEntry>>(json);
                if (loaded != null)
                {
                    Words.Clear();
                    foreach (var w in loaded)
                        Words.Add(w);
                }
            }
        }
    }
}