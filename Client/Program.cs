﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            while (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                Thread.Sleep(2000);
                return;
            }
            await CallApi("", "https://localhost:6001/identity", "", "");
            await CallApi("", "https://localhost:6001/api1", "", "");
            await CallApi("", "https://localhost:6001/api2", "", "");
            await CallApi("", "https://localhost:6001/groot", "", "");
            await CallApi("", "https://localhost:6001/weatherforecast", "", "");

            Console.WriteLine("=====================");

            var clientId = "MyApiApplication";
            var secret = "secret";
            var scope = "api1scope";
            var bearerToken = await GetBearerToken(client, disco.TokenEndpoint, clientId, secret, scope);
            await CallApi(bearerToken, "https://localhost:6001/identity", clientId, scope);
            await CallApi(bearerToken, "https://localhost:6001/api1", clientId, scope);
            await CallApi(bearerToken, "https://localhost:6001/api2", clientId, scope);
            await CallApi(bearerToken, "https://localhost:6001/groot", clientId, scope);
            await CallApi(bearerToken, "https://localhost:6001/weatherforecast", clientId, scope);

            Console.WriteLine("=====================");

            scope = "api2scope";
            bearerToken = await GetBearerToken(client, disco.TokenEndpoint, clientId, secret, scope);
            await CallApi(bearerToken, "https://localhost:6001/identity", clientId, scope);
            await CallApi(bearerToken, "https://localhost:6001/api1", clientId, scope);
            await CallApi(bearerToken, "https://localhost:6001/api2", clientId, scope);
            await CallApi(bearerToken, "https://localhost:6001/groot", clientId, scope);
            await CallApi(bearerToken, "https://localhost:6001/weatherforecast", clientId, scope);

            Console.WriteLine("=====================");

            clientId = "MyGrootApp";
            secret = "supersecret";
            scope = "grootscope";
            bearerToken = await GetBearerToken(client, disco.TokenEndpoint, clientId, secret, scope);
            await CallApi(bearerToken, "https://localhost:6001/identity", clientId, scope);
            await CallApi(bearerToken, "https://localhost:6001/api1", clientId, scope);
            await CallApi(bearerToken, "https://localhost:6001/api2", clientId, scope);
            await CallApi(bearerToken, "https://localhost:6001/groot", clientId, scope);
            await CallApi(bearerToken, "https://localhost:6001/weatherforecast", clientId, scope);

            Console.ReadKey(); 
            return;
        }

        private static async Task<string> GetBearerToken(HttpClient client, string tokenEndpoint, string clientId, string secret, string scope)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = tokenEndpoint,
                ClientId = clientId,
                ClientSecret = secret,
                Scope = scope
            });

            if (tokenResponse.IsError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(tokenResponse.Error);
                return "";
            }

            //Console.ForegroundColor = ConsoleColor.Gray;
            //Console.WriteLine(tokenResponse.Json);
            //Console.WriteLine("\n\n");

            return tokenResponse.AccessToken;
        }

        private static async Task CallApi(string bearerToken, string url, string clientId, string scope)
        {
            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(bearerToken);

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Calling: {url} as {clientId} with {scope}: ");

            var response = await apiClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(response.StatusCode);
            }
        }
    }
}
