using System;
abstract class shape
{
    public abstract double GetAera();
    public abstract void IsTrue();
}
class rectangle : shape
{
    double x1, x2, y1, y2;
    public rectangle()
    {
        Console.Write("请输入矩形第一条边长：");
        x1 = double.Parse(Console.ReadLine());

        Console.Write("请输入矩形第二条边长：");
        x2 = double.Parse(Console.ReadLine());

        Console.Write("请输入矩形第三条边长：");
        y1 = double.Parse(Console.ReadLine());

        Console.Write("请输入矩形第四条边长：");
        y2 = double.Parse(Console.ReadLine());
    }
    public override double GetAera()
    {
        Console.WriteLine(x1 * y1);
        return x1 * y1;
    }
    public override void IsTrue()
    {
        if (x1 > 0 && x2 > 0 && y1 > 0 && y2 > 0 && x1 == x2 && y1 == y2)
        {
            Console.WriteLine("合法");
        }
        else { Console.WriteLine("不合法"); }
    }
}
class square : shape
{
    double a, b, c, d;
    public square()
    {
        Console.Write("请输入正方形第一条边长：");
        a = double.Parse(Console.ReadLine());

        Console.Write("请输入正方形第二条边长：");
        b = double.Parse(Console.ReadLine());

        Console.Write("请输入正方形第三条边长：");
        c = double.Parse(Console.ReadLine());

        Console.Write("请输入正方形第四条边长：");
        d = double.Parse(Console.ReadLine());
    }
    public override double GetAera()
    {
        Console.WriteLine(a * b);
        return a * b;
    }
    public override void IsTrue()
    {
        if (a > 0 && b > 0 && c > 0 && d > 0 && a == b && b == c && c == d)
        {
            Console.WriteLine("合法");
        }
        else { Console.WriteLine("不合法"); }
    }
}
class triangle : shape
{
    double a, b, c, s;
    public triangle()
    {
        Console.Write("请输入三角形第一条边长：");
        a = double.Parse(Console.ReadLine());

        Console.Write("请输入三角形第二条边长：");
        b = double.Parse(Console.ReadLine());

        Console.Write("请输入三角形第三条边长：");
        c = double.Parse(Console.ReadLine());


    }
    public override double GetAera()
    {
        s = (a + b + c) / 2;

        Console.WriteLine(Math.Sqrt(s * (s - a) * (s - b) * (s - c)));
        return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
    }
    public override void IsTrue()
    {
        if (a <= 0 && b <= 0 && c <= 0)
            Console.WriteLine("不合法");
        else if (a + b > c && a + c > b && b + c > a)
            Console.WriteLine("合法");
        else Console.WriteLine("不合法");
    }
}
class Program
{
    public static void Main(string[] args)
    {
        rectangle A = new rectangle();
        A.IsTrue();
        A.GetAera();
        square B = new square();
        B.IsTrue();
        B.GetAera();
        triangle C = new triangle();
        C.IsTrue();
        C.GetAera();

    }
}