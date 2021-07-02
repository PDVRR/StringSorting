using System;

namespace StringSorting.BL
{
    public class MergeSortingAlgorithm : ISortingAlgorithm
    {
        public void Sort(ref string[] stringsToSort)
        {
            MergeSort(ref stringsToSort, 0, stringsToSort.Length - 1);
        }

        private void MergeSort(ref string[] strings, int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            int middle = (left + right) / 2;
            MergeSort(ref strings, left, middle);
            MergeSort(ref strings, middle + 1, right);
            MergeArrays(ref strings, left, middle, right);
        }

        private void MergeArrays(ref string[] strings, int left, int middle, int right)
        {
            string[] leftArray = new string[middle + 1 - left];
            string[] rightArray = new string[right - middle];

            Array.Copy(strings, left, leftArray, 0, middle + 1 - left);
            Array.Copy(strings, middle + 1, rightArray, 0, right - middle);

            int leftArrayIndex = 0, rightArrayIndex = 0, sourceArrayIndex = left;

            while (leftArrayIndex < leftArray.Length && rightArrayIndex < rightArray.Length)
            {
                if (leftArray[leftArrayIndex].CompareTo(rightArray[rightArrayIndex]) < 0)
                {
                    strings[sourceArrayIndex] = leftArray[leftArrayIndex];
                    leftArrayIndex++;
                }
                else
                {
                    strings[sourceArrayIndex] = rightArray[rightArrayIndex];
                    rightArrayIndex++;
                }

                sourceArrayIndex++;
            }

            while (leftArrayIndex < leftArray.Length)
            {
                strings[sourceArrayIndex] = leftArray[leftArrayIndex];
                leftArrayIndex++;
                sourceArrayIndex++;
            }

            while (rightArrayIndex < rightArray.Length)
            {
                strings[sourceArrayIndex] = rightArray[rightArrayIndex];
                rightArrayIndex++;
                sourceArrayIndex++;
            }
        }
    }
}