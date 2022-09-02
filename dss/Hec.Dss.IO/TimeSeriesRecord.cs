using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.Dss.IO
{
   internal class TimeSeriesRecord
   {
      RecordInfo recordInfo;
      ByteReader reader;
      TimeSeriesInternalHeader internalHeader;
      public TimeSeriesRecord(RecordInfo r, ByteReader reader)
      {
         recordInfo = r;
         this.reader = reader;
         var rawInternalHeader= reader(r.InternalHeaderAddress.Address, r.InternalHeaderAddress.Size);
         internalHeader = new TimeSeriesInternalHeader(rawInternalHeader); 
         
        
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
