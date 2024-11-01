Description

Generate and deliver a Visual Studio Solutions to Text Process. The final deliverable should be an UI with WPF that user can input order option and text to order plus a button to process the ordering and can also input text do analyze and a button to perform the analysis. Each button should show the result of text process. The input text will be any text (phrase, a book paragraph, ...) with words separated by a space.

WebAPI Rest Exposing a Controller with 4 methods:

1 Get ORDER OPTIONS 
	o With NO input parameters
	o Returning 3 options: AlphabeticAsc, AlphabeticDesc, LenghtAsc)
* Deliver the list of Order Options

2 Get ORDERED TEXT
	o With 2 input parameters (TextToOrder and OrderOption)
	o Returning a list of words ordered
* Receives the text + one sort option. Should recover all words of the string and sort them based on the received sort option, there are 3 types of sorting (AlphabeticAsc, AlphabeticDesc, LenghtAsc). Should deliver a list of ordered words as a result

3 Get STATISTICS
	o With 1 input parameter (TextToAnalyze)
	o Returning a json with text statistics defined bellow (TextStatistics)
* Receives text only. Should calculate 3 statistics of the text: hyphens quantity, word quantity, spaces quantity. Should deliver a complex POCO object called TextStatistics, with the calculated data.

4 Get LEVHENSTEIN
	o With 2 input parameter (base word, compared word)
	o Returning an int with the Levhenstein distance among both words	
* Receives two text. Should calculate the Levhenstein distance among the both texts. Should return an integer with the distance.


To Run the application (Visual studio 2022):

1- Right-click on the Solution in Solution Explorer.
2- Select Properties.
3- In the Solution Property Pages window, go to Common Properties > Startup Project.
4- Choose Multiple startup projects.
5- For the projects "TextProcess" and "TextProcessUI", set the action to Start or Start without debugging.
Click OK to save the settings.