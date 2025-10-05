using GuessMate.Models;

namespace GuessMate.Classes
{
    public static class WordFiltering
    {
        /// <summary>
        /// Filters candidate words according to Wordle guess feedback.
        /// </summary>
        /// <param name="candidates">All possible candidate words.</param>
        /// <param name="guesses">Past guesses with color feedback.</param>
        /// <returns>Words consistent with all feedback.</returns>
        public static IEnumerable<string> FilterWords(IEnumerable<string> candidates, IEnumerable<WordEntry> guesses)
        {
            ArgumentNullException.ThrowIfNull(candidates);
            ArgumentNullException.ThrowIfNull(guesses);

            // --------------------------------------------------------
            // Constraint containers
            // --------------------------------------------------------
            var greenPositions = new Dictionary<int, char>(); // Exact letters at positions
            var perLetterExcludedPositions = new Dictionary<char, HashSet<int>>(); // Letters that cannot appear at certain positions
            var grayLetters = new HashSet<char>(); // Letters absent globally
            var confirmedLetters = new HashSet<char>(); // Letters confirmed present somewhere

            // Build constraints from all guesses
            BuildConstraints(guesses, greenPositions, perLetterExcludedPositions, grayLetters, confirmedLetters);

            // Filter candidates
            foreach (var word in candidates)
            {
                if (IsWordValid(word, greenPositions, perLetterExcludedPositions, grayLetters))
                    yield return word;
            }
        }

        // =======================================================================
        //  BUILD CONSTRAINTS
        // =======================================================================
        private static void BuildConstraints(
            IEnumerable<WordEntry> guesses,
            Dictionary<int, char> greenPositions,
            Dictionary<char, HashSet<int>> perLetterExcludedPositions,
            HashSet<char> grayLetters,
            HashSet<char> confirmedLetters)
        {
            // Step 1: Collect all letters confirmed present (green or yellow)
            foreach (var guess in guesses)
            {
                foreach (var letter in guess.Letters)
                {
                    char c = letter.Character[0];
                    if (letter.Color.Equals("Green", StringComparison.OrdinalIgnoreCase) ||
                        letter.Color.Equals("Yellow", StringComparison.OrdinalIgnoreCase))
                    {
                        confirmedLetters.Add(c);
                    }
                }
            }

            // Step 2: Build detailed constraints
            foreach (var guess in guesses)
            {
                foreach (var letter in guess.Letters)
                {
                    char c = letter.Character[0];
                    int pos = letter.Index;
                    string color = letter.Color;

                    switch (color)
                    {
                        case "Green":
                            greenPositions[pos] = c;
                            break;

                        case "Yellow":
                            // Letter must exist but not at this position
                            if (!perLetterExcludedPositions.ContainsKey(c))
                                perLetterExcludedPositions[c] = [];
                            perLetterExcludedPositions[c].Add(pos);
                            break;

                        case "Gray":
                            // Only mark as globally gray if letter is not confirmed elsewhere
                            if (!confirmedLetters.Contains(c))
                            {
                                grayLetters.Add(c);
                            }
                            else
                            {
                                // Letter exists elsewhere, but cannot appear here
                                if (!perLetterExcludedPositions.ContainsKey(c))
                                    perLetterExcludedPositions[c] = [];
                                perLetterExcludedPositions[c].Add(pos);
                            }
                            break;
                    }
                }
            }
        }

        // =======================================================================
        //  VALIDATE A CANDIDATE WORD
        // =======================================================================
        private static bool IsWordValid(
            string word,
            IReadOnlyDictionary<int, char> greenPositions,
            IReadOnlyDictionary<char, HashSet<int>> perLetterExcludedPositions,
            IReadOnlySet<char> grayLetters)
        {
            // 1. Reject if contains any globally gray letter
            foreach (var c in word)
            {
                if (grayLetters.Contains(c))
                    return false;
            }

            // 2. Check green letters
            foreach (var (index, requiredChar) in greenPositions)
            {
                if (word[index] != requiredChar)
                    return false;
            }

            // 3. Check per-letter positional exclusions (yellow + gray positions)
            foreach (var (letter, forbiddenIndices) in perLetterExcludedPositions)
            {
                // Must exist in word somewhere
                if (!word.Contains(letter))
                    return false;

                // Cannot appear in forbidden positions
                foreach (var idx in forbiddenIndices)
                {
                    if (word[idx] == letter)
                        return false;
                }
            }

            return true;
        }
    }
}