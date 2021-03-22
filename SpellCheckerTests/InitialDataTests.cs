using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpellChecker;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpellChecker.Tests
{
    [TestClass]
    public class InitialDataTests
    {
        [TestMethod]
        public void InitialDataTest()
        {
            string input = @"rain spain plain plaint pain main mainly
the in on fall falls his was
===
hte rame in pain fells
mainy oon teh lain
was hints pliant
===";
            string[] expectedWords = new string[] { "rain", "spain", "plain", "plaint", "pain", "main", "mainly", "the", "in", "on", "fall", "falls", "his", "was" };
            string expectedText = @"hte rame in pain fells
mainy oon teh lain
was hints pliant";

            InitialData data = new InitialData(input);
            Assert.AreEqual(expectedWords.Length, data.Words.Length);
            CollectionAssert.AreEqual(expectedWords, data.Words);
            Assert.AreEqual(expectedText, data.Text);
        }
    }
}