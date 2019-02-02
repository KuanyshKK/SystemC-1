using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
         

        }

        public static void Exmpl01()
        {
            WebClient client = new WebClient();

            Stream data = client.OpenRead("http://google.com");
            StreamReader reader = new StreamReader(data);

            string s = reader.ReadToEnd();
            Console.WriteLine(s);

            data.Close();
            reader.Close();

        }

        public static void Exmpl02()
        {
            
            Console.WriteLine("Enter URI: ");
            string uriString = Console.ReadLine();

            Console.WriteLine("Enter data: ");
            string postData = Console.ReadLine();

            byte[] postArray =
                Encoding.UTF8.GetBytes(postData);

            WebClient client = new WebClient();
            Stream postStream = client.OpenWrite(uriString);

            postStream.Write(postArray, 0, postArray.Length);
            postStream.Close();
            Console.WriteLine("OK");

        }

        public static void Exmpl03()
        {
            WebClient client = new WebClient();
            NameValueCollection valueCollection =
                new NameValueCollection();

            string name = "";
            string age = "";
            string address = "";

            valueCollection.Add("Name", name);
            valueCollection.Add("Age", age);
            valueCollection.Add("Address", address);

            byte[] responseArray =
                client.UploadValues("URL", valueCollection);

            Console.WriteLine("Responce receive was: {0}", 
                Encoding.UTF8.GetString(responseArray));

        }

        public static void Exmpl04()
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("URL");

            StreamReader reader = new StreamReader(stream);
            string str = reader.ReadToEnd();

            byte[] data = client.DownloadData("remote URL");
            string down = Encoding.UTF8.GetString(data);
        }

        public static void Exmpl05()
        {
            WebRequest request = WebRequest.Create("url");
            request.Credentials = CredentialCache.DefaultCredentials;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Console.WriteLine(response.StatusDescription);

            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);

            reader.Close();
            dataStream.Close();
            response.Close();
        
        }

        public static async void Exmpl06Async()
        {
            WebRequest request = WebRequest.Create("url");
            request.Method = "POST";

            string data = "sName=HelloWorld";

            byte[] dataArray = Encoding.UTF8.GetBytes(data);

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = dataArray.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(dataArray, 0, dataArray.Length);
            }

            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
            
        }
    }
}
