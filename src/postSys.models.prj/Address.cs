using System;
using System.Collections.Generic;

namespace PostSys.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public int? AddressPlot { get; set; }

    public int AddressRecipient { get; set; }

    public int AddressCity { get; set; }

    public int AddressStreet { get; set; }

    public string AddressHome { get; set; } = null!;

    public string AddressApartment { get; set; } = null!;

    public int AddressPostman { get; set; }

    public string AddressGoods { get; set; } = null!;

    public virtual Postman AddressPostmanNavigation { get; set; } = null!;

    public virtual Recipient AddressRecipientNavigation { get; set; } = null!;
}
