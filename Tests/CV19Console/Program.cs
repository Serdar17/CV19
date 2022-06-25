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
            var dates = GetDates();
            Console.WriteLine(string.Join("\r\n", dates));

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
                yield return line;
            }
        }

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(item => DateTime.Parse(item, CultureInfo.InvariantCulture))
            .ToArray();
    }
}
