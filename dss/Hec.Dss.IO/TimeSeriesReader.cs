using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.Dss.IO
{
   internal class TimeSeriesReader
   {
      RecordInfo recordInfo;
      ByteReader reader;
      TimeSeriesInternalHeader internalHeader;
      BitArray compressionBits;
      public TimeSeriesReader(RecordInfo r, ByteReader reader)
      {
         recordInfo = r;
         this.reader = reader;
         var rawInternalHeader= reader(r.InternalHeader.Address, r.InternalHeader.Size);
         internalHeader = new TimeSeriesInternalHeader(rawInternalHeader,r);
         if (internalHeader.ValuesCompressionFlag == (int)TimeSeriesCompression.RepeatingValue)
         {
            var compressionBytes = reader(r.InternalHeader2.Address, r.InternalHeader2.Size);
            compressionBits = new BitArray(compressionBytes);
            int size = internalHeader.BlockEndPosition - internalHeader.BlockStartPosition + 1;
            size = Math.Min(size, compressionBits.Length);
            for (int i = 0; i < size; i++)
            {
               Console.Write(compressionBits[i] ? "1 " : "0 ");
            }
            // values are in data area
            var rawValues = reader(recordInfo.Values1.Address, recordInfo.Values1.Size);
            Decoder d = new Decoder(rawValues);
            bool storedAsFloats = recordInfo.RecordType == RecordType.RegularTimeSeries ? true : false;
            var x = d.DoubleArray(storedAsFloats);
            var values = Compression.UnCompress(x, compressionBits);
            Console.WriteLine();
            for (int i = 0; i < values.Length; i++)
            {
               Console.WriteLine(i+" , "+values[i]);
            }
            
         }
         // User Header may have supplemental -- or vertical datum
         //RecordInfo.InternalHeaderAddress2.
         //r.Values1Address.Address
         /// First Data Area

      }
      public double[] Values
      {
         get
         {
            double[] rval = new double[recordInfo.NumberOfValues];
            
            byte[] data = reader(recordInfo.Values1.Address, recordInfo.NumberOfValues);
            var d = new Decoder(data);
            for (int i = 0; i < recordInfo.NumberOfValues; i++)
            {
               rval[i] = d.Float(i);
            }
            return rval;
         }
      }
   }
}
