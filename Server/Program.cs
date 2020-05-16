using System;
using System.Diagnostics;
using WebSocketSharp;
using WebSocketSharp.Server;


// .dll provided from build at
// https://github.com/sta/websocket-sharp

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // the listener socket
            WebSocketServer server = new WebSocketServer(8080);
            server.AddWebSocketService<DemoBehaviour>("/demo");
           // server.AddWebSocketService<GameBehaviour>("/game");
            server.Start();


            bool continueRunning = true;

            while (continueRunning)
            {
                if (Console.ReadLine() == "q")
                {
                    continueRunning = false;
                }
            }
        }
    }

    // behaviours are used to seperate logic for lobby, games, global chat...
    class DemoBehaviour : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            // id represents a client
            // this also means multithreading - therefore ID OnOpen and ID OnMessage dont have to be same
            Console.WriteLine("OnOpen: " + ID);
            base.OnOpen();
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            // Console.WriteLine("OnMessage: " + e.RawData.Length);
            for (int i = 0; i < e.RawData.Length; i++)
            {
                Console.WriteLine(e.RawData[i]);
            }

            base.OnMessage(e);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Console.WriteLine("OnClose: " + ID);
            base.OnClose(e);
        }
    }
    class GameBehaviour : WebSocketBehavior { }
}
