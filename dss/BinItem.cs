using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.DssInternal
{
   internal class BinItem
   {
      public BinItem(long pathHash, RecordStatus status, int numChars, int size, long infoAddress, int dataType, int sortSequence, DateTime lastWriteTime, int startJulian, int endJulian, long nextPathnameHash)
      {
         PathHash = pathHash;
         Status = status;
         NumChars = numChars;
         Size = size;
         InfoAddress = infoAddress;
         DataType = dataType;
         SortSequence = sortSequence;
         LastWriteTime = lastWriteTime;
         StartJulian = startJulian;
         EndJulian = endJulian;
      }

      public long PathHash { get; }
      public RecordStatus Status { get; }
      public int NumChars { get; }
      public int Size { get; }
      public long InfoAddress { get; }
      public int DataType { get; }
      public int SortSequence { get; }
      public DateTime LastWriteTime { get; }
      public int StartJulian { get; }
      public int EndJulian { get; }
   }
}
