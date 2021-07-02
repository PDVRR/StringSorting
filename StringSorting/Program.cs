using System;
using System.IO;
using StringSorting.BL;

namespace StringSorting
{
    class Program
    {
        private static bool _isExitPressed;
        private static bool _isRandomFileGenerated;
        private static ISortingAlgorithm _sortingAlgorithm = new DefaultArraySortingAlgorithm();
        static void Main(string[] args)
        {
            while (!_isExitPressed)
            {
                MainMenu();
            }
        }

        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Generate random file");
            Console.WriteLine("2. Select sorting algorithm");
            Console.WriteLine("3. Start sorting");
            Console.WriteLine("4. Exit");

            var input = Console.ReadLine();

            if (!int.TryParse(input, out int enteredNumber))
            {
                return;
            }

            switch (enteredNumber)
            {
                case 1:
                    GenerateFileMenu();
                    break;
                case 2:
                    SortingAlgorithmMenu();
                    break;
                case 3:
                    StartSorting();
                    _isRandomFileGenerated = false;
                    break;
                case 4:
                    _isExitPressed = true;
                    break;
            }
        }

        public static void GenerateFileMenu()
        {
            Console.Clear();
            Console.Write("Input rows count: ");

            var rows = Console.ReadLine();

            if (!int.TryParse(rows, out int enteredRows))
            {
                return;
            }

            Console.Write("Input max row length: ");

            var maxLength = Console.ReadLine();

            if (!int.TryParse(maxLength, out int enteredMaxLength))
            {
                return;
            }

            Console.WriteLine("Generating in progress...");
            FileGenerator.Generate(enteredRows, enteredMaxLength);
            _isRandomFileGenerated = true;
            Console.Clear();
            Console.WriteLine("Generating is complete. Press any key to continue.");
            Console.ReadKey();
        }

        public static void SortingAlgorithmMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Default Array sort algorithm");
            Console.WriteLine("2. Self implemented merge sort algorithm");

            var input = Console.ReadLine();

            if (!int.TryParse(input, out int enteredNumber))
            {
                return;
            }

            switch (enteredNumber)
            {
                case 1:
                    _sortingAlgorithm = new DefaultArraySortingAlgorithm();
                    break;
                case 2:
                    _sortingAlgorithm = new MergeSortingAlgorithm();
                    break;
                default:
                    _sortingAlgorithm = new DefaultArraySortingAlgorithm();
                    break;
            }
        }

        public static void StartSorting()
        {
            Console.Clear();
            if (!_isRandomFileGenerated)
            {
                Console.WriteLine("ERROR: You did not generate random file. Press any key to continue.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Sorting in progress...");
            StringSorter sorter = new StringSorter(_sortingAlgorithm);
            sorter.SortFile("GeneratedFile.txt");
            Console.Clear();
            Console.WriteLine("Sorting is complete. Press any key to continue.");
            Console.ReadKey();
        }
    }
}
