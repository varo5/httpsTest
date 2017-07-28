using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Xamarin.Android.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Java.Net;
using System.IO;


namespace httpsTest
{

    [Activity(Label = "httpsTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static TextView view;
        static HttpClient client = new HttpClient(new AndroidClientHandler());
        

        static async Task RunAsync()
        {
            string url = "https://app-donation.com";
            // New code:
            client.DefaultRequestHeaders.Accept.Clear();

            try
            {
                // Create a new product
                string r = null;
                

                // Get the product
                r = await GetAsync(url);
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            
        }

        static async Task<string> GetAsync(string url)
        {
            string r = null;
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                r = await response.Content.ReadAsStringAsync();

                
                view.SetText(r.ToCharArray(),0,r.Length);
            }

            return r;
        }

        public void buttonClick()
        {
            RunAsync().Wait();
            /*

                        var request = HttpWebRequest.Create("https://app-donation.com");

                        using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                        {
                            if (response.StatusCode != HttpStatusCode.OK)
                                Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                            {
                                var content = reader.ReadToEnd();
                                if (string.IsNullOrWhiteSpace(content))
                                {
                                    Console.Out.WriteLine("Response contained empty body...");
                                }
                                else
                                {
                                    Console.Out.WriteLine("Response Body: \r\n {0}", content);
                                }
                            }
                        }*/
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            Button callButton = FindViewById<Button>(Resource.Id.button1);

            callButton.Click += delegate
            {
                buttonClick();
            };

            view = FindViewById<TextView>(Resource.Id.textView1);
           
        }


    }
}

