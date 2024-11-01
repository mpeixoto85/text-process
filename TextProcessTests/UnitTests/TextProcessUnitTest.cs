using Xunit;
using TextProcess.Services;
using TextProcess.Model;
using TextProcess.Services.Helpers;
using TextProcess.Services.Interfaces;
namespace TextProcessTests.UnitTests
{
    public class TextProcessUnitTest
    {
        private readonly TextProcessService _service;
        public TextProcessUnitTest()
        {
            _service = new TextProcessService();
        }
        [Fact]
        public void OrderOptions_ReturnsCorrectOptions()
        {
            var result = _service.OrderOptions();
            Assert.NotNull(result);
            Assert.Contains("AlphabeticAsc", result);
            Assert.Contains("AlphabeticDesc", result);
            Assert.Contains("LenghtAsc", result);
            Assert.Equal(3, result.Count);
        }

        [Theory]
        [InlineData("car helicopter train bus airplane", "0", new[] { "airplane", "bus", "car", "helicopter", "train" })]
        [InlineData("car helicopter train bus airplane", "1", new[] { "train", "helicopter", "car", "bus", "airplane" })]
        [InlineData("car helicopter train bus airplane", "2", new[] { "car", "bus", "train", "airplane", "helicopter" })]
        public void OrderedText_ReturnsOrderedWords(string textToOrder, string orderOption, string[] expectedOrder)
        {
            var result = _service.OrderedText(textToOrder, orderOption);
            Assert.NotNull(result);
            Assert.Equal(expectedOrder, result.ToArray());
        }

        [Fact]
        public void OrderedText_ThrowsException_WhenTextToOrderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _service.OrderedText(null, "ASC"));
        }

        [Fact]
        public void OrderedText_ThrowsException_WhenOrderOptionIsInvalid()
        {
            Assert.Throws<ArgumentException>(() => _service.OrderedText("car helicopter train", "INVALID"));
        }

        [Fact]
        public void Statistics_ReturnsCorrectTextStatistics()
        {
            var text = "This is a test-text with hyphens - and words.";
            var result = _service.Statistics(text);
            Assert.NotNull(result);
            Assert.Equal(2, result.HyphenQuantity);
            Assert.Equal(9, result.WordQuantity);
            Assert.Equal(8, result.SpaceQuantity);
        }

        [Theory]
        [InlineData("kitten", "sitting", 3)]
        [InlineData("flaw", "lawn", 2)]
        [InlineData("apple", "apple", 0)]
        public void LevhensteinDistance_ReturnsCorrectDistance(string baseWord, string comparedWord, int expectedDistance)
        {
            var result = _service.LevhensteinDistance(baseWord, comparedWord);
            Assert.Equal(expectedDistance, result);
        }

        [Fact]
        public void LevhensteinDistance_ReturnsLengthOfComparedWord_WhenBaseWordIsEmpty()
        {
            var baseWord = "";
            var comparedWord = "test";
            var result = _service.LevhensteinDistance(baseWord, comparedWord);
            Assert.Equal(comparedWord.Length, result);
        }

        [Fact]
        public void LevhensteinDistance_ReturnsLengthOfBaseWord_WhenComparedWordIsEmpty()
        {
            var baseWord = "test";
            var comparedWord = "";
            var result = _service.LevhensteinDistance(baseWord, comparedWord);
            Assert.Equal(baseWord.Length, result);
        }
    }
}