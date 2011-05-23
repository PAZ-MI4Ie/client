using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace PAZ.API
{
    class PAZApi
    {
        private String access_token;
        private String ip;
        private int port;

        /**
         * Sets the settings
         */
        public PAZApi(String ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }
        
        /**
         * Logs in
         * TODO: HASH PASSWORD
         * TODO: CHECK IF SUCCESFUL
         */
        public Boolean Login(string username, string password)
        {
            this.access_token = this.SendRequest("login " + username.Replace(" ", "+") + " " + password.Replace(" ", "+"));
            return true;
        }

        /**
         * Adds a pair of students
         * TODO: NON-STATIC variables
         */
        public int AddPair()
        {
            return Int32.Parse(this.SendRequest("addPair [{\"username\":\"teuneboon\",\"firstname\":\"Teun\",\"surname\":\"Beijers\",\"email\":\"teun@beijers.eu\",\"number_of_guests\":12,\"studentnumber\":13371},{\"username\":\"ganonmaster\",\"firstname\":\"Hidde\",\"surname\":\"Jansen\",\"email\":\"ganon@mastah.nl\",\"number_of_guests\":2,\"studentnumber\":13372}]"));
        }

        /**
         * Send a request to the specified server
         */
        private String SendRequest(String request)
        {
            try
            {
                TcpClient socket = new TcpClient(this.ip, this.port);
                NetworkStream stream = socket.GetStream();
                StreamReader input = new StreamReader(stream);
                StreamWriter output = new StreamWriter(stream);

                output.WriteLine(request);
                output.Flush();

                String line = input.ReadLine().Trim();
                String result = line;

                socket.Close();
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
