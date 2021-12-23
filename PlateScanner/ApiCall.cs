using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PlateScanner
{
    internal class ApiCall
    {
        // SDSS API URL   - https://skyserver.sdss.org/dr16/SkyServerWS/SearchTools/SqlSearch?
        // Example Search - https://skyserver.sdss.org/dr16/SkyServerWS/SearchTools/SqlSearch?cmd=select%20top%2010%20ra,dec%20from%20Frame&format=json

        public string Url { get; set; }
        public string UrlParameters { get; set; }
        public string ApiResponse { get; set; }

        //string path = "https://skyserver.sdss.org/dr16/SkyServerWS/SearchTools/SqlSearch?cmd=select%20top%2010%20ra,dec%20from%20Frame&format=json";


        public ApiCall(string url, string urlParameters)
        {
            Url = url;
            UrlParameters = urlParameters;
            ApiResponse = String.Empty;
        }

        
       
    }
}
