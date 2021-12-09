using System;
using System.Collections.Generic;

namespace postSys.application.prj
{
    public partial class City
    {
        public City()
        {
            CodesAddresses = new HashSet<CodeAddress>();
            Recipients = new HashSet<Recipient>();
            Senders = new HashSet<Sender>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; } = null!;

        public virtual ICollection<CodeAddress> CodesAddresses { get; set; }
        public virtual ICollection<Recipient> Recipients { get; set; }
        public virtual ICollection<Sender> Senders { get; set; }
    }
}
