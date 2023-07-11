namespace NcPac_Project2.Models
{
    public class Attendance
    {
        public int AttendanceID { get; set; }
        public bool AttendanceFirstRecent { get; set; }
        public bool AttendanceSecondRecent { get; set; }
        public bool AttendanceThirdRecent { get; set; }

        public int MemberID;

        public int MeetingID;

        public Member Member { get; set; }
        public Meeting Meeting { get; set; }

        //Get Attendance Date
        public DateTime AttendanceDate { get; set; }
    }
}
