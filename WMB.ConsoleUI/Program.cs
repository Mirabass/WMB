using WMB.Logic;

Console.WriteLine("Hello, World!");

try
{

    string loadCaseListPath = @"C:\Users\miroslav.vaculka\source\repos\WMB\WMB.ConsoleUI\TestingData\WeibullDistribution_PRJ-6076_TEST.csv";
    string loadCasesFolderPath = @"C:\Users\miroslav.vaculka\source\repos\WMB\WMB.ConsoleUI\TestingData\PRJ-6076_TIMESERIES\";


    Processor processor = new();
    processor.LoadCaseListPath = loadCaseListPath;
    processor.LoadCasesFolderPath = loadCasesFolderPath;

    processor.Run();


}
catch (Exception ex)
{
    Console.WriteLine("Error:");
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
}
finally
{
    Console.ReadLine();
}