using System;
using System.Collections;
public class primefactor {
    public static void Main(string[] args)
    {
        Console.WriteLine("请输入数据：");
        string s=Console.ReadLine();
        int m=int.Parse(s);
        division(m);
    }
   public  static void division(int n)
    {
        Queue q = new Queue();
        for (int i = 2; i <=n ; i++)
        {
           
            while(n % i == 0)
            {

                q.Enqueue(i);
                n = n / i;

            }         
            
        }
        foreach (int c in q)
            Console.Write(c + " ");
    }
}
