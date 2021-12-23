using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Text;

namespace Utility.Notification {
    public class NotificationService {

        public static string APP_ID = "ae29b90e-4039-43af-bf17-20aa20ab4974";
        public static string API_KEY = "NDQxZGQ1ZmYtNzcxNy00ZWE4LWFhZGQtNTI5MmNlZGZmYTky";

        public static void Send(string title, string message, string imageUrl) {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic " + API_KEY);

            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                    + "\"app_id\": \"" 
                                                    + APP_ID
                                                    + "\","
                                                    + "\"contents\": {\"en\": \""+ message +"\"},"
                                                    + "\"headings\": {\"en\": \"" + title +"\"},"
                                                    + "\"included_segments\": [\"All\"]"
                                                  //  + "\"ios_attachments\": {"+NumberUtilities.GetUniqueNumber()+" : "+imageUrl+"}"

                                                  //  + "\"include_player_ids\": [\"61b295a8-72d9-4577-8767-a8a03127a3a7\"]" 
                                                    + "}"
                                                    );

            string responseContent = null;

            try {
                using (var writer = request.GetRequestStream()) {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse) {
                    using (var reader = new StreamReader(response.GetResponseStream())) {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
        }

    }
}
