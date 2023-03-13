using System;
using System.Collections;
public class prime
{
    public static void Main(string[] args)
    {
        Queue q = new Queue();
        
        for (int i = 2; i <101 ; i++)
        {
            int j = 2;
            while (i % j != 0 && j < i+2)
            {
                
                j++;
            }
            if (j == i)
            {
                q.Enqueue(i);
            }
        }
        foreach (int c in q)
        { Console.Write(c + " "); }
        Console.WriteLine();
        Console.WriteLine("2-100素数总个数："+q.Count);
    }
}