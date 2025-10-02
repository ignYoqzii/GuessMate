using GuessMate.Services;
using System.Collections.ObjectModel;

namespace GuessMate.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly DashboardService _dashboardService;

        [ObservableProperty]
        private string wordleNumberDisplay = string.Empty;

        [ObservableProperty]
        private string wordleSolution = string.Empty;

        [ObservableProperty]
        private string wordleEditor = string.Empty;

        [ObservableProperty]
        private string wordleReviewURL = string.Empty;

        public bool IsRevealed = false;


        public DashboardViewModel(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;

            // Update formatted properties when service changes
            _dashboardService.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(DashboardService.WordleNumber))
                {
                    WordleNumberDisplay = $"#{_dashboardService.WordleNumber}";
                    WordleReviewURL = $"https://www.nytimes.com/2025/10/01/crosswords/wordle-review-{_dashboardService.WordleNumber}.html";
                }
                if (e.PropertyName == nameof(DashboardService.WordleEditor))
                    WordleEditor = _dashboardService.WordleEditor;
            };
        }

        [RelayCommand]
        public void RevealSolution()
        {
            if (IsRevealed)
            {
                IsRevealed = false;
                WordleSolution = string.Empty;
            }
            else
            {
                IsRevealed = true;
                WordleSolution = _dashboardService.WordleSolution.ToUpper();
            }
        }
    }
}