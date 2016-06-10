using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIMySchool.Models
{
    public class Meeting
    {
        public string ID { get; set; }
        public string school_id { get; set; }
        public string from_id { get; set; }
        public string from_type { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public string participants { get; set; }
        public string comments { get; set; }
        public string status { get; set; }
        public string createdbyname { get; set; }
        public IList<Teacher> tmembers { get; set; }
        public IList<Student> smembers { get; set; }
        public IList<Class> sclass { get; set; }
        public IList<Class> tclass { get; set; }   
    }

    public class MeetingHeader
    {
        public IList<Meeting> meetingdetails { get; set; }
        public string APIStatus { get; set; }
    }

    public class MeetingTrn
    {
        public string ID { get; set; }
        public string meeting_id { get; set; }

        public string school_id { get; set; }
        public string created_by { get; set; }
        public string created_type { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public IList<Teacher> tmembers { get; set; }
        public IList<Student> smembers { get; set; }

        public string updated_by { get; set; }
        public string updated_type { get; set; }
        public string updated_status { get; set; }
        public string updated_comments { get; set; }
        public string updated_date { get; set; }
        public string updated_by_name { get; set; }
    }
    public class MeetingTrnHeader
    {
        public IList<MeetingTrn> meetingstatusdetails { get; set; }
    }
}