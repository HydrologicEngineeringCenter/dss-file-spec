using Hec.DssInternal;
// See https://aka.ms/new-console-template for more information


if( args.Length !=2)
{
   Console.WriteLine("Usage: dss-cmd [bucket-name:]file.dss path");
   return ;
}

DssFile dss = new DssFile(args[0]);
dss.PrintCatalog();  
dss.PrintInfo();
dss.PrintRecord(args[1]);

