using System;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace ForecastScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set the URL for the weather.com 10-day forecast page
            string url = "https://weather.com/weather/tenday/l/USNY0996:1:US";

            // Use the WebClient class to download the HTML source code
            WebClient client = new WebClient();
            string html = client.DownloadString(url);

            // Use the HtmlAgilityPack library to parse the HTML
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Extract the forecast data
            var rows = doc.DocumentNode.SelectNodes("//table[@class='twc-table']/tbody/tr");
            foreach (var row in rows)
            {
                // Extract the date, description, and high/low temperatures
                string date = row.SelectSingleNode(".//th").InnerText.Trim();
                string desc = row.SelectSingleNode(".//td[@class='description']").InnerText.Trim();
                string highLow = row.SelectSingleNode(".//td[@class='temp']").InnerText.Trim();

                // Print the extracted data to the console
                Console.WriteLine($"{date}, {desc}, {highLow}");
            }

            // Write the forecast data to a .csv file
            using (StreamWriter writer = new StreamWriter("forecast.csv"))
            {
                // Write the header row
                writer.WriteLine("Date, Description, High/Low");

                // Write the forecast data
                foreach (var row in rows)
                {
                    // Extract the date, description, and high/low temperatures
                    string date = row.SelectSingleNode(".//th").InnerText.Trim();
                    string desc = row.SelectSingleNode(".//td[@class='description']").InnerText.Trim();
                    string highLow = row.SelectSingleNode(".//td[@class='temp']").InnerText.Trim();

                    // Write the data to the .csv file
                    writer.WriteLine($"{date}, {desc}, {highLow}");
                }
            }
        }
    }
}