using System;

namespace MessagesLibrary
{
    public abstract class ConsoleMessages
    {
        public static void WriteWelcome(string appName)
        {
            Console.Title = appName;
            Console.WriteLine(appName);
        }

        public static void WriteStartAction()
        {
            Console.WriteLine();
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Зашифровать");
            Console.WriteLine("2 - Расшифровать");
            Console.WriteLine("3 - Выход");
            Console.Write("Введите номер действия: ");
        }

        public static string GetMessageForEncrypt(string alphabet)
        {
            Console.Write("Введите сообщение: ");
            return ReadString(alphabet);
        }

        public static string GetEncryptedMessage()
        {
            Console.Write("Введите зашифрованное сообщение: ");
            return Console.ReadLine();
        }

        public static int GetKeyForEncrypt(string alphabet)
        {
            Console.Write("Введите ключ: ");
            var key = ReadInt();
            while (key < 0 || key > alphabet.Length)
            {
                Console.WriteLine($"Ключе должен быть в диапазоне от 0 до {alphabet.Length}");
                key = ReadInt();
            }
            Console.WriteLine();

            return key;
        }

        public static void WriteResult(string result)
        {
            Console.WriteLine(result);
        }

        private static string ReadString(string alphabet)
        {
            var inputedString = Console.ReadLine();
            inputedString = inputedString.ToLower();
            foreach (var c in inputedString)
            {
                if (!alphabet.Contains(c.ToString()))
                {
                    Console.Write("Введенное сообщение содержит недопустимые символы. Попробуйте снова: ");
                    inputedString = ReadString(alphabet);
                    break;
                }
            }
            return inputedString;
        }

        public static int ReadInt()
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
    }
}
