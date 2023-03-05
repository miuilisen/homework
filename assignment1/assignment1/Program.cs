using System;
using System.Diagnostics;

public class calcultor
{
    public static void Main(string[] args)
    {
        string a = "",b="", s = "";
        double m=0,n=0,c = 0;
        Console.WriteLine("请输入a、运算符、b：");
        a = Console.ReadLine();
        m = double.Parse(a);
        s = Console.ReadLine();
        b= Console.ReadLine();
        n= double.Parse(b);
        switch (s)
        {
            case "+":
                c = m + n;
                break;
            case "-":
                c = m - n;
                break;
            case "/":
                c = m / n;
                break;
            case "*":
                c = m * n;
                break;
            default: /* 可选的 */
                break;
        }
                Console.WriteLine("结果"+c);

        
    }
    

}
