using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace Alfasoft
{
    class Program
    {
        static void Main(string[] args2)
        {
            string[] args = {"c:\\usuarios.txt"}; //debug only

            List<string> users = new List<String> {};
            if (args.Length == 0) exitApplication();
            if (!File.Exists(args[0])) exitApplication();
            else {
                
                foreach (string line in File.ReadLines(args[0]))
                {
                    users.Add(line);
                }

            }

            foreach (string user in users) {

                var requisicaoWeb = WebRequest.CreateHttp("https://api.bitbucket.org/2.0/users/"+user);
                requisicaoWeb.Method = "GET";
                requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
                requisicaoWeb.UserAgent = "JuvenalViana";
                //ler e exibir a resposta
                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader1 = new StreamReader(streamDados);
                    object objResponse = reader1.ReadToEnd();

                    Console.WriteLine(objResponse.ToString());
                    Thread.Sleep(5000);
                    streamDados.Close();
                    resposta.Close();

                }

            }            

        }

        static void exitApplication() {
            Console.WriteLine("Arquivo de configuração inválido.");
            Thread.Sleep(5000);
            Environment.Exit(-1);
        }
    }
}
