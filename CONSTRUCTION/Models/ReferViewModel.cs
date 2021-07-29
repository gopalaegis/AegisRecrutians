using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CONSTRUCTION.Models
{
    public class ReferViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string FriendName { get; set; }
        public string FriendEmail { get; set; }
        public string FriendPhoneNo { get; set; }
        public string Description { get; set; }
        public int jobId { get; set; }
        public string Job { get; set; }
        public string URL { get; set; }
    }
}