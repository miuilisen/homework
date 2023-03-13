using System;
using System.Runtime.InteropServices;

public class Matrucul
{
    public static void Main(string[] args)
    {
        int[,] a = new int[3, 4] { {1,2,3,4} ,   /*  初始化索引号为 0 的行 */
                                   {5,1,2,3} ,   /*  初始化索引号为 1 的行 */
                                   {9,5,1,2}   /*  初始化索引号为 2 的行 */};
   Console.WriteLine(isMatricul(a));
    }
    public static bool isMatricul(int[,] m)
    {
        bool answer = false;
        for (int i = 0; i < m.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < m.GetLength(1) - 1; j++)
            {
                if (m[i, j] != m[i + 1, j + 1])
                {
                    return answer;
                }
            
            }
        }
        return true;
    }
}
                                                                                                                                                                         