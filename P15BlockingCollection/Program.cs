using System.Collections.Concurrent;

class Example1
{

    static BlockingCollection<int> messages = new BlockingCollection<int>(new ConcurrentBag<int>(),10);

    static Random random = new Random();

    static CancellationTokenSource cts = new CancellationTokenSource();
    private static void RunProducer()
    {
        while (true)
        {
            cts.Token.ThrowIfCancellationRequested();
            int i = random.Next(100);

            messages.Add(i);

            Console.WriteLine($"+{i}\t");

            Thread.Sleep(random.Next(1000));
        }
    }

    private static void RunConsumer()
    {
        foreach (var item in messages.GetConsumingEnumerable())
        {
            cts.Token.ThrowIfCancellationRequested();

            Console.WriteLine($"-{item}");

            Thread.Sleep(random.Next(1000));
        } 
    }

    public static void ProduceAndConsume()
    {
        var producer = Task.Factory.StartNew(RunProducer);
        var consumer = Task.Factory.StartNew(RunConsumer);

        try
        {
            Task.WaitAll(new[] { producer, consumer }, cts.Token);
        }
        catch (AggregateException ae)
        {
            ae.Handle(e=>true);
        }
    }

    static void Main()
    {
        Task.Factory.StartNew(ProduceAndConsume);

        Console.ReadLine();
        cts.Cancel();

        Console.ReadLine();
    }
   

}