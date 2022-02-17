using Helper;
using WMB.Logic.Models;

namespace WMB.Logic
{
    public class Processor
    {
        public string? LoadCaseListPath { get; set; } // = "defaultní hodnota" .. v případě, že nepoužijeme otazník
        public string? LoadCasesFolderPath { get; set; }

        public void Run()
        {
            List<LoadCase> loadCases = LoadLoadCases();
            
        }

        private List<LoadCase> LoadLoadCases()
        {
            if (LoadCaseListPath is null)
            {
                throw new Exception("Load case list není vyplněný.");
            }
            // Dictionary<int, string> ... key : int ... unikátní; value: string 
            Dictionary<(int, int), string> loadCaseData = FileProcessor.LoadDataFromFile_csvSemicolon(LoadCaseListPath);
            // Mapping Dictionary to List of Load Case:
            List<LoadCase> loadCases = new();
            // => .. lambda expression
            for (int row = 0; row <= loadCaseData.Select(lcd => lcd.Key.Item1).Max(); row++)
            {
                LoadCase loadCase = new()
                {
                    Name = loadCaseData[(row, 0)],
                    TimeShare = Convert.ToDouble(loadCaseData[(row, 1)])
                };
                loadCases.Add(loadCase);
            }
            return loadCases;
        }
    }
}