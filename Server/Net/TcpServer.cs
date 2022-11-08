using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Server.Tools;

namespace Server.Net;

public class TcpServer : IDisposable
    {
        private readonly TcpListener _tcpServer;
        private readonly List<Connection> _connections;
        bool disposed;

        public TcpServer(int port)
        {
            _tcpServer = new TcpListener(IPAddress.Any, port);
            _connections = new List<Connection>();
        }
        public async Task ListenAsync()
        {
            try
            {
                _tcpServer.Start();
                Methods.AddTextToOutBox($"Server started at {_tcpServer.LocalEndpoint}");
                while (true)
                {
                    TcpClient client = await _tcpServer.AcceptTcpClientAsync();
                    Methods.AddTextToOutBox($"Connecting: {client.Client.RemoteEndPoint} > {client.Client.LocalEndPoint}");
                    lock (_connections)
                    {
                        _connections.Add(new Connection(client, c => { lock (_connections) { _connections.Remove(c); } c.Dispose(); }));
                    }
                }
            }
            catch (SocketException)
            {
                Methods.AddTextToOutBox("Server stopped!");
            }
        }

        public void Stop()
        {
            _tcpServer.Stop();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool disposing)
        {
            if (disposed)
                throw new ObjectDisposedException(typeof(TcpServer).FullName);
            disposed = true;
            _tcpServer.Stop();
            if (disposing)
            {
                lock (_connections)
                {
                    if(_connections.Count > 0)
                    {
                        Methods.AddTextToOutBox("Disconnection clients...");
                        foreach(Connection client in _connections)
                        {
                            client.Dispose();
                        }
                        Methods.AddTextToOutBox("Clients disconnected");
                    }
                }
            }
        }
        ~TcpServer() => Dispose(false);
    }