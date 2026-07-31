namespace MiQuintoPrograma.Models
{

    public class Computer : BaseEntity
    {
        public required string Name { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public required int Price { get; set; }
    }
}

