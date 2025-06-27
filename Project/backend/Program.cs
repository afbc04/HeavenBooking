using System;
using System.Net;
using System.Text;
using System.Threading;
using HeavenBooking;
using Interface;

namespace HeavenBooking {
    class Program {

        static IRouter? router;

        static void Main(string[] args) {

            HttpListener? serverSocket = InitProgram(25000);

            if (serverSocket == null)
                return;

            while (serverSocket.IsListening) {
                HttpListenerContext context = serverSocket.GetContext();
                ThreadPool.QueueUserWorkItem(HandleContext, context);
            }
            
        }

        /// <summary>
        /// Tries to init the facade and router
        /// </summary>
        /// <param name="port"></param>
        /// <returns>HttpListener is successed</returns>
        private static HttpListener? InitProgram(int port) {

            try {

                Program.router = new Router();
                HttpListener serverSocket = new HttpListener();
                serverSocket.Prefixes.Add($"http://localhost:{port}/");
                serverSocket.Start();

                LogMessage(LogMessages.SUCCESS, $"Backend listening on port {port}...");
                LogMessage(LogMessages.BLANK, $"\n--- [ Logs ] ---\n");

                return serverSocket;

            }
            catch (Exception ex) {
                LogMessage(LogMessages.ERROR, ex.Message);
            }

            return null;

        }


        // Method which processes and handles a context
        private static void HandleContext(object? context_obj) {

            if (context_obj is null || router == null)
                return;

            HttpListenerContext context = (HttpListenerContext) context_obj;
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            if (request == null || response == null)
                return;

            var sw = System.Diagnostics.Stopwatch.StartNew();
            RouterPacket packet = new(500);

            try {
                packet = Program.router.ReadPacket(request);
            }
            catch (Exception error) {
                LogMessage(LogMessages.ERROR, error.Message);
            }
            
            Program.router.WritePacket(response, packet);

            sw.Stop();
            LogRequest(request, packet.StatusCode, sw.ElapsedMilliseconds, packet.JSON == null ? 0 : packet.JSON.Length);  
          
        }

        //Method which logs a HTTP Request
        private static void LogRequest(HttpListenerRequest request, int status_code, long duration_ms, long packet_length)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            ConsoleColor statusColor = ConsoleColor.Gray;

            if (status_code >= 500) statusColor = ConsoleColor.DarkRed;
            else if (status_code >= 400) statusColor = ConsoleColor.DarkYellow;
            else if (status_code >= 300) statusColor = ConsoleColor.DarkCyan;
            else if (status_code >= 200) statusColor = ConsoleColor.DarkGreen;

            ConsoleColor methodColor = request.HttpMethod switch
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
            Console.Write($"{status_code,3} ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{request.Url?.PathAndQuery} ");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"{packet_length}b {duration_ms}ms");

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