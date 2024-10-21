class Example1
{
    public class BankAccount
    {
        public object padlock = new object();

        public int Balance { get => _balance; private set => _balance = value; }

        private int _balance;

        public void Deposit(int amount)
        {
            var lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
                _balance += amount;
            }
            finally
            {
                if (lockTaken) sl.Exit();
            }
        }

        public void Withdraw(int amount)
        {
            var lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
                _balance -= amount;
            }
            finally
            {
                if (lockTaken) sl.Exit();
            }
        }

        SpinLock sl = new SpinLock();

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
    static SpinLock sl = new SpinLock();
    public static void LockRecursion(int x)
    {
        bool lockTaken = false; // zmienna informująca czy Spinlock jest zajęty

        try
        {
            sl.Enter(ref lockTaken); // próba zajęcia Spinlocka
        }
        catch (LockRecursionException e)
        {
            Console.WriteLine("Error: " + e);
        }
        finally
        {
            if(lockTaken)
            {
                Console.WriteLine($"zdobyto x={x}");

                if (x > 0)
                {
                    LockRecursion(x - 1);
                }
                sl.Exit();
            }
            else
            {
                Console.WriteLine($"Cant access x={x}");
            }
        }

    }

    static void Main(string[] args)
    {
        LockRecursion(5);
    }
}