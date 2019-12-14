using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherIO.Backend
{
    class HTTPRequests
    {
        static public string Get(string url,Dictionary<string,string>headers = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            
            if(headers!=null)
            {
                //build request headers
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            return reader.ReadToEnd();
        }

        static public Task<string> GetAsync(string url,Dictionary<string,string>headers = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            
            if(headers!=null)
            {
                //build request headers
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            return reader.ReadToEndAsync();
        }
    }
}
