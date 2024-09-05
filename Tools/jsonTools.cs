using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_Example.Properties;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows;

namespace Database_Example.Tools
{
    public class jsonTools
    {
        private static bool jSonTransactionInProgress = false;

        public static List<T> GetjSonDataList<T>(string This_WEB_API_URL)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(This_WEB_API_URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                List<T> jSonDataList = JsonConvert.DeserializeObject<List<T>>(result);
                return jSonDataList;
            }
        }

        public static T GetjSonData<T>(int ID, string This_WEB_API_URL)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(This_WEB_API_URL + "/" + ID.ToString());
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                T jSonDataObject = JsonConvert.DeserializeObject<T>(result);
                return jSonDataObject;
            }
        }

        public static bool DeletejSonData(int ID, string This_WEB_API_URL)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(This_WEB_API_URL + "/" + ID.ToString());
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "DELETE";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            int Test = 10;

            return (true);
        }

        public static async void InsertjSonData<T>(T jsonObject, string This_WEB_API_URL)
        {
            jSonTransactionInProgress = true;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(This_WEB_API_URL);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            string jsonString = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            
            using (var httpClient = new HttpClient())
            {
                // Do the actual request and await the response
                var httpResponse = await httpClient.PostAsync(This_WEB_API_URL, httpContent);

                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net

                    if (false == Convert.ToBoolean(responseContent))
                    {
                        MessageBox.Show("Data for Student er ikke blevet indsat på grund af fejl");
                    }
                    else
                    {
                        MessageBox.Show("Data for Student er blevet indsat");
                    }
                }
            }
            jSonTransactionInProgress = false;
        }

        public static async void ModifyjSonData<T>(int ID, T jsonObject, string This_WEB_API_URL)
        {
            jSonTransactionInProgress = true;
            string jsonString = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                // Do the actual request and await the response
                var httpResponse = await httpClient.PutAsync(This_WEB_API_URL + "/" + ID.ToString(), httpContent);

                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                    
                    if (false == Convert.ToBoolean(responseContent))
                    {
                        MessageBox.Show("Data for Student er ikke blevet modificeret på grund af fejl");
                    }
                    else
                    {
                        MessageBox.Show("Data for Student er blevet modificeret");
                    }
                }
            }
            jSonTransactionInProgress = false;
        }

        public static bool IsjSonTransactionInProgress()
        {
            return (jSonTransactionInProgress);
        }

    }
}
