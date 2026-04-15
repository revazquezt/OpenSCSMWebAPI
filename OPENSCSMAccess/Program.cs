using Microsoft.Owin.Hosting;

namespace OPENSCSMAccessM
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseUrl = "http://localhost:5001"; 

            using (WebApp.Start<Startup>(url: baseUrl))
            {
                Console.WriteLine($"✅ PowerShell Worker running on {baseUrl}");
                Console.WriteLine("Press ENTER for stop...");
                Console.ReadLine();
            }
        }
    }
}