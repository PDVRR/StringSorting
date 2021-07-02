using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StringSorting.BL
{
    public class StringSorter
    {
        private const long MaxBlockSizeInBytes = 40000000;
        private string _tempFolder;
        private List<string> _tempFiles = new List<string>();
        private ISortingAlgorithm _sortingAlgorithm;

        public StringSorter(ISortingAlgorithm sortingAlgorithm, string tempFolder = "temp")
        {
            _sortingAlgorithm = sortingAlgorithm;
            _tempFolder = tempFolder;
        }

        public void SortFile(string filePath)
        { 
            SplitFile(filePath);
            SortBlocks();
            MergeBlocks();
            ReplaceOriginalFileWithSorted(filePath, _tempFiles[0]);
        }

        private void SplitFile(string filePath)
        {
            long sizeOfBlockInBytes = 0;
            StringBuilder sb = new StringBuilder();

            using var reader = new StreamReader(filePath);
            string line = reader.ReadLine();
            while(line != null)
            {
                sizeOfBlockInBytes += line.Length;
                if (sizeOfBlockInBytes > MaxBlockSizeInBytes)
                {
                    CreateNewBlock(sb);
                    sb.Clear();
                    sizeOfBlockInBytes = line.Length;
                }
                sb.AppendLine(line);
                line = reader.ReadLine();
            }
            if (sb.Length > 0)
            {
                CreateNewBlock(sb);
            }
        }

        private void SortBlocks()
        {
            foreach (var block in _tempFiles)
            {
                SortBlock(block);
            }
        }

        private void SortBlock(string filePath)
        {
            string[] strings;
            using (var reader = new StreamReader(filePath))
            {
                strings = reader.ReadToEnd().Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            }
            _sortingAlgorithm.Sort(ref strings);
            using (var writer = new StreamWriter(filePath))
            {
                writer.Write(string.Join('\n', strings));
            }
        }

        private void CreateNewBlock(StringBuilder sb)
        {
            Directory.CreateDirectory(_tempFolder);
            var filePath = $"{_tempFolder}/block{_tempFiles.Count}.txt";
            _tempFiles.Add(filePath);
            using StreamWriter file = new StreamWriter(filePath);
            file.Write(sb);
        }

        private void MergeBlocks()
        {
            int mergeGeneration = 1;
            while (_tempFiles.Count > 1)
            {
                int filesToDeleteCount = _tempFiles.Count;
                for (int i = 0; i < filesToDeleteCount; i++)
                {
                    if (i + 1 >= filesToDeleteCount)
                    {
                        filesToDeleteCount--;
                        break;
                    }

                    MergeTwoFiles(_tempFiles[i], _tempFiles[i+1], $"{_tempFolder}/{mergeGeneration}merge{i}.txt");
                    _tempFiles.Add($"{_tempFolder}/{mergeGeneration}merge{i}.txt");
                    i++;
                    mergeGeneration++;
                }

                DeleteUnnecessaryFiles(filesToDeleteCount);
            }
        }

        private void DeleteUnnecessaryFiles(int filesToDeleteCount)
        {
            for (int i = 0; i < filesToDeleteCount; i++)
            {
                File.Delete(_tempFiles[i]);
            }
            _tempFiles.RemoveRange(0, filesToDeleteCount);
        }

        private void MergeTwoFiles(string firstFilePath, string secondFilePath, string outputFileName)
        {
            using var writer = new StreamWriter(outputFileName);
            using var firstReader = new StreamReader(firstFilePath);
            using var secondReader = new StreamReader(secondFilePath);

            string leftString = firstReader.ReadLine();
            string rightString = secondReader.ReadLine();

            while (!String.IsNullOrEmpty(leftString) && !String.IsNullOrEmpty(rightString))
            {
                if (leftString.CompareTo(rightString) < 0)
                {
                    writer.WriteLine(leftString);
                    leftString = firstReader.ReadLine();
                }
                else
                {
                    writer.WriteLine(rightString);
                    rightString = secondReader.ReadLine();
                }
            }

            while (!String.IsNullOrEmpty(leftString))
            {
                writer.WriteLine(leftString);
                leftString = firstReader.ReadLine();
            }

            while (!String.IsNullOrEmpty(rightString))
            {
                writer.WriteLine(rightString);
                rightString = secondReader.ReadLine();
            }
        }

        private void ReplaceOriginalFileWithSorted(string originalFilePath, string sortedFilePath)
        {
            //var originalFileName = Path.GetFileName(originalFilePath);
            File.Delete(originalFilePath);
            File.Move(sortedFilePath,originalFilePath);
        }
    }
}