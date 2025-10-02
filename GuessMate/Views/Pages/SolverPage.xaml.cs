using GuessMate.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace GuessMate.Views.Pages
{
    public partial class SolverPage : INavigableView<SolverViewModel>
    {
        public SolverViewModel ViewModel { get; }

        public SolverPage(SolverViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
