using Helper;

namespace WMB.Logic
{
    public class Processor
    {
        public string? LoadCaseListPath { get; set; } // = "defaultní hodnota" .. v případě, že nepoužijeme otazník
        public string? LoadCasesFolderPath { get; set; }

        public void Run()
        {
            if (LoadCaseListPath is null)
            {
                throw new Exception("Load case list není vyplněný.");
            }
            Dictionary<(int, int), string> result = FileProcessor.LoadDataFromFile_csvSemicolon(LoadCaseListPath);

        }

    }
}