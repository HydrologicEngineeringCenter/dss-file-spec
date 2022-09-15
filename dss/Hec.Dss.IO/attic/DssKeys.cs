using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.Dss.Attic
{
	/// <summary>
	/// zdssKeys are positions in the main file table, ifltab, for
	//  run-time items, such as the handle, etc.
	//  There are 3 integrity keys to help identify if the array
	//  becomes corrupt (from something overwriting it in memory)
	/// 
	/// </summary>
	internal class DssKeys111
  {
    public DssKeys111()
    {
			init(this);
    }

    private void init(DssKeys111 zdssKeys)
    {
			//  knumVersion is the numerical version number (i.e., 7) so applications
			//  can quickly tell what version is being used
			zdssKeys.knumVersion = 0;

			// khandle points to the handle for this file.  Must be SECOND word for Java functions
			zdssKeys.khandle = zdssKeys.knumVersion + 1;

			zdssKeys.kintegrityKey1 = zdssKeys.khandle + 1;

			zdssKeys.kfiHeadSize = zdssKeys.kintegrityKey1 + 1;

			//----------------------
			//  kmultiUserAccess points to the multiple user access flag
			zdssKeys.kmultiUserAccess = zdssKeys.kfiHeadSize + 1;


			zdssKeys.kopenMode = zdssKeys.kmultiUserAccess + 1;


			//     kopenStatus points to a flag indicating the open status of the file
			//  e.g., (read only)
			zdssKeys.kopenStatus = zdssKeys.kopenMode + 1;

			//  kremote point to a flag that indicates if the file is on a remote
			//  disk (network mounted or client server).  Not guaranteed.
			//  Set to 1 if remote, 0 if local or unknown
			zdssKeys.kremote = zdssKeys.kopenStatus + 1;


			/*     KSWAP points to a flag indicating if all bytes need to be swapped, */
			/*     because the file is a different Endian from the machine. */
			zdssKeys.kswap = zdssKeys.kremote + 1;


			// kdswap points to a flag indicating if words for DOUBLE
			// values need to be swapped to keep files compatible between
			// different Endian machines.  (swap on unix side)
			zdssKeys.kdswap = zdssKeys.kswap + 1;


			//   klocked indicates if this file is currently write locked
			zdssKeys.klocked = zdssKeys.kdswap + 1;

			//   klockLevel indicates the function level that has the lock
			zdssKeys.klockLevel = zdssKeys.klocked + 1;

			//  klockCheckSet indicates if the file was locked since the last check
			zdssKeys.klockCheckSet = zdssKeys.klockLevel + 1;

			//  klocksDenied is the number of times a lock request has been denied
			//  (probably) due to another process having the file locked
			//  (and then this process waiting for it to be unlocked)
			zdssKeys.klocksDenied = zdssKeys.klockCheckSet + 1;

			zdssKeys.klockExclusiveWord = zdssKeys.klocksDenied + 1;
			zdssKeys.klockWriteMyAddress = zdssKeys.klockExclusiveWord + 1;
			zdssKeys.klockReadMyAddress = zdssKeys.klockWriteMyAddress + 1;

			//   kpidMyAddress points to the file address when this process id
			//   is stored while writing to the file (for multi-user purpose)
			//   (The actual address starts coincidently with the lock address)
			zdssKeys.kpidMyAddress = zdssKeys.klockReadMyAddress + 1;

			//     kwritingNow indicates if file is being written to (in zwriteInternal)
			zdssKeys.kwritingNow = zdssKeys.kpidMyAddress + 1;

			//   kmyLastWriteTime is the last write time of this process (in milliseconds)
			//   (compare to file last write time, which is for all processes)
			zdssKeys.kmyLastWriteTime = zdssKeys.kwritingNow + 1;

			//  kerrorSevere points to flag that is set to non-zero should a severe error occur
			//  In this case, a severe error is usually memory corruption or damaged file,
			//  not a bad address or read or write error
			zdssKeys.kerrorSevere = zdssKeys.kmyLastWriteTime + 1;

			//  kerrorCondition indicates if an error has occurred and its severity
			//   0 - No errors
			//   See severity
			zdssKeys.kerrorCondition = zdssKeys.kerrorSevere + 1;

			//  errorCode is the actual error.  See errorProcessing for different codes
			zdssKeys.kerrorCode = zdssKeys.kerrorCondition + 1;


			//  If an important message (error) has been set,
			zdssKeys.kmessagesAvail = zdssKeys.kerrorCode + 1;

			//  kmessLevel points to the message level of this file.  If -1, the default is used
			zdssKeys.kmessLevel = zdssKeys.kmessagesAvail + 1;

			//  kmessHandle points to the output unit for this file.  If -1, stdout is used
			zdssKeys.kmessHandle = zdssKeys.kmessLevel + 1;

			//  kfortMessUnit points a Fortran output unit for this file.  If 0, it is not written to
			zdssKeys.kfortMessUnit = zdssKeys.kmessHandle + 1;



			//  kaddLast is the address of the last physical read or write
			//  It is typically used for debugging and error processing */
			zdssKeys.kaddLast = zdssKeys.kfortMessUnit + 1;


			//     kfileWritten points to a flag indicating if the file has been written to
			//    The value of IFLTAB will be set to 1 when it has.
			zdssKeys.kfileWritten = zdssKeys.kaddLast + 1;

			// kwritesSinceFlush is the number of physical writes since last flush
			zdssKeys.kwritesSinceFlush = zdssKeys.kfileWritten + 1;


			//  number of reads is the physical number of disk reads since open
			zdssKeys.knumberReads = zdssKeys.kwritesSinceFlush + 1;

			//  number of writes is the physical number of disk writes since open
			zdssKeys.knumberWrites = zdssKeys.knumberReads + 1;

			//     KSUSER points to a flag indicating if the user is the super user
			//     (i.e., their password matches the file password) (0=no, 1=yes).
			zdssKeys.ksuser = zdssKeys.knumberWrites + 1;

			//     KEXCL indicats if the file has been exclusively opened
			zdssKeys.kexclusive = zdssKeys.ksuser + 1;

			//     kpathsThisHash is the number of pathnames for this hash;
			//    statistics only
			zdssKeys.kpathsThisHash = zdssKeys.kexclusive + 1;

			//  A flag indicating that the current pathname has the same hash as another in
			//  the file (and same length); a non-unique condition (the non-unique flag
			//  will be updated on a new write)
			zdssKeys.ksameHash = zdssKeys.kpathsThisHash + 1;


			//     Other information in IFLTAB
			//  Indicator of last pathname checked found or not
			zdssKeys.kfound = zdssKeys.ksameHash + 1;

			//  Table hash of last pathanem
			zdssKeys.ktableHash = zdssKeys.kfound + 1;

			//  Bin hash (Unique hash) of last pathname
			zdssKeys.kpathnameHash = zdssKeys.ktableHash + 1;


			//  kiftPathHash is the full hash code of the pathname for the buffer area
			zdssKeys.kiftPathHash = zdssKeys.kpathnameHash + 1;

			//  Address to table hash of last pathname
			zdssKeys.kaddTableHash = zdssKeys.kiftPathHash + 1;

			//  Length of last pathname
			zdssKeys.klenLastPath = zdssKeys.kaddTableHash + 1;

			// Bin address of this table hash (may be 0 + - )
			zdssKeys.khashTableBinAdd = zdssKeys.klenLastPath + 1;

			zdssKeys.klastType = zdssKeys.khashTableBinAdd + 1;

			zdssKeys.kisaCollection = zdssKeys.klastType + 1;

			//  kpathBinAddress is the address of the pathname hash (in the path bin)
			//  This is the start of the short segment that contains the path and info address
			zdssKeys.kpathBinAddress = zdssKeys.kisaCollection + 1;

			//
			zdssKeys.kbinTypeAndCatSort = zdssKeys.kpathBinAddress + 1;
			zdssKeys.kbinLastWrite = zdssKeys.kbinTypeAndCatSort + 1;
			zdssKeys.kbinDates = zdssKeys.kbinLastWrite + 1;
			//     Last Pathname accessed Information

			// Address of pathname in pathname bin for last record checked
			zdssKeys.kpathAddressInBin = zdssKeys.kbinDates + 1;

			//     Read or computed - not stored in perm area.
			//     KAINFO points to the location of the last paths info block
			zdssKeys.kaddInfoLastPath = zdssKeys.kpathAddressInBin + 1;

			//  kbinRecordAddress points to the bin location that has
			//  the address of the record (info area)
			zdssKeys.kinfoAddInBin = zdssKeys.kaddInfoLastPath + 1;

			zdssKeys.kbinStatus = zdssKeys.kinfoAddInBin + 1;

			zdssKeys.kbinPathLen = zdssKeys.kbinStatus + 1;

			//     kbinAddCurrent points to the address of the last pathname's bin
			zdssKeys.kbinAddCurrent = zdssKeys.kbinPathLen + 1;

			//     kbinAddplist points to the address of the last pathname's bin for zplist
			//     (a function for DSS-6 compatibility only)
			zdssKeys.kbinAddplist = zdssKeys.kbinAddCurrent + 1;

			//     kbinPosPlist points to the position in that pathname bin for zplist
			zdssKeys.kbinPosPlist = zdssKeys.kbinAddplist + 1;

			//     kbinCountplist points to the count of pathname bin for that section for zplist
			zdssKeys.kbinCountplist = zdssKeys.kbinPosPlist + 1;

			//  kbinWithSpace points to a bin that has reclaimed space
			//  that has room to store the current pathname being checked.
			//  If someone were to delete a record, then write it again,
			//  this would use the same address in the same bin.
			//  If this happened a lot (sometimes it does), this would
			//  prevent long searches through deleted records
			zdssKeys.kbinWithSpace = zdssKeys.kbinCountplist + 1;

			zdssKeys.kdataFirstDate = zdssKeys.kbinWithSpace + 1;
			zdssKeys.kdataLastDate = zdssKeys.kdataFirstDate + 1;

			zdssKeys.kgetLogicalNumberData = zdssKeys.kdataLastDate + 1;
			zdssKeys.ksetLogicalNumberData = zdssKeys.kgetLogicalNumberData + 1;

			//  Filename, pointer in ifltab
			zdssKeys.kfilename = zdssKeys.ksetLogicalNumberData + 1;

			zdssKeys.kfullFilename = zdssKeys.kfilename + 1;

			//  A set catalog for comparing what's changed
			zdssKeys.kcatStruct = zdssKeys.kfullFilename + 1;

			//  CRC Table for determining CRC value for data record
			zdssKeys.kCRCtable = zdssKeys.kcatStruct + 1;

			zdssKeys.kintegrityKey2 = zdssKeys.kCRCtable + 1;




			//  The following keys include pointers to allocated memory
			//  To ensure integrity of each memory component, a flag is
			//  put at the beginning of the allocated array and at then end
			//  and checked during accesses.  The flags are not written to disk.

			////////////////////////////////////////////////

			//  File Header memory area
			zdssKeys.kfileHeader = zdssKeys.kintegrityKey2 + 1;


			//////////////////////////////////////////////
			//  Pathname Bin memory area
			/*     kbinMem is a pointer to pathname bin memory that has
			been allocated for use in (only) reading and writing the pathname bin
			It is initialized by zmemoryGet and released by zmemoryFree.
			zmemoryFree is called at zclose, and must always be called
			to avoid a memory leak.  kbinMem is only to be accessed by
			zmemoryGet and zmemoryFree, never outside those functions*/

			zdssKeys.kpathBin = zdssKeys.kfileHeader + 1;


			//  Info memory area
			zdssKeys.kinfo = zdssKeys.kpathBin + 1;
			//  kiftInfoSize is the length being used by the buffer
			zdssKeys.kinfoSize = zdssKeys.kinfo + 1;
			//  kinfoAddress is the file address for the current info table
			zdssKeys.kinfoAddress = zdssKeys.kinfoSize + 1;

			//  zdssKeys.kreclaim points to malloced memory containing the reclaim array read from disk
			zdssKeys.kreclaim = zdssKeys.kinfoAddress + 1;

			//  kreclaimLevel points to the current reclaim level,
			//  which  indicates how aggressive we should be at reclaiming space.
			//  kreclaimLevel is current setting and can be lowered temporarily
			//  kreclaimLevelFile is the file level (which kreclaimLevel is set from) and is permanent (can only be lowered)
			//  ifltab[zdssKeys.kreclaimLevel] == 0 Undefined
			//  ifltab[zdssKeys.kreclaimLevel] == 1  RECLAIM_NONE   Don't use space reclamation
			//  ifltab[zdssKeys.kreclaimLevel] == 2  RECLAIM_EXCESS Reclaim space left over from extending records, etc.  (can recover records)
			//  ifltab[zdssKeys.kreclaimLevel] == 3  RECLAIM_ALL    Reclaim all possible space, including deleted records (cannot recover)
			zdssKeys.kreclaimLevel = zdssKeys.kreclaim + 1;


			zdssKeys.kintegrityKey3 = zdssKeys.kreclaimLevel + 1;


		}

		int kaddTableHash;
		int kaddInfoLastPath;    //
		int kaddLast;
		int kfound;
		int kpathnameHash;    //  Points to the bin hash of the last pathname checked
		int khashTableBinAdd;   //  Points to the bin address of the table hash (in hash table)
		int kpathBinAddress;  //  Points to the address of the segment in the path bin (the address of the pathname hash)
		int kbinPathLen;
		int kbinStatus;
		int kbinLastWrite;
		int kbinDates;
		int kbinTypeAndCatSort;
		int kcatStruct;
		int kCRCtable;
		int kpathAddressInBin;
		int kinfoAddInBin;
		int kiftPathHash;  //  the bin hash code of the pathname for the buffer area
		int kinfoAddress; //  the file address of the buffer area
		int kinfoSize;   //  the length being used by the buffer
		int kinfo; //  start of the buffer in ifltab
		int kfiHeadSize;
		int kdswap;    //
		int kerrorCode;  //  the actual error.  See errorProcessing for different codes
		int kerrorCondition;  //  indicates if an error has occurred and its severity
		int kerrorSevere;
		int kmessagesAvail;  //  Number of important messages available (generally errors) to calling program
		int kexclusive;     //
		int kintegrityKey1;      //  pointers to words in IFLTAB that should contain the KEY value (NKEY) after ZOPEN
		int kintegrityKey2;      //  pointers to words in IFLTAB that should contain the KEY value (NKEY) after ZOPEN
		int kintegrityKey3;  //  pointers to words in IFLTAB that should contain the KEY value (NKEY) after ZOPEN
		int kfileWritten;
		int khandle;    //
		int kdataFirstDate;
		int kdataLastDate;
		int kgetLogicalNumberData;
		int ksetLogicalNumberData;
		int klocked;     //
		int klockExclusiveWord;
		int klocksDenied;    //
		int klockLevel;
		int klockCheckSet;
		int klockWriteMyAddress;
		int klockReadMyAddress;
		int kpidMyAddress;
		int kmyLastWriteTime;
		int kpathBin;
		int kmessLevel;     // points to the message level of this file.  If -1, the default is used
		int kmultiUserAccess;
		int kmessHandle;    // points to the output unit for this file.  If 0, the default is used
		int kfilename;  //  Filename, in memory (excludes path)
		int kfullFilename;  //  Full filename with path (and drive)
		int kopenMode;   //  Mode (e.g., read only) on when the file was open (not necessarily current mode)
		int kfortMessUnit;   //  The Fortran message unit... if 0, not written to.
		int klenLastPath;    // length of last pathname
		int kisaCollection;
		int klastType;    //  Record type of last non-internal record checked (e.g., time series, not location)
		int knumVersion;       //
		int kbinAddCurrent;     //
		int kbinWithSpace;
		int kbinAddplist;
		int kbinPosPlist;
		int kbinCountplist;
		int kfileHeader;     //
		int kpathsThisHash;
		int ksameHash;
		int kopenStatus;    //
		int knumberReads;
		int knumberWrites;
		int kreclaimLevel;
		int kreclaimLevelFile;
		int kreclaim;
		int kremote;   //
		int ksuser;    //
		int kswap;     //
		int ktableHash;   //  Points to the table hash of the last pathname checked
		int kwritingNow;  //  Used for inter function processes
		int kwritesSinceFlush;
	}
   
}
