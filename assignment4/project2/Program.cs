using System;

class Clock
{
    public event EventHandler Tick;
    public event EventHandler Alarm;

    public void Start()
    {
        while (true)
        {
        Thread.Sleep(1000); 
            // 检查是否需要触发响铃事件
            if (DateTime.Now.Hour == 21 && DateTime.Now.Minute == 30)          
                OnAlarm(EventArgs.Empty);
            else
                // 触发嘀嗒事件
                Ontick(EventArgs.Empty);
        }
    }

     void Ontick(EventArgs e)
    {
       if (Tick != null)
                Tick(this, e);
    }

    void OnAlarm(EventArgs e)
    {
        if (Alarm != null)
            Alarm(this, e);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Clock clock = new Clock();

        // 注册嘀嗒事件的处理方法
        clock.Tick += (sender, e) =>
        {
            Console.WriteLine("Dida.");
        };

        // 注册响铃事件的处理方法
        clock.Alarm += (sender, e) =>
        {
            Console.WriteLine("Alarm!");
        };
        clock.Start();
    }
}
