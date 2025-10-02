using GuessMate.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace GuessMate.Services
{
    public class WordService
    {
        private readonly string _wordsFilePath = Path.Combine(App.AppFolderPath, "Words.json");
        private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

        public ObservableCollection<WordEntry> Words { get; } = [];
        public List<string> AllWords { get; set; } = [];

        public void AddWord(string newWord)
        {
            var entry = new WordEntry
            {
                Index = Words.Count + 1,
                Word = newWord.ToUpper()
            };

            for (int i = 0; i < newWord.Length; i++)
            {
                entry.Letters.Add(new LetterEntry
                {
                    Index = i,
                    Character = newWord[i].ToString().ToUpper()
                });
            }

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