using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.DssInternal
{
  /// <summary>
  /// Item locations within the pathname bin
  /// </summary>
  class DssBinKeys
  {
    public DssBinKeys()
    {
      init(this);
    }

    private void init(DssBinKeys zdssBinKeys)
    {
      zdssBinKeys.kbinHash = 0;
      zdssBinKeys.kbinStatus = 1;
      zdssBinKeys.kbinPathLen = 2;
      zdssBinKeys.kbinInfoAdd = 3;
      zdssBinKeys.kbinTypeAndCatSort = 4;
      zdssBinKeys.kbinLastWrite = 5;
      zdssBinKeys.kbinDates = 6;
      zdssBinKeys.kbinPath = 7;

      //  the number of words used in the bin for each record, excluding the pathname
      //  Do not change this value - it is critical.
      zdssBinKeys.kbinSize = zdssBinKeys.kbinPath - zdssBinKeys.kbinHash;

    }
    int kbinHash;
    int kbinStatus;
    int kbinPathLen;
    int kbinInfoAdd;
    int kbinTypeAndCatSort;
    int kbinLastWrite;
    int kbinDates;
    int kbinPath;
    int kbinSize;
  }
}
