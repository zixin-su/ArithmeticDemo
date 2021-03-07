using System;
namespace KMP
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var idx = KMPSearch(
                "ababaababaa",
                "aababaa");
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
            if (str.Length < pattern.Length)
                return -1;

            //首先构造index数组
            //index[i]表示 pattern[0~i] 的最大公共前后缀的长度(这里将长度做了一个减1处理，看做是公共前缀结尾的下标)，
            //如果index[i]<0,则表示 pattern[0~i] 没有公共前后缀
            var len = pattern.Length;
            var index = new int[len];
            index[0] = -1;
            for (int i = 1; i < len; i++)
            {
                var c = pattern[i];
                var lastComIdx = index[i - 1];
                //计算pattern[0~i]的公共前后缀
                if (lastComIdx >= 0)
                {
                    //如果pattern[0~i-1]存在公共前后缀
                    //则比较当前字符与index[i-1]的后一个字符
                    //如果这两个字符相等，index[i]可以在index[i-1]的基础上直接+1
                    if (c == pattern[lastComIdx + 1])
                    {
                        index[i] = lastComIdx + 1;
                    }
                    else
                    {
                        //重点:
                        //如果当前字符与index[i-1]的后一个字符不等
                        //则可以看做是寻找在index[i-1]之后加上当前字符的字符串的公共前后缀（因为前缀和后缀相同, 这里有一点动态规划的思想）
                        //然后就可以使用index数组中已有的数据来进行递推计算公共前后缀了（非常精妙）
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
                    //如果pattern[0~i-1]不存在公共前后缀，则直接比较当前字符和首字符是否相等
                    index[i] = c == pattern[0] ? 0 : -1;
                }
            }

//            for (int i = 0; i < index.Length; i++)
//            {
//                Console.Write(index[i] + "  ");
//            }

            //接下来就是很简单的字符串比较，当比较到某个字符不相等时，根据Index中记录的公共前后缀信息来决定下一次比较的起始字符
            //可以大幅减少比较次数
            var comIdx = -1;
            for (int i = 0; i < str.Length;)
            {
                for (int j = comIdx + 1; j < pattern.Length && i < str.Length; j++)
                {
                    if (pattern[j] != str[i])
                    {
                        if (j < 1)
                            ++i;
                        else
                            comIdx = index[j - 1];
                        break;
                    }

                    if (j == pattern.Length - 1)
                        return i - j;
                    ++i;
                }
            }
            return -1;
        }
    }
}