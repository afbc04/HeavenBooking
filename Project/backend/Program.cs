using System;
using System.Net;
using System.Text;
using System.Threading;
using HeavenBooking;
using Interface;

namespace HeavenBooking
{
    class Program
    {

        static IRouter? router;

        static void Main(string[] args)
        {
            HttpListener serverSocket = new HttpListener();
            serverSocket.Prefixes.Add("http://localhost:25000/");
            serverSocket.Start();

            //---- [ Initializing Backend ] ----
            try
            {
                Program.router = new Router();
            }
            catch (FacadeDataBaseException)
            {
                LogMessage(LogMessages.ERROR, "Database is not connected...");
                return;
            }

            LogMessage(LogMessages.SUCCESS, "Backend listening on port 25000...");
            LogMessage(LogMessages.BLANK, $"\n--- [ Logs ] ---\n");

            while (true)
            {
                HttpListenerContext context = serverSocket.GetContext();
                ThreadPool.QueueUserWorkItem(HandleContext, context);

            }
        }


        // Method which processes and handles a context
        static void HandleContext(object? context_obj)
        {

            if (context_obj is null || router == null)
                return;

            HttpListenerContext context = (HttpListenerContext) context_obj;
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            if (request == null)
                return;

            var sw = System.Diagnostics.Stopwatch.StartNew();
            int statusCode = 500;

            try
            {

                //CORS
                response.AddHeader("Access-Control-Allow-Origin", "*");
                response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                response.AddHeader("Access-Control-Allow-Headers", "Content-Type");

                if (request.HttpMethod == "OPTIONS")
                {
                    statusCode = 200;
                    response.StatusCode = statusCode;
                    response.Close();
                    LogRequest(request, statusCode, 0, 0);
                    return;
                }

                if (request.Url == null)
                {
                    statusCode = 400;
                    response.StatusCode = statusCode;
                    response.Close();
                    LogRequest(request, statusCode, 0, 0);
                    return;
                }

                string[] segments_of_path = request.Url.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
                if (segments_of_path.Length == 0)
                    segments_of_path = [""];

                string responseJSON = "";

                try
                {

                    responseJSON = request.HttpMethod switch
                    {
                        "GET" => router.GetRequests(request, response, request.Url.AbsolutePath, segments_of_path),
                        "POST" => router.PostRequests(request, response, request.Url.AbsolutePath, segments_of_path),
                        "PUT" => router.PutRequests(request, response, request.Url.AbsolutePath, segments_of_path),
                        "DELETE" => router.DeleteRequests(request, response, request.Url.AbsolutePath, segments_of_path),
                        _ => throw new RouterException(501),
                    };

                    statusCode = response.StatusCode;

                }
                catch (RouterException error)
                {
                    statusCode = error.error_code;
                    router.ErrorRequest(response, statusCode);
                }


                byte[] buffer = Encoding.UTF8.GetBytes(responseJSON);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "application/json";

                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Dispose();
                

            }
            catch (Exception ex)
            {
                LogMessage(LogMessages.ERROR, $"Error processing Request: {ex.Message}");
            }
            finally
            {

                sw.Stop();
                LogRequest(request, statusCode, sw.ElapsedMilliseconds, 0);
                response?.Close();
            }
        }

        //Method which logs a HTTP Request
        static void LogRequest(HttpListenerRequest request, int statusCode, long durationMs, long responseLength)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            ConsoleColor statusColor = ConsoleColor.Gray;
            ConsoleColor methodColor = ConsoleColor.Gray;

            if (statusCode >= 500)
                statusColor = ConsoleColor.DarkRed;
            else if (statusCode >= 400)
                statusColor = ConsoleColor.DarkYellow;
            else if (statusCode >= 300)
                statusColor = ConsoleColor.DarkCyan;
            else if (statusCode >= 200)
                statusColor = ConsoleColor.DarkGreen;

            methodColor = request.HttpMethod switch
            {
                "GET" => ConsoleColor.Green,
                "POST" => ConsoleColor.Yellow,
                "PUT" => ConsoleColor.Blue,
                "OPTIONS" => ConsoleColor.Magenta,
                "DELETE" => ConsoleColor.Red,
                _ => ConsoleColor.Gray
            };

            Console.ForegroundColor = methodColor;
            Console.Write($"{request.HttpMethod,-7} ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($":: ");

            Console.ForegroundColor = statusColor;
            Console.Write($"{statusCode,3} ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{request.Url?.AbsolutePath} ");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"{responseLength}b {durationMs}ms");

            Console.Write("\n");
            Console.ForegroundColor = originalColor;
        }
        
        enum LogMessages {ERROR,WARNING,SUCCESS,BLANK};

        //Method which print a custom message
        static void LogMessage(LogMessages type, string Message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            switch (type)
            {

                case LogMessages.ERROR:

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"[ERROR] ");

                    break;

                case LogMessages.WARNING:

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"[WARNING] ");

                    break;

                case LogMessages.SUCCESS:

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"[DONE] ");

                    break;

                default:

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"");

                    break;

            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{Message}\n");
            Console.ForegroundColor = originalColor;
        }

    }

}