using System;

namespace StringSorting.BL
{
    public class DefaultArraySortingAlgorithm : ISortingAlgorithm
    {
        public void Sort(ref string[] stringsToSort)
        {
            Array.Sort(stringsToSort);
        }
    }
}