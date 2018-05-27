namespace SoftAP.Core
{
    public class DeviceInfo
    {
        public string Id { get; }
		
        public bool Claimed { get; }

        public DeviceInfo(string id, bool claimed)
        {
            Id = id;
            Claimed = claimed;
        }
    }
}