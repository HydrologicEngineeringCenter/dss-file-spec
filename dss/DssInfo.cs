using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.DssInternal
{
   /// <summary>
   /// record header (info section)
   /// information and addresses for the record.  (This does not include
   /// information about the data, that is in the data's "internal header")
   /// </summary>
   class DssInfo
   {

      Decoder decoder;
      public DssInfo(byte[] data)
      {
         decoder = new Decoder(data);
         long flag = decoder.Long(DssInfoKeys.kinfoFlag);
         if (Constants.DSS_INFO_FLAG != flag)
            throw new Exception("Invalid DSS Info Block");
      }

      public RecordStatus Status
      {
         get
         {
            RecordStatus status = (RecordStatus)decoder.Integer(DssInfoKeys.kinfoStatus);
            return status;
         }
      }

    public AddressInfo InternalHeaderAddress
      {
         get {
            return decoder.GetAddressInfo(DssInfoKeys.kinfoInternalHeadAddress,
                                          DssInfoKeys.kinfoInternalHeadNumber); 
         }
      }
      public AddressInfo InternalHeaderAddress2
      {
         get
         {
            return decoder.GetAddressInfo(DssInfoKeys.kinfoHeader2Address,
                                          DssInfoKeys.kinfoHeader2Number);
         }
      }
      public AddressInfo UserHeaderAddress
      {
         get
         {
            return decoder.GetAddressInfo(DssInfoKeys.kinfoUserHeadAddress,
                                          DssInfoKeys.kinfoUserHeadNumber);
         }
      }
      public AddressInfo Values1Address
      {
         get
         {
            return decoder.GetAddressInfo(DssInfoKeys.kinfoValues1Address,
                                          DssInfoKeys.kinfoValues1Number);
         }
      }
      public AddressInfo Values2Address
      {
         get
         {
            return decoder.GetAddressInfo(DssInfoKeys.kinfoValues2Address,
                                          DssInfoKeys.kinfoValues2Number);
         }
      }
      public AddressInfo Values3Address
      {
         get
         {
            return decoder.GetAddressInfo(DssInfoKeys.kinfoValues3Address,
                                          DssInfoKeys.kinfoValues3Number);
         }
      }

      public String Pathname
      {
         get
         {
            int len = decoder.Integer(DssInfoKeys.kinfoPathnameLength);
            if (len <= 0)
               return "";
            return decoder.String(DssInfoKeys.kinfoPathname, len);
         }
      }
   }
}
