namespace PostSys.Models;

public partial class Sender
{
    public Sender()
    {
        Recipients = new HashSet<Recipient>();
    }

    public int SenderId { get; set; }
    public string SenderSurname { get; set; } = null!;
    public string SenderName { get; set; } = null!;
    public string? SenderPatronymic { get; set; }
    public int SenderCity { get; set; }
    public int SenderStreet { get; set; }
    public string SenderHome { get; set; } = null!;
    public int SenderApartment { get; set; }
    public string SenderPhone { get; set; } = null!;

    public virtual City SenderCityNavigation { get; set; } = null!;
    public virtual CodeAddress SenderStreetNavigation { get; set; } = null!;
    public virtual ICollection<Recipient> Recipients { get; set; }
}
