using System;
using System.Text;

namespace RSA
{
    public class Decryptor
    {
        public string Decrypt(string message, double secretKey, ulong module)
        {
            var decryptedBlockBuilder = new StringBuilder();

            var countOfBlockSizes = (ulong)message.Length > module ? (int)Math.Ceiling(message.Length / (double)module) : 1;
            for (int i = 0; i < countOfBlockSizes; i++)
            {
                var block = message.Substring(i * (int)module, (ulong)message.Length < module ? message.Length : (int)module);
                foreach (var symbol in block)
                {
                    var decryptedChar = Math.Pow(symbol, secretKey) % module;
                    decryptedBlockBuilder.Append((char)decryptedChar);
                }
            }

            return decryptedBlockBuilder.ToString();
        }

        public string Decrypt(int message, double secretKey, ulong module)
        {
            var decryptedChar = Math.Pow(message, secretKey) % module;

            return decryptedChar.ToString();
        }
    }
}