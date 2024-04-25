using System.Net;

namespace TestCrmApi
{
    internal class Program
    {

        static void Main(string[] args)
        {
            SimpleListenerExample(new string[]{ "http://localhost:8080/lol/" });
        }

        public static void SimpleListenerExample(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();

            while (true) {

                Console.WriteLine("Listening...");
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();

                HttpListenerRequest request = context.Request;

                using var ms = new MemoryStream();
                request.InputStream.CopyTo(ms);
                ms.Position = 0;
                using var sr = new StreamReader(ms);
                var s = sr.ReadToEnd();
                Console.WriteLine(s);
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.
                string responseString = "<HTML><BODY> <div style=\"background: red; width: 100px; height: 100px;\"></div> Hello world!</BODY></HTML>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
            
            listener.Stop();
        }
    }


    public class Rootobject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public string status { get; set; }
        public DateTime bookedAt { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string checkin { get; set; }
        public string checkout { get; set; }
        public Money money { get; set; }
        public string id { get; set; }
        public string lead_id { get; set; }
        public int docNum { get; set; }
        public Billdata[] billData { get; set; }
    }

    public class Money
    {
        public string total { get; set; }
        public string paid { get; set; }
    }

    public class Billdata
    {
        public int billNum { get; set; }
        public int billSum { get; set; }
        public int billPaidSum { get; set; }
    }

}
