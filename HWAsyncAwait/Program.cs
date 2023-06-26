

using System.Net;

class Program
    {
        static async Task Main(string[] args)
        {
            // Список адресов сайтов для опроса
            List<string> urls = new List<string>
            {
                "http://yandex.ru",
                "http://google.ru",
                "http://vk.com",
                "http://msdn.com"
            };

            
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

           
            List<Task<string>> tasks = new List<Task<string>>();

            
            foreach (string url in urls)
            {
                tasks.Add(GetResponseAsync(url, cancellationToken));
            }

            
            Task<string> completedTask = await Task.WhenAny(tasks);

            
            cancellationTokenSource.Cancel();

            
            string fastestResponse = await completedTask;

            
            Console.WriteLine("Самый быстрый ответ:");
            Console.WriteLine(fastestResponse);

            Console.ReadLine();
        }

        static async Task<string> GetResponseAsync(string url, CancellationToken cancellationToken)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    
                    using (cancellationToken.Register(() => client.CancelAsync()))
                    {
                        
                        return await client.DownloadStringTaskAsync(url);
                    }
                }
                catch (WebException ex)
                {
                    
                    if (ex.Status == WebExceptionStatus.RequestCanceled)
                    {
                        Console.WriteLine($"Запрос к {url} был отменен.");
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка при выполнении запроса к {url}: {ex.Message}");
                    }

                    return string.Empty;
                }
            }
        }
    }