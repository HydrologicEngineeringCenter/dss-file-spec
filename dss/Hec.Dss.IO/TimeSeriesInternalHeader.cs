using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.Dss.IO
{
   /// <summary>
   /// TimeSeriesInternalHeader supports reading the timeseries storage information
   /// This layer defines how to read time series data.
   /// </summary>
   internal class TimeSeriesInternalHeader
   {
      //  Time series internal header
      public bool TimeGranualityFlag { get; set; }
      public int Precision { get; private set; }
      public int NumberProfileDepths { get; }
      public int TimeOffsetSeconds { get; }
      public int BlockStartPosition { get; }
      public int BlockEndPosition { get; }
      public int ValuesNumber { get; }
      public int ValueSize { get; }
      public int ValueElementSize { get; }
      public string Units { get; }
      public int ValuesCompressionFlag { get; }
      public int QualityNumber { get; }
      public int QualityElementSize { get; }
      public int QualityCompressionFlag { get; }
      public int InotesNumber { get; }
      public int InotesElementSize { get; }

      private int inotesCompressionFlag;

      public int CnotesLength { get; }

     const int INT_HEAD_timeGranularity	=	0;
     const int INT_HEAD_precision			=	1;
     const int INT_HEAD_timeOffset			=	2;
     const int INT_HEAD_profileDepthsNumber=	3;
     const int INT_HEAD_blockStartPosition	=	4;
     const int INT_HEAD_blockEndPosition	=	5;
     const int INT_HEAD_valuesNumber		=	6;
     const int INT_HEAD_valueSize			=	7;
     const int INT_HEAD_valueElementSize	=	8;
     const int INT_HEAD_valuesCompressionFlag=	9;
     const int INT_HEAD_qualityNumber			=10;
     const int INT_HEAD_qualityElementSize		=11;
     const int INT_HEAD_qualityCompressionFlag =12;
     const int INT_HEAD_inotesNumber			=13;
     const int INT_HEAD_inotesElementSize		=14;
     const int INT_HEAD_inotesCompressionFlag	=15;
     const int INT_HEAD_cnotesLength			=16;
     const int INT_HEAD_units = 17; 
      public TimeSeriesInternalHeader(byte[] rawInternalHeader, RecordInfo recordInfo)
      {
         Decoder d = new Decoder(rawInternalHeader);
         int start = 0;
         TimeGranualityFlag     = d.Integer(start, offset: 4* INT_HEAD_timeGranularity) != 0;
         Precision              = d.Integer(start, offset: 4* INT_HEAD_precision);
         TimeOffsetSeconds      = d.Integer(start, offset: 4* INT_HEAD_timeOffset );
         NumberProfileDepths    = d.Integer(start, offset: 4* INT_HEAD_profileDepthsNumber);
         BlockStartPosition     = d.Integer(start, offset: 4* INT_HEAD_blockStartPosition );
         BlockEndPosition       = d.Integer(start, offset: 4* INT_HEAD_blockEndPosition);
         ValuesNumber           = d.Integer(start, offset: 4* INT_HEAD_valuesNumber );
         ValueSize              = d.Integer(start, offset: 4* INT_HEAD_valueSize );
         ValueElementSize       = d.Integer(start, offset: 4* INT_HEAD_valueElementSize);
         ValuesCompressionFlag  = d.Integer(start, offset: 4* INT_HEAD_valuesCompressionFlag);
         QualityNumber          = d.Integer(start, offset: 4* INT_HEAD_qualityNumber);
         QualityElementSize     = d.Integer(start, offset: 4* INT_HEAD_qualityElementSize);
         QualityCompressionFlag = d.Integer(start, offset: 4* INT_HEAD_qualityCompressionFlag);
         InotesNumber           = d.Integer(start, offset: 4* INT_HEAD_inotesNumber);
         InotesElementSize      = d.Integer(start, offset: 4* INT_HEAD_inotesElementSize);
         inotesCompressionFlag  = d.Integer(start, offset: 4* INT_HEAD_inotesCompressionFlag);
         CnotesLength           = d.Integer(start, offset: 4* INT_HEAD_cnotesLength);
         ValueElementSize       = d.Integer(start, offset: 4* INT_HEAD_valueElementSize);
         int len = recordInfo.InternalHeader.Size - INT_HEAD_units;
         if (len > 1)
         {
            Units               = d.String(start, len, offset: 4 * INT_HEAD_units).Trim();
         }
         Console.WriteLine(Units);
            
         

 
      }
   }
}
