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
using RestSharp.Extensions;
using System.Web;

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
            var request = new RestRequest(this.Url + UrlParameters);

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

            string myUrlString = "SELECT plate, ObjId, s.cx, s.cy FROM PhotoObj AS p JOIN SpecObj AS s ON s.bestobjid = p.objid WHERE s.plate <= 2534 AND s.plate >= 2533";
            string myEncodedString = HttpUtility.UrlEncode(myUrlString);
            Console.WriteLine(myEncodedString);
            Console.WriteLine(this.ApiResponse);

        }
        
       
    }
}

// Example Search - https://skyserver.sdss.org/dr16/SkyServerWS/SearchTools/SqlSearch?cmd=select%20top%2010%20ra,dec%20from%20Frame&format=json

// URL encoding

// '+' or '%02' is ' ' (space)
// '%3D' is '=' (equals)
// '%2C' is ',' (equals)
// '<' or '%3c' is '<' (less than)
// '>' or '%3e' is '>' (greater than)
// '%0D%0A' is (enter/return/newline)

// Base URL: http://skyserver.sdss.org/dr17/en/tools/search/x_results.aspx?searchtool=SQL&TaskName=Skyserver.Search.SQL&syntax=NoSyntax&ReturnHtml=true&cmd=
// SQL Select statement = SELECT plate, ObjId, s.cx, s.cy FROM PhotoObj AS p JOIN SpecObj AS s ON s.bestobjid = p.objid WHERE s.plate <= 2534 AND s.plate >= 2533
// URL query string = SELECT+plate%2C+ObjId%2C+s.cx%2C+s.cy+FROM+PhotoObj+AS+p+JOIN+SpecObj+AS+s+ON+s.bestobjid+%3D+p.objid+WHERE+s.plate+<%3D+2534+AND+s.plate+>%3D+2533
// return data format = &format=html OR &format=jsonx
// full URL = http://skyserver.sdss.org/dr17/en/tools/search/x_results.aspx?searchtool=SQL&TaskName=Skyserver.Search.SQL&syntax=NoSyntax&ReturnHtml=true&cmd=SELECT+plate%2C+ObjId%2C+s.cx%2C+s.cy+FROM+PhotoObj+AS+p+JOIN+SpecObj+AS+s+ON+s.bestobjid+%3D+p.objid+WHERE+s.plate+<%3D+2534+AND+s.plate+>%3D+2533&format=html

// Example API Calls


//Finds all objects on a plate?!?!
//SELECT
//plate, specObjID
//FROM 
//SpecObjAll
//WHERE
//plate = 2534

//SELECT
//*
//FROM 
//SpecObjAll
//WHERE
//specObjID = 9262920495199967232
//Order by xFocal asc



//SELECT
//plate, ObjId, s.cx, s.cy
//FROM PhotoObj AS p
//JOIN SpecObj AS s ON s.bestobjid = p.objid
//WHERE 
//s.plate = 2534


//SELECT
//plate, p.ObjId, s.cx, s.cy
//FROM 
//PhotoObjAll AS p
//JOIN 
//SpecObjAll AS s ON s.specObjID = p.specObjID
//WHERE 
//s.plate = 2534


//Primary Keys
//SpecObjAll - primary   specObjID
//PhotoObjAll - primary - objID
//Plate2Target - Primary - Plate3TargetID