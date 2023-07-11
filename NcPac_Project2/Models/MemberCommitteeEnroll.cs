namespace NcPac_Project2.Models
{
    public class MemberCommitteeEnroll
    {
        public int MemberID { get; set; }
        public int CommitteeID { get; set; }
        public int CommitteeRoleID { get; set; }
        public  Member Member { get; set; }
        public  Committee Committee { get; set; }
        public CommitteeRole CommitteeRole { get; set; }

    }
}
