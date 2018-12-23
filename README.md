
NBoilerpipe is a C# port of boilerpipe 1.2 (http://code.google.com/p/boilerpipe/) library to .NET Standard 2.0

Based on the original NBoilerpipe library by Arif Ogan (https://github.com/oganix/NBoilerpipe) and the edits of Illiescu Constantin (https://github.com/ictin/NBoilerpipe).

Illiescu already removed dependency on Sharpen library. Only the needed files from Sharpen are inclued in a folder inside NBoilerpipe project. The code is forked from his version with two changes:

- The BoilerPipe project is now a .NET Standard 2.0 library.
- BoilerPipeTest is a more elaborate test suite for different publishers and produces a sample.html file which allows easy inspection. 

You can expand the test suite by modifying the samples.json file and adding the HTML of your test pages to the textSamples folder.

Usage:

using NBoilerpipe.Extractors;  
...  
String html = GetHtmlText();  
var text = ArticleExtractor.INSTANCE.GetText (html);  
//var text = DefaultExtractor.INSTANCE.GetText (html);   
...
