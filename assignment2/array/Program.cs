using System;
public class array
{
    public static void Main(string[] args)
    {
        Console.WriteLine("请输入数组");
        int[] m=new int [8];
        for (int i = 0; i < 8; i++)
        {
            string s=Console.ReadLine();    
            int n=int.Parse(s);
            m[i]= n;
        }
        max(m);
        min(m);
        sum(m);
    }
    public static void max(int[] m1)
    {
        int maxsh= m1[0];
        for (int i = 1;i < m1.Length;i++)
        {
            if ( maxsh< m1[i])
                maxsh = m1[i]; 
           
        }
        Console.WriteLine("数组最大值"+ maxsh);
    }
    public static void min(int[] m1)
    {
        int minsh= m1[0];
        for (int i = 1; i < m1.Length; i++)
        {
            if (minsh > m1[i])
                minsh = m1[i];

        }
        Console.WriteLine("数组最小值" + minsh);
    }
    public static void sum(int[] m1) {
        int sumsh = 0;
        for (int i = 0;i < m1.Length;i++)
        {
            sumsh += m1[i];
        }
        double avaer=sumsh/m1.Length;
        Console.WriteLine("数组总和"+sumsh);
        Console.WriteLine("数组平均值" + avaer);
    }
}
