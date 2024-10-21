class Example1
{
    public static void Main1(string[] args)
    {
        var ctn = new CancellationTokenSource();
        var token = ctn.Token;

        var t = new Task(() =>
         {
             for (int i = 0; i < 5; i++)
             {
                 token.ThrowIfCancellationRequested();
                 Thread.Sleep(1000);
             }

         }, token);

        t.Start();


        var t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);


        Task.WaitAll(new[] { t, t2 }, 4000, token);

        Console.WriteLine($"Task t status: {t.Status}");
        Console.WriteLine($"Task t2 status: {t2.Status}");

        Console.ReadKey();

    }
}

class Example2
{
    public static void Main(string[] args)
    {
        var ctn = new CancellationTokenSource();
        var token = ctn.Token;

        var t = new Task(() =>
        {
            for (int i = 0; i < 5; i++)
            {
                token.ThrowIfCancellationRequested();
                Thread.Sleep(1000);
            }

        }, token);

        t.Start();


        var t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);


        Task.WaitAny(new[] { t, t2 }); 

        Console.WriteLine($"Task t status: {t.Status}");
        Console.WriteLine($"Task t2 status: {t2.Status}");

        Console.ReadKey();

    }

    // statusy: 
/*
 * runing - zadanie jest wykonywane
 * RanToCompletion - zadanie zostało zakończone
 * Cancelled - zadanie zostało anulowane
 * Faulted - zadanie zakończyło się błędem
 */ 

}