using System;
using Android.App;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Text;
using System.Security.Cryptography;

namespace httpsTest
{

    [Activity(Label = "httpsTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        TextView view;
        EditText eT1;
        EditText eT2;

        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA1.Create();  // SHA1.Create()
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public void LogIn()
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                webClient.Encoding = Encoding.UTF8;

                var url = new Uri("https://app-donation.com/php/LogIn_1_0.php"); // Html home page

                var reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("User_Email", eT1.Text);
                string pass = GetHashString(eT2.Text).ToLower();
                reqparm.Add("User_Password", pass);
                byte[] responsebytes = webClient.UploadValues(url, "POST", reqparm);
                string r = Encoding.UTF8.GetString(responsebytes);

                view.Text = r;
            }
        }

        public void LogOut()
        {
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;

                var url = new Uri("https://app-donation.com/php/LogOut_1_0.php"); // Html home page

                string r = webClient.DownloadString(url);
                view.Text = r;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            Button login = FindViewById<Button>(Resource.Id.button1);

            login.Click += delegate
            {
                LogIn();
            };

            Button logOut = FindViewById<Button>(Resource.Id.button2);

            logOut.Click += delegate
            {
                LogOut();
            };

            view = FindViewById<TextView>(Resource.Id.textView1);
            view.Text = "hola";

            eT1 = FindViewById<EditText>(Resource.Id.editText1);
            eT2 = FindViewById<EditText>(Resource.Id.editText2);
        }


    }

    /* public class MainActivity : Activity
    {
        static TextView view;
        static HttpClient client = new HttpClient(new AndroidClientHandler());
        

        static async Task RunAsync()
        {
            string url = "http://en.wikipedia.org/";
            // New code:
            client.BaseAddress = new Uri(url);
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
            client.Timeout = new TimeSpan(0,0,1);
            
            using (var r = await client.GetAsync(new Uri(url)))
            {
                 string result = await r.Content.ReadAsStringAsync();
                 return result;
            }
            
        }

        public void LogIn()
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
                        }
}

protected override void OnCreate(Bundle bundle)
{
    base.OnCreate(bundle);
    SetContentView(Resource.Layout.Main);

    Button callButton = FindViewById<Button>(Resource.Id.button1);

    callButton.Click += delegate
    {
        LogIn();
    };

    view = FindViewById<TextView>(Resource.Id.textView1);

}


    }
    */
}

