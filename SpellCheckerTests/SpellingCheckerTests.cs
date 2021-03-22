using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpellChecker.Tests
{
    [TestClass]
    public class SpellingCheckerTests
    {
        [TestMethod]
        public void CheckMultipleWordsTest()
        {
            string input = @"rain spain plain plaint pain main mainly
the in on fall falls his was
===
hte rame in pain fells
mainy oon teh lain
was hints pliant
===";
            string expectedResult = @"the {rame?} in pain falls
{main mainly} on the plain
was {hints?} plaint";

            InitialData data = new InitialData(input);
            SpellingChecker checker = new SpellingChecker(data.Words);
            string fixedText = checker.CheckText(data.Text);
            Assert.AreEqual(expectedResult, fixedText);
        }
    }
}
