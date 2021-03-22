using System.Collections.Generic;
using System.Linq;

namespace SpellChecker
{
    public class SpellingWord
    {
        public List<Correction> Corrections { get; set; } = new List<Correction>();

        public string Word { get; private set; }

        public SpellingWord(string word)
        {
            this.Word = word;
        }

        public override string ToString()
        {
            if (this.Corrections.Count == 0)
            {
                return $"{{{this.Word}?}}";
            }

            if (this.Corrections.Count == 1)
            {
                return this.Corrections[0].Word;
            }

            return $"{{{string.Join(" ", this.Corrections.Select(_ => _.Word))}}}";
        }
    }
}
