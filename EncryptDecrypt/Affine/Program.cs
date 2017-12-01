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
            Console.Title = appName;
            Console.WriteLine(appName);
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Зашифровать");
                Console.WriteLine($"2 - Расшифровать {_lastEncryptedMessage}");
                Console.WriteLine("3 - Выход");
                Console.Write("Вводите: ");
                var choose = ReadInt();
                while (choose < 1 || choose > 3)
                {
                    Console.Write("Неверный выбор! Введите 1, 2 или 3: ");
                    choose = ReadInt();
                }
                Console.WriteLine();

                switch (choose)
                {
                    case 1:
                        EncryptPrepare();
                        break;
                    case 2:
                        DecryptPrepate();
                        break;
                    case 3:
                        return;
                }
                Console.WriteLine();
            }
        }

        private static void EncryptPrepare()
        {
            Console.Write("Введите сообщение: ");
            var message = ReadString();

            Console.WriteLine();
            Console.Write("Введите ключ для мультипликативного шифра: ");
            var keyMulty = ReadInt();
            while (keyMulty >= _alphabet.Length || !CheckKey(keyMulty))
            {
                Console.WriteLine("Недействительный ключ");
                Console.WriteLine("1. Ключ и размер алфавита должны быть взаимно простыми числами (не иметь общего делителя)");
                Console.WriteLine("2. Ключ не может быть больше размера алфавита");
                Console.WriteLine($"3. Размер алфавита достигает {_alphabet.Length} символов");
                Console.Write("Введите снова:");
                keyMulty = ReadInt();
            }
            Console.Write("Введите ключ для шифра Цезаря (аддитивный шифр): ");
            var keyCesar = ReadInt();
            while (keyCesar < 0 || keyCesar > _alphabet.Length)
            {
                Console.WriteLine($"Ключе должен быть в диапазоне от 0 до {_alphabet.Length}");
                keyCesar = ReadInt();
            }
            Console.WriteLine();

            _lastEncryptedMessage = Encrypt(message, keyCesar, keyMulty);
            Console.WriteLine($"Зашифрованное сообщение: {_lastEncryptedMessage}");
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

        private static void DecryptPrepate()
        {
            var encodedMessage = _lastEncryptedMessage;
            if (encodedMessage == null || encodedMessage.Length == 0)
            {
                Console.Write("Сначала следует зашифровать какое-нибудь слово");
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
                        var newChar = _alphabet[multyiplicativeNumber];
                        builder.Append(newChar);
                    }
                    Console.WriteLine($@"Если ключ мультипликативного шифра={keyMulty}, ключ шифра Цезаря={keyCesar}, тогда расшифрованное сообщение ""{builder.ToString()}""");
                }
            }
        }

        private static int ReadInt()
        {
            var result = 0;
            var parseResult = false;
            do
            {
                var line = Console.ReadLine();
                parseResult = int.TryParse(line, out result);
                if (!parseResult)
                {
                    Console.Write("Неверные данные! Введите число: ");
                }
            } while (!parseResult);
            return result;
        }

        private static string ReadString()
        {
            var inputedString = Console.ReadLine();
            inputedString = inputedString.ToLower();
            foreach (var c in inputedString)
            {
                if (!_alphabet.Contains(c.ToString()))
                {
                    Console.Write("Введенное сообщение содержит недопустимые символы. Попробуйте снова: ");
                    inputedString = ReadString();
                    break;
                }
            }
            return inputedString;
        }
    }
}
