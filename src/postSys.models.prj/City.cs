using System;
using System.Collections.Generic;

namespace PostSys.Models;

public partial class City
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public virtual ICollection<AddressCode> AddressCodes { get; set; } = new List<AddressCode>();

    public virtual ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();

    public virtual ICollection<Sender> Senders { get; set; } = new List<Sender>();
}
