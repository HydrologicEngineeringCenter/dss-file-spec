using Hec.DssInternal;
// See https://aka.ms/new-console-template for more information


if( args.Length !=1)
{
   Console.WriteLine("Usage: dss-cmd file.dss");
   return;
}

DssFile dss = new DssFile(args[0]);
dss.PrintInfo();

