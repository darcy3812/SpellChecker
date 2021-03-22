using System;

namespace SpellChecker
{
    public class InitialData
    {
        public string[] Words { get; private set; }

        public string Text { get; private set; }

        public InitialData(string input)
        {
            this.ParseInput(input);
        }

        private void ParseInput(string text)
        {
            string[] parts = text.Split("===", StringSplitOptions.RemoveEmptyEntries);
            this.Words = parts[0].Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            this.Text = parts[1].Trim();
        }
    }
}
