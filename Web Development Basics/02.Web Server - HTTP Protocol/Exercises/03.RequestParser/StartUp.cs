namespace _03.RequestParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        private static Dictionary<int, string> StatusTextByResponseCode = 
            new Dictionary<int, string>()
        {
            {200, "OK" },
            {404, "Not Found" }
        };

        public static void Main()
        {
            var pathsByMethods = new Dictionary<string, HashSet<string>>();

            var input = Console.ReadLine();

            while (true)
            {
                if (input == "END")
                {
                    break;
                }

                var tokens = input.Split('/').Skip(1).ToList();

                var path = tokens[0];
                var method = tokens[1];

                if (!pathsByMethods.ContainsKey(method))
                {
                    pathsByMethods.Add(method, new HashSet<string>());
                }

                pathsByMethods[method].Add(path);

                input = Console.ReadLine();
            }

            var httpRequest = Console.ReadLine().Split(" ");
            var requestMethod = httpRequest[0].ToLower();
            var requestPath = httpRequest[1].Substring(1);
            var http = httpRequest[2];

            var isValid = false;

            if (pathsByMethods.ContainsKey(requestMethod))
            {
                if (pathsByMethods[requestMethod].Contains(requestPath))
                {
                    isValid = true;
                }
            }

            var statusCode = isValid == true ? 200 : 404;
            var statusText = StatusTextByResponseCode[statusCode];

            var result = new StringBuilder();

            result.AppendLine($"{http} {statusCode} {statusText}");
            result.AppendLine($"Content-Length: {statusText.Length}");
            result.AppendLine($"Content-Type: text/plain");
            result.AppendLine();
            result.Append(statusText);

            Console.WriteLine(result.ToString());
        }
    }
}