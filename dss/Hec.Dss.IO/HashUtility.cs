using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.Dss.IO
{
   /// <summary>
   /// HashUtility has functions for the two hashes used in DSS.
   /// </summary>
   internal class HashUtility
   {
      /// <summary>
      /// TableHash is used as the first level hash of a dss path.
      /// This hash is in the range [0...maxHash]
      /// </summary>
      /// <param name="pathname"></param>
      /// <param name="maxHash"></param>
      /// <returns></returns>
      public static long TableHash(string pathname,int maxHash)
      {
         //string expression = ".*(c:\\d{6})|.*/$";
         //bool isCollection = path.IndexOf("/C:") >= 0;
         var path = pathname.ToUpper();
         int ibit=0;
         double fraction = C.math.frexp((double)maxHash, ref ibit);
         if (fraction == 0.50)
         {
            ibit--;
         }
         //  Find the number of characters that include full number groups
         //  This rounds up to full number of 8 bit char to process when
         //  you treat them in IBIT chunks
         int nt = (((path.Length * 8) - 1) / ibit) + 1;
         nt = (((nt * ibit) - 1) / 8) + 1;

         int it = 0;
         int i2 = 0;
         int ineed = ibit;
         int ihave = 0;
         int ibyte = 0;
         while (true)
         {
            //  Get a group of bits
            if (ihave > 0)
            {
               //  imove = min(ineed, ihave);
               int imove = ineed;
               if (ihave < imove) imove = ihave;
               //  left shift
               i2 = i2 << imove;
               ineed -= imove;
               ihave -= imove;
               if (ineed <= 0)
               {
                  int itp = i2 / 256;
                  i2 %= 256;
                  //Use Exclusive OR to form value
                  it ^= itp;
                  ineed = ibit;
               }
            }
           
            //  Refill a character
            if (ihave <= 0)
            {
               ++ibyte;
               if (ibyte > nt)
               {
                  break;
               }
               //  Use the collection path, if it is one.
               if(ibyte<=path.Length)
                  i2 += path[ibyte - 1];
               ihave = 8;
            }
         }
         //  Compute final table hash
         int tableHash = it % maxHash;
         return tableHash;


      }
      /// <summary>
      /// PathHash is on of the entries in a BinItem.
      /// It provdes ability to compare paths by a number representation.
      /// 
      /// example pathHash result for  the path: //SACRAMENTO/PRECIP-INC/01Jan1879/1Day/OBS/     
      /// is  -5795173172863832242
      /// </summary>
      /// <param name="path"></param>
      /// <returns></returns>
      public static long PathHash(string path)
      {
         long pathHash = 0;
         var pathname = path.ToUpper();
         for (int i = 0; i < pathname.Length; i++)
         {
            pathHash = (31 * pathHash) + (int)pathname[i];
         }
         //  Never allow a pathname bin hash to be zero
         if (pathHash == 0)
            pathHash = 1;

         return pathHash;
      }
   }
}
    

