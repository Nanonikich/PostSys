using System;
using System.Collections.Generic;

namespace PostSys.Models;

public partial class Recipient
{
    public int RecipientId { get; set; }

    public string? RecipientSeries { get; set; }

    public string? RecipientNumber { get; set; }

    public string RecipientSurname { get; set; } = null!;

    public string RecipientName { get; set; } = null!;

    public string? RecipientPatronymic { get; set; }

    public int RecipientCity { get; set; }

    public int RecipientStreet { get; set; }

    public string RecipientHome { get; set; } = null!;

    public int RecipientApartment { get; set; }

    public string RecipientPhone { get; set; } = null!;

    public int RecipientSender { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual City RecipientCityNavigation { get; set; } = null!;

    public virtual Sender RecipientSenderNavigation { get; set; } = null!;

    public virtual AddressCode RecipientStreetNavigation { get; set; } = null!;
}
