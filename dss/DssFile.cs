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
      DssFileKeys keys;
      public DssFile(String fileName)
      {
         this.fileName = fileName;
         keys = new DssFileKeys();
         ReadHeader();
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
         Console.WriteLine("file header size = " + fileHeader.Integer(keys.kfileHeaderSize));
         Console.WriteLine("number of records = " + fileHeader.Long(keys.knumberRecords));

      }
      void ReadHeader()
      {
         if (File.Exists(fileName))
         {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
               using (BinaryReader r = new BinaryReader(stream, Encoding.UTF8))
               {
                  fileHeader = new Decoder(r.ReadBytes(100)); // todo header size is in the file

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
