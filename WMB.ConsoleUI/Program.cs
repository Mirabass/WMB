using WMB.Logic;

Console.WriteLine("Welcome to WMB!");

try
{

    string loadCaseListPath = @"C:\Users\miroslav.vaculka\source\repos\WMB\WMB.ConsoleUI\TestingData\WeibullDistribution_PRJ-6076_TEST.csv";
    string loadCasesFolderPath = @"C:\Users\miroslav.vaculka\source\repos\WMB\WMB.ConsoleUI\TestingData\PRJ-6076_TIMESERIES\";
    string resultsFolderPath = @"C:\Users\miroslav.vaculka\source\repos\WMB\WMB.ConsoleUI\TestingData\";
    string resultsFileName = "results";

    Processor processor = new();
    processor.LoadCaseListPath = loadCaseListPath;
    processor.LoadCasesFolderPath = loadCasesFolderPath;
    processor.ResultsFolderPath = resultsFolderPath;
    processor.ResultsFileName = resultsFileName;

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