using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeolabPortfolio.Extensions
{
    public class WordSplitter
    {
        public static List<string> SplitWords(string words, char splitChar)
        {
            words.ToLower();

            List<string> splitted = new List<string>();
            words.TrimEnd();

            if (!words.Contains(" "))
            {
                splitted.Add(words);
                return splitted;
            }


            foreach (var split in words.Split(splitChar))
            {
                splitted.Add(split);
            }

            return splitted;
        }
    }
}