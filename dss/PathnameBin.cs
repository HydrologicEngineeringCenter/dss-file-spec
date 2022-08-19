using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.DssInternal
{
   internal class PathnameBin
   {

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
      private Decoder decoder;
      public PathnameBin(byte[] data)
      {
         decoder = new Decoder(data);
      }


      public BinItem FindBinItem(string pathname)
      {
         //TO DO loop until path matches.
         long pathHash = decoder.Long(0);
         RecordStatus status = (RecordStatus)decoder.Integer(1);
         (int numChars, int size) = decoder.Integers(2);
         var infoAddress = decoder.Long(3);
         (int dataType, int sortSequence) = decoder.Integers(4);
         DateTime lastWriteTime = decoder.UnixEpochDateTime(5);
         (int startJulian, int endJulian) = decoder.Integers(6);
         string path = decoder.String(7, numChars);
         
         int pathWords = WordMath.WordsInString(path);
         long nextPathnameHash = decoder.Long(8 + pathWords);

         Console.WriteLine("pathHash:" + pathHash);
         Console.WriteLine("status: " + status.ToString());
         Console.WriteLine("numChars:" + numChars);
         Console.WriteLine("size: " + size);
         Console.WriteLine("info address: " + infoAddress);
         Console.WriteLine("data type: " + dataType);
         Console.WriteLine("sort sequence: " + sortSequence);
         Console.WriteLine("lastwrite: " + lastWriteTime);
         Console.WriteLine("julian start: " + startJulian);
         Console.WriteLine("julian end: " + endJulian);
         Console.WriteLine("pathname: " + path);
         Console.WriteLine("next pathname hash: " + nextPathnameHash);
         // TO Do path must match pathname

         var item = new BinItem(pathHash, status, numChars, size, infoAddress, dataType, sortSequence, lastWriteTime, startJulian, endJulian, nextPathnameHash);

         return item;
      }
   }
}
