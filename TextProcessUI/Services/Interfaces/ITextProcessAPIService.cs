using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextProcessUI.Services.DTOs;

namespace TextProcessUI.Services.Interfaces
{
    public interface ITextProcessAPIService
    {
        public Task<List<string>> LoadOptions();
        public Task<List<string>> OrderedText(string textToOrder, string orderOption);
        public Task<TextStatisticsUI> AnalyzeText(string textToAnalyze);
        public Task<string> CalculateLevenshtein(string baseWord, string comparedWord);
    }
}
