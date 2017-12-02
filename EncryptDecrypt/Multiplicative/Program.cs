using MessagesLibrary;
using System;
using System.Text;

namespace EncryptDecrypt
{
    class Program
    {
        static int[] _oldCharNumbers;
        const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";
        static string _lastEncryptedMessage;

        static void Main(string[] args)
        {
            var appName = "Мультипликативный шифр";
            ConsoleMessages.WriteWelcome(appName);

            while (true)
            {
                ConsoleMessages.WriteStartAction();

                var choose = ConsoleMessages.ReadInt();
                while (choose < 1 || choose > 3)
                {
                    Console.Write("Неверный выбор! Введите 1, 2 или 3: ");
                    choose = ConsoleMessages.ReadInt();
                }
                Console.WriteLine();

                switch (choose)
                {
                    case 1:
                        EncryptPrepare();
                        break;
                    case 2:
                        DecryptPrepare();
                        break;
                    case 3:
                        return;
                }
            }
        }

        private static void EncryptPrepare()
        {
            var message = ConsoleMessages.GetMessageForEncrypt(ALPHABET);

            var key = ConsoleMessages.GetKeyForEncrypt(ALPHABET);
            while (key >= ALPHABET.Length || !CheckKey(key))
            {
                Console.WriteLine("Недействительный ключ");
                Console.WriteLine("1. Ключ и размер алфавита должны быть взаимно простыми числами (не иметь общего делителя)");
                Console.WriteLine("2. Ключ не может быть больше размера алфавита");
                Console.WriteLine($"3. Размер алфавита достигает {ALPHABET.Length} символов");
                Console.WriteLine("Введите снова:");
                key = ConsoleMessages.ReadInt();
            }
            Console.WriteLine();

            _lastEncryptedMessage = Encrypt(message, key);
            ConsoleMessages.WriteResult($"Зашифрованное сообщение: {_lastEncryptedMessage}");
        }

        private static bool CheckKey(int key)
        {
            var commonFactor = GetCommonFactor(key);
            if (commonFactor > 1)
                return false;
            else
                return true;
        }

        private static int GetCommonFactor(int key)
        {
            var alphabetLength = ALPHABET.Length;
            var tempKey = key;
            while (alphabetLength != 0 && tempKey != 0)
            {
                if (alphabetLength > tempKey)
                    alphabetLength = alphabetLength % tempKey;
                else
                    tempKey = tempKey % alphabetLength;
            }
            return alphabetLength + tempKey;
        }

        private static void DecryptPrepare()
        {
            var encodedMessage = _lastEncryptedMessage;
            if (encodedMessage == null || encodedMessage.Length == 0)
            {
                Console.Write("Сначала следует зашифровать какое-нибудь слово");
                return;
            }
            Decrypt(encodedMessage);
        }

        private static string Encrypt(string message, int key)
        {
            _oldCharNumbers = new int[message.Length];
            var builder = new StringBuilder();
            for (int i = 0; i < message.Length; i++)
            {
                var numberOfChar = ALPHABET.IndexOf(message[i]);
                var newNumberNoModed = (numberOfChar * key);
                _oldCharNumbers[i] = newNumberNoModed;
                var newNumber = newNumberNoModed % ALPHABET.Length;
                var newChar = ALPHABET[newNumber];
                builder.Append(newChar);
            }
            return builder.ToString().ToUpper();
        }

        private static void Decrypt(string encodedMessage)
        {
            var lower = encodedMessage.ToLower();
            var alphabetLength = ALPHABET.Length;
            for (int i = 0; i < alphabetLength; i++)
            {
                var commonFactor = GetCommonFactor(i);
                if (commonFactor > 1)
                    continue;

                var builder = new StringBuilder();
                for (int j = 0; j < lower.Length; j++)
                {
                    var numberOfChar = ALPHABET.IndexOf(lower[j]);
                    var oldNumber = _oldCharNumbers[j] / i % alphabetLength;
                    var newChar = ALPHABET[oldNumber];
                    builder.Append(newChar);
                }
                ConsoleMessages.WriteResult($@"Если ключ равен {i}, тогда расшифрованное сообщение ""{builder.ToString()}""");
            }
        }
    }
}
