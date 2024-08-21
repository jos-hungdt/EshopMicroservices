using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
            return;

        // Marten UPSERT will cater for existing records
        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
    {
        new Product()
        {
            Id = new Guid("01916467-6e9c-4e0f-911c-020d2ea87539"),
            Name = "IPhone X",
            Description = "The exception handling middleware re-executes the request using the original HTTP method.",
            ImageFile = "product-1.png",
            Price = 950.00M,
            Category = new List<string> { "Smart Phone" }
        },
        new Product()
        {
            Id = new Guid("01916467-6e9c-4e0f-911c-020d2ea87540"),
            Name = "Samsung Galaxy 24 Ultra",
            Description = "Re-executes the request in an alternate pipeline using the path indicated. The request isn't re-executed if the response has started.",
            ImageFile = "product-2.png",
            Price = 840.00M,
            Category = new List<string> { "Smart Phone" }
        }
    };
}
