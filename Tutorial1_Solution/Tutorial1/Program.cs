using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tutorial1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
             var websiteUrl = args.Length > 0 ? args[0] : throw new ArgumentNullException();
            ///string websiteUrl = null;
            //come new comment
            // var x = websiteUrl ?? throw new ArgumentException("URL not null!");
            
            var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync(websiteUrl); //working as a thread
                httpClient.Dispose();
          
                if (response.IsSuccessStatusCode)
                {
                    var htmlContent = await response.Content.ReadAsStringAsync();
                    //ctrl + shift + space 
                    var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+\\.[a-z]+", RegexOptions.IgnoreCase);

                    var emailAddresses = regex.Matches(htmlContent);

                    //tab to do the tample for method
                    if(emailAddresses.Count > 0)
                    {
                        foreach (var emailAddress in emailAddresses)
                        {
                            Console.WriteLine(emailAddress.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("No email addreeses!");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR when downloading the page");
            }

            Console.ReadKey();
        }
    }
}
