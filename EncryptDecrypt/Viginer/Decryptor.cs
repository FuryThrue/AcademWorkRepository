using System.Linq;

namespace Vigener
{
    class Decryptor
    {
        public string Decrypt(string message, string key, string ab)
        {
            var result = "";
            var m = getM(message, key, ab);
            foreach (int index in m)
                result += ab[index];

            return result;
        }


        private int[] getM(string text, string key, string ab)
        {
            var result = new int[text.Count()];

            var k = CommonOperations.getK(key, text, ab);
            var p = CommonOperations.getP(text, ab);

            for (int i = 0; i < text.Count(); i++)
                result[i] = getMNumber(p[i], k[i], ab);

            return result;
        }

        private int getMNumber(int n1, int n2, string ab)
        {
            var c = (n1 - n2) % ab.Count();
            if (c < 0)
                c = ab.Count() + c;

            return c;
        }
    }
}
