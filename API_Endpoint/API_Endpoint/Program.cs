using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Runtime.InteropServices.JavaScript;

class Program
{
    static async Task Main(string[]args)
    {
        //Asking user for 3 names
        Console.WriteLine("Enter the first name");
        string name1 = Console.ReadLine();
        Console.WriteLine("Enter the second name");
        string name2 = Console.ReadLine();
        Console.WriteLine("Enter the third name");
        string name3 = Console.ReadLine();

        //Get ages

        int age1 = await GetAgeFromAgify(name1);
        int age2 = await GetAgeFromAgify(name2);
        int age3 = await GetAgeFromAgify(name3);

        //Display names and ages
        Console.WriteLine($"Name 1: {name1} is apx {age1} years old");
        Console.WriteLine($"Name 2: {name2} is apx {age2} years old");
        Console.WriteLine($"Name 3: {name3} is apx {age3} years old");

        //which is oldest
        string oldestName = name1;
        int oldestAge = age1;

        if (age2 > oldestAge)
        {
            oldestName = name2;
            oldestAge = age2;
        }
        if (age3 > oldestAge)
        {
            oldestName = name3;
            oldestAge = age3;
        }

        Console.WriteLine($"{oldestName} is the oldest");
    }

    static async Task<int> GetAgeFromAgify(string name)
    {
        using (HttpClient client = new HttpClient())
        {
            string url = $"https://api.agify.io?name={name}";
            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            JSObject json = JSObject.Parse(responseBody);
            return (int)json["age"];
        }
    }
}
