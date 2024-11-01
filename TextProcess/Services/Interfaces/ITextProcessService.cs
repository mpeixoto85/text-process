using TextProcess.Model.DTOs;
using TextProcess.Model.Enums;

namespace TextProcess.Services.Interfaces
{
    public interface ITextProcessService
    {
        public List<string> OrderOptions();
        public List<string> OrderedText(string textToOrder, string optionText);
        public TextStatistics Statistics(string text);
        public int LevhensteinDistance(string baseWord, string comparedWord);
    }
}
