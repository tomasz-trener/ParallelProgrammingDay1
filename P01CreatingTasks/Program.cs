
class Example1
{

    public static void Write(char c)
    {
        for (int i = 0; i < 1000; i++)
        {
            Console.Write(c);
        }
    }

    public static void Main0(string[] args)
    {
        Task.Factory.StartNew(() => Write('.')); // tworzy i od razu uruchamia nowe zadanie

        var t = new Task(() => Write('?')); // tworzy nowe zadanie
        t.Start(); // uruchamia nowe zadanie

        Write('-');

        Console.WriteLine("done");
        Console.ReadKey();
    }


}

class Example2
{

    public static void Write(object o)
    {
        for (int i = 0; i < 1000; i++)
        {
            Console.Write(o);
        }
    }

    public static void Main1(string[] args)
    {
        Task.Factory.StartNew(Write,"hey"); // tworzy i od razu uruchamia nowe zadanie

        var t = new Task(Write,213); // tworzy nowe zadanie
        t.Start(); // uruchamia nowe zadanie

        Write('-');

        Console.WriteLine("done");
        Console.ReadKey();
    }


}

class Example3
{

    public static int TextLength(object o)
    {
        return o.ToString().Length;
    }

    public static async Task Main(string[] args)
    {
        string text1 = "testing", text2 = "this";

        Task<int> task1 = new Task<int>(TextLength, text1);
        task1.Start();

        Task<int> task2 = Task.Factory.StartNew(TextLength, text2);


        Console.WriteLine("Length of '{0}' is {1}", text1, task1.Result);

        var result = await task2;
        Console.WriteLine("Length of '{0}' is {1}", text2, result);

        Console.WriteLine("done");
        Console.ReadKey();
    }


}