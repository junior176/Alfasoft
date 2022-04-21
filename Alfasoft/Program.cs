using Alfasoft.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace Alfasoft
{
    class Program
    {
        static int intervalRequest = 5000;
        static string logFile = "log.txt";
        static void Main(string[] args)
        {
            //string[] args = {"c:\\usuarios.txt"}; //debug only

            List<string> users = new List<string>{};
            
            if (args.Length == 0) exitApplication("Parameter is mandatory.");

            if (File.Exists(logFile)) {

                FileInfo log_info = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\"+logFile);

                TimeSpan dateDiff = DateTime.Now - log_info.LastWriteTime;

                if (dateDiff.TotalSeconds < 60) {
                    exitApplication("Wait "+ (60 - dateDiff.TotalSeconds).ToString("0") + " seconds and run again.");
                }

            }

            if (!File.Exists(args[0])) exitApplication("File not found.");
            else {
                
                foreach (string line in File.ReadLines(args[0]))
                {
                    users.Add(line);
                }

            }

            foreach (string user in users) {

                string url = "https://api.bitbucket.org/2.0/users/" + user;
                var requisicaoWeb = WebRequest.CreateHttp(url);
                requisicaoWeb.Method = "GET";
                requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
                requisicaoWeb.UserAgent = "JuvenalViana";
                try
                {

                    using (var resposta = requisicaoWeb.GetResponse())
                    {
                        var streamDados = resposta.GetResponseStream();
                        StreamReader reader = new StreamReader(streamDados);
                        object objResponse = reader.ReadToEnd();
                        WriteLog("User " + user + ": " + objResponse.ToString());
                        User result = JsonConvert.DeserializeObject<User>(objResponse.ToString());
                        Console.WriteLine("User " + user + ", name: " + result.display_name + ", url: " + url + ", output: "+ objResponse.ToString());
                        Thread.Sleep(intervalRequest);
                        streamDados.Close();
                        resposta.Close();

                    }
                }
                catch (WebException we) {
                    HttpWebResponse response = (HttpWebResponse)we.Response;
                    WriteLog("User " + user + " not found.");
                    Console.WriteLine("User "+user+" not found.");
                    Thread.Sleep(intervalRequest);
                }

            }            

        }

        static void exitApplication(string msg) {
            Console.WriteLine(msg);
            Thread.Sleep(intervalRequest);
            Environment.Exit(-1);
        }
        static void WriteLog(string log) {
            string docPath = AppDomain.CurrentDomain.BaseDirectory;
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, logFile), true))
            {
                outputFile.WriteLine(log);
            }
        }
    }
}
