using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.DssInternal
{
    internal class Decoder
    {
        byte[] data;
        public Decoder(byte[] data)
        {
            this.data = data;
        }
        public int Length
        {
            get { return data.Length; }
        }
        public string String(int word, int count, int wordSize = 8)
        {
            //var z =BitConverter.ToString(permanantSection, keys.kdss, 4);
            var rval = System.Text.Encoding.ASCII.GetString(data, word * wordSize, count);
            return rval;
        }
        public int Integer(long word, int wordSize = 8)
        {
            return BitConverter.ToInt32(data, (int)word * wordSize);
        }
        public (int, int) Integers(long word, int wordSize = 8)
        {
            var a = BitConverter.ToInt32(data, (int)word * wordSize);
            var b = BitConverter.ToInt32(data, (int)word * wordSize + 4);
            return (a, b);
        }
        public long Long(long word, int wordSize = 8)
        {
            return BitConverter.ToInt64(data, (int)word * wordSize);
        }

        public AddressInfo GetAddressInfo(long wordAddress, long wordSize)
        {
            return new AddressInfo(Long(wordAddress), (int)Long(wordSize));
        }

        public DateTime UnixEpochDateTime(long word, int wordSize = 8)
        {
            long epochs = Long(word, wordSize);
            DateTime baseDate = new DateTime(1970, 1, 1);
            DateTime date = baseDate.AddMilliseconds(epochs);
            return date;
        }

        public float Float(int word)
        {
            return BitConverter.ToSingle(data, word * 4);
        }

        public (float, float) Floats(int word)
        {
            float a = BitConverter.ToSingle(data, word * 8);
            float b = BitConverter.ToSingle(data, (word * 8) + 4);
            return (a, b);
        }

        //public DateTime JulianDateTime(long word, int wordSize = 8)
        //  {
        //      (int, int) integers = Integers(word, wordSize);
        //      int day = integers.Item1;
        //      int seconds = integers.Item2;

        //  }
    }
}
