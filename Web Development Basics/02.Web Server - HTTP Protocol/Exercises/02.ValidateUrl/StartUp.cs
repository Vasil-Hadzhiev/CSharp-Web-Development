namespace _02.ValidateUrl
{
    using System;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    public class StartUp
    {
        private const string InvalidUrlMsg = "Invalid URL";
        private const string HttpDefaultPortValue = "80";
        private const string HttpsDefaultPortValue = "443";

        public static void Main()
        {
            var urlPattern = @"^(https?):\/\/([0-9a-zA-Z\-\.]+)(?::(\d+))?(\/[0-9a-zA-Z-.]*)?(?:\?([0-9a-zA-Z-.=+&_]+))?(?:#([0-9a-zA-Z\-\.]+))?$";
            var urlMatcher = new Regex(urlPattern);
            var result = new StringBuilder();

            var inputUrl = Console.ReadLine();
            var decodedUrl = WebUtility.UrlDecode(inputUrl);
            var matchedUrl = urlMatcher.Match(decodedUrl);

            if (matchedUrl.Success)
            {
                var isValid = true;
                var protocol = matchedUrl.Groups[1].Value;
                var host = matchedUrl.Groups[2].Value;
                var port = matchedUrl.Groups[3].Value;
                var path = matchedUrl.Groups[4].Value;
                var query = matchedUrl.Groups[5].Value;
                var fragment = matchedUrl.Groups[6].Value;
             
                if (port != string.Empty)
                {
                    switch (protocol)
                    {
                        case "http":
                            if (port != HttpDefaultPortValue)
                            {
                                isValid = false;
                            }
                            break;
                        case "https":
                            if (port != HttpsDefaultPortValue)
                            {
                                isValid = false;
                            }
                            break;
                    }
                }
                else
                {
                    port = protocol == "http" ? HttpDefaultPortValue : HttpsDefaultPortValue;
                }

                if (!isValid)
                {
                    Console.WriteLine(InvalidUrlMsg);
                    return;
                }

                result.AppendLine($"Protocol: {protocol}");
                result.AppendLine($"Host: {host}");
                result.AppendLine($"Port: {port}");
                result.AppendLine($"Path: {path}");

                if (query != string.Empty)
                {
                    result.AppendLine($"Query: {query}");
                }

                if (fragment != string.Empty)
                {
                    result.AppendLine($"Fragment: {fragment}");
                }
            }
            else
            {
                Console.WriteLine(InvalidUrlMsg);
                return;
            }

            Console.WriteLine(result.ToString().Trim());
        }
    }
}