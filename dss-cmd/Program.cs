using Hec.DssInternal;
// See https://aka.ms/new-console-template for more information

//S3Reader.S3Test();



if( args.Length !=2)
{
   Console.WriteLine("Usage: dss-cmd file.dss path");
   return ;
}

DssFile dss = new DssFile(args[0]);
dss.PrintCatalog();  
dss.PrintInfo();
dss.PrintRecord(args[1]);

