using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.Dss.IO
{

   internal class TimeSeriesInternalHeader
   {
      //  Time series internal header
      public bool TimeGranualityFlag { get; set; }
      public int Precision { get; private set; }
      public int NumberProfileDepths { get; }
      public int TimeOffsetSeconds { get; }
      public int BlockStartPosition { get; }
      public int BlockEndPosition { get; }

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
      public TimeSeriesInternalHeader(byte[] rawInternalHeader)
      {
         Decoder d = new Decoder(rawInternalHeader);
         int start = 0;
         TimeGranualityFlag  = d.Integer(start,offset:INT_HEAD_timeGranularity*4) != 0;
         Precision           = d.Integer(start, offset: INT_HEAD_precision * 4);
         TimeOffsetSeconds   = d.Integer(start, offset: INT_HEAD_timeOffset * 4);
         NumberProfileDepths = d.Integer(start, offset: INT_HEAD_profileDepthsNumber * 4);
         BlockStartPosition  = d.Integer(start, offset:INT_HEAD_blockStartPosition * 4);
         BlockEndPosition = d.Integer(start, offset: INT_HEAD_blockEndPosition*4);
 
      }
   }
}
