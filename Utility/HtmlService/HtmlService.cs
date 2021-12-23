using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Utility {
    public class HtmlService {

        public static string GetHTML(string url) {
            try {
                var uri = new Uri(url);
                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.97 Safari/537.36";
                request.Credentials = CredentialCache.DefaultCredentials;
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();           

                var encoding = Encoding.UTF8;
                var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding);
                string responseText = reader.ReadToEnd();


                return responseText;
            }
            catch (Exception e) {
                return "";
            }

        }

    }
}
