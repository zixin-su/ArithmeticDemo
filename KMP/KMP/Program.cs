using System;
using System.CodeDom;

namespace KMP
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var idx = KMPSearch(
                "asdaaaaabcdaabaaaabcdaabaabfjyytaabcdcsbfjyytaabcdcsabcdaabfjyytaabcdcsfjjkashgklajslkgjlksajkaabcdaabfjyytaabsjhfjsafaabcdaabfjyytaabcdcs",
                "aabcdaabaabfjyytaabcdcs");
            Console.WriteLine("------->>>  " + idx);
        }


        /// <summary>
        /// 使用kmp算法查找子字符串的位置
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static int KMPSearch(string str, string pattern)
        {
            //构造index数组
            var len = pattern.Length;
            var index = new int[len];
            index[0] = -1;
            for (int i = 1; i < len; i++)
            {
                var c = pattern[i];
                var lastComIdx = index[i - 1];
                if (lastComIdx >= 0)
                {
                    if (c == pattern[lastComIdx + 1])
                    {
                        index[i] = lastComIdx + 1;
                    }
                    else
                    {
                        var j = lastComIdx - 1;
                        while (true)
                        {
                            if (j < 0 || index[j] < 0)
                            {
                                index[i] = c == pattern[0] ? 0 : -1;
                                break;
                            }

                            if (c == pattern[j + 1])
                            {
                                index[i] = index[j] + 1;
                                break;
                            }

                            j = index[j];
                        }
                    }
                }
                else
                {
                    index[i] = c == pattern[0] ? 0 : -1;
                }
            }

            var comIdx = -1;
            var finded = true;
            for (int i = 0; i < str.Length;)
            {
                for (int j = comIdx + 1; j < pattern.Length; j++)
                {
                    finded = true;
                    if (pattern[j] != str[i - comIdx + j])
                    {
                        comIdx = index[j];
                        finded = false;
                        break;
                    }
                }

                if (finded)
                {
                    return i - comIdx;
                }

                ++i;
            }


//            for (int i = 0; i < index.Length; i++)
//            {
//                Console.Write(index[i] + "  ");
//            }

            return -1;
        }
    }
}