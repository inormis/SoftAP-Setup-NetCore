namespace SoftAP.Core
{
    public class SoftAPOptions
    {
        public string Host { get; }
        public bool KeepAlive { get; }
        public int Timeout { get; }
        public bool NoDelay { get; }
        public int Channel { get; }
        public int Port { get; }


        public SoftAPOptions(string host = "192.168.0.1", bool keepAlive = true, int timeout = 8000,
            bool noDelay = true, int channel = 6, int port = 5609)
        {
            Host = host;
            KeepAlive = keepAlive;
            Timeout = timeout;
            NoDelay = noDelay;
            Channel = channel;
            Port = port;
        }
    }
}