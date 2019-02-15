using Newtonsoft.Json;
using QnaMaker;
using QnaMaker.Model;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace QnAMaker
{
    /// <summary>
    /// Enviar uma pergunta ao BOT
    /// </summary>
     static class Program
    {
        // NOTE: Replace this with a valid host name.
        static string host = "https://qna-oraculo.azurewebsites.net/";

        // NOTE: Replace this with a valid endpoint key.
        // This is not your subscription key.
        // To get your endpoint keys, call the GET /endpointkeys method.
        static string endpoint_key = "f3661bc0-b413-4aa6-81bb-226e617b5aea";

        // NOTE: Replace this with a valid knowledge base ID.
        // Make sure you have published the knowledge base with the
        // POST /knowledgebases/{knowledge base ID} method.
        static string kb = "71670d15-7cde-456f-a501-ee68481d2d3e";

        static string service = "/qnamaker";
        static string method = "/knowledgebases/" + kb + "/generateAnswer/";

        static string question = @"
{
    'question': 'Oi',
}
";

        async static Task<string> Post(string uri, string body)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                request.Headers.Add("Authorization", "EndpointKey " + endpoint_key);

                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }

        async static void GetAnswers()
        {
            var uri = host + service + method;
            Console.WriteLine("Calling " + uri + ".");
            var response = await Post(uri, question);

            var obj = JsonConvert.DeserializeObject<RootObject>(response);

            Console.WriteLine(obj.answers[0].answer);
            Console.WriteLine("Press any key to continue.");
        }

        static void Main(string[] args)
        {
            GetAnswers();
            Console.ReadLine();
        }
    }
}