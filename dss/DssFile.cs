﻿using System;
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
      FileHeader fileHeader1;

      Decoder tableHash = new Decoder(new byte[0]);
      long[] fileHashTable = new long[0];
      DssFileKeys keys;
      public DssFile(String fileName)
      {
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

         fileHeader = new Decoder(ReadBytes(0,headerSize));
         fileHeader1 = new FileHeader(fileHeader);

         int hashSize = (int)fileHeader.Long(keys.kmaxHash);
         long addHashTableStart = fileHeader.Long(keys.kaddHashTableStart);
         tableHash = new Decoder(ReadBytes(addHashTableStart,hashSize));
         fileHashTable = tableHash.LongArray();

      }

      public void PrintCatalog()
      {
         for (int i = 0; i < fileHashTable.Length; i++)
         {
            var address = fileHashTable[i];
            if( address!=0)
            { // look in Bin

            }
         }
      }

      public void PrintInfo()
      {
         // permanantSection[keys.kfileSize] 

         //       binAddress = fileHeader[zdssFileKeys.kaddFirstBin];
         //     pathnameBin = (long long*)ifltab[zdssKeys.kpathBin];

      }

        public void PrintRecord(string path)
        {
            // TO DO. create Record class to do the read.
            var maxHash = (int)fileHeader.Long(keys.kmaxHash);
            var addressToHash = HashUtility.TableHash(path,maxHash);
            
            var address = fileHashTable[addressToHash];
            Console.WriteLine("bin address:" + address);
         //int binSize = (int)fileHeader.Long(keys.kbinSize);
            var bin = new PathnameBin(ReadBytes(address, fileHeader1.BinSize));
            var binItem = bin.FindBinItem(path);

            int wordstoRead = RecordInfo.infoSize + WordMath.WordsInString(path);
            RecordInfo info = new RecordInfo(ReadBytes(binItem.InfoAddress, wordstoRead));
            AddressInfo valueAddressInfo = info.Values1Address;
            int numValues = info.NumberOfValues;
            long valueAddress = valueAddressInfo.Address;
            byte[] data = ReadBytes(valueAddress, numValues);
            Decoder d = new Decoder(data);
            for (int i = 0; i < numValues; i++)
            {
                Console.WriteLine(d.Float(i));
            }

        }

      byte[] ReadBytes(long wordOffset, int wordCount, int wordSize = 8)
      {
         byte[] rval = new byte[0];

         if (fileName.StartsWith("s3:"))
         {
            // read from AWS/S3
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
