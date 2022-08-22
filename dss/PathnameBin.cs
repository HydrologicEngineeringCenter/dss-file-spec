using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.DssInternal
{
   internal class PathnameBin
   {

      const int kbinHash = 0;
      const int kbinStatus = 1;
      const int kbinPathLen = 2;
      const int kbinInfoAdd = 3;
      const int kbinTypeAndCatSort = 4;
      const int kbinLastWrite = 5;
      const int kbinDates = 6;
      const int kbinPath = 7;

      //  the number of words used in the bin for each record, excluding the pathname
      //  Do not change this value - it is critical.
      const int kbinSize = kbinPath - kbinHash;
      private Decoder decoder;
      public PathnameBin(byte[] data)
      {
         decoder = new Decoder(data);
      }


      public BinItem FindBinItem(string pathname)
      {
         //TO DO loop until path matches.
         long pathHash = decoder.Long(kbinHash);
         RecordStatus status = (RecordStatus)decoder.Integer(kbinStatus);
         (int numChars, int size) = decoder.Integers(kbinPathLen);
         var infoAddress = decoder.Long(kbinInfoAdd);
         (int dataType, int sortSequence) = decoder.Integers(kbinTypeAndCatSort);
         DateTime lastWriteTime = decoder.UnixEpochDateTime(kbinLastWrite);
         (int startJulian, int endJulian) = decoder.Integers(kbinDates);
         string path = decoder.String(kbinPath, numChars);
         
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
