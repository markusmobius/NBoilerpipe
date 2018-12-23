using System;
using System.Collections.Generic;
using System.IO;
using BoilerPipe;
using Newtonsoft.Json;
using System.Text;

namespace BoilerPipeTest
{
    public class singleSample
    {
        public string url;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var samples = JsonConvert.DeserializeObject<Dictionary<string, singleSample>>(File.ReadAllText("samples.json"));
            var output = new Dictionary<string, BoilerArticle>();
            var html_base = File.ReadAllText("sample_template.html");
            var html_tabs = new List<string>();
            var html_content = new List<string>();
            bool firstrow = true;
            foreach(var kvp in samples)
            {
                var html = File.ReadAllText("textSamples/" + kvp.Key + ".txt");
                var bpOutput = BoilerPipeMachine.process(html);
                if (firstrow)
                {
                    html_tabs.Add(string.Format("<li class=\"active\"><a href=\"#{0}\" data-toggle=\"tab\">{0}</a></li>", kvp.Key));
                }
                else
                {
                    html_tabs.Add(string.Format("<li><a href=\"#{0}\" data-toggle=\"tab\">{0}</a></li>", kvp.Key));
                }
                var sb = new StringBuilder();
                if (firstrow)
                {
                    sb.AppendLine(string.Format("<div class=\"tab-pane active\" id=\"{0}\">", kvp.Key));
                }
                else
                {
                    sb.AppendLine(string.Format("<div class=\"tab-pane\" id=\"{0}\">", kvp.Key));
                }
                sb.AppendLine(string.Format("<p><a target=\"_blank\" href=\"{0}\">URL</a></p>",kvp.Value.url));
                sb.AppendLine(string.Format("<h3>{0}</h3>",bpOutput.Title));
                var paras = bpOutput.Text.Split('\n');
                foreach(var para in paras)
                {
                    sb.AppendLine(string.Format("<p>{0}</p>",para));
                }
                sb.AppendLine("</div>");
                html_content.Add(sb.ToString());
                output.Add(kvp.Key, bpOutput);
                firstrow = false;
            }
            var htmlFile = html_base.Replace("TABS", string.Join("\n", html_tabs));
            htmlFile = htmlFile.Replace("CONTENT", string.Join("\n", html_content));
            File.WriteAllText("output.txt", JsonConvert.SerializeObject(output));
            File.WriteAllText("sample.html", htmlFile);
        }
    }
}
