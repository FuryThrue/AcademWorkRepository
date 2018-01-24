using System.Linq;

namespace Vigener
{
    static class CommonOperations
    {
        public static int[] getK(string key, string text, string ab)
        {
            var result = new int[text.Count()];
            if (key.Count() < text.Count())
            {
                for (int i = 0; i < text.Count() / key.Count(); i++)
                    for (int j = 0; j < key.Count(); j++)
                        result[i * key.Count() + j] = getIndexChar(key[j], ab);
                for (int i = text.Count() / key.Count() * key.Count(); i < text.Count(); i++)
                    result[i] = getIndexChar(key[i - text.Count() / key.Count() * key.Count()], ab);
            }
            else
                for (int i = 0; i < text.Count(); i++)
                    result[i] = getIndexChar(key[i], ab);

            return result;

        }

        public static int[] getP(string text, string ab)
        {
            var result = new int[text.Count()];
            for (int i = 0; i < text.Count(); i++)
                result[i] = getIndexChar(text[i], ab);

            return result;
        }

        static int getIndexChar(char inChar, string ab)
        {
            for (int i = 0; i < ab.Count(); i++)
                if (ab[i] == inChar)
                    return i;

            return -1;
        }
    }
}
