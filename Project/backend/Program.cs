using System.Net;
using System.Text;
using System.Threading;

class Program {
    
    private static readonly int threadPoolNumber = 10;

    static void Main(string[] args)
    {
        HttpListener serverSocket = new HttpListener();
        serverSocket.Prefixes.Add("http://localhost:25000/");
        serverSocket.Start();
        Console.WriteLine("Backend listenning in 25000...");

//        ThreadPool.QueueUserWorkItem(HandleRequest);


                        while (true)
                        {
                            var context = serverSocket.GetContext();
                            var request = context.Request;
                            var response = context.Response;
                
                            // Tratamento CORS básico
                            response.AddHeader("Access-Control-Allow-Origin", "*");
                            response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                            response.AddHeader("Access-Control-Allow-Headers", "Content-Type");
                
                            if (request.HttpMethod == "OPTIONS")
                            {
                                // Responde requisição OPTIONS com status 200 e fecha
                                response.StatusCode = 200;
                                response.Close();
                                continue;
                            }

                            //Task.Run(() => HandleRequest(request));

                            string responseString = "[{\"title\" : \"Ola Biazinha <3\"}]";
                            byte[] buffer = Encoding.UTF8.GetBytes(responseString);

                            response.ContentLength64 = buffer.Length;
                            response.ContentType = "text/json";

                            // Escreve no socket de saída HTTP
                            using (var output = response.OutputStream)
                            {
                                output.Write(buffer, 0, buffer.Length);
                            }
                        }
    }

    static void HandleRequest(Object request) {

    }
}
