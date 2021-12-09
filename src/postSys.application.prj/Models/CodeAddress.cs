using System;
using System.Collections.Generic;

namespace postSys.application.prj
{
    public partial class CodeAddress
    {
        public CodeAddress()
        {
            Recipients = new HashSet<Recipient>();
            Senders = new HashSet<Sender>();
        }

        public int CodeAddressId { get; set; }
        public int CodeAddressCity { get; set; }
        public int CodeAddressPlot { get; set; }
        public int CodeAddressStreet { get; set; }
        public string CodeAddressHouses { get; set; } = null!;

        public virtual City CodeAddressCityNavigation { get; set; } = null!;
        public virtual Street CodeAddressStreetNavigation { get; set; } = null!;
        public virtual ICollection<Recipient> Recipients { get; set; }
        public virtual ICollection<Sender> Senders { get; set; }
    }
}
