
using System.Collections.Concurrent;

class Example1
{
    static void Main()
    {

        var q = new ConcurrentQueue<int>();
        q.Enqueue(1);
        q.Enqueue(2);

        // przód kolejki to 1 

        if (q.TryDequeue(out int result))
        {
            System.Console.WriteLine($"Dequeued {result}");
        }

        if(q.TryPeek(out int peek))
        {
            System.Console.WriteLine($"Peeked {peek}");
        }
    }
}