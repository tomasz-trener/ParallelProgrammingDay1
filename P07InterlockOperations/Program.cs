using System.Reflection.Metadata;

class Example2
{
    public class BankAccount
    {
        public object padlock = new object();

        public int Balance { get => _balance; private set => _balance = value; }

        private int _balance;

        public void Deposit(int amount)
        {
            Interlocked.Add(ref _balance, amount);
        }

        public void Withdraw(int amount)
        {
            Interlocked.Add(ref _balance, -amount);
        }

        public static void Main2(string[] args)
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

class Example3
{
    public class BankAccount
    {
        private bool transactionCompleted = false;
        public int Balance { get => _balance; private set => _balance = value; }

        private int _balance;

        public void Deposit(int amount)
        {
            transactionCompleted = false;
            _balance += amount;

            Thread.MemoryBarrier();

            transactionCompleted = true;
        }

        public void Withdraw(int amount)
        {
            while (!transactionCompleted) 
            {
                
            }
          

            _balance -= amount;
          
        }

        public static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();


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


            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");

        }

    }
}