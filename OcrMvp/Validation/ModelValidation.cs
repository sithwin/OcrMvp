using System;

namespace OcrMvp.Validation
{
    public class ModelValidation
    {
        private readonly int[] Multiples = { 2, 7, 6, 5, 4, 3, 2 };

        public bool IsValid(object value)
        {
            string nric = value.ToString();
            if (nric.StartsWith("S") || nric.StartsWith("T"))
            {
                return IsNRICValid(nric);
            }
            if (nric.StartsWith("F") || nric.StartsWith("G"))
            {
                return IsFINValid(nric);
            }
            return true;
        }
        public bool IsNRICValid(string nric)
        {
            if (string.IsNullOrEmpty(nric))
            {
                return false;
            }

            //	check length must be 9 digits
            if (nric.Length != 9)
            {
                return false;
            }

            int total = 0
                , count = 0
                , numericNric;
            char first = nric[0]
                , last = nric[nric.Length - 1];

            // first chat alwatas T or S
            if (first != 'S' && first != 'T')
            {
                return false;
            }

            // ensure first chars is char and last

            if (!int.TryParse(nric.Substring(1, nric.Length - 2), out numericNric))
            {
                return false;
            }

            while (numericNric != 0)
            {
                total += numericNric % 10 * Multiples[Multiples.Length - (1 + count++)];

                numericNric /= 10;
            }

            char[] outputs;
            // first S, pickup different array( read specification)
            if (first == 'S')
            {
                outputs = new char[] { 'J', 'Z', 'I', 'H', 'G', 'F', 'E', 'D', 'C', 'B', 'A' };
            }
            // T pickup different arrary ( read specification)
            else
            {
                outputs = new char[] { 'G', 'F', 'E', 'D', 'C', 'B', 'A', 'J', 'Z', 'I', 'H' };
            }

            return last == outputs[total % 11];
        }

        public bool IsFINValid(string fin)
        {
            if (string.IsNullOrEmpty(fin))
            {
                return false;
            }

            //	check length
            if (fin.Length != 9)
            {
                return false;
            }

            int total = 0
                , count = 0
                , numericNric;
            char first = fin[0]
                , last = fin[fin.Length - 1];

            if (first != 'F' && first != 'G')
            {
                return false;
            }

            if (!int.TryParse(fin.Substring(1, fin.Length - 2), out numericNric))
            {
                return false;
            }

            while (numericNric != 0)
            {
                total += numericNric % 10 * Multiples[Multiples.Length - (1 + count++)];

                numericNric /= 10;
            }

            char[] outputs;
            if (first == 'F')
            {
                outputs = new char[] { 'X', 'W', 'U', 'T', 'R', 'Q', 'P', 'N', 'M', 'L', 'K' };
            }
            else
            {
                outputs = new char[] { 'R', 'Q', 'P', 'N', 'M', 'L', 'K', 'X', 'W', 'U', 'T' };
            }

            return last == outputs[total % 11];
        }

    }
}
