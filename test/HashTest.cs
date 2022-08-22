using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hec.DssInternal;
namespace test
{
   [TestClass]
   public class HashTest
   {
      const string path = "/RedFox RedFox/0/FLOW/01Jan2000/10Second/GATE/";
      [TestMethod]
      public void TestTableHash()
      {
         int maxHash = 8192;
         Assert.AreEqual(8182, HashUtility.TableHash(path,maxHash));
      }
      [TestMethod]
      public void TestPathHash()
      {
         Assert.AreEqual(3794281770676537607, HashUtility.PathHash(path));
      }

      [TestMethod]
      public void Test_frexp()
      {
         int n = 0;
        var mantissa= C.math.frexp((double)8192, ref n);
         Assert.AreEqual(14, n);

         double f = 123.45;
         int i = 0;
         //http://en.cppreference.com/w/c/numeric/math/frexp
         mantissa = C.math.frexp(f, ref i);
         Assert.AreEqual(0.964453d,mantissa,0.000001);
         Assert.AreEqual(7, i);
      }
   }
}