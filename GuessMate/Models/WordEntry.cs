using System.Collections.ObjectModel;

namespace GuessMate.Models
{
    public class WordEntry
    {
        public int Index { get; set; }
        public string Word { get; set; } = "";
        public ObservableCollection<LetterEntry> Letters { get; set; } = [];
    }
}

