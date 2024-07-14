// See https://aka.ms/new-console-template for more information

using System;
using System.IO;

namespace OfficeFileEnumerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup directory and file paths
            string directoryName = "FileCollection";
            string resultsFileName = "results.txt";

            // Create helper function to check if a file is an Office file
            Func<string, bool> isOfficeFile = (fileExtension) =>
            {
                return fileExtension.ToLower() == ".xlsx" || fileExtension.ToLower() == ".docx" || fileExtension.ToLower() == ".pptx";
            };

            // Initialize counters and variables
            int excelCount = 0;
            int wordCount = 0;
            int powerPointCount = 0;
            long excelSize = 0;
            long wordSize = 0;
            long powerPointSize = 0;
            int totalCount = 0;
            long totalSize = 0;

            // Create DirectoryInfo object to access the specified directory
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);

            // Enumerate files
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                string fileExtension = file.Extension.ToLower();

                if (isOfficeFile(fileExtension))
                {
                    totalCount++;

                    switch (fileExtension)
                    {
                        case ".xlsx":
                            excelCount++;
                            excelSize += file.Length;
                            break;
                        case ".docx":
                            wordCount++;
                            wordSize += file.Length;
                            break;
                        case ".pptx":
                            powerPointCount++;
                            powerPointSize += file.Length;
                            break;
                    }

                    totalSize += file.Length;
                }
            }

            // Write results to file
            using (StreamWriter writer = new StreamWriter(resultsFileName))
            {
                writer.WriteLine("Office File Summary:");
                writer.WriteLine($"Total Count: {totalCount}");
                writer.WriteLine($"Total Size: {totalSize / 1024} KB");
                writer.WriteLine();
                writer.WriteLine("File Type Counts:");
                writer.WriteLine($"Excel Files: {excelCount} ({excelSize / 1024} KB)");
                writer.WriteLine($"Word Files: {wordCount} ({wordSize / 1024} KB)");
                writer.WriteLine($"PowerPoint Files: {powerPointCount} ({powerPointSize / 1024} KB)");
            }

            Console.WriteLine("Results written to results.txt");
        }
    }
}

