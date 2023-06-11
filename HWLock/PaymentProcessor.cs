namespace HWLock;

public class PaymentProcessor
{
    private List<decimal> History { get; set; }
    private object historyLock;
    
    public PaymentProcessor()
    {
        History = new List<decimal>();
        historyLock = new object();
    }

    public void AddPayment(decimal amount)
    {
        lock (historyLock) 
        {
            
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
    
}