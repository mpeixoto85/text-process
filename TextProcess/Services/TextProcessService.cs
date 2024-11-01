using TextProcess.Model.DTOs;
using TextProcess.Model.Enums;
using TextProcess.Services.Helpers;
using TextProcess.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace TextProcess.Services
{
    public class TextProcessService : ITextProcessService
    {
        public TextProcessService() { }

        public List<string> OrderOptions() 
        {
              return EnumHelper.GetEnumDescriptionList<OrderOptions>();  
        }

        public List<string> OrderedText(string textToOrder, string optionText) 
        {
            if(!Enum.TryParse(optionText, out OrderOptions options))
            {
                throw new ArgumentException(optionText, nameof(optionText));
            }
            List<string> orderedTextList = new List<string>();
            if (string.IsNullOrEmpty(textToOrder)) 
            {
                throw new ArgumentNullException(nameof(textToOrder));
            }
            var textList = textToOrder.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            switch(options)
            {
                case Model.Enums.OrderOptions.ASC:
                    orderedTextList = textList.OrderBy(x => x).ToList();
                    break;
                case Model.Enums.OrderOptions.DESC:
                    orderedTextList = textList.OrderByDescending(x => x).ToList();
                    break;
                case Model.Enums.OrderOptions.LENGHT:
                    orderedTextList = textList.OrderBy(x => x.Length).ToList(); ;
                    break;
            }
            return orderedTextList;
        }

        public TextStatistics Statistics(string text)
        {
            TextStatistics statistics = new TextStatistics
            {
                HyphenQuantity = !string.IsNullOrEmpty(text) ? text.Count(c => c == '-'): 0,
                WordQuantity = !string.IsNullOrEmpty(text) ? text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length: 0,
                SpaceQuantity = !string.IsNullOrEmpty(text) ? text.Count(c => c == ' ') : 0,
            };

            return statistics;
        }

        public int LevhensteinDistance(string baseWord, string comparedWord)
        {
            if(string.IsNullOrEmpty(baseWord))
                return comparedWord.Length;
            else if(string.IsNullOrEmpty(comparedWord))
                return baseWord.Length;

            int lenghtBase = baseWord.Length;
            int lenghtCompared = comparedWord.Length;
            var distances = new int[lenghtBase + 1, lenghtCompared + 1];

            for (int i = 0; i <= lenghtBase; i++)
                distances[i,0] = i;

            for (int j = 0; j <= lenghtCompared; j++)
                distances[0,j]  = j;

            for (int i = 1; i <= lenghtBase; i++)
            {
                for (int j = 1; j <= lenghtCompared; j++)
                {
                    int cost = baseWord[i - 1] == comparedWord[j - 1] ? 0 : 1;
                    distances[i,j] = Math.Min(
                        Math.Min(distances[i - 1, j] + 1, //Deletion
                             distances[i, j - 1] + 1), //Insertion
                        distances[i - 1, j - 1] + cost); //Substitution
                }
            }
            return distances[lenghtBase,lenghtCompared];
        }
    }
}
