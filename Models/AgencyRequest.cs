namespace RPS_DB.Models
{
    public class EditAgencyRequest
    {
        public int ID { get; set; }
        public string AgencyName { get; set; } = string.Empty;
    }

    public class DeleteAgencyRequest
    {
        public int ID { get; set; }
    }

    public class AddAgencyRequest
    {
        public string AgencyName { get; set; } = string.Empty;
        public bool AgencyState {get; set; } = true;
    }
}
