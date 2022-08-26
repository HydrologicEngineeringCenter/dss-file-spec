using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Hec.DssInternal
{
   public class S3Reader
   {
      private const string bucketName = "hec-dss";
      private const string keyName = "sample7.dss";
      // Specify your bucket region (an example region is shown).
      private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest1;
      private static IAmazonS3 client;

      public static void S3Test()
      {
         client = new AmazonS3Client(bucketRegion);
         ReadObjectDataAsync().Wait();
      }
      //https://stackoverflow.com/questions/34144494/how-can-i-get-the-bytes-of-a-getobjectresponse-from-s3
      public static byte[] ReadStream(Stream responseStream)
      {
         byte[] buffer = new byte[256];
         using (MemoryStream ms = new MemoryStream())
         {
            int read;
            while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
            {
               ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
         }
      }
      static async Task ReadObjectDataAsync()
      {
         string responseBody = "";
         try
         {
            GetObjectRequest request = new GetObjectRequest
            {
               BucketName = bucketName,
               Key = keyName,
               ByteRange = new ByteRange(0, (100 * 8) - 1)
         };
            var size = request.ByteRange.End - request.ByteRange.Start;
            using (GetObjectResponse response = await client.GetObjectAsync(request))
            using (Stream responseStream = response.ResponseStream)
            {
               byte[] header = ReadStream(responseStream);
               FileHeader h = new FileHeader(new Decoder(header));
               for (int i = 0; i < 15; i++)
               {
                  Console.WriteLine(header[i]);
               }

            }
           
         }
         catch (AmazonS3Exception e)
         {
            // If bucket or object does not exist
            Console.WriteLine("Error encountered ***. Message:'{0}' when reading object", e.Message);
         }
         catch (Exception e)
         {
            Console.WriteLine("Unknown encountered on server. Message:'{0}' when reading object", e.Message);
         }
      }
   }
}
