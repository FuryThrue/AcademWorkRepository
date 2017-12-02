using MessagesLibrary;
using System;
using System.Text;

namespace Autoclavable
{
    class Program
    {
        const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";
        static string _lastEncryptedMessage;

        static void Main(string[] args)
        {
            var appName = "Автоключевой шифр";
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

            var encryptor = new Cryptographer();
            _lastEncryptedMessage = encryptor.Encrypt(message, key, ALPHABET);
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

            var cryptographer = new Cryptographer();
            var decryptedMessages = cryptographer.Decrypt(encodedMessage, ALPHABET);
            for (int i = 0; i < decryptedMessages.Length; i++)
                ConsoleMessages.WriteResult($@"Если ключ равен {i}, тогда расшифрованное сообщение ""{decryptedMessages[i]}""");
        }
    }
}
