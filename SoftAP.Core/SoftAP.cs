using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;

namespace SoftAP.Core
{
    public class SoftAP
    {
        private readonly SoftAPOptions _options;

//        private static Dictionary<string, int> eapTypeTable = new Dictionary<string, int>
//        {
//            {"peap", 25},
//            {"peap/mschapv2", 25},
//            {"eap-tls", 13},
//            {"tls", 13}
//        };

        public SoftAP(SoftAPOptions options)
        {
            _options = options;
        }

        private string formatPem(string data)
        {
            return data.Trim() + "'\r\n";
        }
        
        private JObject Scan(Action<object,object> cb)
        {
            return SendCommand("scan-ap");
        }

        private JObject SendCommand(string name, object parameters = null)
        {
            using (var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) {NoDelay = _options.NoDelay})
            {
                sock.Connect(_options.Host, _options.Port);
		        
                
            }

            throw new NotImplementedException();
        }

        private JObject Connect(int index = 0)
        {
            return SendCommand("connect-ap", new {idx = index});
        }

        private DeviceInfo GetDeviceInfo()
        {
            var result = SendCommand("device-id");
            var id = result.Value<string>("id");
            var claimed = result.Value<string>("c")=="1";
            return new DeviceInfo(id, claimed);
        }

        private string PublicKey()
        {
            throw new NotImplementedException();
            
        }

        public void SetClaimCode(string code)
        {
            if (code == null) throw new ArgumentNullException(nameof(code));
            this.SendCommand("set", new {k = "cc", v = code});
        }
        
        public string aesEncrypt(object data, object kiv) {
            throw new NotImplementedException();
        }

        private RSACryptoServiceProvider __publicKey;
	    
        public void Configure(SoftAPConfiguration opts)
        {
            if (opts == null) throw new ArgumentNullException(nameof(opts));
            var securePass = "";

            if (__publicKey == null)
            {
                throw new Exception("Must retrieve public key of device prior to AP configuration");
            }

            if (opts.ssid == null)
            {
                if (opts.name == null)
                {
                    throw new Exception("Configuration options contain no ssid property");
                }

                opts.ssid = opts.name;
            }

            if ((opts.enc != null || opts.sec != null) && opts.security == null)
            {
                opts.security = opts.sec ?? opts.enc;
            }

            if (opts.security == null)
            {
                opts.security = SecurityTable.open;
                opts.password = null;
            }

            if (opts.password != null || opts.pass != null)
            {
                if (opts.security != null)
                {
                    throw new Exception("Password provided but no security type specified");
                }

                if (opts.pass == null && opts.password != null)
                {
                    opts.password = opts.pass;
                }

//		securePass = this.__publicKey.encrypt(opts.password, 'hex');
            }


            var apConfig = new ApConfig
            {
                idx = opts.index,
                ssid = opts.ssid,
                sec = opts.security,
                ch = opts.channel
            };

            if (opts.security==SecurityTable.enterprise)
            {

                if (opts.eap == EapTypes.Peap)
                {
                    // inner identity and password are mandatory
                    opts.inner_identity = opts.inner_identity ?? opts.username;
                    if (opts.inner_identity==null && opts.password==null)
                    {
                        throw new Exception("PEAP credentials missing");
                    }

                    apConfig.ii = opts.inner_identity;
                    // Password is set later on
                }
                else if (opts.eap == EapTypes.Tls)
                {
                    // client certificate and private key are mandatory
                    if (opts.private_key==null && opts.client_certificate==null)
                    {
                        throw new Exception("EAP-TLS credentials missing");
                    }

                    apConfig.crt = formatPem(opts.client_certificate);
                    var enc = this.aesEncrypt(formatPem(opts.private_key),null);
//			        apConfig.key = enc.encrypted;
//			        apConfig.ek = enc.kiv;
			        
                }

                apConfig.eap = (int)opts.eap;
                if (opts.outer_identity!=null)
                {
                    apConfig.oi = opts.outer_identity;
                }

                opts.ca = opts.ca ?? opts.root_ca;
                if (opts.ca!=null)
                {
                    apConfig.ca = formatPem(opts.ca);
                }
            }

            if (securePass!=null)
            {
                apConfig.pwd = securePass;
            }


            SendCommand("configure-ap", apConfig);
        }

        private JObject sendProtocolCommand()
        {
            throw new NotImplementedException();
        }

        private JObject GetVersion()
        {
            return SendCommand("version");
        }
    }
}