using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TextProcessUI.Services.Interfaces;

namespace TextProcessUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ITextProcessAPIService _service;
        public MainWindow(ITextProcessAPIService service)
        {
            InitializeComponent();
            _service = service;
            LoadOrderOptions();
        }

        private async void LoadOrderOptions()
        {
            try
            {                
                var options = await _service.LoadOptions();
                cmbOrderOptions.ItemsSource = options;
                cmbOrderOptions.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void OrderText_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var textToOrder = txtOrderText.Text;
                var orderOption = cmbOrderOptions.SelectedIndex.ToString();                
                var orderedWords = await _service.OrderedText(textToOrder, orderOption);
                lstOrderedText.ItemsSource = orderedWords;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void AnalyzeText_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var textToAnalyze = txtAnalyzeText.Text;
                var textStatistics = await _service.AnalyzeText(textToAnalyze);

                txtHyphenCount.Text = textStatistics.HyphenQuantity.ToString();
                txtWordCount.Text = textStatistics.WordQuantity.ToString();
                txtSpaceCount.Text = textStatistics.SpaceQuantity.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void CalculateLevenshtein_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var baseWord = txtBaseWord.Text;
                var comparedWord = txtComparedWord.Text;
                var distance = await _service.CalculateLevenshtein(baseWord, comparedWord);
                txtLevenshteinDistance.Text = distance;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}