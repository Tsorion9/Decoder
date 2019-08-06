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
            StringBuilder sourceText = new StringBuilder();
            String alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            cipherText = cipherText.ToLower();
            Char[] sourceTextVar = new Char[cipherText.Length];

            for (Int32 key = 1; key <= 32; key++)
            {
                for (Int32 i = 0; i < cipherText.Length; i++)
                {
                    if (!Char.IsLetter(cipherText[i]))
                        sourceTextVar[i] = cipherText[i];
                    else
                    {
                        for (Int32 j = 0; j < alphabet.Length; j++)
                        {
                            if (cipherText[i] == alphabet[j])
                            {
                                if ((j - key) < 0)
                                    sourceText[i] = alphabet[(j - key) + 33];
                                else sourceText[i] = alphabet[j - key];
                                break;
                            }
                        }
                    }
                }
                sourceText.Append(String.Format("Вариант {0}\n", key));
                sourceText.Append(new String(sourceTextVar) + "\n");
            }
            return sourceText.ToString();
        }

        public String CaesarCipher(String cipherText, Int32 key)
        {
            String alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            cipherText = cipherText.ToLower();
            Char[] sourceText = new Char[cipherText.Length];

            for (Int32 i = 0; i < cipherText.Length; i++)
            {
                if (!Char.IsLetter(cipherText[i]))
                    sourceText[i] = cipherText[i];
                else
                {
                    for (Int32 j = 0; j < alphabet.Length; j++)
                    {
                        if (cipherText[i] == alphabet[j])
                        {
                            if ((j - key) < 0)
                                sourceText[i] = alphabet[(j - key) + 33];
                            else
                                sourceText[i] = alphabet[j - key];
                            break;
                        }
                    }
                }
            }

            return new String(sourceText);
        }

        public String CipherAtbash(String cipherText, params Char[] key)
        {
            String alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            Char[] sourceText = new Char[cipherText.Length];
            cipherText = cipherText.ToLower();

            for (Int32 i = 0; i < cipherText.Length; i++)
            {
                if (!Char.IsLetter(cipherText[i]))
                    sourceText[i] = cipherText[i];
                else
                {
                    for (Int32 j = 0; j < alphabet.Length; j++)
                    {
                        if (cipherText[i] == key[j])
                        {
                            sourceText[i] = alphabet[j];
                            break;
                        }
                    }
                }
            }

            return new String(sourceText);
        }

        public String StatisticalAnalysis(String cipherText)
        {
            Dictionary<Char, Double> keyValuePairs = new Dictionary<Char, Double>();
            String frequency = "оеаинтсрвлкмдпуяыьгзбчйжчшюцщэфёъ";
            cipherText = cipherText.ToLower();
            Char[] sourceText = new Char[cipherText.Length];
            Int32 letters = 0;

            for (Int32 i = 0; i < cipherText.Length; i++)
            {
                if (Char.IsLetter(cipherText[i]))
                    letters++;
            }

            for (Int32 i = 0; i < cipherText.Length; i++)
            {
                if (!Char.IsLetter(cipherText[i]))
                    sourceText[i] = cipherText[i];
                else
                {
                    if (keyValuePairs.ContainsKey(cipherText[i]))
                        keyValuePairs[cipherText[i]] = keyValuePairs[cipherText[i]] + 1D / letters;
                    else
                        keyValuePairs.Add(cipherText[i], 1D / letters);
                }
            }


            keyValuePairs = keyValuePairs.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            Char[] vs = keyValuePairs.Keys.ToArray();
            Array.Reverse(vs);

            for (Int32 i = 0; i < cipherText.Length; i++)
            {
                for (Int32 j = 0; j < vs.Length; j++)
                {
                    if (cipherText[i] == vs[j])
                        sourceText[i] = frequency[j];
                }
            }

            return new String(sourceText);
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
            foreach(Char item in text)
            {
                if (Char.IsLetter(item) && !((item >= 'А' && item <= 'я') || item == 'Ё' || item == 'ё'))
                    return false;
            }

            return true;
        }

        public String GronsfeldCipher(String cipherText, String key)
        {
            String alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            cipherText = cipherText.ToLower();
            Char[] sourceText = new Char[cipherText.Length];
            Int32 o = 0;


            for (Int32 i = 0; i < cipherText.Length; i++)
            {
                if (!Char.IsLetter(cipherText[i]))
                    sourceText[i] = cipherText[i];
                else
                {
                    for (Int32 j = 0; j < alphabet.Length; j++)
                    {
                        if (cipherText[i] == alphabet[j])
                        {
                            if (j - (Int32)Char.GetNumericValue(key[o]) < 0)
                                sourceText[i] = alphabet[j - (Int32)Char.GetNumericValue(key[o]) + 33];
                            else
                                sourceText[i] = alphabet[j - (Int32)Char.GetNumericValue(key[o])];
                            break;
                        }
                    }

                    if (o == key.Length - 1)
                        o = 0;
                    else
                        o++;
                }
            }

            return new String(sourceText);
        }
    }
}
