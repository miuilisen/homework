using System;

// 定义形状接口
public interface IShape
{
    double GetArea();
}

// 定义圆形类
public class Circle : IShape
{
    private double radius;

    public Circle(double r)
    {
        radius = r;
    }

    public double GetArea()
    {
        Console.WriteLine("创建圆，面积:"+Math.PI * radius * radius);
        return Math.PI * radius * radius;
    }
}

// 定义矩形类
public class Rectangle : IShape
{
    private double width;
    private double height;

    public Rectangle(double w, double h)
    {
        width = w;
        height = h;
    }

    public double GetArea()
    {
        Console.WriteLine("创建矩形，面积:"+width * height);
        return width * height;
    }
}
//定义三角形类
public class Triangle : IShape
{
    private double a;
    private double b;
    private double c;
    private double s;
    public Triangle(double x, double y,double z)
    {
        this.a = x; 
        this.b = y; 
        this.c = z;
    }
    public double GetArea()
    {
        s = (a + b + c) / 2;

        Console.WriteLine("创建三角形，面积:" + Math.Sqrt(s * (s - a) * (s - b) * (s - c)));
        return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
    }

}

// 定义形状工厂类
public class ShapeFactory
{
    private static Random rand = new Random();

    public static IShape CreateShape()
    {
        int shapeType = rand.Next(3);
        switch (shapeType)
        {
            case 0:
                double radius = rand.Next(1, 10);
                return new Circle(radius);
            case 1:
                double width = rand.Next(1, 10);
                double height = rand.Next(1, 10);
                return new Rectangle(width, height);
                case 2:
                
                return new Triangle(3,4,5);
            default:
                return null;
        }
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        List<IShape> shapes = new List<IShape>();
        for (int i = 0; i < 10; i++)
        {
            IShape shape = ShapeFactory.CreateShape();
            shapes.Add(shape);
        }

        double totalArea = 0.0;
        foreach (IShape shape in shapes)
        {
            totalArea += shape.GetArea();
        }

        Console.WriteLine("Total area of the shapes: {0}", totalArea);
       
    }
}

