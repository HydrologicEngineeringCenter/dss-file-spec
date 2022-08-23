using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.DssInternal
{
   internal class FileHeader
   {
      private Decoder decoder;
      private DssFileKeys keys = new DssFileKeys();
      public FileHeader(Decoder decoder)
      {
         this.decoder = decoder;
      }

      /// <summary>
      /// Size of one BinItem
      /// </summary>
      internal int BinSize { 
         get { return (int)decoder.Long(keys.kbinSize); } 
      }

      int BinBlockSize
      {
         get
         {
            return (int)(BinSize * decoder.Integer(keys.kbinsPerBlock)) + 1;
         }
      }
      

      public void PrintInfo()
      {
         double mbytes = decoder.Long(keys.kfileSize) * 8.0 / 1024.0 / 1024.0;

         Console.WriteLine("                        Number records:         " + decoder.Long(keys.knumberRecords));
         Console.WriteLine("                        File size:              " + decoder.Long(keys.kfileSize) + " 64-bit words");
         Console.WriteLine("                        File size:              " + mbytes.ToString("f1") + " Mb");
         Console.WriteLine("                        Dead space:             " + decoder.Long(keys.kdead));
         Console.WriteLine("                        Hash range:             " + decoder.Long(keys.kmaxHash));
         Console.WriteLine("                        Number hash used:       " + decoder.Long(keys.khashsUsed));
         Console.WriteLine("                        Max paths for hash:     " + decoder.Long(keys.kmaxPathsOneHash));
         Console.WriteLine("                        Corresponding hash:     " + decoder.Long(keys.kmaxPathsHashCode));
         Console.WriteLine("                        Number non unique hash: " + decoder.Long(keys.khashCollisions));
         Console.WriteLine("                        Number bins used:       " + decoder.Long(keys.ktotalBins));
         Console.WriteLine("                        Number overflow bins:   " + decoder.Long(keys.kbinsOverflow));
         //Console.WriteLine("                        Number physical reads:  " + ifltab.Long(keys.knumberReads));
         //Console.WriteLine("                        Number physical writes: " + ifltab.Long(keys.knumberWrites));
         //Console.WriteLine("                        Number denied locks:    " + ifltab.Long(keys.klocksDenied));
         Console.WriteLine("                        address of first bin:   " + decoder.Long(keys.kaddFirstBin));
         Console.WriteLine("                        start of hash table :   " + decoder.Long(keys.kaddHashTableStart));

      }
   }
}
