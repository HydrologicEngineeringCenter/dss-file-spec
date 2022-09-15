using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.Dss.IO
{
   /// <summary>
   /// DssFile has an API to read TimeSeries data from a DSS file
   /// 
   /// </summary>
   public class DssFile
   {

      String fileName;
      FileHeader fileHeader;

      Decoder tableHash = new Decoder(new byte[0]);
      long[] fileHashTable = new long[0];
      DssFileKeys keys;
      public DssFile(String fileName)
      {
         if (!File.Exists(fileName))
            throw new FileNotFoundException(fileName);

         this.fileName = fileName;
         keys = new DssFileKeys();
         var word = new Decoder(ReadBytes(0, 1));
         var s = word.String(0, Constants.DSS_FILE_KEY.Length);
         if(s != Constants.DSS_FILE_KEY)
            throw new Exception("Invalid DSS file.  File must start with :"+Constants.DSS_FILE_KEY);

         word = new Decoder(ReadBytes(1, 1));
         int headerSize = word.Integer(0);
         if( headerSize != 100)
            throw new Exception("Invalid DSS version 7 file.  Expected Header size of 100");

         // 77265 -- to do -- onlu support that version or higher..

         Console.WriteLine("Reading " + Path.GetFullPath(fileName));
         fileHeader = new FileHeader(new Decoder(ReadBytes(0, headerSize)));

         tableHash = new Decoder(ReadBytes(fileHeader.HashTableAddress,fileHeader.HashSize));
         fileHashTable = tableHash.LongArray();

      }

      public void PrintCatalog()
      {
         int count = 0;
         for (int i = 0; i < fileHashTable.Length; i++)
         {
            var address = fileHashTable[i];
            if( address!=0)
            { // get bin block at this address
               BinTable block = new BinTable(address, fileHeader.BinBlockSize, ReadBytes);
               foreach (BinItem item in block.GetBins())
               {
                  if (item.Valid)
                  {
                     Console.WriteLine(item.Path);
                     count ++;
                  }
             
               }
            }
         }
         Console.WriteLine("catalog count = "+count);
      }

      public void PrintRecord(string path)
      {
         RecordInfo info = GetRecordInfo(path);
         if (info.RecordType == RecordType.RegularTimeSeries)
         {
            Console.WriteLine("Reading TimeSeries '"+path+"'");
            TimeSeriesReader ts = new TimeSeriesReader(info, ReadBytes);
         }
      }
      RecordInfo GetRecordInfo(string path)
      {
         var addressToHash = HashUtility.TableHash(path, fileHeader.HashSize);
         Console.WriteLine("path bin block size: " + fileHeader.BinBlockSize);
         var address = fileHashTable[addressToHash];
         Console.WriteLine("bin address:" + address);
         if( address == 0)
         {
            throw new Exception("Record not Found'"+path+"'");
         }
         var bin = new BinTable(address, fileHeader.BinSize, ReadBytes);
         var binItem = bin.FindBinItem(path);

         int wordstoRead = RecordInfo.infoSize + WordMath.WordsInString(path);
         RecordInfo info = new RecordInfo(ReadBytes(binItem.InfoAddress, wordstoRead));
         return info;
      }
      



      byte[] ReadBytes(long wordOffset, int wordCount, int wordSize = 8)
      {
         byte[] rval = new byte[0];

         if (fileName.Contains(':') && !fileName.Contains(":\\"))
         {
            string s3BucketName = fileName.Split(':')[0];
            string s3ObjectName = fileName.Split(':')[1];
            rval = S3Reader.ReadBytes(s3BucketName, s3ObjectName, wordOffset, wordCount, wordSize).Result;
         }
         else
         {
            if (File.Exists(fileName))
            {
               using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
               {
                  using (BinaryReader r = new BinaryReader(stream, Encoding.UTF8))
                  {
                     r.BaseStream.Seek(wordOffset * wordSize, SeekOrigin.Begin);
                     rval = r.ReadBytes(wordCount * wordSize);
                  }
               }
            }
         }
         return rval;
      }

   }
}
