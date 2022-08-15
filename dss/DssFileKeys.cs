using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.DssInternal
{
	/// <summary>
	/// zdssFileKeys are positions in the file header (permanent section) for
	///  file related items, such as addresses, sizes and use information
	/// </summary>
	class DssFileKeys
  {
    public DssFileKeys()
    {
			/////////////////////////////////////////////////////////////
			/////////////////////////////////////////////////////////////

			//     Header section file pointers (this area stored on disk)

			//     KDSS points to the identifier 'ZDSS', indicating that this is
			//     a DSS file
			this.kdss = 0;


			//  kfileHeaderSize is the size of the permanent section.
			//  This is a variable so that additional information can be
			//  added to DSS file.  Of course, programs linked with
			//  older libraries will not know what it means, so they will ignore it
			this.kfileHeaderSize = this.kdss + 1;

			//     KVERS is the DSS software version for this file
			//     KVERS must always remain in the same location in the file
			//     so past and future versions can recognize the file as DSS

			this.kversion = this.kfileHeaderSize + 1;

			//     KNRECS points to the number of records in the file
			this.knumberRecords = this.kversion + 1;

			//  Number of alias pathnames
			this.knumberAliases = this.knumberRecords + 1;

			//     kfileSize is one word greater than the file size (in words)
			//		It is the next area to write, and should contain
			//		the end of file flag
			this.kfileSize = this.knumberAliases + 1;


			//     KDEAD is the dead space pointer
			this.kdead = this.kfileSize + 1;

			//  The number of records that have been expanded since squeeze
			//  (includes same record being expanded multiple times)
			this.knumberExpansions = this.kdead + 1;

			this.knumberCollections = this.knumberExpansions + 1;

			//  Number of records renamed since last squeeze
			this.knumberRenames = this.knumberCollections + 1;

			//  Number of record deleted since last squeeze
			this.knumberDeletes = this.knumberRenames + 1;

			//  Number of aliases deleted since last squeeze
			this.knumberAliasDeletes = this.knumberDeletes + 1;

			//     KCREAT pointes to the date/time the file was created
			this.kcreateDate = this.knumberAliasDeletes + 1;

			//     klastWriteTime points to the time in mills the file was last written to
			this.klastWriteTime = this.kcreateDate + 1;

			//     klockAddressWord points to the word position of the lock record
			this.klockAddressWord = this.klastWriteTime + 1;

			//     kmaxHash points to the maximum hash value for this file
			this.kmaxHash = this.klockAddressWord + 1;

			//     KHUSED are the number of hash codes used.  This will always be
			//     less than or equal to kmaxHash
			this.khashsUsed = this.kmaxHash + 1;

			//     KMAXPH is the maximum number of pathnames for any one table hash code
			this.kmaxPathsOneHash = this.khashsUsed + 1;

			//     KMAXHC is the hash code for this
			this.kmaxPathsHashCode = this.kmaxPathsOneHash + 1;

			//     KAHASH points to the address of the hash table
			this.kaddHashTableStart = this.kmaxPathsHashCode + 1;

			// khashCollisions is the number of non-unique pathname hashes (for
			// a single table hash).  This is the number of additional info areas,
			// and should be either zero or a very small number
			this.khashCollisions = this.kaddHashTableStart + 1;

			//     kbinsPerBlock points to the number of bins per block (except
			//     for the first block).
			this.kbinsPerBlock = this.khashCollisions + 1;

			//     kbinsRemainInBlock points to the number of bins remaining in the current block
			this.kbinsRemainInBlock = this.kbinsPerBlock + 1;

			//     kbinSize points to the size of the pathname bin (in words)
			this.kbinSize = this.kbinsRemainInBlock + 1;

			//     kaddFirstBin points to the location of the first bin
			this.kaddFirstBin = this.kbinSize + 1;

			//     kaddNextEmptyBin points to the location of the next empty bin
			this.kaddNextEmptyBin = this.kaddFirstBin + 1;

			//     File efficiency variables

			//     ktotalBins indicates the number of pathname bins used in the file
			this.ktotalBins = this.kaddNextEmptyBin + 1;

			//     KBOVER is the number of overflow bins - previous bins filled
			this.kbinsOverflow = this.ktotalBins + 1;


			//     KFPASS points to the file password (encoded)
			this.kfilePassword = this.kbinsOverflow + 1;


			//  Note - for errors, there are two error situations -
			//  kerror refers to any error that occurred and may be just for that session (e.g., no write access)
			//  kfileError is when the system error occurs, such as no more disk space.
			//  kfileError generally indicates a potentially damaged file, where
			//  kerror indicates any error.  kfileError is saved with the file to indicate on subsequent opens that
			//  the file might be damaged.
			//  A flag that indicates if a file error occurred in previous write
			this.kfileError = this.kfilePassword + 2;

			//  The error code given by _get_errno
			this.kfileErrorCode = this.kfileError + 1;

			//  the catalog sort list is an array of index numbers indicating the sort order of the catalog
			//  The sequence number helps how to build the sort list so that added records don't get it out of order
			///  OBSOLETE - REMOVE ME  (Unused)
			this.kcatSequenceNumber = this.kfileErrorCode + 1;

			//  kcatSortStatus is the status of the catalog sort list
			//  0 = none
			//  1 = sorted, no updates and ready for use
			//  2 = records have been added
			//  3 =  significant changes (deleted or renamed); sort list is unusable
			this.kcatSortStatus = this.kcatSequenceNumber + 1;

			//  kcatSortWrites is the number of new records written since
			//  the last sort list saved.  This helps determine how much
			//  the sort list might be out of date
			this.kcatSortNewWrites = this.kcatSortStatus + 1;
			this.kcatSortDeletes = this.kcatSortNewWrites + 1;

			//  kcatSortSize is the physical length of sort list (in long longs, 8 bytes)
			this.kcatSortSize = this.kcatSortDeletes + 1;

			//  kcatSortNumber is number of values in sort list , usually equal to length
			this.kcatSortNumber = this.kcatSortSize + 1;

			//  kcatSortAddress is the address of the current sort list flag
			//  The sort list is composed of the (long long) address of the path hash
			//  for each pathname in default sorted order (for a full sort only!)
			this.kcatSortAddress = this.kcatSortNumber + 1;
			//		  flag  (-97535)
			//        sort list
			//        ending flag (-97536)

			//  Reclaimed Space
			//  Reclaimed space is usable dead space in the file
			//  reclaim space is not allocated until the first delete or expansion
			//  We only reclaim significant size segments; that includes deleted records,
			//  space from expanded records and deleted catalog sort sequences
			//  We don't reclaim anything from pathname bins, etc.
			//  We only use reclaimed space for record info/data areas,
			//  not for bins or catalog sort areas (these generally require too much space)

			// kreclaimMin is the minimum length of space to use for reclamation
			//  If you release space smaller than this, it is ignored.
			//  kreclaimMin also indicates if the file allows the use of reclaimed space
			//  (0 = no, >0 = yes)  (Reclaimed space can be slower)
			this.kreclaimMin = this.kcatSortAddress + 1;//zdssnz_1.npass;

			//  kreclaimMaxAvailable is the maximum amount of reclaimed space
			//  available in one contiguous segment (we don't do multiple segments)
			this.kreclaimMaxAvailable = this.kreclaimMin + 1;

			//  kreclaimTotal is the total amount of reclaimed space available in the file
			this.kreclaimTotal = this.kreclaimMaxAvailable + 1;

			//  kreclaimTableAddress is the address of the reclaim array in the file
			//  it is a 2x array, with the first word being the length for that segment
			//  and the second word being the address of that segment
			this.kreclaimTableAddress = this.kreclaimTotal + 1;

			//  Points to Address of reclaim segment with available space
			this.kreclaimSegAvailableAdd = this.kreclaimTableAddress + 1;

			//  Array number that corresponds to kreclaimSegAvailableAdd
			this.kreclaimSegNumber = this.kreclaimSegAvailableAdd + 1;

			//  Total maximum number of segments that can be allocated to reclaiming (first int)
			//  After this is reached, releasing space is ignored (time for a squeeze)
			//  And number of segments allocated for this file (second int)
			this.kreclaimMaxSegment = this.kreclaimSegNumber + 1;

			this.kreclaimSegmentsUsed = this.kreclaimMaxSegment + 1;


			//  kreclaimNumber is the number of reclaim location (pairs) for each reclaim segment
			this.kreclaimNumber = this.kreclaimSegmentsUsed + 1;

			//  kreclaimSize is the physcial length of the reclaim segment
			//  Note, reclaim number <= (reclaim lengh) * 2
			this.kreclaimSize = this.kreclaimNumber + 1;

			//  kreclaimedSpaceUsed is the number of times space has been reclaimed
			//  The first int is the number of pathnames reclaimed from the pathname bin
			//  The second is the number of record areas reclaimed
			this.kreclaimedPaths = this.kreclaimSize + 1;
			this.kreclaimedSpace = this.kreclaimedPaths + 1;

			/*  maximum lengths of pathname parts */
			this.kmaxPath = this.kreclaimedSpace + 1;
			this.kmaxA = this.kmaxPath + 1;
			this.kmaxB = this.kmaxA + 1;
			this.kmaxC = this.kmaxB + 1;
			this.kmaxD = this.kmaxC + 1;
			this.kmaxE = this.kmaxD + 1;
			this.kmaxF = this.kmaxE + 1;

			// maximum header, data lengths (these lengths are for byte words)
			this.kmaxInternalHeader = this.kmaxF + 1;
			this.kmaxHeader2 = this.kmaxInternalHeader + 1;
			this.kmaxUserHeader = this.kmaxHeader2 + 1;
			this.kmaxValues1Size = this.kmaxUserHeader + 1;
			this.kmaxValues2Size = this.kmaxValues1Size + 1;
			this.kmaxValues3Size = this.kmaxValues2Size + 1;

			//  Number of records with internal header (8 byte)
			this.knumberInternalHeader = this.kmaxValues3Size + 1;
			//  Number of records with compression header
			this.knumberHeader2 = this.knumberInternalHeader + 1;
			//  Number of records with description header
			this.knumberDataArea3 = this.knumberHeader2 + 1;
			//  Number of records with user header
			this.knumberUserHeader = this.knumberDataArea3 + 1;
			//  Number of records that utilize data area (should be all)
			this.knumberDataArea1 = this.knumberUserHeader + 1;
			this.knumberDataArea2 = this.knumberDataArea1 + 1;

			// maximum record length (8 byte word)
			this.kmaxRecordSize = this.knumberDataArea2 + 1;

			//  Maximum length of a regular interval time series float record, including info
			this.kmaxRtsSize = this.kmaxRecordSize + 1;
			//  Maximum length of a regular interval time series double record, including info
			this.kmaxRtdSize = this.kmaxRtsSize + 1;
			//  Maximum length of a regular interval time series double record, including info
			this.kmaxItsSize = this.kmaxRtdSize + 1;
			//  Maximum length of a regular interval time series double record, including info
			this.kmaxItdSize = this.kmaxItsSize + 1;
			//  Maximum length of a regular interval time series double record, including info
			this.kmaxPdSize = this.kmaxItdSize + 1;
			//  Maximum length of a regular interval time series double record, including info
			this.kmaxPddSize = this.kmaxPdSize + 1;
			//  Maximum length of other data records, including info
			this.kmaxOtherSize = this.kmaxPddSize + 1;

			//  Maximum number of pathnames set upon original creation
			this.kmaxExpectedPathnames = this.kmaxOtherSize + 1;

			//  Error counters for any severe errors that this file
			//  has gone through (since last squeeze)
			this.kerrorMemory = this.kmaxExpectedPathnames + 1;
			this.kerrorAddress = this.kerrorMemory + 1;
			this.kerrorWrite = this.kerrorAddress + 1;
			this.kerrorRead = this.kerrorWrite + 1;

			//  Total errors for this file for all time before squeezes
			//  (i.e., add this with above error counts for complete error count)
			this.kerrorTotal = this.kerrorRead + 1;

			//  Location bounding, LR = Lower Right (usually lat, long)
			this.klocBoundLR = this.kerrorTotal + 1;
			this.klocBoundLL = this.klocBoundLR + 1;
			this.klocBoundUR = this.klocBoundLL + 1;
			this.klocBoundUL = this.klocBoundUR + 1;
			this.klocBoundElev = this.klocBoundUL + 1;


			this.kdetune = this.klocBoundElev + 1;

			this.klockArraySizes = this.kdetune + 1;
			this.klockWriteArrayAddress = this.klockArraySizes + 1;
			this.klockReadArrayAddress = this.klockWriteArrayAddress + 1;
			this.kpidArrayAddress = this.klockReadArrayAddress + 1;

			//  endian identifies if the file has been written to on a
			//  big endian (e.g., Solaris) machine
			//  1 means yes, 0 means no (default)
			this.kendian = this.kpidArrayAddress + 1;

			//     additional words are reserved after the perm
			//     section for any future use

			this.kreserved1 = this.kendian + 1;
			this.kreserved2 = this.kreserved1 + 1;
			this.kreserved3 = this.kreserved2 + 1;
			this.kreserved4 = this.kreserved3 + 1;


			//     *** End of permanent area written to file ***
			this.kendFileHeader = this.kreserved4 + 1;

			//  Allocate any additional space for future use...
			//	size = zdssFileKeys.kendFileHeader - zdssKeys.kfileHeader + 1;
			//	if (size < DSS_MIN_FILE_HEADER_SIZE) {
			//  This should always be the case
			//		zdssFileKeys.kendFileHeader = zdssKeys.kfileHeader + DSS_MIN_FILE_HEADER_SIZE - 1;
			//	}


		}
		
		public readonly int kdss;
		public readonly int kfileHeaderSize;
		public readonly int kversion;
		public readonly int knumberRecords;
		public readonly int knumberAliases;
		public readonly int kfileSize;
		public readonly int kdead;
		public readonly int knumberExpansions;
		public readonly int knumberCollections;
		public readonly int knumberRenames;
		public readonly int knumberDeletes;
		public readonly int knumberAliasDeletes;
		public readonly int kcreateDate;
		public readonly int klastWriteTime;
		public readonly int klockAddressWord;
		public readonly int kmaxHash;
		public readonly int khashsUsed;
		public readonly int kmaxPathsOneHash;
		public readonly int kmaxPathsHashCode;
		public readonly int kaddHashTableStart;
		public readonly int khashCollisions;
		public readonly int kbinsPerBlock;
		public readonly int kbinsRemainInBlock;
		public readonly int kbinSize;
		public readonly int kaddFirstBin;
		public readonly int kaddNextEmptyBin;
		public readonly int ktotalBins;
		public readonly int kbinsOverflow;
		public readonly int kfilePassword;
		public readonly int kfileError;   //  indicates if an error occurred last time the file was written to (previous session)  UNUSED
		public readonly int kfileErrorCode;  //  What, specifically that error was (according to _get_errno)  UNUSED
		public readonly int kcatSequenceNumber;
		public readonly int kcatSortStatus;  //  the status of the catalog sort list
		public readonly int kcatSortNewWrites;  //  number of new records since last sort saved
		public readonly int kcatSortDeletes;
		public readonly int kcatSortSize;  //  physical length of sort list in longs, excluding flags and lengths
		public readonly int kcatSortNumber;  //  number of values in sort list, usually equal to length
		public readonly int kcatSortAddress;
		public readonly int kreclaimMin;
		public readonly int kreclaimMaxAvailable;
		public readonly int kreclaimTotal;
		public readonly int kreclaimTableAddress;
		public readonly int kreclaimSegAvailableAdd;
		public readonly int kreclaimSegNumber;
		public readonly int kreclaimMaxSegment;
		public readonly int kreclaimSegmentsUsed;
		public readonly int kreclaimNumber;
		public readonly int kreclaimSize;
		public readonly int kreclaimedPaths;
		public readonly int kreclaimedSpace;
		public readonly int kmaxPath;
		public readonly int kmaxA;
		public readonly int kmaxB;
		public readonly int kmaxC;
		public readonly int kmaxD;
		public readonly int kmaxE;
		public readonly int kmaxF;
		public readonly int kmaxInternalHeader;
		public readonly int kmaxHeader2;
		public readonly int kmaxUserHeader;
		public readonly int kmaxValues1Size;
		public readonly int kmaxValues2Size;
		public readonly int kmaxValues3Size;
		public readonly int knumberInternalHeader;
		public readonly int knumberHeader2;
		public readonly int knumberDataArea3;
		public readonly int knumberUserHeader;
		public readonly int knumberDataArea1;
		public readonly int knumberDataArea2;
		public readonly int kmaxRecordSize;
		public readonly int kmaxRtsSize;
		public readonly int kmaxRtdSize;
		public readonly int kmaxItsSize;
		public readonly int kmaxItdSize;
		public readonly int kmaxPdSize;
		public readonly int kmaxPddSize;
		public readonly int kmaxOtherSize;
		public readonly int kmaxExpectedPathnames;
		public readonly int kerrorMemory;
		public readonly int kerrorAddress;
		public readonly int kerrorWrite;
		public readonly int kerrorRead;
		public readonly int kerrorTotal;
		public readonly int klocBoundLR;
		public readonly int klocBoundLL;
		public readonly int klocBoundUR;
		public readonly int klocBoundUL;
		public readonly int klocBoundElev;
		public readonly int kdetune;
		public readonly int klockArraySizes;
		public readonly int klockWriteArrayAddress;
		public readonly int klockReadArrayAddress;
		public readonly int kpidArrayAddress;
		public readonly int kendian;
		public readonly int kreserved1;
		public readonly int kreserved2;
		public readonly int kreserved3;
		public readonly int kreserved4;
		public readonly int kendFileHeader;
	}
}
