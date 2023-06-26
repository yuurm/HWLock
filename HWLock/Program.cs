// See https://aka.ms/new-console-template for more information

using HWLock;

PaymentProcessor processor = new PaymentProcessor(500, 0);

Thread addPaymentThread1 = new Thread(() =>
{
    try
    {
        processor.AddPayment(100);
    }
    catch (MaxBalanceExceededException e)
    {
        Console.WriteLine($"Ошибка: {e.Message}");
    }
    catch (MinBalanceExceededException e)
    {
        Console.WriteLine($"Ошибка: {e.Message}");
    }
});

Thread addPaymentThread2 = new Thread(() =>
{
    try
    {
        processor.AddPayment(600);
    }
    catch (MaxBalanceExceededException e)
    {
        Console.WriteLine($"Ошибка: {e.Message}");
    }
    catch (MinBalanceExceededException e)
    {
        Console.WriteLine($"Ошибка: {e.Message}");
    }
});

Thread printHistoryThread1 = new Thread(processor.PrintTransactionHistory);
Thread printHistoryThread2 = new Thread(processor.PrintTransactionHistory);

addPaymentThread1.Start();
addPaymentThread2.Start();
printHistoryThread1.Start();
printHistoryThread2.Start();

addPaymentThread1.Join();
addPaymentThread2.Join();
printHistoryThread1.Join();
printHistoryThread2.Join();

Console.WriteLine("Введите 'cancel', чтобы отменить все транзакции:");
string input = Console.ReadLine();
if (input == "cancel")
{
    processor.CancelTransactions();
}

Console.ReadLine();

