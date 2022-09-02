using Hec.Dss.IO;
// See https://aka.ms/new-console-template for more information


if( args.Length !=2)
{
   Console.WriteLine("Usage: dss-cmd [bucket-name:]file.dss path");
   return ;
}

DssFile dss = new DssFile(args[0]);
//dss.PrintCatalog();  
dss.PrintRecord(args[1]);

