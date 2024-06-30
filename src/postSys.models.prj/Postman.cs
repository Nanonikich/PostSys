using System;
using System.Collections.Generic;

namespace PostSys.Models;

public partial class Postman
{
    public int PostmanId { get; set; }

    public string PostmanSurname { get; set; } = null!;

    public string PostmanName { get; set; } = null!;

    public string? PostmanPatronymic { get; set; }

    public string PostmanPhone { get; set; } = null!;

    public int PostmanPlot { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
