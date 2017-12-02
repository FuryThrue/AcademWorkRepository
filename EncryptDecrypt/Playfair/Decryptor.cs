using System;
using System.Linq;
using System.Text;

namespace Playfair
{
    class Decryptor
    {
        public string Decrypt(string message)
        {
            //TODO подбор ключа
            var key = new string[5,5];

            return Decrypt(message, key);
        }

        public string Decrypt(string message, string[,] key)
        {
            //message = PasteFakeLetters(message.ToLower()).ToUpper();

            var builder = new StringBuilder();
            for (int i = 0; i < message.Length; i += 2)
            {
                var piece = message.Substring(i, 2);
                var encryptedPart = DecryptCouple(piece, key);
                builder.Append(encryptedPart);
            }
            return builder.ToString().ToLower();
        }

        private string DecryptCouple(string twoCharString, string[,] key)
        {
            var dataFirst = GetRowAndColumnOfChar(twoCharString.First(), key);
            var dataSecond = GetRowAndColumnOfChar(twoCharString.Last(), key);

            var firstChar = "";
            var secondChar = "";

            if (dataFirst.Item1 == dataSecond.Item1)
            {
                var firstIndexofColumn = dataFirst.Item2 - 1 < 0 ? key.GetLength(0) - 1 : dataFirst.Item2 - 1;
                firstChar = key[dataSecond.Item1, firstIndexofColumn];

                var secondIndexofColumn = dataSecond.Item2 - 1 < 0 ? key.GetLength(1) - 1 : dataSecond.Item2 - 1;
                secondChar = key[dataSecond.Item1, secondIndexofColumn];
            }
            else if (dataFirst.Item2 == dataSecond.Item2)
            {
                var firstIndexofRow = dataFirst.Item1 - 1 < 0 ? key.GetLength(0) - 1 : dataFirst.Item1 - 1;
                firstChar = key[firstIndexofRow, dataSecond.Item2];

                var secondIndexofRow = dataSecond.Item1 - 1 < 0 ? key.GetLength(1) - 1 : dataSecond.Item1 - 1;
                secondChar = key[secondIndexofRow, dataSecond.Item2];
            }
            else
            {
                firstChar = key[dataFirst.Item1, dataSecond.Item2];
                secondChar = key[dataSecond.Item1, dataFirst.Item2];
            }

            return $"{firstChar}{secondChar}";
        }

        //private string PasteFakeLetters(string message)
        //{
        //    var builder = new StringBuilder();
        //    var lastChar = char.MinValue;
        //    for (int i = 0; i < message.Length; i++)
        //    {
        //        var c = message[i];
        //        if (lastChar == c)
        //            builder.Append((c != 'x' && message[i + 1] != 'x') ? 'x' : 'y');
        //        else
        //            lastChar = c;

        //        builder.Append(c);
        //    }

        //    if (builder.Length % 2 != 0)
        //        builder.Append(lastChar != 'x' ? 'x' : 'y');

        //    return builder.ToString();
        //}

        private Tuple<int, int> GetRowAndColumnOfChar(char c, string[,] key)
        {
            var row = -1;
            var column = -1;

            for (int i = 0; i < key.GetLength(0); i++)
                for (int j = 0; j < key.GetLength(1); j++)
                    foreach (var charOfKey in key[i, j])
                        if (c == charOfKey)
                        {
                            row = i;
                            column = j;
                            break;
                        }

            return Tuple.Create(row, column);
        }
    }
}
