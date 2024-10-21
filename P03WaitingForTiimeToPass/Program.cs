using System.Management;

class Example1
{
    public static void Main1(string[] args)
    {
        var t = new Task(()=>
        {
            Thread.Sleep(5000);
            // wstrzymuje wątek na 5 sekund
            // To jest oczekiwanie pasywne -- oddaje zasoby procesora 

            Thread.SpinWait(5);
            // oczekiwanie aktywne nie oddaje zasobów procesora 

            SpinWait.SpinUntil(() => false, 5000);
        });


        // czas trwania jednego cyklu = 1/częstotliwość procesora ( w Hz ) 

        // przykład : dla procesora o częstotliwości 2.5 GHz
        // 1/2.5 * 10^9 = 0.4 * 10^-9 = 0.4 ns
    }



}

class Example2
{
    public static void Main2(string[] args)
    {
        var searcher = new ManagementObjectSearcher("select MaxClockSpeed from Win32_Processor");

        foreach (var item in searcher.Get())
        {
            Console.WriteLine(item["MaxClockSpeed"]);
        }
    }



}

class Example3
{
    public static void Main(string[] args)
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        var t1 = new Task(() =>
        {
            Console.WriteLine("Press any key to disarm");

            bool cancelled = token.WaitHandle.WaitOne(5000);



            Console.WriteLine(cancelled? "Bomb1 disarm" : "Boom!!");


        }, token);

        t1.Start();

        var t2 = new Task(() =>
        {
            Console.WriteLine("Press any key to disarm");

            bool cancelled = token.WaitHandle.WaitOne(2000);



            Console.WriteLine(cancelled ? "Bomb2 disarm" : "Boom!!");


        }, token);

        t2.Start();

        Console.ReadKey();
        cts.Cancel();

        Console.WriteLine("Done!");
        Console.ReadKey();

    }
  

}