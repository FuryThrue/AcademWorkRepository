using MessagesLibrary;
using System;

namespace Vigener
{
    class Program
    {
        static string _alphabet = "abcdefghijklmnopqrstuvwxyz";
        static string _lastEncryptedMessage;

        static void Main(string[] args)
        {
            var appName = "Шифр Виженера";
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
            var message = ConsoleMessages.GetMessageForEncrypt(_alphabet);
            var key = ConsoleMessages.GetStringKeyForEncrypt(_alphabet);

            var encryptor = new Encryptor();
            _lastEncryptedMessage = encryptor.Encrypt(message, key, _alphabet);

            ConsoleMessages.WriteResult($"Зашифрованное сообщение: {_lastEncryptedMessage}");
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
                    _lastEncryptedMessage = "";
                    break;
            }
            Decrypt(encodedMessage);
        }

        private static void Decrypt(string encodedMessage)
        {
            var key = ConsoleMessages.GetStringKeyForEncrypt(_alphabet);

            var decryptor = new Decryptor();
            var decodedMessage = decryptor.Decrypt(encodedMessage, key, _alphabet);

            ConsoleMessages.WriteResult($@"Расшифрованное сообщение ""{decodedMessage}""");
        }
    }
}
