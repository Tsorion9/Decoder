using System;
using System.Windows;
using Decoder.BL;

namespace Decoder
{
    class Presenter
    {
        IMainWindow view;
        IManager manager;

        public Presenter(IMainWindow view, IManager manager)
        {
            this.view = view;
            this.manager = manager;

            this.view.DecipherClick += new EventHandler(View_DecipherClick);
        }

        private void View_DecipherClick(object sender, EventArgs e)
        {
            string decryptionMethod = view.GetDecryptionMethod;

            switch (decryptionMethod)
            {
                case "Шифр Цезаря (ключ неизвестен)":
                    if (manager.IsRusSymbol(view.GetCipherText))
                        view.DecryptedText = manager.CaesarCipher(view.GetCipherText);
                    else
                        MessageBox.Show("В зашифрованном тексте имеются запрещенные символы.");
                    break;

                case "Шифр Цезаря (ключ известен)":
                    Int32 keyCS;
                    Boolean isNumeric = Int32.TryParse(view.GetKey, out keyCS);
                    if (isNumeric && keyCS > 0 && keyCS < 33 && manager.IsRusSymbol(view.GetCipherText))
                        view.DecryptedText = manager.CaesarCipher(view.GetCipherText, keyCS);
                    else
                        MessageBox.Show("Ключ введен неверно или в зашифрованном тексте имеются" +
                            " запрещенные символы.");
                    break;

                case "Шифр Атбаш":
                    String strKeyCA = view.GetKey.Replace(" ", "");
                    if (view.GetKey.Length == 65 && strKeyCA.Length == 33 && manager.IsRusSymbol(view.GetCipherText))
                    {
                        Char[] keyCA = strKeyCA.ToCharArray();
                        view.DecryptedText = manager.CipherAtbash(view.GetCipherText, keyCA);
                    }
                    else
                        MessageBox.Show("Ключ введен неверно или в зашифрованном тексте имеются" +
                            " запрещенные символы.");
                    break;

                case "Метод статистического анализа":
                    if (manager.IsRusSymbol(view.GetCipherText))
                    {
                        view.DecryptedText = manager.StatisticalAnalysis(view.GetCipherText);
                        view.Statistics = manager.Statistics(view.GetCipherText);
                    }
                    else
                        MessageBox.Show("В зашифрованном тексте имеются запрещенные символы.");
                    break;

                case "Шифр Гронсфельда":
                    if(manager.IsRusSymbol(view.GetCipherText))
                    {
                        view.DecryptedText = manager.GronsfeldCipher(view.GetCipherText, view.GetKey);
                    }
                    else
                        MessageBox.Show("В зашифрованном тексте имеются запрещенные символы.");
                    break;
            }
        }
    }
}
