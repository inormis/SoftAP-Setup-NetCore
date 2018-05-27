namespace SoftAP.Core
{
    public class ApConfig
    {
        public string pwd;
        public int idx { get; set; }
        public string ssid { get; set; }
        public SecurityTable? sec { get; set; }
        public int ch { get; set; }
        public string ii { get; set; }
        public string crt { get; set; }
        public int eap { get; set; }
        public string oi { get; set; }
        public string ca { get; set; }
    }
}