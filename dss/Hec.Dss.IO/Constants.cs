using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hec.Dss.IO
{
   delegate byte[] ByteReader(long wordOffset, int wordCount, int wordSize = 8);
   enum RecordStatus { Unused = 0, Good = 1, Alias = 2, Deleted = 11, Renamed = 12 };

    enum RecordType { RegularTimeSeries = 100, IrregularTimeSeries = 110 }

   class Constants
  {
      internal const int MAX_PROGRAM_NAME_LENGTH = 16;
    internal const int MAX_LEN_ERROR_MESS = 500;
    internal const int DSS_END_HEADER_FLAG = -97531;
    internal const int DSS_END_FILE_FLAG = -97532;
    internal const int DSS_INFO_FLAG = -97534;
    internal const int DSS_START_CAT_SORT_FLAG = -97536;
    internal const int DSS_END_CAT_SORT_FLAG = -97537;

    internal const int DSS_NUMBER_LOCK_ATTEMPS = 100;

    internal const int DSS_MIN_FILE_HEADER_SIZE = 100;
    //  Keep track of how many other files are accessing this one
    internal const int DSS_LOCK_ARRAY_SIZE = 25;

    internal const int DSS_INTEGRITY_KEY = 13579;
    internal const int DSS_MEMORY_INTEG_KEY = 24680;
    static int messageHandle = -1;
    static int fortranMessageUnit = -1;


    internal const int MAX_PATHNAME_LENGTH = 393;
      internal const int MIN_PATHNAME_LENGTH = 8; // /a//////
    internal const int MAX_PATHNAME_SIZE = 394;
    internal const int MAX_PART_SIZE = 65;
    
    internal const int MAX_FILENAME_LENGTH = 256;


    //  Buffer Action
    internal const int BUFF_NO_ACTION = 0;
    internal const int BUFF_WRITE = 1;
    internal const int BUFF_WRITE_FLUSH = 2;
    internal const int BUFF_READ = 1;
    internal const int BUFF_LOAD = 2;

    //  Buffer Control
    internal const int BUFF_SIZE = 0;
    internal const int BUFF_STAT = 1;
    internal const int BUFF_ADDRESS = 2;
    internal const int BUFF_INTS_USED = 3;

    //  Buffer statuses (for BUFF_STAT)
    internal const int BUFF_STAT_UNUSED = 0;
    internal const int BUFF_STAT_NOT_DIRTY = 1;
    internal const int BUFF_STAT_DIRTY = 2;

    internal const int OPEN_STAT_CLOSED = 0;
    internal const int OPEN_STAT_READ_ONLY = 1;
    internal const int OPEN_STAT_WRITE = 2;

    internal const int REC_STATUS_VALID = 0;
    internal const int REC_STATUS_PRIMARY = 1;
    internal const int REC_STATUS_ALIAS = 2;
    internal const int REC_STATUS_MOVED = 10;
    internal const int REC_STATUS_DELETED = 11;
    internal const int REC_STATUS_RENAMED = 12;
    internal const int REC_STATUS_ALIAS_DELETED = 13;
    internal const int REC_STATUS_REMOVED = 15;     //  Deleted and then space used elsewhere
    internal const int REC_STATUS_ANY = 100;

    //		RECLAIM_UNDEFINED	0
    internal const int RECLAIM_NONE = 1;  //  Don't use space reclamation
    internal const int RECLAIM_EXCESS = 2;  //  Reclaim space left over from extending records, etc.  (can recover records)
    internal const int RECLAIM_ALL = 3; //  Reclaim all unused space, including deleted records (cannot recover)

    internal const string DSS_VERSION = "7-IC";
    internal const string DSS_VERSION_DATE = "21 January 2021";
      internal const string DSS_FILE_KEY = "ZDSS";


  }
}
