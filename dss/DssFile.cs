using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.DssInternal
{
   public class DssFile
   {
      String fileName;
      Decoder fileHeader = new Decoder(new byte[0]);
      Decoder tableHash = new Decoder(new byte[0]);   
      DssFileKeys keys;
      public DssFile(String fileName)
      {
         this.fileName = fileName;
         keys = new DssFileKeys();
         ReadFileHeader();
         if (!Valid())
            throw new Exception("Invalid DSS file");
      }

       bool Valid()
      {
         if (fileHeader.Length == 0 ) 
            return false;
         var z = fileHeader.String(keys.kdss, Constants.DSS_FILE_KEY.Length);
         
         return z == Constants.DSS_FILE_KEY;
      }
      public void PrintInfo()
      {
         // permanantSection[keys.kfileSize] 

         //       binAddress = fileHeader[zdssFileKeys.kaddFirstBin];
         //     pathnameBin = (long long*)ifltab[zdssKeys.kpathBin];
         double mbytes = fileHeader.Long(keys.kfileSize) * 8.0 / 1024.0/1024.0;

         Console.WriteLine("                        Number records:         "+ fileHeader.Long(keys.knumberRecords));
         Console.WriteLine("                        File size:              " + fileHeader.Long(keys.kfileSize) + " 64-bit words");
         Console.WriteLine("                        File size:              " + mbytes.ToString("f1") + " Mb");
         Console.WriteLine("                        Dead space:             " + fileHeader.Long(keys.kdead));
         Console.WriteLine("                        Hash range:             " + fileHeader.Long(keys.kmaxHash));
         Console.WriteLine("                        Number hash used:       " + fileHeader.Long(keys.khashsUsed));
         Console.WriteLine("                        Max paths for hash:     " + fileHeader.Long(keys.kmaxPathsOneHash));
         Console.WriteLine("                        Corresponding hash:     " + fileHeader.Long(keys.kmaxPathsHashCode));
         Console.WriteLine("                        Number non unique hash: " + fileHeader.Long(keys.khashCollisions));
         Console.WriteLine("                        Number bins used:       " + fileHeader.Long(keys.ktotalBins));
         Console.WriteLine("                        Number overflow bins:   " + fileHeader.Long(keys.kbinsOverflow));
         //Console.WriteLine("                        Number physical reads:  " + ifltab.Long(keys.knumberReads));
         //Console.WriteLine("                        Number physical writes: " + ifltab.Long(keys.knumberWrites));
         //Console.WriteLine("                        Number denied locks:    " + ifltab.Long(keys.klocksDenied));
         Console.WriteLine("                        address of first bin:   " + fileHeader.Long(keys.kaddFirstBin));
         Console.WriteLine("                        start of hash table :   " + fileHeader.Long(keys.kaddHashTableStart));

      }
      void ReadFileHeader()
      {
         if (File.Exists(fileName))
         {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
               using (BinaryReader r = new BinaryReader(stream, Encoding.UTF8))
               {
                  int headerSize = 100 * 8;
                  fileHeader = new Decoder(r.ReadBytes(headerSize)); // todo read header size from near beginning of file.

                  // load the hash table into memory.
                  long hashSize = fileHeader.Long(keys.kmaxHash);
                  r.BaseStream.Seek(fileHeader.Long(keys.kaddHashTableStart), SeekOrigin.Begin);
                  tableHash = new Decoder(r.ReadBytes((int)hashSize * 8));
                  // TO Do.   catalog. read pathname bin
                  // zcatalogInternal.c -- need some keys.
                  //binAddress = fileHeader[zdssFileKeys.kaddFirstBin];
                  // binAddress = 9732


               }
            }
         }

      }
   }
}
