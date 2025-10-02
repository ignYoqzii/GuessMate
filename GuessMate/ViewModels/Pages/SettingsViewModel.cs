using GuessMate.Classes;
using Wpf.Ui.Abstractions.Controls;
using Wpf.Ui.Appearance;

namespace GuessMate.ViewModels.Pages
{
    public partial class SettingsViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _appVersion = String.Empty;

        [ObservableProperty]
        private ApplicationTheme _currentTheme = ApplicationTheme.Unknown;

        [ObservableProperty]
        private ApplicationTheme _currentAppTheme = ParseTheme(ConfigManager.GetTheme());

        public Task OnNavigatedToAsync()
        {
            if (!_isInitialized)
                InitializeViewModel();

            return Task.CompletedTask;
        }

        public Task OnNavigatedFromAsync() => Task.CompletedTask;

        private void InitializeViewModel()
        {
            AppVersion = $"GuessMate - {GetAssemblyVersion()}";

            _isInitialized = true;
        }

        private string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
                ?? String.Empty;
        }

        private static ApplicationTheme ParseTheme(string theme)
        {
            if (Enum.TryParse(theme, out ApplicationTheme appTheme))
            {
                return appTheme;
            }
            return ApplicationTheme.Light; // Default theme
        }

        partial void OnCurrentAppThemeChanged(ApplicationTheme oldValue, ApplicationTheme newValue)
        {
            CurrentAppTheme = newValue;
            ApplicationThemeManager.Apply(newValue);
            ConfigManager.SetTheme(newValue.ToString());
        }
    }
}
