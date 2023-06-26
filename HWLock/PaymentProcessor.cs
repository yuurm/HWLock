namespace HWLock;

public class PaymentProcessor
    {
        private List<decimal> History { get; set; }
        private object historyLock;
        private decimal MaxBalance { get; }
        private decimal MinBalance { get; }

        public PaymentProcessor(decimal maxBalance, decimal minBalance)
        {
            History = new List<decimal>();
            historyLock = new object();
            MaxBalance = maxBalance;
            MinBalance = minBalance;
        }

        public void AddPayment(decimal amount)
        {
            lock (historyLock)
            {
                if (amount > MaxBalance)
                {
                    throw new MaxBalanceExceededException($"Превышен максимальный баланс: {MaxBalance}");
                }

                if (amount < MinBalance)
                {
                    throw new MinBalanceExceededException($"Превышен минимальный баланс: {MinBalance}");
                }

                History.Add(amount);
                Console.WriteLine($"Платеж на сумму {amount} добавлен.");
            }
        }

        public void PrintTransactionHistory()
        {
            lock (historyLock)
            {
                Console.WriteLine("История платежей:");
                foreach (decimal payment in History)
                {
                    Console.WriteLine(payment);
                }
            }
        }

        public void CancelTransactions()
        {
            lock (historyLock)
            {
                History.Clear();
                Console.WriteLine("Транзакции успешно отменены.");
            }
        }
    }

    public class MaxBalanceExceededException : Exception
    {
        public MaxBalanceExceededException(string message) : base(message)
        {
        }
    }

    public class MinBalanceExceededException : Exception
    {
        public MinBalanceExceededException(string message) : base(message)
        {
        }
    }