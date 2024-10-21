class Example1
{
    public static void Main1(string[] args)
    {

        Task.Factory.StartNew(() =>
        {
            throw new Exception("Task exception");

        });

        Console.WriteLine("Main thread completed");
        Console.ReadKey();
    }

}

class Example2
{
    public static void Main2(string[] args)
    {

        var t = Task.Factory.StartNew(() =>
        {
            throw new InvalidOperationException("Task exception") { Source = "t"};
        });

        var t2 = Task.Factory.StartNew(() =>
        {
            throw new InvalidOperationException("Task exception") { Source = "t2" };
        });


        try
        {
            Task.WaitAll(t, t2);
        }
        catch (AggregateException ex)
        {
            foreach (var e in ex.InnerExceptions)
            {
                Console.WriteLine($"Exception from {e.Source}: {e.Message}");
            }
        }

        Console.WriteLine("Main thread completed");
        Console.ReadKey();
    }

}


class Example3
{
    private static void Test()
    {
        var t = Task.Factory.StartNew(() =>
        {
            throw new InvalidOperationException("Task exception") { Source = "t" };
        });

        var t2 = Task.Factory.StartNew(() =>
        {
            throw new AccessViolationException("Cant Access") { Source = "t2" };
        });


        try
        {
            Task.WaitAll(t, t2);
        }
        catch (AggregateException ae)
        {
            ae.Handle(e =>
            {
                if (e is InvalidOperationException)
                {
                    Console.WriteLine("Invaild op");
                    return true; // oznacza ze obsluzylimy ten blad
                }
                else return false;
            });
        }
    }


    public static void Main3(string[] args)
    {
        Test();

        Console.WriteLine("Main thread completed");
        Console.ReadKey();
    }

}


class Example4
{
    private static void Test()
    {
        var t = Task.Factory.StartNew(() =>
        {
            throw new InvalidOperationException("Task exception") { Source = "t" };
        });

        var t2 = Task.Factory.StartNew(() =>
        {
            throw new AccessViolationException("Cant Access") { Source = "t2" };
        });


        try
        {
            Task.WaitAll(t, t2);
        }
        catch (AggregateException ae)
        {
            ae.Handle(e =>
            {
                if (e is InvalidOperationException)
                {
                    Console.WriteLine("Invaild op");
                    return true; // oznacza ze obsluzylimy ten blad
                }
                else return false;
            });
        }
    }


    public static void Main(string[] args)
    {
        try
        {
            Test();
        }
        catch (AggregateException ae)
        {
            foreach (var e in ae.InnerExceptions)
            {
                Console.WriteLine($"Exception from {e.Source}: {e.Message}");
            }

        }
       

        Console.WriteLine("Main thread completed");
        Console.ReadKey();
    }

}