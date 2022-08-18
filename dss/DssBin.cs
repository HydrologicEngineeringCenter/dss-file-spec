using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.DssInternal
{
   internal class DssBin
   {
      enum BinStatus { Unused=0,Good=1, Alias=2, Deleted=11, Renamed=12};

      /*zdssBinKeys.kbinHash = 0;
      zdssBinKeys.kbinStatus = 1;
      zdssBinKeys.kbinPathLen = 2;
      zdssBinKeys.kbinInfoAdd = 3;
      zdssBinKeys.kbinTypeAndCatSort = 4;
      zdssBinKeys.kbinLastWrite = 5;
      zdssBinKeys.kbinDates = 6;
      zdssBinKeys.kbinPath = 7;

      //  the number of words used in the bin for each record, excluding the pathname
      //  Do not change this value - it is critical.
      zdssBinKeys.kbinSize = zdssBinKeys.kbinPath - zdssBinKeys.kbinHash;
      */
      public DssBin(byte[] data)
      {
         Decoder d = new Decoder(data);

         long pathHash =d.Long(0);
         BinStatus status =(BinStatus) d.Integer(1);
         (int numChars, int size) = d.Integers(2);
         var infoAddress = d.Long(3);
         (int dataType, int sortSequence) = d.Integers(4);
         DateTime laswWriteTime = d.DateTime(5);
         (int startJulian, int endJulian) = d.Integers(6);
         string pathname = d.String(7,numChars);
         long nextPathnameHash = d.Long(8);
         
         Console.WriteLine("pathHash:" +pathHash);
         Console.WriteLine("status: " + status.ToString());
         Console.WriteLine("numChars:" +numChars);
         Console.WriteLine("size: "+size);
         Console.WriteLine("info address: "+infoAddress);
         Console.WriteLine("data type: "+dataType);
         Console.WriteLine("sort sequence: "+sortSequence);
         Console.WriteLine("lastwrite: "+ laswWriteTime);
         Console.WriteLine("julian start: " + startJulian);
         Console.WriteLine("julian end: "+endJulian);
         Console.WriteLine("pathname: "+pathname);
         Console.WriteLine("next pathname hash: "+nextPathnameHash);

      }
   }
}
