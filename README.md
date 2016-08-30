
NBoilerpipe is a C# port of boilerpipe 1.2 (http://code.google.com/p/boilerpipe/) library.  
Based on the original NBoilerpipe library by Arif Ogan (https://github.com/oganix/NBoilerpipe)
Removed dependency on Sharpen library. Only the needed files from Sharpen are inclued in a folder inside NBoilerpipe project.

Target: Have a PCL NuGet package of Boilerpipe for C#

Usage:

using NBoilerpipe.Extractors;  
...  
String html = GetHtmlText();  
var text = ArticleExtractor.INSTANCE.GetText (html);  
//var text = DefaultExtractor.INSTANCE.GetText (html);   
...