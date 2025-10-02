using CommunityToolkit.Mvvm.ComponentModel;
using GuessMate.Classes;
using GuessMate.Models;
using GuessMate.Services;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Wpf.Ui.Controls;

namespace GuessMate.ViewModels.Pages
{
    public partial class SolverViewModel : ObservableObject
    {
        private readonly WordService _wordService;

        public ObservableCollection<WordEntry> Board { get; } = [];

        public ObservableCollection<string> PossibleWords { get; } = [];

        private const int MaxRows = 5;
        private const int WordLength = 5;

        public SolverViewModel(WordService wordService)
        {
            _wordService = wordService;

            BuildBoard(); // Initialize board

            PossibleWords = new ObservableCollection<string>(_wordService.AllWords); // Start with all words

            _wordService.Words.CollectionChanged += (_, __) => BuildBoard(); // Auto-refresh
        }

        // Build or refresh the board
        private void BuildBoard()
        {
            Board.Clear();

            for (int i = 0; i < MaxRows; i++)
            {
                if (i < _wordService.Words.Count)
                    Board.Add(_wordService.Words[i]);
                else
                    Board.Add(CreateEmptyWordEntry(i));
            }
        }

        private static WordEntry CreateEmptyWordEntry(int index)
        {
            var word = new WordEntry
            {
                Index = index + 1,
                Word = ""
            };

            for (int i = 0; i < WordLength; i++)
                word.Letters.Add(new LetterEntry { Index = i, Character = "", Color = "Gray" });

            return word;
        }

        [RelayCommand]
        private async Task UpdatePossibleWordsList()
        {
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = "Confirm Refresh",
                Content = "Are you sure you want to filter the words based on your current guesses? Make sure the board on the left is the same as the one on Wordle.",
                PrimaryButtonText = "Yes",
                PrimaryButtonAppearance = ControlAppearance.Primary,
                SecondaryButtonText = "No",
                SecondaryButtonAppearance = ControlAppearance.Secondary
            };

            var result = await uiMessageBox.ShowDialogAsync();
            if (result == Wpf.Ui.Controls.MessageBoxResult.Primary)
            {
                if (_wordService.Words.Count == 0 || _wordService.AllWords.Count == 0)
                {
                    PossibleWords.Clear();
                    foreach (var word in _wordService.AllWords)
                        PossibleWords.Add(word);
                    return;
                }
                PossibleWords.Clear();
                var candidates = WordFiltering.FilterWords(_wordService.AllWords, _wordService.Words);
                foreach (var word in candidates)
                    PossibleWords.Add(word);
            }
        }
    }
}