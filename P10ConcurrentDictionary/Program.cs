
using System.Collections.Concurrent;

class Example1 {

    private static ConcurrentDictionary<string, string> capitals = new ConcurrentDictionary<string, string>();

    public static void AddCity()
    {
        bool success = capitals.TryAdd("Japan", "Tokyo");

         string who = Task.CurrentId.HasValue ? ("Task " + Task.CurrentId.Value) : "Main Thread";
        Console.WriteLine($"{who} { (success ? "added" : "did not add")} the element");

    }

    public static async Task Main(String[] args)
    {
     //   Task.Factory.StartNew(AddCity);
        await Task.Run(() => AddCity());

        capitals["Poland"] = "Krakow";

        capitals.AddOrUpdate("Poland", "Warsaw", (k, v) => v + " => Warsaw");

        AddCity();
    }


}