using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hec.DssInternal;
namespace test
{
   [TestClass]
   public class UnitTest1
   {
      [TestMethod]
      public void TestHash()
      {
         DssHash h = new DssHash("/RedFox RedFox/0/FLOW/01Jan2000/10Second/GATE/");
         Assert.Equals(8182, h.TableHash);
         Assert.Equals(8182, h.PathHash());
      }
   }
}