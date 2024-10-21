﻿using System.Numerics;

class Example1
{
    public class BankAccount
    {
        public object padlock = new object();

        public int Balance { get => _balance; private set => _balance = value; }

        private int _balance;

        public void Deposit(int amount)
        {
            var lockTaken = mutex.WaitOne();
            try
            {
                _balance += amount;
            }
            finally
            {
                if (lockTaken) mutex.ReleaseMutex();
            }
        }

        public void Withdraw(int amount)
        {
            var lockTaken = mutex.WaitOne();
            try
            {
                _balance -= amount;
            }
            finally
            {
                if (lockTaken) mutex.ReleaseMutex();
            }
        }

        Mutex mutex = new Mutex();

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

        public int Balance { get => _balance; private set => _balance = value; }

        private int _balance;

        public void Deposit(int amount)
        {

            _balance += amount;

        }
        public void Withdraw(int amount)
        {

            _balance -= amount;
        }

        public void Transfer(BankAccount where, int amount)
        {
            Balance -= amount;
            where.Balance += amount;
        }

        class Program
        {
            public static void Main(string[] args)
            {
                var tasks = new List<Task>();
                var ba = new BankAccount();
                var ba2 = new BankAccount();

                Mutex mutex = new Mutex();
                Mutex mutex2 = new Mutex();

                for (int i = 0; i < 10; i++)
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        for (int j = 0; j < 1000; j++)
                        {
                            bool haveLock = mutex.WaitOne();
                            try
                            {
                                ba.Deposit(1);
                            }
                            finally
                            {
                                if (haveLock) mutex.ReleaseMutex();
                            }
                        }
                    }));

                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        for (int j = 0; j < 1000; j++)
                        {
                            bool haveLock = mutex2.WaitOne();
                            try
                            {
                                ba2.Deposit(1);
                            }
                            finally
                            {
                                if (haveLock) mutex2.ReleaseMutex();
                            }
                        }
                    }));

                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        for (int j = 0; j < 1000; j++)
                        {
                            bool haveLock = WaitHandle.WaitAll(new[] { mutex, mutex2 });
                            try
                            {
                                ba.Transfer(ba2, 1);
                            }
                            finally
                            {
                                if (haveLock)
                                {
                                    mutex.ReleaseMutex();
                                    mutex2.ReleaseMutex();
                                }
                            }
                        }
                    }));


                }

                Task.WaitAll(tasks.ToArray());
                Console.WriteLine($"Final balance is {ba.Balance}");
                Console.WriteLine($"Final balance in ba2 is {ba2.Balance}");
            }

          
        }
    }
}