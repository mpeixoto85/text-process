using Microsoft.AspNetCore.Mvc;
using TextProcess.Model.DTOs;
using TextProcess.Model.Enums;
using TextProcess.Services.Interfaces;

namespace TextProcess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TextProcessController : ControllerBase
    {
        

        private readonly ILogger<TextProcessController> _logger;
        private readonly ITextProcessService _textProcess;

        public TextProcessController(ILogger<TextProcessController> logger, ITextProcessService textProcess)
        {
            _logger = logger;
            _textProcess = textProcess;
        }

        [HttpGet("GetOrderOptions")]
        public Task<List<string>> GetOrderOptions()
        {
            try
            {
                return Task.FromResult(_textProcess.OrderOptions());
            }
            catch (Exception ex)
            {
                return (Task<List<string>>)Task.FromException(ex);
            }
            
        }
        [HttpGet("GetOrderedText")]
        public Task<List<string>> GetOrderedText(string textToOrder, string orderOption)
        {
            try
            {
                return Task.FromResult(_textProcess.OrderedText(textToOrder, orderOption));
            }
            catch (Exception ex)
            {
                return (Task<List<string>>)Task.FromException(ex);
            }
        }
        [HttpGet("GetStatistics")]
        public Task<TextStatistics> GetStatistics(string textToAnalyze)
        {
            return Task.FromResult(_textProcess.Statistics(textToAnalyze));
        }
        [HttpGet("GetLevenshtein")]
        public Task<int> GetLevenshtein(string baseWord, string comparedWord)
        {
            return Task.FromResult(_textProcess.LevhensteinDistance(baseWord, comparedWord));
        }


    }
}
