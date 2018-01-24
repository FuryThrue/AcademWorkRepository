using System.Linq;

namespace Vigener
{
    class Encryptor
    {
        public string Encrypt(string text, string key, string ab)
        {
            var result = "";

            var c = getC(text, key, ab);
            foreach (int index in c)
                result += ab[index];

            return result;
        }

        static int[] getC(string text, string key, string ab)
        {
            var result = new int[text.Count()];

            var k = CommonOperations.getK(key, text, ab);
            var p = CommonOperations.getP(text, ab);

            for (int i = 0; i < text.Count(); i++)
                result[i] = (p[i] + k[i]) % ab.Count();

            return result;
        }
    }
}
