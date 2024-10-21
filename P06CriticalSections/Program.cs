class Example1
{
    public class BankAccount
    {
        public int Balance { get; private set; }

        public void Deposit(int amount)
        {
            Balance += amount; // to nie jest 1 operacja tylko 2 :  1) sumowanie , 2) przypisanie te 2 operacje
            // nie są atomowe
        }

        public void Withdraw(int amount)
        {
            Balance -= amount;
        }

        public static void Main1(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Deposit(100);
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Withdraw(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");

        }

    }
}


class Example2
{
    public class BankAccount
    {
        public object padlock = new object();

        public int Balance { get; private set; }

        public void Deposit(int amount)
        {
            lock (padlock)
            {
                Balance += amount;
            }
        
        }

        public void Withdraw(int amount)
        {
            lock (padlock)
            {
                Balance -= amount;
            }
        }

        public static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Deposit(100);
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Withdraw(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");

        }

    }
}