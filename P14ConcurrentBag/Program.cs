
using System.Collections.Concurrent;

class Example1
{

    static void Main()
    {

        var bag = new ConcurrentBag<int>();

        var tasks = new List<Task>();

        for (int i = 0; i < 10; i++)
        {
            var iCopy = i;
            tasks.Add(Task.Run(() => bag.Add(iCopy)));


        }

        Task.WaitAll(tasks.ToArray());

        int last; 
        if(bag.TryTake(out last))
        {
            Console.WriteLine($"{last}");
        }


    }
}