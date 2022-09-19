using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.Dss.IO
{
   internal struct AddressInfo
   {
      public long Address;
      private int rawSize;

      public int Size {
         get {
            if (wordSize == 4)
            {
               bool odd = rawSize % 2 != 0;
               int rval = rawSize / 2;
               if (odd) rval++;
               return rval;
            }
            else if( wordSize == 8)
            {
               return rawSize;
            }
            else
            {
               throw new Exception("only 4 and 8 byte word sizes are supported");
            }
         }
         set { rawSize = value; } 
      }

      private int wordSize;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="address"></param>
      /// <param name="size"></param>
      /// <param name="wordSize">size of word in bytes (4 or 8)</param>
      public AddressInfo(long address, int size, int wordSize = 8)
      {
         this.Address = address;
         this.rawSize = size;
         this.wordSize=wordSize;
      }
   }
}
