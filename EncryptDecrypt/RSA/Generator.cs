using System;

namespace RSA
{
    class Generator
    {
        private ulong _p, _q, _module, _f, _openKey;
        private double _secretKey;
        private int _byteSizeOfNUMBER;

        public Generator(int byteSizeOfNUMBER)
        {
            _byteSizeOfNUMBER = byteSizeOfNUMBER;
        }

        public ulong GetModule
        {
            get => _module;
        }

        public Tuple<ulong, double> GenerateKeys()
        {
            GeneratePandQ();
            CalculateModule();
            CalculateEiler();

            _openKey = ChooseE();
            _secretKey = GetD();

            Console.WriteLine($"Module = {_module}");
            Console.WriteLine($"f = {_f}");
            Console.WriteLine($"Opened key = {_openKey}");
            Console.WriteLine($"Closed key = {_secretKey}");

            return Tuple.Create(_openKey, _secretKey);
        }

        private void GeneratePandQ()
        {
            var randomizer = new Random();
            var upperLimit = _byteSizeOfNUMBER / (8*4);

            _p = (uint)randomizer.Next(1, upperLimit);
            do
                _q = (uint)randomizer.Next(1, upperLimit);
            while (_p == _q || _p == 1 || _q == 1);
        }

        private void CalculateModule()
        {
            _module = _p * _q;
        }

        private void CalculateEiler()
        {
            _f = (_p - 1) * (_q - 1);
        }

        private uint ChooseE()
        {
            var randomizer = new Random();
            var e = 0;

            do
                e = randomizer.Next(1, (int)_f);
            while (!CheckKey(e) && e <= 1);

            return (uint)e;
        }

        private double GetD()
        {
            var secretKey = 0;
            while (((ulong)secretKey * _openKey) % _f != 1 || (ulong)secretKey == _openKey)
            {
                secretKey++;
            }
            return secretKey;
        }

        private bool CheckKey(int key)
        {
            var commonFactor = GetCommonFactor(key);
            if (commonFactor > 1)
                return false;
            else
                return true;
        }

        private int GetCommonFactor(int key)
        {
            var alphabetLength = (int)_f;
            var tempKey = key;
            while (alphabetLength != 0 && tempKey != 0)
            {
                if (alphabetLength > tempKey)
                    alphabetLength = alphabetLength % tempKey;
                else
                    tempKey = tempKey % alphabetLength;
            }
            return alphabetLength + tempKey;
        }
    }
}
