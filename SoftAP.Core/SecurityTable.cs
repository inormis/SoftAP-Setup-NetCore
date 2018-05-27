namespace SoftAP.Core
{
	public enum SecurityTable
    {
        open = 0,
        none = 0,
        wep_psk = 1,
        wep_shared = 0x8001,
        wpa_tkip = 0x00200002,
        wpa_aes = 0x00200004,
        wpa2_aes = 0x00400004,
        wpa2_tkip = 0x00400002,
        wpa2_mixed = 0x00400006,
        wpa2 = 0x00400006,
        wpa_enterprise_aes = 0x02200004,
        wpa_enterprise_tkip = 0x02200002,
        wpa2_enterprise_aes = 0x02400004,
        wpa2_enterprise_tkip = 0x02400002,
        wpa2_enterprise_mixed = 0x02400006,
        wpa2_enterprise = 0x02400006,
        enterprise = 0x02000000
    }
}