using System;
using System.Collections.Generic;

namespace PostSys.Models;

public partial class Street
{
    public int StreetId { get; set; }

    public string StreetName { get; set; } = null!;

    public virtual ICollection<AddressCode> AddressCodes { get; set; } = new List<AddressCode>();
}
