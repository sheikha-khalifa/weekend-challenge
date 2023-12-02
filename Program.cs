using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
//using System.Threading.Tasks;
namespace HTTPS_REQUEST
{
    internal class Program
    {
        private static readonly HttpClient client = new HttpClient();

        public static string apikey = "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";
        public static string endpointURL = "https://api.openai.com/v1/completions";
        public static string medelType = "text-davinci-003";
        public static int maxTokens = 256;
        public static double temperature = 1.0f;

        static async Task Main(string[] args)
        {
            await Askforanything();
        }
        public static async Task Askforanything()
        {
            Console.WriteLine("Type something and press enter:");
            string prompt = Console.ReadLine();
            Console.WriteLine("\n" + "....thinking....");
            string response = await OpenAIComplete(apikey, endpointURL, medelType,prompt, maxTokens, temperature);
            Console.WriteLine("Response:");
            Console.WriteLine(response);
            Console.WriteLine("-----------------------------------------");
            await Askforanything();
        }
        public static async Task<string> OpenAIComplete(string apikey, string endpoint, string modeltype,string prompt, int maxtokens, double temp)
        {
            var requestbody = new
            {
                model = modeltype,
                prompt = prompt,
                max_tokens = maxtokens,
                temperature = temp
            };
            string jsonPlayload = JsonConvert.SerializeObject(requestbody);

            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Headers.Add("Authorization", $"Bearer {apikey}"); // Fix: Added space after 'Bearer'
            request.Content = new StringContent(jsonPlayload, Encoding.UTF8, "application/json");

            var httpResponse = await client.SendAsync(request);
            string responseContent = await httpResponse.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}