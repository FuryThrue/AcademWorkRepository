using System;
using System.Collections.Generic;
using System.Text;

namespace RSA
{
    public class Encryptor
    {
        public List<string> Encrypt(string message, ulong openedKey, ulong module)
        {
            var encryptedMessageBlocks = new List<string>();

            var countOfBlockSizes = (ulong)message.Length > module ? (int)Math.Ceiling(message.Length / (double)module) : 1;
            for (int i = 0; i < countOfBlockSizes; i++)
            {
                var encryptedBlockBuilder = new StringBuilder();

                var block = message.Substring(i * (int)module, (ulong)message.Length < module ? message.Length : (int)module);
                foreach (var symbol in block)
                {
                    var encryptedChar = Math.Pow(symbol, openedKey) % module;
                    encryptedBlockBuilder.Append((char)encryptedChar);
                }
                encryptedMessageBlocks.Add(encryptedBlockBuilder.ToString());
            }

            return encryptedMessageBlocks;
        }

        public List<string> Encrypt(int message, ulong openedKey, ulong module)
        {
            var encryptedMessageBlocks = new List<string>();

            var encryptedChar = Math.Pow(message, openedKey) % module;
            encryptedMessageBlocks.Add(encryptedChar.ToString());

            return encryptedMessageBlocks;
        }
    }
}