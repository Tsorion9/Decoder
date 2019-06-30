using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decoder.BL
{
    static class Decoder
    {
        public static String CaesarCipher(String cipherText)
        {
            StringBuilder sourceText = new StringBuilder();
            String alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            String cipherTextLow = cipherText.ToLower();
            Char[] sourceTextVar = new Char[cipherTextLow.Length];

            for (Int32 key = 1; key <= 32; key++)
            {
                for (Int32 i = 0; i < cipherTextLow.Length; i++)
                {
                    if (!Char.IsLetter(cipherTextLow[i]))
                        sourceTextVar[i] = cipherTextLow[i];
                    else
                    {
                        for(Int32 j = 0; j < alphabet.Length; j++)
                        {
                            if (cipherTextLow[i] == alphabet[j])
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

        public static String CaesarCipher(String cipherText, Int32 key)
        {
            String alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            String cipherTextLow = cipherText.ToLower();
            Char[] sourceText = new Char[cipherTextLow.Length];

            for (Int32 i = 0; i < cipherTextLow.Length; i++)
            {
                if (!Char.IsLetter(cipherTextLow[i]))
                    sourceText[i] = cipherTextLow[i];
                else
                {
                    for(Int32 j = 0; j< alphabet.Length; j++)
                    {
                        if (cipherTextLow[i] == alphabet[j])
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

        public static String CipherAtbash(String cipherText, params Char[] key)
        {
            String alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            Char[] sourceText = new Char[cipherText.Length];

            for (Int32 i = 0; i < cipherText.Length; i++)
            {
                if (!Char.IsLetter(cipherText[i]))
                    sourceText[i] = cipherText[i];
                else
                {
                    for(Int32 j = 0; j < alphabet.Length; j++)
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

        public static String StatisticalAnalysis(String cipherText)
        {
            Dictionary<Char, Double> keyValuePairs = new Dictionary<Char, Double>();
            String frequency = "оеаинтсрвлкмдпуяыьгзбчйжчшюцщэфёъ";
            String cipherTextLow = cipherText.ToLower();
            Char[] sourceText = new Char[cipherTextLow.Length];
            Int32 letters = 0;

            for (Int32 i = 0; i < cipherTextLow.Length; i++)
            {
                if (Char.IsLetter(cipherTextLow[i]))
                    letters++;
            }

            for (Int32 i = 0; i < cipherTextLow.Length; i++)
            {
                if (!Char.IsLetter(cipherTextLow[i]))
                    sourceText[i] = cipherTextLow[i];
                else
                {
                    if (keyValuePairs.ContainsKey(cipherTextLow[i]))
                        keyValuePairs[cipherTextLow[i]] = keyValuePairs[cipherTextLow[i]] + 1D / letters;
                    else
                        keyValuePairs.Add(cipherTextLow[i], 1D / letters);
                }
            }


            keyValuePairs = keyValuePairs.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            Char[] vs = keyValuePairs.Keys.ToArray();
            Array.Reverse(vs);

            for (Int32 i = 0; i < cipherTextLow.Length; i++)
            {
                for (Int32 j = 0; j < vs.Length; j++)
                {
                    if (cipherTextLow[i] == vs[j])
                        sourceText[i] = frequency[j];
                }
            }

            return new String(sourceText);
        }
    }
}
