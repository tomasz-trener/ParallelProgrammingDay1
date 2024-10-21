class Example1
{

    

    public static void Main1(string[] args)
    { 
        var t = new Task(() =>
        {
            int i = 0;
            while (true)
            {
                Console.WriteLine($"{i++}\t");
            }
        });

        t.Start();

        Console.WriteLine("Press any key to cancel the task");
        Console.ReadKey();
    }


}


class Example2
{
    public static void Main(string[] args)
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;


        var t = new Task(() =>
        {
            int i = 0;
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Task cancelled");
                    break;
                }
                Console.WriteLine($"{i++}\t");
            }
        }, token);

        t.Start();

        Console.ReadKey();
        cts.Cancel();

        Console.WriteLine("Press any key to cancel the task");
        Console.ReadKey();
    }


}