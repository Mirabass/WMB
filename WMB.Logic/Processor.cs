using Helper;
using WMB.Logic.Models;

namespace WMB.Logic
{
    public class Processor
    {
        public string? LoadCaseListPath { get; set; } // = "defaultní hodnota" .. v případě, že nepoužijeme otazník
        public string? LoadCasesFolderPath { get; set; }
        public string? ResultsFolderPath { get; set; }
        public string? ResultsFileName { get; set; }

        public void Run()
        {
            Console.WriteLine("Process started.");


            List<LoadCase> loadCases = LoadLoadCases();
            List<LoadCase> orderedLoadCases = OrderByTimeShare(loadCases);
            Export(orderedLoadCases);


            Console.WriteLine("Done.");
            Console.WriteLine("Elapsed time: " + SystemOperation.GetElapsedTimeSinceApplicationStarted());
        }

        private List<LoadCase> OrderByTimeShare(List<LoadCase> loadCases)
        {
            return loadCases.OrderBy(lc => lc.TimeShare).ToList();
        }

        private void Export(List<LoadCase> loadCases)
        {
            if (ResultsFolderPath is null)
            {
                throw new Exception("Results folder path is empty");
            }
            if (ResultsFileName is null)
            {
                throw new Exception("Result File Name is empty");
            }
            Dictionary<(int, int), object> loadCaseData = new();

            // Header:
            loadCaseData.Add((0, 0), "Load Case Name");
            loadCaseData.Add((0, 1), "Tihe Share");

            // Body:
            int row = 1;
            foreach (LoadCase loadCase in loadCases)
            {
                if (loadCase.Name is null)
                {
                    throw new Exception("There is empty name of loadCase at row " + row);
                }
                loadCaseData.Add((row, 0), loadCase.Name);
                loadCaseData.Add((row, 1), loadCase.TimeShare);
                row++;
            }
            FileProcessor.Export(loadCaseData, ResultsFolderPath, ResultsFileName, ';', "csv");
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