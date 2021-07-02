using System;
using System.IO;

namespace StringSorting.BL
{
    public class FileGenerator
    {
        public static void Generate(long rowsCount, long maxLength, string path = "GeneratedFile.txt")
        {
            using StreamWriter file = new StreamWriter(path);

            for (int i = 0; i < rowsCount; i++)
            {
                var randomString = GenerateRandomString(maxLength);
                file.WriteLine(randomString);
            }
        }

        private static string GenerateRandomString(long maxLength)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var randomStringChars = new char[maxLength];

            var random = new Random();
            for (int i = 0; i < randomStringChars.Length; i++)
            {
                randomStringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(randomStringChars);
        }
    }
}