namespace SoftAP.Core
{
    public class SoftAPConfiguration
    {
        public int channel;
        public string inner_identity;
        public string client_certificate;
        public SecurityTable? security { get; set; }
        public string password { get; set; }
        public string pass { get; set; }
        public int index { get; set; }
        public string ssid { get; set; }
        public string name { get; set; }
        public SecurityTable? enc { get; set; }
        public SecurityTable? sec { get; set; }
        public EapTypes eap { get; set; }
        public string username { get; set; }
        public string private_key { get; set; }
        public string outer_identity { get; set; }
        public string root_ca { get; set; }
        public string ca { get; set; }
    }
}