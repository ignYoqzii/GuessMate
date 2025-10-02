using GuessMate.Models;

namespace GuessMate.Classes
{
    class WordFiltering
    {
        public static IEnumerable<string> FilterWords(IEnumerable<string> candidates, IEnumerable<WordEntry> words)
        {
            var grayLetters = new HashSet<char>();
            var greenConstraints = new Dictionary<int, char>();
            var yellowConstraints = new Dictionary<char, HashSet<int>>();

            // Build constraints from all guesses
            foreach (var word in words)
            {
                foreach (var letter in word.Letters)
                {
                    if (letter.Color == "Gray")
                    {
                        var c = letter.Character[0];
                        grayLetters.Add(c);
                    }
                    else if (letter.Color == "Yellow")
                    {
                        if (!yellowConstraints.ContainsKey(letter.Character[0]))
                            yellowConstraints[letter.Character[0]] = [];
                        yellowConstraints[letter.Character[0]].Add(letter.Index);
                    }
                    else if (letter.Color == "Green")
                    {
                        greenConstraints[letter.Index] = letter.Character[0];
                    }
                }
            }

            // Filter candidate words
            foreach (var candidate in candidates)
            {
                bool valid = true;

                // Check gray letters first
                if (candidate.Any(c => grayLetters.Contains(c)))
                    continue;

                // Check green constraints
                foreach (var kv in greenConstraints)
                {
                    if (candidate[kv.Key] != kv.Value)
                    {
                        valid = false;
                        break;
                    }
                }
                if (!valid) continue;

                // Check yellow constraints
                foreach (var kv in yellowConstraints)
                {
                    char letter = kv.Key;
                    var forbiddenIndices = kv.Value;

                    if (!candidate.Contains(letter))
                    {
                        valid = false;
                        break;
                    }

                    // Cannot be in forbidden indices
                    if (forbiddenIndices.Any(idx => candidate[idx] == letter))
                    {
                        valid = false;
                        break;
                    }
                }
                if (!valid) continue;

                yield return candidate;
            }
        }
    }
}