using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetzweltExam.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public List<string> Roles { get; set; }
    }
}
