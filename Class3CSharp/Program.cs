﻿using System;
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
            Console.WriteLine("\nSWAPI PLANETS");
            string planetsURL = "planets/?page=";
            var i = 1;
            while (true) {
                
                JObject planetResult = CallRestMethod(BASE_URL + planetsURL + i);
                    JArray planets = (JArray)planetResult["results"];
                if(planets == null)
                {
                    break;

                }else{ 
                    foreach (JObject planet in planets)
                    {
                        Console.WriteLine("Planet Name: " + planet["name"]);
                        Console.WriteLine("Films: ");
                        JArray films = (JArray)planet["films"];
                        if (films.Count != 0)
                        {
                            foreach (JValue film in films)
                            {
                                Console.WriteLine(CallRestMethod(film.ToString())["title"]);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Not in any films");
                        }

                        Console.WriteLine();
                    }
                }
                i++;
               

            }

            Console.ReadLine();
        }
        static JObject CallRestMethod(string uri)
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
                string result = $"{{'Error':'An error has occured. Could not get to the URL', 'Message': '{e.Message}'}}";
                return JObject.Parse(result);
            }
        }

}

}
