using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringSorting.BL.Tests
{
    [TestClass]
    public class FileGeneratorTests
    {
        private int _rows = 5;
        private int _maxLength = 12;
        private string _filePath = "GeneratedFile.txt";

        [TestMethod]
        public void FileCreatedTest()
        {
            FileGenerator.Generate(_rows, _maxLength);
            Assert.IsTrue(File.Exists(_filePath));
        }

        [TestMethod]
        public void RowsCountAsExpectedTest()
        {
            FileGenerator.Generate(_rows, _maxLength);

            var lines = File.ReadAllLines(_filePath);

            Assert.AreEqual(_rows, lines.Length);
        }

        [TestMethod]
        public void MaxRowLengthAsExpectedTest()
        {
            FileGenerator.Generate(_rows, _maxLength);

            var lines = File.ReadAllLines(_filePath);

            Assert.AreEqual(_maxLength, lines[0].Length);
        }
    }
}
