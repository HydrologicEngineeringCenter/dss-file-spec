using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Hec.Dss.IO
{
   /// <summary>
   /// S3Reader supports reading a range of bytes from a S3 object
   /// </summary>
   public class S3Reader
   {
      // Specify your bucket region (an example region is shown).
      private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest1;

  
      //https://stackoverflow.com/questions/34144494/how-can-i-get-the-bytes-of-a-getobjectresponse-from-s3
      private static byte[] ReadStream(Stream responseStream)
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
      public static async Task<byte[]> ReadBytes(string bucketName, string objectName, long wordOffset, int wordCount, int wordSize = 8)
      {
         IAmazonS3 client = new AmazonS3Client(bucketRegion);
         var rval = new byte[0];
         try
         {
            GetObjectRequest request = new GetObjectRequest
            {
               BucketName = bucketName,
               Key = objectName,
               ByteRange = new ByteRange(wordOffset, (wordCount * wordSize) - 1)
         };
            var size = request.ByteRange.End - request.ByteRange.Start;
            using (GetObjectResponse response = await client.GetObjectAsync(request))
            {
               using (Stream responseStream = response.ResponseStream)
               {
                  rval = ReadStream(responseStream);
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
         return rval;
      }
   }
}
