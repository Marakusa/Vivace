using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Vivace
{
    public static class Lyrics
    {
        public static string Find(string query)
        {
            string newQuery = "";

            foreach (var q in Regex.Matches(query, "[a-zA-Z0-9 ]"))
            {
                newQuery += q;
            }

            newQuery = newQuery.Replace(" ", "+");

            WebRequest webRequest = WebRequest.Create("https://www.google.com/search?q=" + query + "+lyrics");
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseFromServer);

            try
            {
                return doc.GetElementbyId("main")
                    .ChildNodes[3]
                    .ChildNodes[0]
                    .ChildNodes[2]
                    .ChildNodes[0]
                    .ChildNodes[0]
                    .ChildNodes[0]
                    .ChildNodes[0]
                    .ChildNodes[0]
                    .ChildNodes[0]
                    .ChildNodes[0]
                    .ChildNodes[1].InnerText;
            }
            catch
            {
                return "Lyrics not found...";
            }
        }
    }
}
