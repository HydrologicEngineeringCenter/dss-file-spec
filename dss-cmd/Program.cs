using Hec.DssInternal;
// See https://aka.ms/new-console-template for more information


if( args.Length !=2)
{
   Console.WriteLine("Usage: dss-cmd file.dss path");
   return;
}

DssFile dss = new DssFile(args[0]);
dss.PrintInfo();
dss.PrintRecord(args[1]);

