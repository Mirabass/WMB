using System.Text;

namespace Helper
{
    public static class FileProcessor
    {
        public static Dictionary<(int, int), string> LoadTableDataFromFile(string filePath, char separator)
        {
            Dictionary<(int, int), string> data = new Dictionary<(int, int), string>();
            List<string> lines = File.ReadAllLines(filePath).ToList();
            int row = 0, column = 0;
            foreach (string line in lines)
            {
                string[] cells = line.Split(separator);
                foreach (string cell in cells)
                {
                    data.Add((row, column++), cell);
                }
                row++;
                column = 0;
            }

            return data;
        }

        public static Dictionary<(int, int), double> ConvertTableToDoubles(Dictionary<(int, int), string> stringDataTable)
        {
            Dictionary<(int, int), double> data = new Dictionary<(int, int), double>();
            foreach (var item in stringDataTable)
            {
                string stringWithSpaces = item.Value;
                string stringWithoutSpaces = stringWithSpaces.Trim();
                double number;
                try
                {
                    number = Convert.ToDouble(stringWithoutSpaces);
                }
                catch (Exception ex)
                {
                    ex.Data.Add("Extention Message", $"Problem at position ({item.Key.Item1},{item.Key.Item2}).");
                    throw;
                }
                data.Add((item.Key.Item1, item.Key.Item2), number);
            }
            return data;
        }

        /// <summary>
        /// Loads file with data separated by fixed widths.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="startingRow">First row has index 0</param>
        /// <param name="widths">First item is startIndex, others are widths</param>
        /// <returns></returns>
        public static Dictionary<(int, int), string> LoadTableDataFromFileWithFixedWidths(string filePath, int startingRow, int nOmittedEndLines, List<int> widths)
        {
            Dictionary<(int, int), string> data = new Dictionary<(int, int), string>();
            List<string> lines = File.ReadAllLines(filePath).ToList();
            if (startingRow > lines.Count)
            {
                throw new Exception("Starting row is higher than lines in file");
            }
            int row = 0;
            for (int lineIndex = startingRow; lineIndex < lines.Count - nOmittedEndLines; lineIndex++)
            {
                for (int column = 0; column < widths.Count - 1; column++)
                {
                    int startIndex = widths[0];
                    int length = widths[column + 1];
                    for (int i = 1; i < column + 1; i++)
                    {
                        startIndex += widths[i];
                    }
                    string cell = lines[lineIndex].Substring(startIndex, length);
                    data.Add((row, column), cell);
                }
                row++;
            }
            return data;
        }

        public static Dictionary<(int, int), string> LoadDataFromFile_csvSemicolon(string filePath)
        {
            return LoadTableDataFromFile(filePath, ';');
        }

        public static Dictionary<(int, int), string> LoadDataFromFile_tableWithTabs(string filePath)
        {
            return LoadTableDataFromFile(filePath, '\t');
        }
        /// <summary>
        /// Exports data to text file separated by choosed separator, saves file to choosed location with choosed name
        /// </summary>
        /// <param name="dataToExport">key item 1 = row; key item 2 = column; object = value - can be string, int, double, decimal etc.</param>
        /// <param name="exportFolderPath"></param>
        /// <param name="fileName"></param>
        /// <param name="separator">'\t', ';',...</param>
        /// <param name="fileExtension">"csv", "tsv", "txt", ...</param>
        /// <exception cref="Exception"></exception>
        public static void Export(Dictionary<(int, int), object> dataToExport, string exportFolderPath, string fileName, char separator, string fileExtension)
        {
            if (exportFolderPath == null)
            {
                throw new Exception();
            }
            if (!Directory.Exists(exportFolderPath))
            {
                throw new Exception();
            }
            fileName += "." + fileExtension;

            string filePath = exportFolderPath + "\\" + fileName;
            StreamWriter streamWriter = new StreamWriter(filePath, false, Encoding.UTF8);
            dataToExport = dataToExport.OrderBy(d => d.Key.Item2).OrderBy(d => d.Key.Item1).ToDictionary(d => d.Key, d => d.Value);
            for (int row = 0; row < dataToExport.Select(d => d.Key.Item1).Max() + 1; row++)
            {
                List<object> rowItems = dataToExport.Where(d => d.Key.Item1 == row).Select(d => d.Value).ToList();
                if (rowItems is not null)
                {
                    List<string?> rowItemsString = rowItems.ConvertAll(ri => ri.ToString());
                    string line = string.Join(separator, rowItemsString);
                    streamWriter.WriteLine(line);
                }
            }
            streamWriter.Close();
        }
        public static void ExportCSV(List<List<string>> exportTabulka, string pracovniAdresar, string nazevSouboru)
        {
            if (pracovniAdresar == null)
            {
                throw new Exception();
            }
            if (!Directory.Exists(pracovniAdresar))
            {
                throw new Exception();
            }
            nazevSouboru += ".csv";

            string cestaSouboru = pracovniAdresar + "\\" + nazevSouboru;
            StreamWriter streamWriter = new StreamWriter(cestaSouboru, false, Encoding.UTF8);
            for (int row = 0; row < exportTabulka[0].Count; row++)
            {
                string radek = "";
                for (int column = 0; column < exportTabulka.Count; column++)
                {
                    string bunka = exportTabulka[column][row];
                    radek += bunka;
                    if (column != exportTabulka.Count - 1)
                    {
                        radek += ";";
                    }
                }
                streamWriter.WriteLine(radek);
            }
            streamWriter.Close();
        }

        /// <summary>
        /// Obracene sloupec / radek
        /// </summary>
        /// <param name="exportTabulka"></param>
        /// <param name="pracovniAdresar"></param>
        /// <param name="nazevSouboru"></param>
        public static void ExportCSV2(List<List<string>> exportTabulka, string pracovniAdresar, string nazevSouboru)
        {
            if (pracovniAdresar == null)
            {
                throw new Exception();
            }
            if (!Directory.Exists(pracovniAdresar))
            {
                throw new Exception();
            }
            nazevSouboru += ".csv";

            string cestaSouboru = pracovniAdresar + "\\" + nazevSouboru;
            StreamWriter streamWriter = new StreamWriter(cestaSouboru, false, Encoding.UTF8);
            for (int column = 0; column < exportTabulka[0].Count; column++)
            {
                string radek = "";
                for (int row = 0; row < exportTabulka.Count; row++)
                {
                    string bunka = exportTabulka[row][column];
                    radek += bunka;
                    if (row != exportTabulka.Count - 1)
                    {
                        radek += ";";
                    }
                }
                streamWriter.WriteLine(radek);
            }
            streamWriter.Close();
        }
    }
}