namespace VendingMachineSystem.Models
{
    public class Base
    {
        public bool is_deleted {  get; set; }
        public string created_user_id {  get; set; }
        public string? updated_user_id { get; set; }
        public string? deleted_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime? updated_date_time { get; set; }

    }
}
