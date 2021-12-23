using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using RestSharp;
using RestSharp.Authenticators;

namespace PlateScanner
{
    class ApiCallObject
    {
        // SDSS API URL   - https://skyserver.sdss.org/dr16/SkyServerWS/SearchTools/SqlSearch?
        // Example Search - https://skyserver.sdss.org/dr16/SkyServerWS/SearchTools/SqlSearch?cmd=select%20top%2010%20ra,dec%20from%20Frame&format=json

        public string Url { get; set; }
        public string UrlParameters { get; set; }
        public string ApiResponse { get; set; }

        //string path = "https://skyserver.sdss.org/dr16/SkyServerWS/SearchTools/SqlSearch?cmd=select%20top%2010%20ra,dec%20from%20Frame&format=json";


        public ApiCallObject(string url, string urlParameters)
        {
            Url = url;
            UrlParameters = urlParameters;
            ApiResponse = String.Empty;
        }

        public void MakeTheApiCall()
        {

            var client = new RestClient();
            var request = new RestRequest(this.Url);

            //request.AddQueryParameter("UrlParameters", this.UrlParameters);

            //Console.WriteLine("Request GetType" + ": " + request.GetType);
            //Console.WriteLine(this.Url + this.UrlParameters);

            var response = client.ExecuteAsync(request, Method.GET).GetAwaiter().GetResult();

            if (!response.IsSuccessful)
            {
                string message = "Error retrieving API response: " + response.ErrorMessage;
                Console.WriteLine(message);
                var exception = new Exception(message, response.ErrorException);
                //throw exception;
            }
            else
            {
                Console.WriteLine("Response status code: " + response.StatusCode);
                //Console.WriteLine(response.StatusCode);
                this.ApiResponse = response.Content;
            }

            Console.WriteLine(this.ApiResponse);

        }
        
       
    }
}
