using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace ET
{
    public static class HttpClientHelper
    {
        public static async ETTask<string> Request(string url,string Method="GET")
        {
            var handler = new HttpClientHandler();
            var client = new HttpClient(handler);
            try
            {
                string result = default;
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Stream myResponseStream = await response.Content.ReadAsStreamAsync();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    result = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                    myResponseStream.Close();
                }

                return result;
            }
            catch (Exception e)
            {
               Log.Error(e.ToString());
            }

            return null;
        }
    }
}