using System.Text;

namespace Autoclavable
{
    class Cryptographer
    {
        public string Encrypt(string message, int key, string alphabet)
        {
            var lastKey = key;

            var builder = new StringBuilder();
            for (int i = 0; i < message.Length; i++)
            {
                var currentChar = message[i];
                var numberOfChar = alphabet.IndexOf(currentChar);

                var newNumber = (numberOfChar + lastKey) % alphabet.Length;
                var newChar = alphabet[newNumber];
                builder.Append(newChar);

                lastKey = numberOfChar;
            }
            return builder.ToString().ToUpper();
        }

        public string[] Decrypt(string encodedMessage, string alphabet)
        {
            var lowerEncodedMessage = encodedMessage.ToLower();

            var alphabetLength = alphabet.Length;
            var decryptedMessages = new string[alphabetLength];
            for (int i = 0; i < alphabetLength; i++)
            {
                var lastKey = i;

                var builder = new StringBuilder();
                for (int j = 0; j < lowerEncodedMessage.Length; j++)
                {
                    var currentChar = lowerEncodedMessage[j];
                    var numberOfChar = alphabet.IndexOf(currentChar);

                    var newNumber = (numberOfChar - lastKey) % alphabet.Length;
                    if (newNumber < 0)
                        newNumber += alphabetLength;
                    var newChar = alphabet[newNumber];
                    builder.Append(newChar);

                    lastKey = newNumber;
                }
                decryptedMessages[i] = builder.ToString();
            }
            return decryptedMessages;
        }
    }
}
