class Example1
{


    static Barrier barrier = new Barrier(2, (b) =>
    {
        Console.WriteLine($"Barrier action {b.CurrentPhaseNumber} has been called");
    });

    public static void Water()
    {
        Console.WriteLine("Im putting the kettle on the stove");
        Thread.Sleep(2000);

        barrier.SignalAndWait();

        Console.WriteLine("Pouring water inro the cup");

        barrier.SignalAndWait();
        Console.WriteLine("Putting the kettle away ");
    }

    public static void Tea()
    {
        Console.WriteLine("Finding the most beautyfull cup");

        barrier.SignalAndWait();
        Console.WriteLine("Putting the tea bag in the cup");
        barrier.SignalAndWait();
        Console.WriteLine("Putting the cup away");
    }

    static void Main(string[] args)
    {
        var t1 = Task.Factory.StartNew(Water);
        var t2 = Task.Factory.StartNew(Tea);

        var tea = Task.Factory.ContinueWhenAll(new[] { t1, t2 }, (tasks) =>
        {
            Console.WriteLine("All tasks are finished");
        });

        tea.Wait();
    }
}