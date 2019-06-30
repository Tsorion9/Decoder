using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decoder.BL
{
    public interface IManager
    {
        String CaesarCipher(String cipherText);
        String CaesarCipher(String cipherText, Int32 key);
        String CipherAtbash(String cipherText, params Char[] key);
        String StatisticalAnalysis(String cipherText);
        String GronsfeldCipher(String cipherText, String key);
        List<String> Statistics(String cipherText);
        Boolean IsRusSymbol(String text);
    }

    public class Manager : IManager
    {
        public String CaesarCipher(String cipherText)
        {
            return Decoder.CaesarCipher(cipherText);
        }

        public String CaesarCipher(String cipherText, Int32 key)
        {
            return Decoder.CaesarCipher(cipherText, key);
        }

        public String CipherAtbash(String cipherText, params Char[] key)
        {
            return Decoder.CipherAtbash(cipherText, key);
        }

        public String StatisticalAnalysis(String cipherText)
        {
            return Decoder.StatisticalAnalysis(cipherText);
        }

        public List<String> Statistics(String cipherText)
        {
            List<String> statistics = new List<string>();
            Dictionary<Char, Double> keyValuePairs = new Dictionary<Char, Double>();
            String cipherTextLow = cipherText.ToLower();
            Int32 letters = 0;

            for (Int32 i = 0; i < cipherTextLow.Length; i++)
            {
                if (Char.IsLetter(cipherTextLow[i]))
                    letters++;
            }

            for (Int32 i = 0; i < cipherTextLow.Length; i++)
            {
                if (Char.IsLetter(cipherTextLow[i]))
                    if (keyValuePairs.ContainsKey(cipherTextLow[i]))
                        keyValuePairs[cipherTextLow[i]] = keyValuePairs[cipherTextLow[i]] + 1D / letters;
                    else
                        keyValuePairs.Add(cipherTextLow[i], 1D / letters);
            }

            keyValuePairs = keyValuePairs.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            foreach (var item in keyValuePairs)
            {
                statistics.Add(String.Format("{0} - {1}%", item.Key.ToString(), Math.Round(item.Value * 100, 3).ToString()));
            }

            statistics.Reverse();
            return statistics;
        }

        public Boolean IsRusSymbol(String text)
        {
            Boolean flag = true;

            foreach(Char item in text)
            {
                if (Char.IsLetter(item) && !((item >= 'А' && item <= 'я') || item == 'Ё' || item == 'ё'))
                    flag = false;
            }

            return flag;
        }

        public String GronsfeldCipher(String cipherText, String key)
        {
            return Decoder.GronsfeldCipher(cipherText, key);
        }
    }
}
