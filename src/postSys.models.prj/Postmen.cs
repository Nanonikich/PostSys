namespace PostSys.Models;

public partial class Postmen
{
    public Postmen()
    {
        Addresses = new HashSet<Address>();
    }

    public int PostmenId { get; set; }
    public string PostmenSurname { get; set; } = null!;
    public string PostmenName { get; set; } = null!;
    public string? PostmenPatronymic { get; set; }
    public string PostmenPhone { get; set; } = null!;
    public int PostmenPlot { get; set; }

    public virtual ICollection<Address> Addresses { get; set; }
}
