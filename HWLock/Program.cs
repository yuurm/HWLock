// See https://aka.ms/new-console-template for more information

using HWLock;

PaymentProcessor processor = new PaymentProcessor();


Thread addPaymentThread1 = new Thread(() => processor.AddPayment(100));
Thread addPaymentThread2 = new Thread(() => processor.AddPayment(200));


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

