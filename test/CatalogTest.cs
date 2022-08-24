using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hec.DssInternal;
using System.IO;
using System;

namespace test
{
   [TestClass]
   public class CatalogTest
   {
      [TestMethod]
      public void TestCatalog()
      {
         string path = Directory.GetCurrentDirectory();
         Console.WriteLine("The current directory is {0}", path);
         DssFile dss = new DssFile("sample7.dss");
         dss.PrintCatalog();
      }
   }
}