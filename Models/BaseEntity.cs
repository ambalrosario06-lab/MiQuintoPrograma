namespace MiQuintoPrograma.Models;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public Guid Uuid { get; set; }
}
