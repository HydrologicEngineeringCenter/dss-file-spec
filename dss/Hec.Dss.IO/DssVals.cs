using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hec.Dss.IO
{
	class hec_zdssVals
	{
		long integrityKey;      //
		int maxInfoSize;   //  allocated space in long longs for Info
		int fortranMessageUnit;
		int messageHandle;
		int buffMaxSize;  //  the maximum size (that can be used) of the buffer
		int pid;
		int infoSize;
		int maxExpectedPathnames;
		int newCollectionFile;
		int globalErrorFlag;
		int numberProgram;
		int copyEmptyRecords;
		string czdss;
		string czVersion;
		string czVersionDate;
		string cprogramName;
		string[] globalErrorMess;

		public hec_zdssVals()
		{
			init(this);
		}
		private void init(hec_zdssVals zdssVals)
		{ 
			//  Set values
			//  Minimum permanent sections is 100 words
			//  Use Caution - this can cause programs ifltab to be exceeded!!
			zdssVals.maxExpectedPathnames = 0;
			zdssVals.newCollectionFile = 0;

			//  The current process id, for seeing who is writing to the file
//			zdssVals.pid = getpid();


			//  The flags to be used in different areas in ifltab to check that it has not become corrupt.
			zdssVals.integrityKey = Constants.DSS_INTEGRITY_KEY;
			//  For our old Fortran friends, still supported.
			zdssVals.fortranMessageUnit = fortranMessageUnit;
			zdssVals.messageHandle = messageHandle;
			//  Any major error is saved here (for all files in session)
			zdssVals.globalErrorFlag = 0;   //  Same as severity. 0 = no errors
			zdssVals.globalErrorMess = new string[1] {"" }; //[0] = '\0';

			zdssVals.copyEmptyRecords = 1;

			//  Identifiers at the start of a DSS file to identify it is DSS and its version
			zdssVals.czdss = "ZDSS\0";
			zdssVals.czVersion = Constants.DSS_VERSION;
			zdssVals.czVersionDate = Constants.DSS_VERSION_DATE;


		}

	}

}
