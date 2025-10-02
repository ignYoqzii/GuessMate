using GuessMate.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace GuessMate.Views.Pages
{
    public partial class GuessesPage : INavigableView<GuessesViewModel>
    {
        public GuessesViewModel ViewModel { get; }

        public GuessesPage(GuessesViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
