using MessagesLibrary;
using System;
using System.Text;

namespace EncryptDecrypt
{
    class Program
    {
        static int[] _oldCharNumbers;
        const string _alphabet = "abcdefghijklmnopqrstuvwxyz";
        static string _lastEncryptedMessage;

        static void Main(string[] args)
        {
            var appName = "Афинный шифр";
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
            var message = ConsoleMessages.GetMessageForEncrypt(_alphabet);

            Console.Write("Введите ключ для мультипликативного шифра: ");
            var keyMulty = ConsoleMessages.ReadInt();
            while (keyMulty >= _alphabet.Length || !CheckKey(keyMulty))
            {
                Console.WriteLine("Недействительный ключ");
                Console.WriteLine("1. Ключ и размер алфавита должны быть взаимно простыми числами (не иметь общего делителя)");
                Console.WriteLine("2. Ключ не может быть больше размера алфавита");
                Console.WriteLine($"3. Размер алфавита достигает {_alphabet.Length} символов");
                Console.Write("Введите снова:");
                keyMulty = ConsoleMessages.ReadInt();
            }

            Console.Write("Введите ключ для шифра Цезаря (аддитивный шифр): ");
            var keyCesar = ConsoleMessages.ReadInt();
            while (keyCesar < 0 || keyCesar > _alphabet.Length)
            {
                Console.WriteLine($"Ключе должен быть в диапазоне от 0 до {_alphabet.Length}");
                keyCesar = ConsoleMessages.ReadInt();
            }
            Console.WriteLine();

            _lastEncryptedMessage = Encrypt(message, keyCesar, keyMulty);
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
            var alphabetLength = _alphabet.Length;
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
                ConsoleMessages.WriteResult("Сначала следует зашифровать какое-нибудь слово");
                return;
            }
            Decrypt(encodedMessage);
        }

        private static string Encrypt(string message, int keyCesar, int keyMulty)
        {
            var alphabetLength = _alphabet.Length;
            _oldCharNumbers = new int[message.Length];

            var builder = new StringBuilder();
            for (int i = 0; i < message.Length; i++)
            {
                var numberOfChar = _alphabet.IndexOf(message[i]);

                var cesarNumber = (numberOfChar * keyMulty + keyCesar);
                _oldCharNumbers[i] = cesarNumber;

                var cesarNumberModed = cesarNumber % alphabetLength;
                var newChar = _alphabet[cesarNumberModed];
                builder.Append(newChar);
            }
            return builder.ToString().ToUpper();
        }

        private static void Decrypt(string encodedMessage)
        {
            var lower = encodedMessage.ToLower();
            var alphabetLength = _alphabet.Length;
            for (int keyMulty = 0; keyMulty < alphabetLength; keyMulty++)
            {
                var commonFactor = GetCommonFactor(keyMulty);
                if (commonFactor > 1)
                    continue;

                for (int keyCesar = 0; keyCesar < alphabetLength; keyCesar++)
                {
                    var builder = new StringBuilder();
                    for (int i = 0; i < lower.Length; i++)
                    {
                        var multyiplicativeNumber = ((_oldCharNumbers[i] - keyCesar) / keyMulty) % alphabetLength;
                        if (multyiplicativeNumber < 0)
                            multyiplicativeNumber += alphabetLength;
                        var newChar = _alphabet[multyiplicativeNumber];
                        builder.Append(newChar);
                    }
                    ConsoleMessages.WriteResult($@"Если ключ мультипликативного шифра={keyMulty}, ключ шифра Цезаря={keyCesar}, тогда расшифрованное сообщение ""{builder.ToString()}""");
                }
            }
        }
    }
}
