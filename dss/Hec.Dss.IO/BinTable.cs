using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.Dss.IO
{
   /// <summary>
   /// A BinTable contains pathnames that have the same TableHash
   /// </summary>
   internal class BinTable
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
      private long startAddress = 0;
      private int binBlockSize = 0;
      public BinTable(long startAddress, int binBlockSize, ByteReader reader )
      {
         this.startAddress = startAddress;
         this.binBlockSize = binBlockSize;
         decoder = new Decoder(reader(startAddress, binBlockSize));
      }

      public IEnumerable<BinItem> GetBins()
      {
         int counter = 0;
         while(counter < binBlockSize )
         {
            long pathHash = decoder.Long(kbinHash+counter);
            if( pathHash ==0)
               yield break;
            if( counter >0)
               Console.WriteLine("two bins in a block?");
            RecordStatus status = (RecordStatus)decoder.Integer(kbinStatus+counter);
            (int numChars, int size) = decoder.Integers(kbinPathLen+counter);
            var infoAddress = decoder.Long(kbinInfoAdd+counter);
            (int dataType, int sortSequence) = decoder.Integers(kbinTypeAndCatSort+counter);
            DateTime lastWriteTime = decoder.UnixEpochDateTime(kbinLastWrite+counter);
            (int startJulian, int endJulian) = decoder.Integers(kbinDates+counter);
            string path = decoder.String(kbinPath+counter, numChars);
            int pathWords = WordMath.WordsInString(path);
            long nextPathnameHash = decoder.Long(8 + pathWords);

            var item = new BinItem(pathHash, status, numChars, size, infoAddress, dataType, sortSequence, lastWriteTime, startJulian, endJulian, path,nextPathnameHash);
            yield return item;
            counter += kbinSize + pathWords;
         }

      }

      internal BinItem FindBinItem(string pathname)
      {
         foreach (BinItem item in GetBins())
         {
            if( ( item.Status == RecordStatus.Good|| item.Status == RecordStatus.Alias )
               && String.Compare(item.Path, pathname, StringComparison.OrdinalIgnoreCase) == 0
               )
            return item;
         }

         return null;
      }
   }
}
