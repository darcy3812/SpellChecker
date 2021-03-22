using System;
using System.Linq;

namespace SpellChecker
{
    public class SpellingChecker
    {
        public string[] Dictionary { get; private set; }

        public SpellingChecker(string[] dict)
        {
            this.Dictionary = dict;
        }        

        public string CheckText(string text)
        {
            string[] words = text.Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < words.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(words[i]))
                {
                    continue;
                }

                SpellingWord fixedWord = this.CheckWordSpelling(words[i]);
                text = text.Replace(words[i], fixedWord.ToString());
            }

            return text;
        }

        private SpellingWord CheckWordSpelling(string initialWord)
        {
            SpellingWord word = new SpellingWord(initialWord);
            this.SetPossibleCorrections(word);

            return word;
        }

        /// <summary>
        /// Добавляет варианты исправления слова
        /// </summary>
        /// <param name="word">Слово для проверки</param>
        private void SetPossibleCorrections(SpellingWord word)
        {
            foreach (string dictWord in this.Dictionary)
            {
                int diff = this.LevenshteinDistance(dictWord, word.Word);

                //пропускаем слова с большой разницой в длине
                if (Math.Abs(word.Word.Length - dictWord.Length) >= 2)
                {
                    continue;
                }

                //пропускаем слова с расстоянием больше 2, либо если в вариантах есть слова с расстоянем 1, то пропускаем > 1
                if ((word.Corrections.Any(_ => _.Diff == 1) && diff > 1) || diff > 2)
                {
                    continue;
                }

                word.Corrections.Add(new Correction
                {
                    Diff = diff,
                    Word = dictWord
                });

                //если есть иделальное совпадение, то удаляем все остальные варианты
                if (diff == 0)
                {
                    word.Corrections.RemoveAll(_ => _.Word != dictWord);

                    break;
                }

                //если есть вариант с расстоянием 1, то удаляем > 1
                if (diff == 1)
                {
                    word.Corrections.RemoveAll(_ => _.Diff > 1);
                }
            }
        }

        /// <summary>
        /// Расчет расстояния Левенштейна
        /// </summary>
        private int LevenshteinDistance(string firstWord, string secondWord)
        {
            var n = firstWord.Length + 1;
            var m = secondWord.Length + 1;
            var matrixD = new int[n, m];

            const int deletionCost = 1;
            const int insertionCost = 1;

            for (var i = 0; i < n; i++)
            {
                matrixD[i, 0] = i;
            }

            for (var j = 0; j < m; j++)
            {
                matrixD[0, j] = j;
            }

            for (var i = 1; i < n; i++)
            {
                for (var j = 1; j < m; j++)
                {
                    var substitutionCost = firstWord[i - 1] == secondWord[j - 1] ? 0 : 2;

                    matrixD[i, j] = Minimum(matrixD[i - 1, j] + deletionCost,          // удаление
                                            matrixD[i, j - 1] + insertionCost,         // вставка
                                            matrixD[i - 1, j - 1] + substitutionCost); // замена
                }
            }

            return matrixD[n - 1, m - 1];
        }

        private int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;
    }
}
