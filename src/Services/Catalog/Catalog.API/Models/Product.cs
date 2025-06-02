namespace Catalog.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        // quand le type est evident
        public List<string> Category { get; set; } = new List<string>();
        // je sais que c'est null mais je promets de données une valeur
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
