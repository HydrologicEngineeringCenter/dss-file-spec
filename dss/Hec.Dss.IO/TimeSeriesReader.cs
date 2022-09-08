using System;
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
      public TimeSeriesReader(RecordInfo r, ByteReader reader)
      {
         recordInfo = r;
         this.reader = reader;
         var rawInternalHeader= reader(r.InternalHeaderAddress.Address, r.InternalHeaderAddress.Size);
         internalHeader = new TimeSeriesInternalHeader(rawInternalHeader,r); 
         
         if( internalHeader.ValuesCompressionFlag == 1)
         {
         //   r.InternalHeaderAddress2.Address

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
            
            byte[] data = reader(recordInfo.Values1Address.Address, recordInfo.NumberOfValues);
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
