﻿using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DotNetRESTClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static void Main(string[] args) => ProcessRepositories().Wait();

        private static async Task ProcessRepositories()
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json")
                );
                client.DefaultRequestHeaders.Add(
                    "User-Agent", ".NET Foundation Repository Reporter"
                );

                var taskString = client.GetStringAsync(
                    "https://api.github.com/orgs/dotnet/repos"
                );

                var message = await taskString;
                Console.WriteLine(message);
            }
    }
}
