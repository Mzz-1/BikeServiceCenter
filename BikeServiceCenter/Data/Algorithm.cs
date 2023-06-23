using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeServiceCenter.Data.Model;

namespace BikeServiceCenter.Data
{

    public class Algorithm
        {
            // Sorts the provided list and returns the sorted array
            public static List<Item> MergeSort(List<Item> unsorted)
            {
                if (unsorted.Count <= 1)
                    return unsorted;

                var left = new List<Item>();
                var right = new List<Item>();

                int middle = unsorted.Count / 2;

                for (int i = 0; i < middle; i++)
                {
                    left.Add(unsorted[i]);
                }

                for (int i = middle; i < unsorted.Count; i++)
                {
                    right.Add(unsorted[i]);
                }

                left = MergeSort(left);
                right = MergeSort(right);
                return Merge(left, right);
            }

            public static List<Item> Merge(List<Item> left, List<Item> right)
            {
                var result = new List<Item>();

                while (left.Count > 0 || right.Count > 0)
                {
                    if (left.Count > 0 && right.Count > 0)
                    {
                        if (left.First().DateAdded <= right.First().DateAdded)
                        {
                            result.Add(left.First());
                            left.Remove(left.First());
                        }
                        else
                        {
                            result.Add(right.First());
                            right.Remove(right.First());
                        }
                    }
                    else if (left.Count > 0)
                    {
                        result.Add(left.First());
                        left.Remove(left.First());
                    }
                    else if (right.Count > 0)
                    {
                        result.Add(right.First());

                        right.Remove(right.First());
                    }
                }
                return result;
            }
        }
    
}
