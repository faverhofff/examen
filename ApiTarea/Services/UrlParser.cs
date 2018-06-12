using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
// using System.Web;

namespace Examen.Services
{
  /// <summary>
  /// Summary description for UrlParser
  /// </summary>
  public class UrlParser
  {
        public UrlParser(){}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool checkURLFormat(string url)
        {
            Uri UriResult;

            return Uri.TryCreate(url, UriKind.Absolute, out UriResult) 
                   && 
                   (UriResult.Scheme == Uri.UriSchemeHttp || UriResult.Scheme == Uri.UriSchemeHttps);
        }

  }
}