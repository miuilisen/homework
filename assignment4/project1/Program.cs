using System;
using System.Collections.Generic;

public class Node<T>
{
    public Node<T> Next { get; set; }
    public T Data { get; set; }
    public Node(T t)
    {
        Next = null;
        Data = t;
    }
}
public class GenericList<T>
{
    private Node<T> head;
    private Node<T> tail;
    public GenericList()
    {

        tail = head = null;
    }
    public Node<T> Head
    {
        get => head;
    }
    public void Add(T t) { 
        Node<T> n=new Node<T>(t);
        if(tail==null)
        {
            head=tail = n;
        }
        else
        {
            tail.Next = n;
            tail = n;
        }
}
    //添加Foreach方法
    public void ForEach(Action<T> action)
    {
        Node<T> tem = head;
        while (tem != null)
        {
            action(tem.Data);
            tem = tem.Next;
        }
    }

}
public class program
{
    static void Main(string[] args)
    {
        GenericList<int> list1 = new GenericList<int>();
        list1.Add(1);    
        list1.Add(2);
        list1.Add(3);
        list1.Add(4);
        list1.Add(5);
        list1.ForEach(data => Console.WriteLine(data));

        int max=int.MinValue ;
        int min=int.MaxValue;
        int sum = 0;

        list1.ForEach(data =>
        {
            if (data > max)
                max = data;

            if (data < min)
                min = data;

            sum += data;
        });

        Console.WriteLine("最大值: " + max);
        Console.WriteLine("最小值: " + min);
        Console.WriteLine("Sum: " + sum);
    }
}