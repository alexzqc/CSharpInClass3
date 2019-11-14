using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;


namespace Class3CSharp
{
    class Program
    {
        const string BASE_URL = "https://swapi.co/api/";
        static void Main(string[] args)
        {
            const string PLANETS = "planets/";
                    for(var i = 1; i < 10; i++) {
                         JObject a = CallRestMethod(new Uri(BASE_URL + PLANETS + i));
                        string planetNames = a.GetValue("name").Value<string>();
                        Console.WriteLine("Planet Name: " + planetNames);
                     }

            Console.ReadLine();
        }
        static JObject CallRestMethod(Uri uri)
        {
            try
            {
                // Create a web request for the given uri
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                // Get the web response from the api
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                // Get a stream to read the reponse
                StreamReader responseStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                // Read the response and write it to the console
                JObject result = JObject.Parse(responseStream.ReadToEnd());
                
                // Close the connection to the api and the stream reader
                response.Close();
                responseStream.Close();
                return result;
            }
            catch (Exception e)
            {
                string result = $"{{'Error':'An error has occured. Could not get {uri.LocalPath}', 'Message': '{e.Message}'}}";
                return JObject.Parse(result);
            }
        }
    }

}
//Console.WriteLine(a);

//   const string PEOPLE = "people/";
//       Console.WriteLine(CallRestMethod(new Uri(BASE_URL + PLANETS)));
//       Console.WriteLine(CallRestMethod(new Uri(BASE_URL + PEOPLE)));


//           string linkOfFilms = a["films"][0].Value<string>();


//JArray films = (JArray)a("films");