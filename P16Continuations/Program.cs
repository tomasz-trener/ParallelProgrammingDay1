class Example1
{

    public static void Main1()
    {
        var task = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Boiling water");
        });

        var task2 = task.ContinueWith(t =>
        {
            Console.WriteLine($"Complited task {t.Id} pour the water into cup");
        });

        task2.Wait();
    }
}


class Example2
{

    public static void Main()
    {
        //  SimpleContinuation();
        ContinueWen();
    }

    private static void SimpleContinuation()
    {
        var task = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Boiling water");
            throw new Exception("Error");
        });

        var task2 = task.ContinueWith(t =>
        {
            if (t.IsFaulted)
                Console.WriteLine($"Error in task {t.Id}");
            else
                Console.WriteLine($"Complited task {t.Id} is {t.Status} pour the water into cup {Task.CurrentId}");

        });

        try
        {
            task2.Wait();
        }
        catch (AggregateException ae)
        {
            ae.Handle(e =>
            {
                Console.WriteLine(e.Message);
                return true;
            });
        }

    }


    private static void ContinueWen()
    {
        var task = Task.Factory.StartNew(() => "Task 1");
        var task2 = Task.Factory.StartNew(() => "Task 2");

        var task3 = Task.Factory.ContinueWhenAll(new[] { task, task2 },
            tasks =>
        {
            Console.WriteLine("Tasks completed:");

            foreach (var t in tasks)
            {
                Console.WriteLine(" - " + t.Result);
            }
            return task2;
        });

        task3.Wait();
    }
}