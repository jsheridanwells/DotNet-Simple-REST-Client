using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace DotNetRESTClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static void Main(string[] args) => ProcessRepositories().Wait();

        private static async Task ProcessRepositories()
            {
                var serializer = new DataContractJsonSerializer(
                    typeof(List<Repo>)
                );

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json")
                );
                client.DefaultRequestHeaders.Add(
                    "User-Agent", ".NET Foundation Repository Reporter"
                );

                var taskStream = client.GetStreamAsync(
                    "https://api.github.com/orgs/dotnet/repos"
                );

                var repositories = serializer.ReadObject(await taskStream) as List<Repo>;

                foreach (var repo in repositories)
                    Console.WriteLine(repo.name);
            }
    }
}
