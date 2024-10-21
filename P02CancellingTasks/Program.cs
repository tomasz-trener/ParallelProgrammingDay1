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
    public static void Main2(string[] args)
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
    //https://github.com/tomasz-trener/ParallelProgrammingDay1

}

class Example3
{
    public static void Main3(string[] args)
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;


        var t = new Task(() =>
        {
            int i = 0;
            while (true)
            {
                token.ThrowIfCancellationRequested();
                //if (token.IsCancellationRequested)
                //{
                //    throw new OperationCanceledException(token);
                //}
                Console.WriteLine($"{i++}\t");
            }
        }, token);

        t.Start();

        Console.ReadKey();
        cts.Cancel();

        Console.WriteLine("Press any key to cancel the task");
        Console.ReadKey();
    }
    //https://github.com/tomasz-trener/ParallelProgrammingDay1

}

class Example4
{
    public static void Main(string[] args)
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        token.Register(() =>
        {
            Console.WriteLine("Cancellation requested");
          //  token.ThrowIfCancellationRequested(); // unikamy takiego podejscia
        });

        var t = new Task(() =>
        {
            int i = 0;
            while (true)
            {
                token.ThrowIfCancellationRequested();
                //if (token.IsCancellationRequested)
                //{
                //    throw new OperationCanceledException(token);
                //}
                Console.WriteLine($"{i++}\t");
            }
        }, token);

        t.Start();

        Console.ReadKey();
        cts.Cancel();

        Console.WriteLine("Press any key to cancel the task");
        Console.ReadKey();
    }
    //https://github.com/tomasz-trener/ParallelProgrammingDay1

}