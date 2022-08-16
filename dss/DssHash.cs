using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.DssInternal
{
   internal class DssHash
   {
      string path;
      public DssHash(string path)
      {
         this.path = path.ToUpper();

      }

      public long TableHash()
      {

         return 0;
      }
      public long PathHash()
      {
         long pathHash = 0;
         for (int i = 0; i < path.Length; i++)
         {
            pathHash = (31 * pathHash) + path[i];
         }
         //  Never allow a pathname bin hash to be zero
         if (pathHash == 0)
            pathHash = 1;

         return pathHash;
      }
   }
}
    

