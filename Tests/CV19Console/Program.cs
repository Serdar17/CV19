using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;

namespace CV19Console
{
    class Program
    {
        private const string dataUrl = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        static void Main(string[] args)
        {

            //var cvr_str = response.Content.ReadAsStringAsync().Result;

            //foreach (var dataLine in GetDataLines())
            //    Console.WriteLine(dataLine);
            //var dates = GetDates();
            //Console.WriteLine(string.Join("\r\n", dates));

            var uzbekistanData = GetData()
                .First(item => item.country.Equals("Uzbekistan", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(string.Join("\r\n", GetDates().Zip(uzbekistanData.counts, (date, count) => $"{date} - {count}")));

            Console.WriteLine("Hello World!");
        }

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();

            var response = await client.GetAsync(dataUrl, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()
        {
            using var dataStream = GetDataStream().Result;
            using var dataReader = new StreamReader(dataStream);

            while (!dataReader.EndOfStream)
            {
                var line = dataReader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                yield return line.Replace("Korea,", "Korea -");
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(item => DateTime.Parse(item, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<(string country, string province, int[] counts)> GetData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var countryName = row[1].Trim(' ', '"');
                int[] counts = null;
                if (row.Length == 889)
                    counts = row.Skip(4).Select(int.Parse).ToArray();
                else
                    counts = row.Skip(5).Select(int.Parse).ToArray();

                yield return (countryName, province, counts);
            }

        }
    }
}
