using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Utility.WebParser {
    public class WebParser {

        public static string Parse(string htmlContent, string selector) {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(htmlContent);

            HtmlNode node = document.DocumentNode;

            var result = "";

            try {
                result = node.QuerySelector(selector).InnerText.Trim();
            }catch(NullReferenceException e) {
                result = "";
            }

            return result;
        }

        public static string RemoveUnwantedTags(string content) {
            if (!string.IsNullOrEmpty(content)) {
                return Regex.Replace(
                    Regex.Replace(
                        Regex.Replace(content, @"&[^;]*;", "")
                        , @"(?:(?:\r?\n)+ +){2,}", "\n")
                        , @"[\r\n]+", "\n");
            }else {
                return "";
            }
        }

    }
}
