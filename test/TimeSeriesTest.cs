using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hec.Dss.IO;
using System.IO;
using System;

namespace test
{
   [TestClass]
   public class TimeSeriesTest
   {
      [TestMethod]
      public void Simple()
      {
         string path = Directory.GetCurrentDirectory();
         Console.WriteLine("The current directory is {0}", path);
         DssFile dss = new DssFile("workout.dss");
        
         
      }
   }
}