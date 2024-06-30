using System;
using System.Collections.Generic;

namespace PostSys.Models;

public partial class AddressCode
{
    public int AddressCodeId { get; set; }

    public int AddressCodeCity { get; set; }

    public int AddressCodePlot { get; set; }

    public int AddressCodeStreet { get; set; }

    public string AddressCodeHouses { get; set; } = null!;

    public virtual City AddressCodeCityNavigation { get; set; } = null!;

    public virtual Street AddressCodeStreetNavigation { get; set; } = null!;

    public virtual ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();

    public virtual ICollection<Sender> Senders { get; set; } = new List<Sender>();
}
