using GuessMate.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessMate.Classes
{
    public static class LetterColoring
    {
        /// <summary>
        /// Compares a guess to the solution and returns the colored letters.
        /// Each letter is marked as "Green", "Yellow", or "Gray" following Wordle rules.
        /// </summary>
        /// <param name="guess">The guessed word (uppercase or lowercase).</param>
        /// <param name="solution">The solution word (uppercase or lowercase).</param>
        /// <returns>ObservableCollection of LetterEntry with color information.</returns>
        public static ObservableCollection<LetterEntry> ColorGuess(string guess, string solution)
        {
            if (guess.Length != solution.Length)
                throw new ArgumentException("Guess and solution must have the same length.");

            guess = guess.ToUpper();
            solution = solution.ToUpper();

            var result = new List<LetterEntry>(guess.Length);

            // Step 0: Track counts of letters in solution for duplicates
            var solutionLetterCounts = solution
                .GroupBy(c => c)
                .ToDictionary(g => g.Key, g => g.Count());

            // Step 1: First pass - mark greens
            for (int i = 0; i < guess.Length; i++)
            {
                char guessChar = guess[i];
                char solutionChar = solution[i];

                var letterEntry = new LetterEntry
                {
                    Index = i,
                    Character = guessChar.ToString(),
                    Color = "Gray" // default
                };

                if (guessChar == solutionChar)
                {
                    letterEntry.Color = "Green";
                    solutionLetterCounts[guessChar]--; // Use one occurrence
                }

                result.Add(letterEntry);
            }

            // Step 2: Second pass - mark yellows
            for (int i = 0; i < result.Count; i++)
            {
                var letter = result[i];

                if (letter.Color == "Green") continue;

                char c = letter.Character[0];

                if (solutionLetterCounts.TryGetValue(c, out int remaining) && remaining > 0)
                {
                    letter.Color = "Yellow";
                    solutionLetterCounts[c]--;
                }
            }

            return new ObservableCollection<LetterEntry>(result);
        }
    }
}
