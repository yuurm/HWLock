using HWTasks;

        ProductCard[] cards = new ProductCard[]
        {
            new ProductCard { ProductId = 1, Price = 10.0m },
            new ProductCard { ProductId = 2, Price = 20.0m },
            new ProductCard { ProductId = 3, Price = 30.0m },
        };

       
        decimal totalSum = await CalculateTotalSumParallel(cards);

        Console.WriteLine($"Общая сумма продуктов: {totalSum}");
    

    static async Task<decimal> CalculateTotalSumParallel(ProductCard[] cards)
    {
        decimal totalSum = 0;
        Task<decimal>[] tasks = new Task<decimal>[cards.Length];

        for (int i = 0; i < cards.Length; i++)
        {
            int index = i;
            tasks[i] = Task.Run(() => CalculateCardSum(cards[index]));
        }

        await Task.WhenAll(tasks);

        foreach (var task in tasks)
        {
            totalSum += task.Result;
        }

        return totalSum;
    }

    static decimal CalculateCardSum(ProductCard card)
    {
        
        return card.Price;
    }
