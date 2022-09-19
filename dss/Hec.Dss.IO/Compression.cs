using System.Collections;

namespace Hec.Dss.IO
{
   internal class Compression
   {

      public static double[] UnCompress(double[] rawData, BitArray bitFlags)
      {
         var rval = new List<double>(rawData.Length);
         if (rawData.Length > 0 && bitFlags.Length > 0)
         {
            int counter = 0;
            rval.Add(rawData[counter++]);
            for (int i = 1; i < bitFlags.Length && counter <rawData.Length; i++)
            {
               if (bitFlags[i])
                  rval.Add(rawData[counter-1]);
               else
                  rval.Add(rawData[counter++]);
            }
         }
         return rval.ToArray();
      }
   }
}
