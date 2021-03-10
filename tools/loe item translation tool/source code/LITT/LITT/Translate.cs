using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;


namespace LITT
{
    static class Translate
    {
        public static string get(string text)
        {
            WebRequest request = WebRequest.Create("https://translate.google.com/?hl=ru&sl=en&tl=ru&text="+text+"&op=translate");
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            string responseFromServer;
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
            }
            ParseHTML(responseFromServer);
            return "";
        }
        private static void ParseHTML(string page)
        {

        }
    }
}
