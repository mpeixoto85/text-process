using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TextProcessUI.Configuration;
using TextProcessUI.Services.DTOs;
using TextProcessUI.Services.Interfaces;

namespace TextProcessUI.Services
{
    public class TextProcessAPIService : ITextProcessAPIService
    {
        private readonly HttpClientConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public TextProcessAPIService(HttpClientConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient { BaseAddress = new Uri(_configuration.Uri) };
        }
         

        public async Task<List<string>> LoadOptions()
        {
            try
            {
                var response = await _httpClient.GetAsync("GetOrderOptions");
                var options = await response.Content.ReadFromJsonAsync<List<string>>();
                return options;
            }
            catch (Exception ex) 
            {
                throw new Exception($"Error loading order options: {ex.Message}");
            }
        }

        public async Task<List<string>> OrderedText(string textToOrder, string orderOption)
        {
            try
            {
                var response = await _httpClient.GetAsync($"GetOrderedText?textToOrder={Uri.EscapeDataString(textToOrder)}" +
                    $"&orderOption={Uri.EscapeDataString(orderOption)}");
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Please add text to Order");
                var orderedWords = await response.Content.ReadFromJsonAsync<List<string>>();
                return orderedWords;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ordering text: {ex.Message}");
            }

        }

        public async Task<TextStatisticsUI> AnalyzeText(string textToAnalyze)
        {
            try
            {
                var response = await _httpClient.GetAsync($"GetStatistics?textToAnalyze={Uri.EscapeDataString(textToAnalyze)}");
                var statistics = await response.Content.ReadAsStringAsync();
                var textStatistics = JsonConvert.DeserializeObject<TextStatisticsUI>(statistics);
                return textStatistics;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error analyzing text: {ex.Message}");
            }

        }

        public async Task<string> CalculateLevenshtein(string baseWord, string comparedWord)
        {
            try
            {
                var response = await _httpClient.GetAsync($"GetLevenshtein?baseWord={Uri.EscapeDataString(baseWord)}" +
                    $"&comparedWord={Uri.EscapeDataString(comparedWord)}");
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Please add words to compare");
                var distance = await response.Content.ReadAsStringAsync();
                return distance;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error calculating Levenshtein distance: {ex.Message}");
            }

        }
    }
}
