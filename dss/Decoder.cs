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
      public Decoder(byte[] data )
      {
         this.data = data;
      }
      public int Length {
         get { return data.Length; }
      }
      public string String(int word,int count, int wordSize=8)
      {
         //var z =BitConverter.ToString(permanantSection, keys.kdss, 4);
         var rval = System.Text.Encoding.ASCII.GetString(data, word*wordSize, count);
         return rval; 
      }
      public int Integer(long word, int wordSize=8)
      {
         return BitConverter.ToInt32(data,(int) word*wordSize);
      }
      public (int ,int) Integers(long word, int wordSize = 8)
      {
         var a = BitConverter.ToInt32(data, (int)word * wordSize);
         var b = BitConverter.ToInt32(data, (int)word * wordSize + 4);
         return (a, b);
      }
      public long Long(long word, int wordSize =8)
      {
         return BitConverter.ToInt64(data,(int) word*wordSize );
      }
   }
}
