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
      public string String(int word,int length)
      {
         //var z =BitConverter.ToString(permanantSection, keys.kdss, 4);
         var rval = System.Text.Encoding.ASCII.GetString(data, word, length);
         return rval; 
      }
      public int Integer(int word)
      {
         return BitConverter.ToInt32(data, word*8);
      }
      public long Long(int word)
      {
         return BitConverter.ToInt64(data, word * 8);
      }
   }
}
