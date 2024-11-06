using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TopTenWords
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string url = "https://lms-cdn.skillfactory.ru/assets/courseware/v1/dc9cf029ae4d0ae3ab9e490ef767588f/asset-v1:SkillFactory+CDEV+2021+type@asset+block/Text1.txt";
            string text = GetTextFromUrl(url);

            var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());

            string[] words = noPunctuationText.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var wordCounts = new Dictionary<string, int>();
            foreach (var word in words)
            {
                var lowerWord = word.ToLower();
                if (wordCounts.ContainsKey(lowerWord))
                {
                    wordCounts[lowerWord]++;
                }
                else
                {
                    wordCounts.Add(lowerWord, 1);
                }
            }

            var topWords = wordCounts.OrderByDescending(pair => pair.Value).Take(10);

            Console.Write("Топ 10 самых частых слов:\n");
            foreach(var word in topWords)
            {
                Console.WriteLine($"{word.Key}: {word.Value}");
            }
            Console.ReadKey();
        }

        static string GetTextFromUrl(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                return webClient.DownloadString(url);
            }
        }
    }
}
