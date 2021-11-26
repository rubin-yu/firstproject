  private static async Task TestAPI()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (a, b, c, d) => true;
            HttpClient client = new HttpClient(handler);
            //client.Timeout = TimeSpan.FromSeconds(3);
            string result = null;
            while (true)
            {
                var sw = Stopwatch.StartNew();
                var id = Guid.NewGuid();
                try
                {
                    var api = "https://oers.suss.edu.sg/admin/api/default?"+ id;
                    var response = await client.GetAsync(api);
                    result = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"{DateTime.Now}- failed load data, errors:{result} resp:{response}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{DateTime.Now}- {e.Message}");
                }
                sw.Stop();
                if (sw.Elapsed.TotalSeconds > 1)
                {
                    Console.WriteLine($"{DateTime.Now} - {id} - {sw.Elapsed.TotalSeconds}s, result:{result}");
                }
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
