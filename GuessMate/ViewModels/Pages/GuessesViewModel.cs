using GuessMate.Models;
using GuessMate.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Wpf.Ui.Controls;

namespace GuessMate.ViewModels.Pages
{
    public partial class GuessesViewModel(WordService wordService) : ObservableObject
    {
        private readonly WordService _wordService = wordService;

        [ObservableProperty]
        private string _newWord = string.Empty;

        public ObservableCollection<WordEntry> Words => _wordService.Words;

        [RelayCommand]
        private async Task AddWord()
        {
            if (string.IsNullOrWhiteSpace(NewWord))
                return;

            if (NewWord.Length != 5)
            {
                await ShowMessage("Invalid Word Length", "Please enter a 5-letter word.");
                return;
            }

            if (Words.Count >= 5)
            {
                await ShowMessage("Limit Reached", "You can only add up to 5 words.");
                return;
            }

            _wordService.AddWord(NewWord);
            NewWord = string.Empty;
        }

        [RelayCommand]
        private async Task ResetWords()
        {
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = "Confirm Reset",
                Content = "Are you sure you want to clear all words?",
                PrimaryButtonText = "Yes",
                PrimaryButtonAppearance = ControlAppearance.Primary,
                SecondaryButtonText = "No",
                SecondaryButtonAppearance = ControlAppearance.Secondary
            };

            var result = await uiMessageBox.ShowDialogAsync();
            if (result == Wpf.Ui.Controls.MessageBoxResult.Primary)
            {
                _wordService.ResetWords();
            }
        }

        [RelayCommand]
        private void SaveWords() => _wordService.SaveWords();

        private static async Task ShowMessage(string title, string content)
        {
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = title,
                Content = content
            };
            await uiMessageBox.ShowDialogAsync();
        }

        [RelayCommand]
        private void LoadWords() => _wordService.LoadWords();
    }
}

