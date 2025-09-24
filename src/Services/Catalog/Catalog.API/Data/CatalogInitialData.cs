using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if (await session.Query<Product>().AnyAsync())
        {
            return;
        }

        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() =>
    [
        new Product
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Name = "iPhone 13 Pro",
            Description = "Smartphone premium 64 Go",
            Price = 499.00M,
            Category = [ "Electronics", "Smartphones"],
            ImageFile = "iphone-x.png"
        },
        new Product
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Name = "Samsung Galaxy S23",
            Description = "Android flagship 128 Go",
            Price = 799.00M,
            Category =[ "Electronics", "Smartphones" ],
            ImageFile = "galaxy-s23.png"
        },
        new Product
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Name = "Sony WH-1000XM5",
            Description = "Casque à réduction de bruit",
            Price = 349.00M,
            Category = ["Electronics", "Audio" ],
            ImageFile = "sony-wh1000xm5.png"
        },
        new Product
        {
            Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            Name = "MacBook Air 13\" (M2)",
            Description = "Ultrabook léger et performant",
            Price = 1299.00M,
            Category = ["Computers", "Laptops"],
            ImageFile = "macbook-air-m2.png"
        },
        new Product
        {
            Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
            Name = "Nintendo Switch OLED",
            Description = "Console hybride avec écran OLED",
            Price = 349.00M,
            Category = ["Gaming", "Consoles"],
            ImageFile = "switch-oled.png"
        }
    ];
}
