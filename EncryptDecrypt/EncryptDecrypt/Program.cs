using System;
using System.Text;

namespace EncryptDecrypt
{
    class Program
    {
        const string _alphabet = "abcdefghijklmnopqrstuvwxyz";
        static string _lastEncryptedMessage;

        static void Main(string[] args)
        {
            var appName = "Шифр Цезаря";
            Console.Title = appName;
            Console.WriteLine(appName);
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Зашифровать");
                Console.WriteLine("2 - Расшифровать");
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
                        DecryptPrepare();
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

            Console.Write("Введите ключ: ");
            var key = ReadInt();
            while (key < 0 || key > _alphabet.Length)
            {
                Console.WriteLine($"Ключе должен быть в диапазоне от 0 до {_alphabet.Length}");
                key = ReadInt();
            }
            Console.WriteLine();

            _lastEncryptedMessage = Encrypt(message, key);
            Console.WriteLine($"Зашифрованное сообщение: {_lastEncryptedMessage}");
        }

        private static void DecryptPrepare()
        {
            var decrChoose = 1;
            if (_lastEncryptedMessage?.Length > 0)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Расшифровать новое сообщение");
                Console.WriteLine($@"2 - Расшифровать последнее зашифрованное сообщение ""{_lastEncryptedMessage}""");
                decrChoose = ReadInt();
                while (decrChoose < 1 || decrChoose > 2)
                {
                    Console.Write("Неверный выбор! Введите 1 или 2: ");
                    decrChoose = ReadInt();
                }
            }

            var encodedMessage = "";
            switch (decrChoose)
            {
                case 1:
                    Console.Write("Введите зашифрованное сообщение: ");
                    encodedMessage = Console.ReadLine();
                    break;
                case 2:
                    encodedMessage = _lastEncryptedMessage;
                    break;
            }
            Decrypt(encodedMessage);
        }

        private static string Encrypt(string message, int key)
        {
            var builder = new StringBuilder();
            foreach (var c in message)
            {
                var numberOfChar = _alphabet.IndexOf(c);
                var newNumber = (numberOfChar + key) % _alphabet.Length;
                var newChar = _alphabet[newNumber];
                builder.Append(newChar);
            }
            return builder.ToString().ToUpper();
        }

        private static void Decrypt(string encodedMessage)
        {
            var lower = encodedMessage.ToLower();
            var alphabetLength = _alphabet.Length;
            for (int i = 0; i < alphabetLength; i++)
            {
                var builder = new StringBuilder();
                foreach (var c in lower)
                {
                    var numberOfChar = _alphabet.IndexOf(c);
                    var newNumber = (numberOfChar - i) % alphabetLength;
                    if (newNumber < 0)
                        newNumber += alphabetLength;
                    var newChar = _alphabet[newNumber];
                    builder.Append(newChar);
                }
                Console.WriteLine($@"Если ключ равен {i}, тогда расшифрованное сообщение ""{builder.ToString()}""");
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
