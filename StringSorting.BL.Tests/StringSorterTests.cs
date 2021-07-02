using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringSorting.BL.Tests
{
    [TestClass]
    public class StringSorterTests
    {
        [TestMethod]
        public void FileSortTest()
        {
            string textToSort = "bbbbbbbb\n" +
                          "aaaaaaaa\n" +
                          "ffffffff\n" +
                          "abbbaaaa\n" +
                          "xxxxxxxx";
            string expectedText = "aaaaaaaa\n" +
                                  "abbbaaaa\n" +
                                  "bbbbbbbb\n" +
                                  "ffffffff\n" +
                                  "xxxxxxxx";
                                  string testFilePath = "testFile.txt";
            File.WriteAllText(testFilePath, textToSort);

            StringSorter sorter = new StringSorter(new DefaultArraySortingAlgorithm());

            sorter.SortFile(testFilePath);

            string sortedText = File.ReadAllText(testFilePath);

            Assert.AreEqual(expectedText, sortedText);
        }
    }
}
