using MessagesLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSA
{
    class Program
    {
        static List<string> _lastEncryptedMessage;
        static ulong _module;
        static double _secretKey;

        static void Main(string[] args)
        {
            var appName = "Криптографический алгоритм RSA";
            ConsoleMessages.WriteWelcome(appName);

            while (true)
            {
                var choose = 0;
                ConsoleMessages.WriteStartAction();
                choose = ConsoleMessages.ReadInt();

                while (choose < 1 || choose > 3)
                {
                    Console.Write("Неверный выбор! Введите 1, 2 или 3: ");
                    choose = ConsoleMessages.ReadInt();
                }
                Console.WriteLine();

                switch (choose)
                {
                    case 1:
                        _module = 0;
                        _secretKey = 0;
                        _lastEncryptedMessage?.Clear();
                        Encrypt();
                        break;
                    case 2:
                        DecryptPrepare();
                        break;
                    case 3:
                        return;
                }
            }
        }

        private static void Encrypt()
        {
            var generator = new Generator(512);
            var keys = generator.GenerateKeys();
            var openedKey = keys.Item1;
            _secretKey = keys.Item2;
            _module = generator.GetModule;

            var message = ConsoleMessages.GetMessageForEncrypt();
            var encryptor = new Encryptor();
            _lastEncryptedMessage = encryptor.Encrypt(message, openedKey, _module);

            ConsoleMessages.WriteResult($"Зашифрованное сообщение: {ConnectBlocks()}");
        }

        private static void DecryptPrepare()
        {
            var encodedMessage = "";

            var decrChoose = 1;
            if (_lastEncryptedMessage?.Count > 0)
            {
                encodedMessage = ConnectBlocks();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Расшифровать новое сообщение");
                Console.WriteLine($@"2 - Расшифровать последнее зашифрованное сообщение ""{encodedMessage}""");
                decrChoose = ConsoleMessages.ReadInt();
                while (decrChoose < 1 || decrChoose > 2)
                {
                    Console.Write("Неверный выбор! Введите 1 или 2: ");
                    decrChoose = ConsoleMessages.ReadInt();
                }
            }

            switch (decrChoose)
            {
                case 1:
                    encodedMessage = ConsoleMessages.GetEncryptedMessage();
                    break;
                case 2:
                    encodedMessage = ConnectBlocks();
                    _lastEncryptedMessage.Clear();
                    break;
            }
            Decrypt(encodedMessage);
        }

        private static void Decrypt(string encodedMessage)
        {
            var decryptor = new Decryptor();
            //var decodedMessage = decryptor.Decrypt(encodedMessage, _secretKey, _module);
            var decodedMessage = decryptor.Decrypt(int.Parse(encodedMessage), _secretKey, _module);

            ConsoleMessages.WriteResult($@"Расшифрованное сообщение ""{decodedMessage}""");
        }

        private static string ConnectBlocks()
        {
            var builder = new StringBuilder();
            foreach (var block in _lastEncryptedMessage)
                builder.Append(block);
            return builder.ToString();
        }
    }
}
