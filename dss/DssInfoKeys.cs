using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.DssInternal
{
   internal static class DssInfoKeys
	{
			//     kinfoFlag points to a flag indicating a information block (=-9753)
			public static readonly int kinfoFlag = 0;

			//     kinfoStatus points to this records status
			public static int kinfoStatus = kinfoFlag + 1;

			//     kinfoPathnameLength points to the length of the pathname for this record
			public static readonly int kinfoPathnameLength = kinfoStatus + 1;

			//     kinfoHash is the pathname hash, for easy verification of correct path
			public static readonly int kinfoHash = kinfoPathnameLength + 1;

			// data type, version
			// Type points to the data type (e.g., time-series)
			// Version points to the record version number (number times written)
			public static readonly int kinfoTypeVersion = kinfoHash + 1;

			//  number expansions, expansion flag
			//   Expansion points to the number of times this record was expanded
			//  (written to again, but number data or header larger than on disk
			public static readonly int kinfoExpansion = kinfoTypeVersion + 1;

			//     kinfoLastWriteTime points to the date and time last written to in seconds
			public static readonly int kinfoLastWriteTime = kinfoExpansion + 1;

			//  Up to 16 characters for the program name
			public static readonly int kinfoProgram = kinfoLastWriteTime + 1;

			//     First points to the Julian date of the first valid data, Last, last valid data
			//  (non-missing) for times series data.  0 if not calculated or other
			public static readonly int kinfoFirstDate = kinfoProgram + WordMath.StringLengthToWords(Constants.MAX_PROGRAM_NAME_LENGTH);

			public static readonly int kinfoLastDate = kinfoFirstDate + 1;

			//  When the record was originally created
			public static readonly int kinfoCreationTime = kinfoLastDate + 1;

			//  Unused (used to be password for record)
			public static readonly int kinfoReserved1 = kinfoCreationTime + 1;


			//     kinfoInternalHeadAddress points to the address of the internal header
			public static readonly int kinfoInternalHeadAddress = kinfoReserved1 + 1;

			//     kinfoInternalHeadNumber points to the length of the internal header, in int 4 words
			public static readonly int kinfoInternalHeadNumber = kinfoInternalHeadAddress + 1;

			//     kinfoHeader2Address points to the address of the compression header
			public static readonly int kinfoHeader2Address = kinfoInternalHeadNumber + 1;

			//     kinfoHeader2Number points to the length of the compression header, int 4 words
			public static readonly int kinfoHeader2Number = kinfoHeader2Address + 1;

			//     kinfoUserHeadAddress points to the address of the User's header
			public static readonly int kinfoUserHeadAddress = kinfoHeader2Number + 1;

			//     kinfoUserHeadNumber points to the length of the User's header, int 4 words
			public static readonly int kinfoUserHeadNumber = kinfoUserHeadAddress + 1;

			//     kinfoValues1Address points to the data location for the first data array
			public static readonly int kinfoValues1Address = kinfoUserHeadNumber + 1;

			//     kinfoValues1Number points to the used length of the first data array
			public static readonly int kinfoValues1Number = kinfoValues1Address + 1;

			//     kinfoValues2Address points to the data location for the second data array
			public static readonly int kinfoValues2Address = kinfoValues1Number + 1;

			//     kinfoValues2Number points to the used length of the second data array
			public static readonly int kinfoValues2Number = kinfoValues2Address + 1;

			//     kinfoValues3Address points to the address of the third data array
			public static readonly int kinfoValues3Address = kinfoValues2Number + 1;

			//     kinfoValues3Number points to the size of the third data array
			public static readonly int kinfoValues3Number = kinfoValues3Address + 1;

			//  values1Number + values2Number + values3Number = size, in ints, to store
			//  totalAllocatedSize - size, in ints, to allocate on disk
			//      normally totalAllocatedSize = values1Number + values2Number + values3Number, but if the record
			//      will be expanding much (due to real-time storage), the
			//	    totalAllocatedSize might be the final expected length
			//  numberValues - the number of values represented by the data
			//  logicalNumberValues - the fully expanded data set; what the user finally sees


			//     kinfoAllocatedSize points to the allocated space
			//     This may be greater than kinfoValues1Number to allocate space for expansion
			public static readonly int kinfoAllocatedSize = kinfoValues3Number + 1;

			public static readonly int kinfoNumberData = kinfoAllocatedSize + 1;

			//     kinfoLogicalNumber points to the logical number of data (e.g., uncompressed)
			public static readonly int kinfoLogicalNumber = kinfoNumberData + 1;


			//     kinfoAliasesBinAddress is the number of aliases pointing to this record, addresses follow path
			public static readonly int kinfoAliasesBinAddress = kinfoLogicalNumber + 1;

			//     KIUNUS is unused space in the record
			public static readonly int kinfoReserved = kinfoAliasesBinAddress + 1;

			//     kinfoPathname points to the pathname for this record
			public static readonly int kinfoPathname = kinfoReserved + 1;

			//     compute the length of the information block, less the pathname and alias addresses
			public static readonly int infoSize = kinfoPathname - kinfoFlag;

			//  Max info size allocated (includes 20 spots for alias addresses)
			public static readonly int maxInfoSize = infoSize + WordMath.StringLengthToWords(Constants.MAX_PATHNAME_SIZE) + 20;


		
   }
}
