using System;
using System.Collections.Generic;

namespace postSys.application.prj
{
    public partial class Status
    {
        public Status()
        {
            Users = new HashSet<User>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
