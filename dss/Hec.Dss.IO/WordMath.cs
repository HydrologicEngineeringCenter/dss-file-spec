using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.Dss.IO
{
   internal static class WordMath
   {
      public static int WordsInString(String s)
      {
         return StringLengthToWords(s.Length);
      }

      internal static int StringLengthToWords(int length)
      {
         return ((length - 1) / 8) + 1;
      }
   }
}
