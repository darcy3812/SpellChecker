
using System;
using System.Linq;

namespace SpellChecker
{
    class Program
    {
        static void Main(string[] args)
        {            
            string input = @"rain spain plain plaint pain main mainly
the in on fall falls his was
===
hte rame in pain fells
mainy oon teh lain
was hints pliant
===";
            InitialData data = new InitialData(input);
            SpellingChecker checker = new SpellingChecker(data.Words);            
            string fixedText = checker.CheckText(data.Text);
            Console.WriteLine(fixedText); 
            Console.ReadKey();
        }        
    }
}
