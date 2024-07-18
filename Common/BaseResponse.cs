namespace VendingMachineSystem.Common
{
    public class BaseResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public object error { get; set; }
    }
}
