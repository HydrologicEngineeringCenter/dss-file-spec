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
      public int Size;

      public AddressInfo(long address, int size)  
      {
         this.Address = address;
         this.Size = size;
      }
   }
}
