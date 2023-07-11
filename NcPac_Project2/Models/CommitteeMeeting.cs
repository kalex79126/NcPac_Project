namespace NcPac_Project2.Models
{
    public class CommitteeMeeting
    {
        public int CommitteeID { get; set; }
        public int MeetingID { get; set; }

        public Meeting Meeting { get; set; }
        public Committee Committee { get; set; }
    }
}
