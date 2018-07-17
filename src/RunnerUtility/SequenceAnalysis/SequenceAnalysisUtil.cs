using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SequenceAnalysis
{
    /// <summary>
    /// Specifies technqiue by which sequence
    /// need to be calculated
    /// </summary>
    public enum SeqTechnique
    {
        USING_HASHING = 1,
        USING_MERGESORT = 2,
        USING_LINQ = 3
    }
    public class SequenceAnalysisUtil
    {
        /// <summary>
        /// Finds a sorted subsequence of capital letters in a given string
        /// </summary>
        /// <param name="inputString">input string</param>
        /// <param name="technique">technique used</param>
        /// <param name="executionTime">execution time for the code</param>
        /// <returns>sorted capital letter sequence </returns>
        public string FindCapSequence(string inputString, int technique, out string executionTime)
        {
            string result = "";
            var watch = new Stopwatch();
            watch.Start();

            switch (technique)
            {
                case (int)SeqTechnique.USING_HASHING:
                    result = FindSeqByHashing(inputString);
                    break;
                case (int)SeqTechnique.USING_MERGESORT:
                    result = FindSeqByMergeSort(inputString);
                    break;
                case (int)SeqTechnique.USING_LINQ:
                    result = FindSeqByLINQ(inputString);
                    break;
                default:
                    result = "ERROR";
                    break;
            }

            watch.Stop();
            executionTime = watch.Elapsed.TotalMilliseconds.ToString();

            return result;

        }
        /// <summary>
        /// Finds Sorted Capital Sequence by Hashing O(n) complexity
        /// </summary>
        /// <param name="str">input string</param>
        /// <returns>sorted capital sequence in given string</returns>
        private string FindSeqByHashing(string str)
        {
            string result = "No Sequence Found";

            //Hash table for 26 Capital letters
            int[] hashTable = new int[26];

            //Intialize HashTable with zeros
            //meaning 0 occurence of any capital 
            //letter found so far

            for(int i=0;i<26;i++)
            {
                hashTable[i] = 0;
            }

            //Find capital letters in the input string 
            //and increment count of a capital letter
            //at its hash location
            foreach (char c in str)
            {
                if (c >= 65 && c <= 90)
                {
                    hashTable[c - 'A']++;

                }
            }

            //print Capital letters from the hashTable         
            var resultBuilder = new StringBuilder();

            for(int i=0;i<26;i++)
            {
               for(int j = hashTable[i]; j> 0;j--)
                { 
                    resultBuilder.Append((char)(65 +i));
                    
                }
            }

            if (resultBuilder.ToString().Length > 0)
                result = resultBuilder.ToString();

            return result;
        }
        /// <summary>
        /// Finds Sorted Capital Sequence by MergeSort O(nLogn) complexity
        /// </summary>
        /// <param name="str">input string</param>
        /// <returns>sorted capital sequence in given string</returns>
        private string FindSeqByMergeSort(string str)
        {
            string result = "No Sequence Found";

            //first find out substring of capital letters in given string          
            var capitalSeq = FindUnsortedCapitalSeq(str);

            if (capitalSeq.Length>0)
            {
                //Prepare an Ascii array and perform merge sort
                var asciiArr = new int[capitalSeq.Length];

                for (int i = 0; i < capitalSeq.Length; i++)
                {
                    asciiArr[i] = capitalSeq[i];
                }

                MergeSort(asciiArr, 0, capitalSeq.Length - 1);

                //Convert sorted array back to string
                var resultBuilder = new StringBuilder();

                foreach (var x in asciiArr)
                {
                    resultBuilder.Append((char)x);
                }
                result = resultBuilder.ToString(); 
            }

            //return sorted capital Array
            return result;
        }
        /// <summary>
        /// Finds Sorted Capital Sequence by LINQ O(n) complexity
        /// </summary>
        /// <param name="str">input string</param>
        /// <returns>sorted capital sequence in given string</returns>
        private string FindSeqByLINQ(string s)
        {
            string result = "";

            if (!String.IsNullOrEmpty(s))
            {              
                result= new string(s.OrderBy(c => c).Where(c => c >= 'A' && c <= 'Z').ToArray());
            }

            return (!String.IsNullOrEmpty(result)) ? result : "No Sequence Found";
           
        }
        /// <summary>
        /// Finds Unsorted capital sequence from input string 
        /// </summary>
        /// <param name="s"> input string</param>
        /// <returns></returns>
        private static string FindUnsortedCapitalSeq(string s)
        {
            var builder = new StringBuilder();
            foreach (char c in s)
            {
                if (c >= 65 && c <= 90)
                {
                    builder.Append(c);
                }
            }

            return builder.ToString();
        }

        
        private static void Merge(int[] numbers, int left, int mid, int right)
        {
            int[] temp = new int[25];
            int i, eol, num, pos;
            eol = (mid - 1);
            pos = left;
            num = (right - left + 1);
            while ((left <= eol) && (mid <= right))
            {
                if (numbers[left] <= numbers[mid])
                    temp[pos++] = numbers[left++];
                else
                    temp[pos++] = numbers[mid++];
            }
            while (left <= eol)
                temp[pos++] = numbers[left++];
            while (mid <= right)
                temp[pos++] = numbers[mid++];
            for (i = 0; i < num; i++)
            {
                numbers[right] = temp[right];
                right--;
            }
        }
        private static void MergeSort(int[] numbers, int left, int right)
        {
            int mid;
            if (right > left)
            {
                mid = (right + left) / 2;
                MergeSort(numbers, left, mid);
                MergeSort(numbers, (mid + 1), right);
                Merge(numbers, left, (mid + 1), right);
            }
        }
    }
}
