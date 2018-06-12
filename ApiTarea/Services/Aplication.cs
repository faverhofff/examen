using Examen.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Examen.Services
{
    public class Aplication
    {
        public int maxDepth { get; set; }

        public int Indexs { get; private set; }

        public int Words { get; private set; }

        public Aplication()
        {
            maxDepth = 3;
        }

        public Aplication(int userMaxDepth)
        {
            maxDepth = userMaxDepth;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public void Scrap(List<string> url, int depth=1)
        {
            IsValidUrl(url);

            WebScrap engine = new WebScrap(url);

            if ( depth == 1 )
            {
                Indexs = 0;
                Words = 0;
            }

            if (depth <= maxDepth)
            {
                var pages = engine.scrapPages();

                MongoDBInstance.getInstance.Insert(pages);

                Indexs += url.Count; 

                Words += engine.WordCount;

                Scrap(engine.nextUrls, ++depth);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public List<Page> Search(string word)
        {
            try
            {
                List<Page> output = MongoDBInstance.getInstance.SelectByMatchWord(word);
                foreach (Page p in output)
                {
                    p.Matchs = CountMatchWordIntoResult(word, p.Content);
                    p.Content = "";
                }
                
                return output;
            }
            catch
            {
                // Action if exception is launch
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveIndexContent()
        {
            try
            {
                MongoDBInstance.getInstance.Remove();
            }
            catch
            {
                // Action if exception is launch
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        protected void IsValidUrl(List<string> Url )
        {
            List<string> output = new List<string>();

            foreach (string url in Url)
            {
                if (!UrlParser.checkURLFormat(url))
                    continue;
                else
                    output.Add(url);
            }

            Url.Clear();
            Url.AddRange(output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IndexResult getIndexResult()
        {
            IndexResult indexR = new IndexResult()
            {
                pageIndexTotal = Indexs,
                pageWordsCount = Words
            };

            return indexR;
        }

        public static int CountMatchWordIntoResult(string word, string main_content)
        {
            return Regex.Matches(main_content, word).Count;
        }
    }
}
