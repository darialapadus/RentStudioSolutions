namespace RentStudio.Helpers
{
    public class BankSimulator
    {
        private static readonly string[] BankResponses = { "Insufficient funds", "Succeeded", "Failed" };

        public static async Task<string> BankProcessPaymentAsync()
        {
            // Simulate a 3-second processing time
            await Task.Delay(3000);

            // Generate a random response
            Random random = new Random();
            int responseIndex = random.Next(BankResponses.Length);
            string response = BankResponses[responseIndex];

            return response;
        }

        /*public static async Task Main(string[] args)
        {
            Console.WriteLine("Processing payment...");

            string result = await BankProcessPaymentAsync();

            Console.WriteLine($"Bank response: {result}");
        }*/
    }
}
