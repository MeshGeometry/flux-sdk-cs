using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace flux_listener
{
    class Program
    {

        static void Main(string[] args)
        {
            // Create a listener.
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8081/");

            listener.Start();

            while (true)
            {

                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;


                System.IO.StreamReader reader = new System.IO.StreamReader(request.InputStream);
                ParseCommand(reader.ReadToEnd());


                // Obtain a response object.
                HttpListenerResponse response = context.Response;

                // Construct a response.
                string responseString = "You did a thing...";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();


            }


            listener.Stop();
        }

        static void ParseCommand(string rawCommand)
        {

            Console.WriteLine(rawCommand);


        }

    }
}
