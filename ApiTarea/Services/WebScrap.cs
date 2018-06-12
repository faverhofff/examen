using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Examen.Models;
using static Examen.Services.LinkFinder;
using System.Text.RegularExpressions;

namespace Examen.Services
{
    /// <summary>
    /// Descripción breve de WebScrap
    /// </summary>
    /// 
    public class WebScrap 
    {
        public List<string> MainURL { get; private set; } = new List<string>();

        public List<string> nextUrls { get; private set; } = new List<string>();

        private WebClient _Client { get; set; } = new WebClient();

        private string _Buffer = "";

        public int WordCount { get; private set; }

        protected WebScrap() { }

        public WebScrap(string url)
        {
            MainURL.Add(url);
        }

        public WebScrap(List<string> url)
        {
            MainURL.AddRange(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected Page scrapPage(string url)
        {
            try
            {
                _Buffer = _Client.DownloadString(url);
            }
            catch
            {
                _Buffer = "";
            }

            _Buffer = clearHtmlComments(_Buffer);

            foreach (LinkItem link in LinkFinder.Find(_Buffer))
                nextUrls.Add(link.Href);

            Page row = new Page()
            {
                Id = Guid.NewGuid(),
                Title = GetTitle(_Buffer),
                Content = HtmlToPlainText(_Buffer),
                Url = url
            };

            WordCount += WordCounter(_Buffer);

            return row;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Page> scrapPages()
        {
            List<Page> _result = new List<Page>();

            foreach (string url in MainURL)
                _result.Add( this.scrapPage(url) );

            return _result;
        }

        private string GetTitle(string html)
        {
            string regex = @"(?<=<title.*>)([\s\S]*)(?=</title>)";
            System.Text.RegularExpressions.Regex ex = new System.Text.RegularExpressions.Regex(regex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return ex.Match(html).Value.Trim();
        }

        private string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";
            const string stripFormatting = @"<[^>]*(>|$)";
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            
            text = System.Net.WebUtility.HtmlDecode(text);
            
            text = tagWhiteSpaceRegex.Replace(text, "><");
            
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }

        private int WordCounter(string Text)
        {
            /*
            char[] delimiters = new char[] {' ', '\r', '\n' };
            return Text.Split(delimiters,StringSplitOptions.RemoveEmptyEntries).Length; 
            */
        
            var text = Text.Trim();
            int wordCount = 0, index = 0;

            while (index < text.Length)
            {
                while (index < text.Length && !char.IsWhiteSpace(text[index]))
                    index++;

                wordCount++;

                while (index < text.Length && char.IsWhiteSpace(text[index]))
                    index++;
            }

            return index;
        }

        private string clearHtmlComments(string buffer)
        {
            return Regex.Replace(buffer, @"(\<!--\s*.*?((--\>)|$))", String.Empty);
        }
    }
}