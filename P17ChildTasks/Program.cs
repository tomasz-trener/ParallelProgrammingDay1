class Example1
{

    static void Main1()
    {
        var parent = new Task(() =>
        {
            
        });

        parent.Start();

        var child = new Task(() =>
        {
            Console.WriteLine("Child task starting");
            Thread.Sleep(3000);
            Console.WriteLine("Child task finishing");
        }, TaskCreationOptions.AttachedToParent);

        child.Start();

        try
        {
            parent.Wait();
        }
        catch (AggregateException ae)
        {
            ae.Handle(e =>
            {
                Console.WriteLine(e);
                return true;
            });
        }
    }
}

class Example2
{

    static void Main()
    {
        var parent = new Task(() =>
        {
            var child = new Task(() =>
            {
                Console.WriteLine("Child task starting");
                Thread.Sleep(3000);
                Console.WriteLine("Child task finishing");

                throw new Exception("Child task exception");
            }, TaskCreationOptions.AttachedToParent);

         
            var failHandler = child.ContinueWith(t =>
            {
                Console.WriteLine($"Task {t.Id } faild:  {t.Status}");

            }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

            var completionHandler = child.ContinueWith(t =>
            {
                Console.WriteLine($"Task {t.Id} success:  {t.Status}");

            }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

            child.Start();
            Console.WriteLine("starting ");
            Thread.Sleep(1000);
            Console.WriteLine("finishing ");

        });

        parent.Start();

        try
        {
            parent.Wait();
        }
        catch (AggregateException ae)
        {
            ae.Handle(e =>
            {
                Console.WriteLine(e);
                return true;
            });
        }



    }
}