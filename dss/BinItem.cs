using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.DssInternal
{
   internal class BinItem
   {
      public BinItem(long pathHash, RecordStatus status, int numChars, int size, long infoAddress, int dataType, int sortSequence, DateTime lastWriteTime, int startJulian, int endJulian, string path, long nextPathnameHash)
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
         Path = path;
         NextPathnameHash = nextPathnameHash;
      }
      public void Print()
      {
         Console.WriteLine("pathHash:" + PathHash);
         Console.WriteLine("status: " + Status.ToString());
         Console.WriteLine("numChars:" + NumChars);
         Console.WriteLine("size: " + Size);
         Console.WriteLine("info address: " + InfoAddress);
         Console.WriteLine("data type: " + DataType);
         Console.WriteLine("sort sequence: " + SortSequence);
         Console.WriteLine("lastwrite: " + LastWriteTime);
         Console.WriteLine("julian start: " + StartJulian);
         Console.WriteLine("julian end: " + EndJulian);
         Console.WriteLine("pathname: " + Path);
         Console.WriteLine("next pathname hash: " + NextPathnameHash);
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
      public string Path { get; private set; }
      public long NextPathnameHash { get; }
   }
}
