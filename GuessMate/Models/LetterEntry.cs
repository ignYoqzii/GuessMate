namespace GuessMate.Models
{
    public class LetterEntry
    {
        public required string Character { get; set; }
        public string Color { get; set; } = "Gray"; // Default
        public int Index { get; set; }
    }
}

