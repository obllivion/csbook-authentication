using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace authentication
{
    public class Startup
    {

        public async Task<object> Invoke(dynamic input)
        {
            string username = (string)input.uname;
            string password = (string)input.pass;
            bool success = TryAuth(username, password);

            return success;
        }
        
        bool TryAuth(string uName, string pass)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://cs.elfak.ni.ac.rs/nastava/login/index.php");
            HttpWebRequest webRequest2 = (HttpWebRequest)WebRequest.Create("https://cs.elfak.ni.ac.rs/nastava/login/index.php");
            
            CookieContainer cont = new CookieContainer();
            webRequest.Headers.Add("username", uName);
            webRequest.Headers.Add("password", pass);
            webRequest.CookieContainer = cont;

            webRequest.Method = "POST";
            webRequest.UserAgent = ".NET Client";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            string val = "username=" + uName + "&password=" + pass;
            byte[] bArr = Encoding.ASCII.GetBytes(val);
            webRequest.ContentLength = bArr.Length;

            //upisujemn informaciju
            Stream dataStream2= webRequest.GetRequestStream();
            dataStream2.Write(bArr, 0, bArr.Length);
            dataStream2.Close();

            HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();

            webRequest2.CookieContainer = cont;
            webRequest2.Headers.Add("username", uName);
            webRequest2.Headers.Add("password", pass);

            webRequest2.Method = "POST";
            webRequest2.UserAgent = ".NET Client";
            webRequest2.ContentType = "application/x-www-form-urlencoded";
            webRequest2.ContentLength = bArr.Length;
            
            //upisujemn informaciju
            Stream dataStream = webRequest2.GetRequestStream();
            dataStream.Write(bArr, 0, bArr.Length);
            dataStream.Close();
            
            HttpWebResponse resp2 = (HttpWebResponse)webRequest2.GetResponse();

            var encoding = ASCIIEncoding.ASCII;
            string responseText = "";
            using (var reader = new System.IO.StreamReader(resp2.GetResponseStream(), encoding))
            {
                responseText = reader.ReadToEnd();
            }

            if (responseText.Contains("Prijavljeni ste kao"))
                return true;
            return false;
        }
    }
}
