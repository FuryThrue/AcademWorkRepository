﻿using MessagesLibrary;
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
            var key = ConsoleMessages.GetKeyForEncrypt(_alphabet);

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
                decrChoose = ConsoleMessages.ReadInt();
                while (decrChoose < 1 || decrChoose > 2)
                {
                    Console.Write("Неверный выбор! Введите 1 или 2: ");
                    decrChoose = ConsoleMessages.ReadInt();
                }
            }

            var encodedMessage = "";
            switch (decrChoose)
            {
                case 1:
                    encodedMessage = ConsoleMessages.GetEncryptedMessage();
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
                ConsoleMessages.WriteResult($@"Если ключ равен {i}, тогда расшифрованное сообщение ""{builder.ToString()}""");
            }
        }
    }
}
