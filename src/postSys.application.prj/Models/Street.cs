using System;
using System.Collections.Generic;

namespace postSys.application.prj
{
    public partial class Street
    {
        public Street()
        {
            CodesAddresses = new HashSet<CodeAddress>();
        }

        public int StreetId { get; set; }
        public string StreetName { get; set; } = null!;

        public virtual ICollection<CodeAddress> CodesAddresses { get; set; }
    }
}
